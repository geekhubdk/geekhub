xml.instruct!

def format_rss_description meeting
  costs_money_text = meeting.costs_money ? '' : '(gratis)'

  <<-eos
    <strong>#{meeting.city.name}, #{l meeting.starts_at, :format => :long} - #{meeting.organizer} #{costs_money_text} </strong><br/> 
    <p>#{nl2br h(meeting.description)}</p>
    eos
end

xml.rss 'version' => '2.0', 'xmlns:dc' => 'http://purl.org/dc/elements/1.1/' do
  xml.channel do


    xml.title 'geekhub - kommende events'

    xml.link        url_for :only_path => false, :controller => 'meetings'
    xml.description 'kommende events i Danmark'

    @meetings.each do |m|
      xml.item do
        xml.title       "#{m.title} - #{l(m.starts_at, :format => :short)}"

        xml.link meeting_url(m, :utm_source=>'rss')

        xml.description format_rss_description(m)
        xml.guid meeting_url(m)
     end
    end

 end
end