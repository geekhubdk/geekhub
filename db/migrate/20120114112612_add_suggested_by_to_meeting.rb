class AddSuggestedByToMeeting < ActiveRecord::Migration
  def change
    add_column :meetings, :suggested_by, :string
  end
end
