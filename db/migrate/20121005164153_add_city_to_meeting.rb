class AddCityToMeeting < ActiveRecord::Migration
  def up
    change_table :meetings do |t|
      t.references :city
    end
  end
end
