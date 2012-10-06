class AddAddressToMeeting < ActiveRecord::Migration
  def change
    add_column :meetings, :address, :string
  end
end
