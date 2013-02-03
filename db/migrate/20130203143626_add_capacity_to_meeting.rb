class AddCapacityToMeeting < ActiveRecord::Migration
  def change
    add_column :meetings, :capacity, :integer
  end
end
