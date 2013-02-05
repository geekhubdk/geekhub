$ ->
  $("input[data-behavior='typeahead-address']").typeahead
    source: (query, process) ->
      $.ajax(
        url: "/meetings/typeahead_address"
        data: { query: query }
        dataType: 'json')
        .success(process)

  $("input[data-behavior='typeahead-organizer']").typeahead
    source: (query, process) ->
      $.ajax(
        url:"/meetings/typeahead_organizers"
        data: { query: query }
        dataType: 'json')
        .success(process)