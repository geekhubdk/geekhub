class CreateMeetingSuggestions < ActiveRecord::Migration
  def change
    create_table :meeting_suggestions do |t|
      t.string :url

      t.timestamps
    end
  end
end
