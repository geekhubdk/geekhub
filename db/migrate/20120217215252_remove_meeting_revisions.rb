class RemoveMeetingRevisions < ActiveRecord::Migration
  def up
    drop_table :meeting_revisions
  end

  def down
  end
end
