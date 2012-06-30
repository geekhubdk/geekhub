class CreateOrganizers < ActiveRecord::Migration
  def change
    create_table :organizers do |t|
      t.string :name
      t.text :description
      t.string :url

      t.timestamps
    end
  end
end
