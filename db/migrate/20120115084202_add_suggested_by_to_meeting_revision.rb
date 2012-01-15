class AddSuggestedByToMeetingRevision < ActiveRecord::Migration
  def change
    add_column :meeting_revisions, :suggested_by, :string
  end
end
