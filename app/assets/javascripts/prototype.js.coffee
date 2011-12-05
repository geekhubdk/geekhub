# Place all the behaviors and hooks related to the matching controller here.
# All this logic will automatically be available in application.js.
# You can use CoffeeScript in this file: http://jashkenas.github.com/coffee-script/

$ ->
  draw_distance_circle = ->
    if window.circle?
      window.circle.setMap(null)
      
    circle_data =
      strokeColor: "#FF0000",
      strokeOpacity: 0.8,
      strokeWeight: 2,
      fillColor: "#FF0000",
      fillOpacity: 0.35,
      map: map,
      center: new google.maps.LatLng("57.725004", "10.579185999999936")
      radius: parseInt($("#distance").val()) * 1000

    window.circle = new google.maps.Circle(circle_data);
    
  if $(".step3").length > 0
    latlng = new google.maps.LatLng("57.725004", "10.579185999999936");
    myOptions =
      zoom: 5,
      center: latlng,
      mapTypeId: google.maps.MapTypeId.ROADMAP
      
    map = new google.maps.Map($("#map_canvas")[0],myOptions)
    
    draw_distance_circle()
    
  
    
  $(".step1 #address").keyup ->
    if $(this).val().length >= 4
      geocoder = new google.maps.Geocoder();
    
      geocoder.geocode
        'address': $(this).val() + " DANMARK"
        'region': "dk", 
        (results, status) ->
            if status == google.maps.GeocoderStatus.OK
              $(".step1 #address-lat").val(results[0].geometry.location.lat())
              $(".step1 #address-lng").val(results[0].geometry.location.lng())
              $(".step1 #address-result").show().text(results[0].formatted_address)
              $(".step1 #finish-address").show()
            else
              $(".step1 #finish-address").hide()
      false
    else
      $(".step1 #finish-address").hide()
      $(".step1 #address-result").hide()
        
  $(".step1 .start-search").click ->
    navigator.geolocation.getCurrentPosition(currentPositionFound)
    false
  
  $(".step3 #distance-value").text($(".step3 #distance").val())
  $(".step3 #distance").change ->
    $(".step3 #distance-value").text($(this).val())
    draw_distance_circle()
    
  currentPositionFound = (position) ->
    showMap position
    
    $(".step1 #map-lat").val(position.coords.latitude)
    $(".step1 #map-lng").val(position.coords.longitude)    
    
    $(".step1 .start-search").hide()
    $(".step1 .finish-search").show()
    
    false
    
  showMap = (position) ->
    latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    myOptions =
      zoom: 10,
      center: latlng,
      mapTypeId: google.maps.MapTypeId.ROADMAP
      
    map = new google.maps.Map($("#map_canvas")[0],myOptions)
    
    marker = new google.maps.Marker
      position: latlng,
      map: map