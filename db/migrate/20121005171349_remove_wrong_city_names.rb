# encoding: utf-8
class RemoveWrongCityNames < ActiveRecord::Migration
  def up
    move "Copenhagen", "København"
    move "Tåstrup", "Taastrup"
  end

  def down
  end

  def move wrong_name, right_name
    puts "#{wrong_name} => #{right_name}"

    wrong = City.find_by_name(wrong_name)
    wrong.meetings.each do |m|
      m.city = City.find_by_name(right_name)
      m.save!
    end
    wrong.destroy
  end
end
