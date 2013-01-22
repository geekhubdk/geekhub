class CreateAttendees < ActiveRecord::Migration
  def change
    create_table :attendees do |t|
      t.string :name
      t.string :email
      t.integer :meeting_id
      t.integer :user_id

      t.timestamps
    end
  end
end
