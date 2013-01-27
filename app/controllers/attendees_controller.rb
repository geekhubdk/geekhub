class AttendeesController < ApplicationController
  def create
  	@meeting = Meeting.find(params[:meeting_id])

  	if @meeting.can_add_attendee(params[:attendee][:email])
	  	@attendee = @meeting.attendees.new(params[:attendee])
	  	@attendee.user = current_user

	  	if @attendee.save
	  		redirect_to @meeting, notice: "Du er nu tilmeldt"
	  	else
	  		flash[:error] = "Kunne ikke tilmelde dig."
	  		redirect_to @meeting
	  	end
		 else
	  	flash[:error] = "Der er allerede en deltager tilmeldt med den e-mail-adresse."
	  	redirect_to @meeting
  	end


  end
end
