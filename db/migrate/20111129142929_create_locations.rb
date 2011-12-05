class CreateLocations < ActiveRecord::Migration
  def change
    create_table :locations do |t|
      t.string :address
      t.decimal :lat
      t.decimal :lng

      t.timestamps
    end
  end
end
