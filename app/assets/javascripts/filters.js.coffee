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
