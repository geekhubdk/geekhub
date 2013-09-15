(function() {
  var update_date_time_hidden_field;

  $(function() {
    $('.datepicker').datepicker({
      dateFormat: "dd/mm/yy"
    });

    $(".date input,.time select").change(function() {
      var container;
      container = $(this).closest(".dateselector");
      update_date_time_hidden_field(container);
    });

    $("*[data-visible-field]").each(function() {
      var field, target, value;
      target = $(this);
      field = $(target.attr("data-visible-field"));
      value = target.attr("data-visible-value");
      if (field.filter(":checked").val().toString() === value.toString()) {
        target.show();
      } else {
        target.hide();
      }
      field.change(function() {
        if ($(this).val().toString() === value.toString()) {
          target.show();
        } else {
          target.hide();
        }
      });
    });

    $("input#meeting_address").typeahead({
      remote: "/meetings/typeahead_address?query=%QUERY"
    });

    $("input#meeting_organizer").typeahead({
      remote: "/meetings/typeahead_organizers?query=%QUERY",
    });

    $(".help-tooltip").popover();
  });

  update_date_time_hidden_field = function(container) {
    var date_field, time_field;
    date_field = container.find(".date input");
    time_field = container.find(".time select.time");
    container.find("input[type=hidden]").val("" + (date_field.val()) + " " + (time_field.val()));
  };

}).call(this);
