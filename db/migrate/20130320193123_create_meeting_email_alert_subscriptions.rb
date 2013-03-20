class CreateMeetingEmailAlertSubscriptions < ActiveRecord::Migration
  def change
    create_table :meeting_email_alert_subscriptions do |t|
      t.belongs_to :user

      t.timestamps
    end
    add_index :meeting_email_alert_subscriptions, :user_id
  end
end
