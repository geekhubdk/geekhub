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

  def google_maps_image sizex, sizey, address, scale, zoom
    "http://maps.googleapis.com/maps/api/staticmap?size=#{sizex}x#{sizey}&scale=#{scale}&zoom=#{zoom}&markers=#{CGI.escape address}&sensor=false&key=AIzaSyDF3n_lGmqAvqxgRcRm1n1MPslVJW9oyG0"
  end

  def google_maps_link(address)
    "https://maps.google.dk/maps?q=#{CGI.escape address}"
  end

  def google_maps_image_link(address, sizex = 300, sizey = 300, zoom = 10)
    high_res = google_maps_image(sizex, sizey,address, 2, zoom)
    normal_res = google_maps_image(sizex, sizey,address, 1, zoom)

    html = image_tag("/#{sizex}_#{sizey}.png", 'data-original' => normal_res, style: "width: #{sizex}px; height: #{sizey}px", alt: address, class: "lazy google-map-image hidden-high-res") +
           image_tag("/#{sizex}_#{sizey}.png", 'data-original'=> high_res, width: sizex, height: sizey, alt: address,  class: "lazy google-map-image hidden-normal-res")

    link_to(
      html,
      google_maps_link(address))
  end

  def is_filter_active filter
    @active_location_filters.any?{|l| l.casecmp(filter.name.downcase) == 0} || @active_location_filters.empty?
  end

  def time_options_for_select value
    values = []

    (0..23).each do |h|
      (0..55).step(15).each do |m|
        values << (h < 10 ? "0#{h}" : "#{h}") + ":" + (m < 10 ? "0#{m}" : "#{m}")
      end
    end

    options_for_select(values, value ? value.strftime('%H:%M')  : '00:00')
  end

  def format_description description
    raw nl2br(auto_link_with_twitter(h(description)))
  end
end
