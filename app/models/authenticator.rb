class Authenticator
	def login(user, token)
		Member.find_by_email_and_password(user,token)	
	end
end