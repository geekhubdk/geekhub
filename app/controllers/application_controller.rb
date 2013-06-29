class ApplicationController < ActionController::Base
  protect_from_forgery

protected 

  def redirect_to_login
    redirect_to new_user_session_path
  end

  def permission_denied
    render nothing: true, :status => :unauthorized
  end
end
