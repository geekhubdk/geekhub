# Place all the behaviors and hooks related to the matching controller here.
# All this logic will automatically be available in application.js.
# You can use CoffeeScript in this file: http://jashkenas.github.com/coffee-script/

$ ->
  $('.datepicker').datepicker
    dateFormat: "dd/mm/yy"
  $(".date input,.time select").change ->
    
    container = $(this).closest(".dateselector")
    date_field = container.find(".date input")
    hour_field = container.find(".time select[name=hour]")
    minute_field = container.find(".time select[name=minute]")
    container.find("input[type=hidden]").val("#{date_field.val()} #{hour_field.val()}:#{minute_field.val()}")
