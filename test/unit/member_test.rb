require 'test_helper'

class MemberTest < ActiveSupport::TestCase
  test "Member should have a password field" do
    m = Member.new()
    m.password = "pass"
    assert_not_nil m.password_digest
  end
  
  test "Requires email, name & password" do
    m = Member.new()
    m.valid?
    
    assert !m.errors[:email].empty?
    assert !m.errors[:name].empty?
    assert !m.errors[:password].empty?
  end
end
