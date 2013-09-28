(function() {
  var disable_save_button, enable_save_button;

  $(function() {
    $(".filters input[type=checkbox]").change(function() {
      $(this).closest("form").submit();
      enable_save_button();
    });

    $('.filters form').bind('ajax:success', function(e, data) {
      var html;
      html = $(".meetings", $(data)).html();
      $(".meetings").html(html);
      enable_lazy_loaded_images($(".meetings"));
    });

    $("#save_filter").click(function() {
      var button, form;
      button = $(this);
      form = $(this).closest("form");

      $.post("/meetings/save_filter", form.serialize()).success(function() {
        disable_save_button();
      });

      return false;
    });
  });

  disable_save_button = function() {
    var button;
    button = $("#save_filter");
    $("i", button).addClass("icon-ok").removeClass("icon-hdd");
    return button.attr("disabled", true);
  };

  enable_save_button = function() {
    var button;
    button = $("#save_filter");
    $("i", button).addClass("icon-hdd").removeClass("icon-ok");
    return button.attr("disabled", false);
  };

}).call(this);
