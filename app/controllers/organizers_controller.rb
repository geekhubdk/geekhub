class OrganizersController < ApplicationController
  before_filter :authenticate_user!, :except => [:index, :show]

  def index
    @organizers = Organizer.order("name").all
  end

  def new
    @organizer = Organizer.new
  end

  def edit
    @organizer = Organizer.find(params[:id])
  end

  def create
    @organizer = Organizer.new(params[:organizer])

    if @organizer.save
      redirect_to organizers_url, notice: 'Organizer was successfully created.'
    else
      render action: "new"
    end
  end

  def update
    @organizer = Organizer.find(params[:id])

    if @organizer.update_attributes(params[:organizer])
      redirect_to organizers_url, notice: 'Organizer was successfully updated.'
    else
      render action: "edit"
    end
  end

  def destroy
    @organizer = Organizer.find(params[:id])
    @organizer.destroy

    redirect_to organizers_url
  end
end
