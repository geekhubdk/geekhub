class ApplicationController < ActionController::Base
  protect_from_forgery

  before_filter :ensure_domain

protected 

  def ensure_domain
    if Rails.env == "production"
      if request.env['HTTP_HOST'] == 'www.geekhub.dk' && !request.path.include?('/api/v1')
        # HTTP 301 is a "permanent" redirect
        redirect_to "http://geekhub.dk", :status => 301
      end
    end
  end
  
  def redirect_to_login
    redirect_to new_user_session_path
  end
end
