xml.instruct!

xml.rss "version" => "2.0", "xmlns:dc" => "http://purl.org/dc/elements/1.1/" do
 xml.channel do

   xml.title       "geekhub - kommende events"
   xml.link        url_for :only_path => false, :controller => 'meetings'
   xml.description "kommende events i Danmark"

   @meetings.each do |meeting|
     xml.item do
       xml.title       meeting.title
       xml.link        meeting.url
       xml.description format_rss_description(meeting)
       xml.guid        url_for :only_path => false, :controller => 'meetings', :action => 'show', :id => meeting.id
     end
   end

 end
end