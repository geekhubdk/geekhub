$ ->
	$(".filters input[type=checkbox]").change ->
		$(this).closest(".filters").find("input[type=submit]").click()
		
	$('.filters form').bind 'ajax:success', (e, data) ->
		html = $(".meetings", $(data)).html()
		$(".meetings").html(html)