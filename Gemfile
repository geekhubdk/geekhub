source 'http://rubygems.org'

gem 'rails', '~> 3.2'

gem 'devise'
gem 'jquery-rails'

group :assets do
  gem 'sass-rails'
  gem 'coffee-rails'
  gem 'uglifier'
end

group :production, :staging do
  gem 'pg'
  gem 'newrelic_rpm'
end

group :development, :test do
  # Pretty printed test output
  gem 'test-unit', '>= 2.4.5'
  gem 'capybara'
  gem 'factory_girl_rails'
  gem 'database_cleaner'
  gem 'sqlite3'
  gem 'guard'
  gem 'guard-test'
  gem 'growl'
  gem 'guard-rails'
  gem 'guard-livereload'
  gem 'simplecov', :require => false
end
