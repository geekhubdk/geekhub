require 'test_helper'

class OrganizersControllerTest < ActionController::TestCase
  setup do
    @organizer = organizers(:one)
  end

  test 'should get index' do
    get :index
    assert_response :success
    assert_not_nil assigns(:organizers)
  end

  test 'should get new' do
    sign_in User.first
    get :new
    assert_response :success
  end

  test 'should create organizer' do
    sign_in User.first
    assert_difference('Organizer.count') do
      post :create, organizer: { description: @organizer.description, name: @organizer.name, url: @organizer.url }
    end

    assert_redirected_to organizers_path
  end

  test 'should get edit' do
    sign_in User.first
    get :edit, id: @organizer
    assert_response :success
  end

  test 'should update organizer' do
    sign_in User.first
    put :update, id: @organizer, organizer: { description: @organizer.description, name: @organizer.name, url: @organizer.url }
    assert_redirected_to organizers_path
  end

  test 'should destroy organizer' do
    sign_in User.first
    assert_difference('Organizer.count', -1) do
      delete :destroy, id: @organizer
    end

    assert_redirected_to organizers_path
  end
end
