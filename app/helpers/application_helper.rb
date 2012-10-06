module ApplicationHelper
  def format_rss_description meeting

    costs_money_text = ""
    costs_money_text = "(gratis)" unless meeting.costs_money

    <<-eos
      <strong>#{meeting.city.name}, #{l meeting.starts_at, :format => :long} - #{meeting.organizer} #{costs_money_text} </strong><br/> 
      <p>#{nl2br h(meeting.description)}</p>
      eos

  end

  def nl2br s
    raw h(s).gsub(/\n/, '<br/>')
  end

  def month_name number
    months = %w[januar februar marts april maj juni juli august september oktober november december]
    months[number.to_i - 1]  
  end

  def avatar_url(user)
    gravatar_id = Digest::MD5.hexdigest(user.email.downcase)
    "http://gravatar.com/avatar/#{gravatar_id}.png?s=24"
  end

  def google_maps_image sizex, sizey, address
    "http://maps.googleapis.com/maps/api/staticmap?size=#{sizex}x#{sizey}&zoom=9&markers=#{CGI.escape address}&sensor=false&key=AIzaSyDF3n_lGmqAvqxgRcRm1n1MPslVJW9oyG0"
  end

  def google_maps_link(address)
    "https://maps.google.dk/maps?q=#{CGI.escape address}"
  end

  def google_maps_image_link(address, sizex = 300, sizey = 300)
    link_to image_tag(google_maps_image(sizex, sizey,address)), google_maps_link(address)
  end
end
