require 'test_helper'

class OrganizerTest < ActiveSupport::TestCase
  test 'can_be_edited_by' do
    assert Organizer.first.can_be_edited_by(nil)
  end
end
