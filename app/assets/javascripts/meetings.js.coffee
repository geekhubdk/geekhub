# Place all the behaviors and hooks related to the matching controller here.
# All this logic will automatically be available in application.js.
# You can use CoffeeScript in this file: http://jashkenas.github.com/coffee-script/

$ -> 	
	$("a[data-behavior=intro-show-more]").click ->  	
		$(this).hide()
		$(".intro-more").show(500)

$ ->
  $('.datepicker').datepicker
    dateFormat: "dd/mm/yy"
  $(".date input,.time select").change ->  
    container = $(this).closest(".dateselector")
    update_date_time_hidden_field container


  $('#meeting_url').change ->  
    $("#spinner").slideDown()
    request = $.ajax
      url: "/api/v1/lookups.json"
      data:
        url: $("#meeting_url").val()
      success: (data) ->
        set_meeting_information data unless data == null
    request.complete ->
      $("#spinner").stop().slideUp("fast")
        
set_meeting_information = (data) ->
  $('#meeting_title').val(data.title)
  $("#meeting_description").val(data.description)
  $("#meeting_location").val(data.location)
  $("#meeting_organizer").val(data.organizer)
  set_starts_at(new Date(data.starts_at))
  
set_starts_at = (datetime) ->
  container = $(".dateselector")
  date_field = container.find(".date input")
  hour_field = container.find(".time select.hour")
  minute_field = container.find(".time select.minute")
  date_field.val(datetime.getDate() + "/" + (datetime.getMonth() + 1) + "/" + datetime.getFullYear())
  hours = datetime.getHours()
  minutes = datetime.getMinutes()
  if String(hours).length == 1
    hours = "0" + hours
  if String(minutes).length == 1
    minutes = "0" + minutes
    
  hour_field.val(hours)
  minute_field.val(minutes)
  update_date_time_hidden_field container
  
update_date_time_hidden_field = (container) ->
  date_field = container.find(".date input")
  hour_field = container.find(".time select.hour")
  minute_field = container.find(".time select.minute")
  container.find("input[type=hidden]").val("#{date_field.val()} #{hour_field.val()}:#{minute_field.val()}")