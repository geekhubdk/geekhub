class AddApprovedAtToMeetingRevision < ActiveRecord::Migration
  def change
    add_column :meeting_revisions, :approved_at, :datetime
  end
end
