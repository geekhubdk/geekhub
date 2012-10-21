# encoding: utf-8
#
module MeetingsHelper
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
