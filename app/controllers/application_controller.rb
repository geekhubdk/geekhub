class ApplicationController < ActionController::Base
  protect_from_forgery

  before_filter :ensure_domain

  APP_DOMAIN = 'geekhub.dk'

protected 

  def ensure_domain
    if Rails.env == "production"
      if request.env['HTTP_HOST'] != APP_DOMAIN && !request.path.include?('/api/v1')
        # HTTP 301 is a "permanent" redirect
        redirect_to "http://#{APP_DOMAIN}", :status => 301
      end
    end
  end
  
  def boolean_param(name, default_value = nil)
    name = name.to_sym
    return default_value if params[name].nil?
    
    val = params[name].to_s.to_bool
    return default_value if val.nil?
    
    return val
  end
  
  def integer_param(name, default_value = nil)
    name = name.to_sym
    
    return default_value if params[name].nil?
    return default_value unless params[name] =~ /^[-+]?[0-9]+$/
    
    return params[name].to_i
  end
end
