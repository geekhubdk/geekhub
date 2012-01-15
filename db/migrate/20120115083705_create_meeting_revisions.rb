class CreateMeetingRevisions < ActiveRecord::Migration
  def change
    create_table :meeting_revisions do |t|
      t.references :meeting
      t.string :title
      t.datetime :starts_at
      t.string :location
      t.string :organizer
      t.text :description
      t.string :url
      t.boolean :costs_money

      t.timestamps
    end
    add_index :meeting_revisions, :meeting_id
  end
end
