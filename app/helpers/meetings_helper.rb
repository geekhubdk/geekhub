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
  
  def show_edit_tools meeting
    user_signed_in? &&  meeting.can_be_edited_by(current_user)
  end
  
  def is_online meeting
    @meeting.city.try(:name) == "Online"
  end
  
  def meeting_map meeting
    width = 470
    height = 300
    
    if is_online meeting
      '<div class="online"></div>'
    else
      if meeting.address.blank?
        return <<-eos
        <p class="location" title="#{meeting.city.name}">
    			#{google_maps_image_link meeting.city.name, width, height, 10}
    		</p>
    		eos
      else
        return <<-eos
        <p class="location" title="#{meeting.address}">
    			#{google_maps_image_link meeting.address, width, height, 14}
    		</p>
    		<p>Adresse: #{meeting.address} %></p>
        eos
      end
    end
  end
  
  def meeting_contact_link meeting
    if @meeting.joinable
      mail_to(@meeting.user.email, "Kontakt arrangør", class: "btn btn-link btn-large pull-right")  
    else
		  mail_to("hello@geekhub.dk", "Rapportér fejl", subject: u("Ang. #{meeting.title} (##{meeting.id})"), class: "btn btn-link btn-large pull-right")
	  end
  end
end
