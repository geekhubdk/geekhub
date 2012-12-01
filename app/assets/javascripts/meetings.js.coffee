# Place all the behaviors and hooks related to the matching controller here.
# All this logic will automatically be available in application.js.
# You can use CoffeeScript in this file: http://jashkenas.github.com/coffee-script/

$ ->
  $('.datepicker').datepicker
    dateFormat: "dd/mm/yy"
  $(".date input,.time select").change ->
    container = $(this).closest(".dateselector")
    update_date_time_hidden_field container

update_date_time_hidden_field = (container) ->
  date_field = container.find(".date input")
  time_field = container.find(".time select.time")
  container.find("input[type=hidden]").val("#{date_field.val()} #{time_field.val()}")


