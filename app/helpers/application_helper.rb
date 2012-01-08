module ApplicationHelper
  def format_rss_description meeting
    
    <<-eos
      <strong>#{meeting.location}, #{l meeting.starts_at, :format => :long} - #{meeting.organizer}</strong><br/> 
      <p>#{meeting.description}</p>
      eos
          
  end
end
