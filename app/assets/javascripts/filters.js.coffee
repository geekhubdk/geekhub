$ ->
  $(".filters input[type=checkbox]").change ->
    $(this).closest(".filters").find("input[type=submit]").click()
    enable_save_button()

  $('.filters form').bind 'ajax:success', (e, data) ->
    html = $(".meetings", $(data)).html()
    $(".meetings").html(html)
    calculate_meeting_distance()


  $("#save_filter").click ->
    button = $(this)
    form = $(this).closest("form")
    $.post("/meetings/save_filter", form.serialize()).success ->
      disable_save_button()
    false

disable_save_button = () ->
  button = $("#save_filter")
  $("i",button).addClass("icon-ok").removeClass("icon-hdd")
  button.attr("disabled", true)

enable_save_button = () ->
  button = $("#save_filter")
  $("i",button).addClass("icon-hdd").removeClass("icon-ok")
  button.attr("disabled", false)