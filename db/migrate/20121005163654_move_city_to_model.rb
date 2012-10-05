class MoveCityToModel < ActiveRecord::Migration
  def up
    meetings = Meeting.all

    meetings.each do |m|
      city = City.find_or_create_by_name(m.location)
      city.save
    end
  end

  def down
  end
end
