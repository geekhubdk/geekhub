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

	def destroy
		@meeting = Meeting.find(params[:meeting_id])
		@attendee = @meeting.attendees.find(params[:id])

		if(@attendee.user_id == current_user.id)
			@attendee.destroy
		end

		redirect_to @meeting, notice: "Deltager er afmeldt."
	end

	def destroy_attendee
		@meeting = Meeting.find(params[:meeting_id])
		@attendee = @meeting.attendees.find_by_email(params[:email])
		@attendee.destroy

		redirect_to @meeting, notice: "Deltager er nu afmeldt."
	end
end
