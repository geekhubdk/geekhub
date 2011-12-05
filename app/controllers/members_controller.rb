class MembersController < ApplicationController
  def new
    @member = Member.new
  end
  
  def create
    @member = Member.new(params[:member])
    
    if @member.save
      redirect_to edit_member_path(@member), notice: "Du er nu oprettet som medlem."
    else
      render :new
    end
  end
  
  def edit
    @member = Member.find(params[:id])
  end
  
  def update
    @member = Member.find(params[:id])
    
    unless params[:location].nil?
      update_location
    end
  end
  
  def authenticate
  	@member = Member.find_by_email_and_password(params[:email],params[:password])
  	
  	unless @member.nil?
	  	set_user @member
	  	redirect_to @member, notice: "Du er nu logget ind"
	  else
			redirect_to root_path, error: "Forkert brugernavn eller kodeord"
		end			
  end
  
  protected
  
  def update_location
    @member.location = Location.new(params[:location])
    
    if @member.save
      redirect_to edit_member_path(@member), notice: "Lokation opdateret"
    else
      render :edit
    end
  end
end
