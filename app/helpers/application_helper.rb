# encoding: utf-8
#

require 'rails_autolink'

module ApplicationHelper
  def nl2br s
    raw s.gsub(/\n/, '<br/>')
  end

  def month_name number
    months = %w[januar februar marts april maj juni juli august september oktober november december]
    months[number.to_i - 1]  
  end

  def avatar_url(email, size = 24)
    gravatar_id = Digest::MD5.hexdigest(email.downcase)
    "http://gravatar.com/avatar/#{gravatar_id}.png?s=#{size}&default=http://www.geekhub.dk/person.png"
  end

  def avatar_url_for_attendee(attendee)
    return '' if attendee.nil?

    # if(attendee.twitter.blank?)
      gravatar_id = Digest::MD5.hexdigest(attendee.email.downcase)
      "http://gravatar.com/avatar/#{gravatar_id}.png?s=73&default=http://www.geekhub.dk/person.png"
    # else
    #  "https://api.twitter.com/1/users/profile_image?screen_name=#{attendee.twitter}&size=bigger"
    # end
  end

  def auto_link_with_twitter(message)
    message = auto_link message, :html => { :target => '_blank' }
    message = message.gsub(/( @(\w+))/, %Q{<a href="http://twitter.com/\\2" target="_blank">\\1</a>})
  end

  def icon_with_text css_class, text
    raw("<i style='margin-top: 3px;' class='icon #{css_class}'></i> #{text}")
  end

  def format_description description
    raw nl2br(auto_link_with_twitter(h(description)))
  end

  def friendly_date date
    str = l date, format: :short

    if date.to_date < 7.days.from_now.to_date && date.to_date > 1.day.from_now.to_date
      week_day_name = l(date, :format => '%A')
      "På #{week_day_name}, den #{str}"
    elsif date.to_date == Date.today
      "I dag, den #{str}"
    else
      str
    end
  end
end
