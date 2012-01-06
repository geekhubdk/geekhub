require 'sinatra'

get '/' do
  erb :index
end

get '/app.js' do
  coffee 'scripts/app'
end