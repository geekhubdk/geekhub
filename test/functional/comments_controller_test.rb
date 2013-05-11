require 'test_helper'

class CommentsControllerTest < ActionController::TestCase

  test "Create comment" do
    assert_difference "Comment.count", +1 do
      post :create, meeting_id: meetings(:one).id, comment: { name: 'Jesper', email: 'deldy@deldysoft.dk', content: 'hello world!'}
    end
  end

end
