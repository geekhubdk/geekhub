class CreateMeetings < ActiveRecord::Migration
  def change
    create_table :meetings do |t|
      t.string :title
      t.datetime :starts_at
      t.string :location
      t.string :organizer
      t.text :description
      t.string :url

      t.timestamps
    end
  end
end
