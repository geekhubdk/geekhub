# encoding: utf-8
#
module MeetingsHelper
  def friendly_date date
    str = l date, format: :short

    if date.to_date < 7.days.from_now.to_date
      week_day_name = l(date, :format => '%A')
      "På #{week_day_name}, den #{str}"
    else
      str
    end

  end
end
