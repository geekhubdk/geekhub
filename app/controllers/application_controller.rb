class ApplicationController < ActionController::Base
  protect_from_forgery
	
	def set_user member
		session[:user] = member	
	end
end
