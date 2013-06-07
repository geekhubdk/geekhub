Geekhub::Application.configure do
  # Settings specified here will take precedence over those in config/application.rb

  # In the development environment your application's code is reloaded on
  # every request.  This slows down response time but is perfect for development
  # since you don't have to restart the web server when you make code changes.
  config.cache_classes = false

  # Log error messages when you accidentally call methods on nil.
  config.whiny_nils = true

  # Show full error reports and disable caching
  config.consider_all_requests_local       = true
  config.action_controller.perform_caching = false

  # Don't care if the mailer can't send
  config.action_mailer.raise_delivery_errors = true

  # Print deprecation notices to the Rails logger
  config.active_support.deprecation = :log

  # Only use best-standards-support built into browsers
  config.action_dispatch.best_standards_support = :builtin

  # Do not compress assets
  config.assets.compress = false

  # Expands the lines which load the assets
  config.assets.debug = true

  config.action_mailer.default_url_options = { :host => 'localhost:3000' }

  config.action_mailer.perform_deliveries = true

  ActionMailer::Base.delivery_method = :smtp
  ActionMailer::Base.smtp_settings = {
    :user_name => 'geekhub-bb385ef35e52bbf0',
    :password => '8015e86d95590264',
    :address => 'mailtrap.io',
    :port => '2525',
    :authentication => :plain,
  }

  # for twitter
  Twitter.configure do |config|
    config.consumer_key = '35QL7O6w7PEEhIGz8fbOw'
    config.consumer_secret = 'bkzA8oF0L7qoj6XuTb6yi3P2hqtV5zyq0fWRkCpzqMM'
    config.oauth_token = '1490607336-SFWq0QqNXqc6alCsvCAQ6pdB7HrqwjnbLVxXbCq'
    config.oauth_token_secret = 'qRUW1ShDMeiESkyyCSom0RApxK7VGBsZQl8hU8Efx4'
  end
end
