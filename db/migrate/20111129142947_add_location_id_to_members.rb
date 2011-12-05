class AddLocationIdToMembers < ActiveRecord::Migration
  def change
    add_column :members, :location_id, :integer
  end
end
