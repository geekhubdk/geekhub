class AddCreatedByToMeeting < ActiveRecord::Migration
	change_table :meetings do |t|
		t.references :user
	end
end
