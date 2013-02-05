$ ->
  $("input#meeting_address").typeahead
    source: (query, process) ->
      $.ajax(
        url: "/meetings/typeahead_address"
        data: { query: query, city: $("#meeting_city_id").val() }
        dataType: 'json')
        .success(process)

  $("input#meeting_organizer").typeahead
    source: (query, process) ->
      $.ajax(
        url:"/meetings/typeahead_organizers"
        data: { query: query }
        dataType: 'json')
        .success(process)