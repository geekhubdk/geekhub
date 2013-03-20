class CreateMeetingEmailAlerts < ActiveRecord::Migration
  def change
    create_table :meeting_email_alerts do |t|
      t.integer :meeting_id
      t.string :emails

      t.timestamps
    end
  end
end
