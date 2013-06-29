xml.instruct!

xml.rss 'version' => '2.0', 'xmlns:dc' => 'http://purl.org/dc/elements/1.1/' do
  xml.channel do


    xml.title 'geekhub - event forslag'

    xml.link        url_for :only_path => false, :controller => 'meetings'
    xml.description 'Forslag til events'

    @meeting_suggestions.each do |m|
      xml.item do
        xml.title       "Forslag: #{m.url} - oprettet: #{l(m.created_at, :format => :short)}"

        xml.link m.url

        xml.description m.url
        xml.guid m.url + "##{m.id.to_s}"
     end
    end

 end
end