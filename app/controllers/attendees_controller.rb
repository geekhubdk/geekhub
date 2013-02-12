class AttendeesController < ApplicationController
	def index

  	@meeting = Meeting.find(params[:meeting_id])

  	unless !current_user.nil? && @meeting.can_be_edited_by(current_user)
  		raise "Not allowed to see attendee list"
  	end

  	@attendees = @meeting.attendees
	end

  def create
  	@meeting = Meeting.find(params[:meeting_id])

  	if @meeting.can_add_attendee(params[:attendee][:email])
	  	@attendee = @meeting.attendees.new(params[:attendee])
	  	@attendee.user = current_user
      @attendee.twitter.slice!(0) if @attendee.twitter.starts_with?("@")

	  	if @attendee.save
        AttendeeMailer.new_attendee_email(@attendee).deliver
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
      AttendeeMailer.destroy_attendee_email(@attendee).deliver
		end

		redirect_to @meeting, notice: "Deltager er afmeldt."
	end

	def destroy_attendee
		@meeting = Meeting.find(params[:meeting_id])
		@attendee = @meeting.attendees.where('lower(email) = ?', params[:email].downcase).first

    if @attendee.nil?
      flash[:error] = "Ingen deltager med den email."
      redirect_to @meeting
    else
      @attendee.destroy
      AttendeeMailer.destroy_attendee_email(@attendee).deliver
      redirect_to @meeting, notice: "Deltager er nu afmeldt."
    end
	end
end
