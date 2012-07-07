class Api::V1::LookupsController < ApplicationController
  respond_to :json
  
  def index
    url = params[:url]
    
    meeting = lookup(url)
    
    unless meeting.nil?
      respond_with meeting
    else
      respond_with(nil)
    end    
  end
  
  def test
    create
  end
  
  private
  
  def lookup(url)
    if is_facebook_event url
      return parse_facebook_event url
    end
    
    if is_eventbrite_event url
      return parse_eventbrite_event url
    end
    
    return nil
  end
  
  def is_eventbrite_event url
    return /http:\/\/www.eventbrite.com\/event\/(.*)/.match url
  end
    
  def parse_eventbrite_event url
    doc = Nokogiri::HTML(open(url))
    location = doc.css(".vcard .adr .locality").text.strip
    {
      title: doc.css(".summary").first.text.strip,
      description: doc.css(".description").first.text.strip,
      starts_at: eventbrite_parse_date(doc.css(".dtstart .value-title").first["title"]),
      url: url,
      organizer: doc.css("#hostedByDiv h2").text.strip,
      location: location[5..location.length]
    }
  end
  
  def eventbrite_parse_date date_str
    date = Time.parse(date_str)
    if Time.now.gmt_offset / 1.hour == 2
      date = date - 1.hour
    end
    
    return date
  end
  
  def is_facebook_event url
    return /http:\/\/www.facebook.com\/events\/(.*)\//.match url
  end
  
  def parse_facebook_event url
    id = /http:\/\/www.facebook.com\/events\/(.*)\//.match(url).captures[0]
    
    app = FbGraph::Application.new("265896203512153", :secret => "f6683249f7769b8bb5216a82bc66005d")
    token = app.get_access_token
    event = FbGraph::Event.fetch(id, :access_token => token)
    venue = FbGraph::Place.fetch(event.venue.identifier, :access_token => token)
    
    return ({
      title: event.name,
      starts_at: event.start_time,
      description: event.description,
      url: url,
      organizer: '',
      location: venue.location.city
    })
  end
end
