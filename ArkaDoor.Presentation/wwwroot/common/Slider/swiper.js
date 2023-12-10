(function () {

    function Swiper(option) {

        debugger;

        this.imageList = option.imageList;
        this.imageNum = this.imageList.length;
        this.imageWidth = option.imageWidth;
        this.wrap = option.wrap || $('body');
        this.animateType = option.animateType;
        this.slideBtn = option.slideBtn;
        this.changeBtn = option.changeBtn;
        this.isAuto = option.isAuto;
        this.showWidth = this.wrap.width();
        this.showHeight = this.wrap.height();
        this.showIndex = 0;
        this.showImgNum = this.imageWidth && Math.floor(this.showWidth / this.imageWidth);
        this.showImgGorp = this.imageWidth == undefined ? this.imageNum : this.imageNum / this.showImgNum;
        this.lock = true;
        this.time;

        this.init = function () {
            this.addDom();
            this.addStyle();
            this.bindEvent();
        };

        Swiper.prototype.addDom = function () {

            var self = this;
            var $ul = $('<ul class="innerWrap"></ul>');
            var $slideWrap = $('<div class="slideWrap"></div>');
            this.imageList.forEach(function (ele) {
                $('<li><a href="#"><img src="' + ele + '"></a></li>').appendTo($ul);
            });

            if (this.animateType == 'animate') {
                for (var n = 0; n < (this.imageWidth == undefined ? 1 : this.imageNum / this.showImgNum); n++) {
                    $('<li><a href="#"><img src="' + this.imageList[n] + '"></a></li>').appendTo($ul);
                };

            }

            $ul.appendTo(this.wrap);
            if (this.slideBtn) {
                for (var n = 0; n < (this.imageWidth == undefined ? this.imageNum : this.imageNum / this.showImgNum); n++) {
                    $('<span></span>').appendTo($slideWrap);
                };
                $slideWrap.appendTo(this.wrap);
            };
            if (this.changeBtn) {
                $('<div class="btn leftBtn">&lt;</div>').appendTo(this.wrap);
                $('<div class="btn rightBtn">&gt;</div>').appendTo(this.wrap);
            }


        };

        Swiper.prototype.addStyle = function () {

            debugger;

            $('*', this.wrap).css({
                padding: 0,
                margin: 0,
                listStyle: 'none',
            });

            $('li', this.wrap).css({
                width: this.imageWidth || this.showWidth,
                height: this.showHeight,
            });

            $('img', this.wrap).css({
                width: this.imageWidth || '100%',
                height: '100%',
            });

            if (this.animateType == 'fade') {
                $(this.wrap).css({
                    position: 'relative',
                    overflow: 'hidden',
                });

                $('.innerWrap', this.wrap).css({
                    height: '100%',
                    width: '100%',
                    position: 'relative'
                });

                $('li', this.wrap).css({
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    display: 'none'
                }).eq(0).css('display', 'inline-block');
            };

            if (this.animateType == 'animate') {
                $('.innerWrap', this.wrap).css({
                    width: this.showWidth * (this.imageNum + 1),
                    overflow: 'hidden',
                    position: 'absolute',
                });

                $(this.wrap).css({
                    position: 'relative',
                    overflow: 'hidden',
                });

                $('li', this.wrap).css({
                    float: 'left',

                });
            };

            if (this.changeBtn) {
                $('.btn', this.wrap).css({
                    width: 30,
                    height: 30,
                    position: 'absolute',
                    left: 0,
                    top: '50%',
                    marginTop: -15,
                    backgroundColor: 'rgba(0,0,0,0.3)',
                    color: '#fff',
                    zIndex: 10,
                    textAlign: 'center',
                    cursor: 'pointer'
                });

                $('.rightBtn', this.wrap).css({
                    left: this.showWidth - 30,
                });
            };

            if (this.slideBtn) {
                $('.slideWrap', this.wrap).css({
                    position: 'absolute',
                    bottom: 0,
                    width: '100%',
                    height: 30,
                    textAlign: 'center',
                    Zindex: 50
                }).find('span').css({
                    display: 'inline-block',
                    width: 10,
                    height: 10,
                    backgroundColor: '#222',
                    margin: '10px 10px',
                    borderRadius: '50%',
                    cursor: 'pointer'
                }).eq(0).css({ backgroundColor: '#fff' });

            };
        };

        Swiper.prototype.bindEvent = function () {

            debugger;

            var time;
            var self = this;
            $('.rightBtn', this.wrap).click(function () {
                if (self.animateType == 'animate' && self.lock) {
                    self.showIndex++;
                    self.lock = false;
                    $('.innerWrap', self.wrap).animate({ left: - self.showIndex * self.showWidth }, 300, 'swing', function () {
                        if (self.showIndex == self.showImgGorp) {
                            $('.innerWrap', self.wrap).css({ left: 0, });

                        };
                        if (self.showIndex > self.showImgGorp - 1) {
                            self.showIndex = 0;
                        };
                        self.changSlideBtn();
                        self.lock = true;
                    });

                };

                if (self.animateType == 'fade') {
                    self.showIndex++;
                    if (self.showIndex > self.showImgGorp - 1) {
                        self.showIndex = 0;
                    };
                    $('li', self.wrap).eq(self.showIndex).fadeIn(600, 'swing').end().eq(self.showIndex - 1).fadeOut(600, 'swing');
                    self.changSlideBtn();
                };

            });

            $('.leftBtn', this.wrap).click(function () {
                if (self.animateType == 'animate' && self.lock) {
                    self.showIndex--;
                    self.lock = false;
                    if (self.showIndex < 0) {
                        $('.innerWrap', self.wrap).css({ left: - self.showImgGorp * self.showWidth });
                        self.showIndex = self.showImgGorp - 1;
                    };
                    $('.innerWrap', self.wrap).animate({ left: - self.showIndex * self.showWidth }, 300, 'swing', function () {
                        self.changSlideBtn();
                        self.lock = true;
                    });

                };

                if (self.animateType == 'fade') {
                    self.showIndex--;
                    if (self.showIndex < 0) {
                        self.showIndex = self.showImgGorp - 1;
                    };
                    $('li', self.wrap).eq(self.showIndex).fadeIn(600, 'swing').end().eq(self.showIndex + 1).fadeOut(600, 'swing');
                    self.changSlideBtn();
                };
            });

            $('.slideWrap', this.wrap).find('span').click(function () {
                self.showIndex = $(this).index() - 1;
                $('.rightBtn', self.wrap).trigger('click');
            });
            if (self.isAuto) {
                self.time = setInterval(function () {
                    $('.rightBtn', self.wrap).trigger('click');
                }, 2000);
                // clearInterval(self.time);
                $(self.wrap).on('mouseenter', function () {
                    clearInterval(self.time);
                    console.log(self.time)
                }).on('mouseleave', function () {
                    self.time = setInterval(function () {
                        $('.rightBtn', self.wrap).trigger('click');
                    }, 2000);
                });
            };

        };

        Swiper.prototype.changSlideBtn = function () {
            $('.slideWrap', this.wrap).find('span').css({ backgroundColor: '#222' }).eq(this.showIndex).css({ backgroundColor: '#fff' });
        };
    };
    $.fn.extend({
        swiper: function (option) {
            option.wrap = this;
            var swiperObj = new Swiper(option);
            swiperObj.init();
        }
    })
}())
