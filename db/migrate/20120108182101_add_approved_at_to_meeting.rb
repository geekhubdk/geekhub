class AddApprovedAtToMeeting < ActiveRecord::Migration
  def change
    add_column :meetings, :approved_at, :datetime
  end
end
