module  Api::V1::MeetingsHelper
  def timeline_text m
    m.description + "<br/><br/><a href=\"#{m.url}\" target=\"_blank\">Se mere / tilmelding</a>"
  end
end
