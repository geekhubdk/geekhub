class DestroyMeetingVotes < ActiveRecord::Migration
  def change
    drop_table :meeting_votes
  end
end
