class AddJoinableToMeeting < ActiveRecord::Migration
  def change
    add_column :meetings, :joinable, :bool, default: false
  end
end
