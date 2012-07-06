class Api::V1::LookupsController < ApplicationController
  respond_to :json
  
  def create
    url = params[:url]
    
    meeting = lookup(url)
    
    unless meeting.nil?
      respond_with meeting
    else
      respond_with nil
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
    return /http:\/\/www.eventbrite.com\/event\/(.*)\/eorg/
  end
    
  def parse_eventbrite_event url
    doc = Nokogiri::HTML(open(url))
    location = doc.css(".vcard .adr .locality").text
    {
      title: doc.css(".summary").first.text,
      description: doc.css(".description").first.text,
      starts_at: doc.css(".dtstart .value-title").first["title"],
      url: url,
      organizer: doc.css("#hostedByDiv h2").text,
      location: location[5..location.length]
    }
  end
  
  def is_facebook_event url
    return /http:\/\/www.facebook.com\/events\/(.*)\//.match url
  end
  
  def parse_facebook_event url
    token = "AAADx1MjB6VkBABoVFZBoxytl6Gnv3EgpxADzZAS8wcVNDO03qottBJfYss1yiNqMem6uK2IeBe4HEr7aW52xIQHx3QjzMNvISQ1W3h8wZDZD"
    id = /http:\/\/www.facebook.com\/events\/(.*)\//.match(url).captures[0]
    
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
