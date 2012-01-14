class AddCostsMoneyToMeeting < ActiveRecord::Migration
  def change
    add_column :meetings, :costs_money, :boolean
  end
end
