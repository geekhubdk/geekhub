source 'http://rubygems.org'

gem 'rails', '3.2'

# Bundle edge Rails instead:
# gem 'rails',     :git => 'git://github.com/rails/rails.git'

gem 'devise'

# Gems used only for assets and not required
# in production environments by default.
group :assets do
  gem 'sass-rails'
  gem 'coffee-rails'
  gem 'uglifier'
end

gem 'jquery-rails'

gem 'ri_cal'

# To use ActiveModel has_secure_password
# gem 'bcrypt-ruby', '~> 3.0.0'

# Use unicorn as the web server
# gem 'unicorn'

# Deploy with Capistrano
# gem 'capistrano'

# To use debugger
# gem 'ruby-debug19', :require => 'ruby-debug'

gem 'simplecov', :require => false, :group => :test
  
group :production do
  gem 'pg'
  gem 'newrelic_rpm'
end

group :development, :test do
  # Pretty printed test output
  gem 'test-unit', '>= 2.4.5'
  gem 'sqlite3'
  gem 'guard'
  gem 'guard-test'
  gem 'growl_notify'
  gem 'guard-rails'
end
