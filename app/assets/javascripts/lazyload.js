$(function() {
	enable_lazy_loaded_images($("body"));
})

window.enable_lazy_loaded_images = function(container) {
  $(container).find('img.lazy').lazyload();
  $(window).trigger("load")
}