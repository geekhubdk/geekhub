# Disable delivery errors, bad email addresses will be ignored
config.action_mailer.raise_delivery_errors = true
config.action_mailer.delivery_method = :smtp
config.action_mailer.default_url_options = { :host => '##YOUR_PROJECTNAME##.heroku.com' }
ActionMailer::Base.smtp_settings = {
  :address    => "smtp.sendgrid.net",
  :port       => 25,
  :user_name  => ENV['SENDGRID_USERNAME'],
  :password   => ENV['SENDGRID_PASSWORD'],
  :domain     => ENV['SENDGRID_DOMAIN'],
  :authentication  => :plain
}