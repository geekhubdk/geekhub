class CreateMeetingTags < ActiveRecord::Migration
  def change
    create_table :meeting_tags do |t|
      t.belongs_to :meeting, index: true
      t.belongs_to :tag, index: true

      t.timestamps
    end
  end
end
