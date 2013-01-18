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

  def avatar_url(user, size = 24)
    gravatar_id = Digest::MD5.hexdigest(user.email.downcase)
    "http://gravatar.com/avatar/#{gravatar_id}.png?s=#{size}"
  end

end
