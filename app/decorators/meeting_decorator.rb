# encoding: utf-8
#

class MeetingDecorator < Draper::Decorator
  include Draper::LazyHelpers

  delegate_all

  def description
    format_description(object.description)
  end

  def contact_link
    if self.joinable
     mail_to(self.user.email, "Kontakt arrangør", class: "btn btn-link btn-large pull-right")
    else
     mail_to("hello@geekhub.dk", "Rapportér fejl", subject: u("Ang. #{self.title} (##{self.id})"), class: "btn btn-link btn-large pull-right")
    end
  end

  def image_map
    width = 470
    height = 300
    is_real_address = self.address.present?
    location = is_real_address ? self.address : self.city.name
    zoom = is_real_address ? 14 : 10

    if self.is_online
      tag :div, class: 'online'
    else
      out = content_tag(:p, google_maps_image_link(location, width, height, zoom), class: 'location', title: location)
      out << raw("<p>Adresse: #{location}</p>") if is_real_address
    end
  end

  def show_edit_tools
    user_signed_in? &&  self.can_be_edited_by(current_user)
  end
end