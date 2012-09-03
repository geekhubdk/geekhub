$ ->
  $(".filters input[type=checkbox]").change ->
    $(this).closest(".filters").find("input[type=submit]").click()

  $('.filters form').bind 'ajax:success', (e, data) ->
    html = $(".meetings", $(data)).html()
    $(".meetings").html(html)
    calculate_meeting_distance()

  $("input[name=max-distance]").change ->
    $(".max-distance-value").text($(this).val())
    max_distance = $(this).val()
    $(".meetings .meeting").each ->
      distance = $(this).data("distance")
      if(distance > max_distance)
        $(this).addClass("blur")
      else
        $(this).removeClass("blur")

  $("#change_to_old_filter").click ->
    $("#location-filter").show()
    $("#distance-filter").hide()
    return false

$ ->
  if navigator.geolocation
    navigator.geolocation.getCurrentPosition(success, null)

success = (data) ->
  window.lat = data.coords.latitude
  window.lng = data.coords.longitude
  calculate_meeting_distance()
  $("#location-filter").hide()
  $("#distance-filter").show()

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
