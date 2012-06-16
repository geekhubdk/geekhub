class CreateMeetingVotes < ActiveRecord::Migration
  def change
    create_table :meeting_votes do |t|
      t.belongs_to :user
      t.belongs_to :meeting

      t.timestamps
    end
    add_index :meeting_votes, :user_id
    add_index :meeting_votes, :meeting_id
  end
end
