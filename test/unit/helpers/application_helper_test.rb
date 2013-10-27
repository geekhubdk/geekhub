require 'test_helper'
require 'action_view/test_case'
require 'action_view/helpers'

class ApplicationHelperTest < ActionView::TestCase
  include ERB::Util

  test 'month_name' do
    assert_equal 'februar', month_name('02')
  end

  test 'avatar_url_for_attendee, returns nothing is attendee is nothing' do
    actual = avatar_url_for_attendee(nil)

    assert_equal '', actual
  end

  test 'avatar_url_for_attendee, returns gravatar if twitter is empty' do
    actual = avatar_url_for_attendee(Attendee.new({ email: 'test@test.dk', twitter: nil }))

    assert_match /gravatar/, actual
    assert_match /http:\/\/www.geekhub\.dk\/person.png/, actual, 'should use person.png as default image'
  end

  test 'avatar_url_for_attendee, returns twitter if twitter is not empty' do
    skip # Twitter API no longer supported. Must be replased by new API

    actual = avatar_url_for_attendee(Attendee.new({ email: 'test@test.dk', twitter: 'jesperbjensen'}))

    assert_match /twitter/, actual 
  end
end
