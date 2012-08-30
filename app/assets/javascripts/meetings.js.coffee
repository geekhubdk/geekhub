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

update_date_time_hidden_field = (container) ->
  date_field = container.find(".date input")
  hour_field = container.find(".time select.hour")
  minute_field = container.find(".time select.minute")
  container.find("input[type=hidden]").val("#{date_field.val()} #{hour_field.val()}:#{minute_field.val()}")

$ ->
  if navigator.geolocation
    navigator.geolocation.getCurrentPosition(success, null)

success = (data) ->
  window.lat = data.coords.latitude
  window.lng = data.coords.longitude
  calculate_meeting_distance()

calculate_meeting_distance = () ->
  $(".meetings .meeting").each ->
    meeting_lat = Number($(this).data("lat"))
    meeting_lng = Number($(this).data("lng"))
    $(this).attr("data-distance", distance(lat,lng,meeting_lat,meeting_lng))

###
Converts numeric degrees to radians
###
if typeof (Number::toRad) is "undefined"
  Number::toRad = ->
    this * Math.PI / 180

distance = (lat1, lon1, lat2, lon2) ->
  R = 6371; # km
  dLat = (lat2-lat1).toRad()
  dLon = (lon2-lon1).toRad()
  lat1 = lat1.toRad()
  lat2 = lat2.toRad()

  a = Math.sin(dLat/2) * Math.sin(dLat/2) + Math.sin(dLon/2) * Math.sin(dLon/2) * Math.cos(lat1) * Math.cos(lat2); 
  c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a)); 
  d = R * c;
