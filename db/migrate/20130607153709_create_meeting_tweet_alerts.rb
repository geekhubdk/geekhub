class CreateMeetingTweetAlerts < ActiveRecord::Migration
  def change
    create_table :meeting_tweet_alerts do |t|
      t.belongs_to :meeting

      t.timestamps
    end
    add_index :meeting_tweet_alerts, :meeting_id
  end
end
