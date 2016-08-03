(function ($) {
    $.fn.extend({
        roll: function (options) {
            var defaults = { speed: 1, containerCss: { position: 'relative', overflow: 'hidden' },
                contentCss: { position: 'absolute', top: '0px', "white-space": 'nowrap' }
            };
            var options = $.extend(true, defaults, options);
            var speed = (document.all) ? options.speed : Math.max(1, options.speed - 1);
            if ($(this) == null) return;
            var $container = $(this).css(options.containerCss);
            var $content = $(this).children();
            var init_left = $container.width();
            $content.css($.extend(true, { left: parseInt(init_left) + "px" }, options.contentCss));
            if (options.restart) {
                options.restart();
            }
            var This = this;
            var time = setInterval(function () { This.move($container, $content, speed, options.restart); }, 20);
            $container.bind("mouseover", function () { clearInterval(time); });
            $container.bind("mouseout", function () {
                time = setInterval(function () { This.move($container, $content, speed, options.restart); }, 20);
            });
            return this;
        },
        move: function ($container, $content, speed, restart) {
            var container_width = $container.width();
            var content_width = $content.width();
            if (parseInt($content.css("left")) + content_width > 0) {
                $content.css({ left: parseInt($content.css("left")) - speed + "px" });
            }
            else {
                if (restart) {
                    restart();
                }
                $content.css({ left: parseInt(container_width) + "px" });
            }
        }
    });
})(jQuery);