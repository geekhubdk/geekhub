class ApplicationController < ActionController::Base
  protect_from_forgery

  before_filter :ensure_domain

  APP_DOMAIN = 'geekhub.dk'

  def ensure_domain
    if Rails.env == "production"
      if request.env['HTTP_HOST'] != APP_DOMAIN
        # HTTP 301 is a "permanent" redirect
        redirect_to "http://#{APP_DOMAIN}", :status => 301
      end
    end
  end
  
  def boolean_param(name, default_value = nil)
    return default_value if params[name].nil?
    
    params[name].to_s.to_bool
  end
  
  def integer_param(name, default_value = nil)
    return default_value if params[name].nil?
    return default_value if params[name] !=~ /^[-+]?[0-9]+$/
    params[name].to_i
  end
end
