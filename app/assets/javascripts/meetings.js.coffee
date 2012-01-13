# Place all the behaviors and hooks related to the matching controller here.
# All this logic will automatically be available in application.js.
# You can use CoffeeScript in this file: http://jashkenas.github.com/coffee-script/

$ ->
  $(".meeting").click ->
    $("h2 a", this).click()
  $(".meeting h2 a").click ->
    $(this).closest(".meeting").find(".details").toggle(200)
    false
  $(".meeting a").click (e) ->
    e.stopPropagation()
    true
  $(".intro-show-more").click ->
    $(this).hide()
    $(".intro-more").show(500)