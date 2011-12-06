class CreateEvents < ActiveRecord::Migration
  def change
    create_table :events do |t|
      t.string :title
      t.string :organizer
      t.integer :organizer_member_id
      t.string :description
      t.integer :location_id
      t.datetime :starts_at
      t.datetime :ends_at

      t.timestamps
    end
  end
end
