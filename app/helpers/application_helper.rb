module ApplicationHelper
  def format_rss_description meeting
    
    <<-eos
      <strong>#{meeting.location}, #{l meeting.starts_at, :format => :long} - #{meeting.organizer}</strong><br/> 
      <p>#{nl2br h(meeting.description)}</p>
      eos
          
  end

  def nl2br s
    raw h(s).gsub(/\n/, '<br/>')
  end

  def month_name number
    months = %w[januar feburar marts april maj juni juli august september oktober november december]
    months[number.to_i]  
  end

end
