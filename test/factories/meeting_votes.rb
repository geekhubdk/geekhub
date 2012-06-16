# Read about factories at https://github.com/thoughtbot/factory_girl

FactoryGirl.define do
  factory :meeting_vote, :class => 'MeetingVotes' do
    user nil
    meeting nil
  end
end
