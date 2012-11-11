# encoding: utf-8
#
module MeetingsHelper
  def friendly_date date
    str = l date, format: :short

    if date.to_date < 7.days.from_now.to_date && date.to_date > 1.day.from_now.to_date
      week_day_name = l(date, :format => '%A')
      "På #{week_day_name}, den #{str}"
    elsif date.to_date == Date.today
      "I dag, den #{str}"
    else
      str
    end

  end

  def google_maps_image sizex, sizey, address
    "http://maps.googleapis.com/maps/api/staticmap?size=#{sizex}x#{sizey}&zoom=10&markers=#{CGI.escape address}&sensor=false&key=AIzaSyDF3n_lGmqAvqxgRcRm1n1MPslVJW9oyG0"
  end

  def google_maps_link(address)
    "https://maps.google.dk/maps?q=#{CGI.escape address}"
  end

  def google_maps_image_link(address, sizex = 300, sizey = 300)
    link_to image_tag(google_maps_image(sizex, sizey,address)), google_maps_link(address)
  end
end
