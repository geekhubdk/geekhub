class HookCityToMeeting < ActiveRecord::Migration
  def up
    puts "Migrating meetings"
    Meeting.all.each do |m|
      puts m.location
      m.update_column(:city_id, City.find_by_name(m.location).id)
    end
  end

  def down
  end
end
