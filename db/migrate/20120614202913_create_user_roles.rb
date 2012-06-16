class CreateUserRoles < ActiveRecord::Migration
  def change
    create_table :user_roles do |t|
      t.belongs_to :user
      t.string :role

      t.timestamps
    end
    add_index :user_roles, :user_id
  end
end
