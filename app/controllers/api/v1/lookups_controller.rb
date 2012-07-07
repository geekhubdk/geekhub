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
    return /http:\/\/www.eventbrite.com\/event\/([0-9]*)/.match url
  end
    
  def parse_eventbrite_event url
    id = /http:\/\/www.eventbrite.com\/event\/([0-9]*)/.match(url).captures[0]
    # hack to make the gem work
    old_parser = YAML::ENGINE.yamler
    YAML::ENGINE.yamler = "syck"
    # request event
    c = EventbriteClient.new({app_key: "HTEHZULE5EXWWADVKM"})
    e = c.event_get({id: id.to_i})['event']
    # rollback parser
    YAML::ENGINE.yamler = old_parser
    
    {
      title: e['title'],
      description: view_context.strip_tags(e['description']),
      starts_at: e['start_date'],
      url: url,
      organizer: e['organizer']['name'],
      location: e['venue']['city']
    }
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
