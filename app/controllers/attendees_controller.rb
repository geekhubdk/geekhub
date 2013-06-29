class AttendeesController < ApplicationController
	def index

  	@meeting = Meeting.find(params[:meeting_id])

  	unless @meeting.can_be_edited_by(current_user)
      permission_denied
      return
  	end

  	@attendees = @meeting.attendees
	end

  def create
  	@meeting = Meeting.find(params[:meeting_id])

  	if @meeting.can_add_attendee(params[:attendee][:email])
	  	@attendee = @meeting.attendees.new(params[:attendee])
	  	@attendee.user = current_user
      @attendee.twitter.slice!(0) if @attendee.twitter.present? && @attendee.twitter.starts_with?('@')

	  	if @attendee.save
        AttendeeMailer.new_attendee_email(@attendee).deliver
	  		redirect_to @meeting, notice: t('attendee.attending')
	  	else
	  		flash[:error] = t('attendee.invalid')
	  		redirect_to @meeting
	  	end
		else
	  	flash[:error] = t('attendee.already_attending')
	  	redirect_to @meeting
  	end
  end

	def destroy
		@meeting = Meeting.find(params[:meeting_id])
		@attendee = @meeting.attendees.find(params[:id])

    if(@attendee.user_id != current_user.id)
      permission_denied
      return
    end

		@attendee.destroy
    AttendeeMailer.destroy_attendee_email(@attendee).deliver

		redirect_to @meeting, notice: t('attendee.destroyed')
  end

	def destroy_attendee
		@meeting = Meeting.find(params[:meeting_id])
		@attendee = @meeting.attendees.where('lower(email) = ?', params[:email].downcase).first

    if @attendee.nil?
      flash[:error] = t('attendee.not_found_by_email')
      redirect_to @meeting
    else
      @attendee.destroy
      AttendeeMailer.destroy_attendee_email(@attendee).deliver
      redirect_to @meeting, notice: t('attendee.destroyed')
    end
	end
end
