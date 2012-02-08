xml.instruct!

xml.rss "version" => "2.0", "xmlns:dc" => "http://purl.org/dc/elements/1.1/" do
  xml.channel do


    if @mode == :upcomming
      xml.title "geekhub - kommende events"
    elsif @mode == :approve
      xml.title "geekhub - events der mangler godkendelse"
    end

    xml.link        url_for :only_path => false, :controller => 'meetings'
    xml.description "kommende events i Danmark"

    @meetings.each do |meeting|
      xml.item do
        xml.title       meeting.title

        if @mode == :upcomming
          xml.link meeting_url(meeting)
        elsif @mode == :approve
          xml.link url_for :only_path => false, :controller => 'meetings', :action => 'edit', :id => meeting.id
        end
        
        xml.description format_rss_description(meeting)
        xml.guid meeting_url(meeting)
     end
    end

 end
end