(function($) {
	
	"use strict";
	
	
	//Preloader
	function handlePreloader() {
		if($('.preloader').length){
			$('.preloader').delay(500).fadeOut(500);
		}
	}
	
	
	//Update Header Style + Scroll to Top
	function headerStyle() {
		if($('.main-header').length){
			var windowpos = $(window).scrollTop();
			if (windowpos >= 80) {
				$('.main-header').addClass('fixed-header');
				$('.scroll-to-top').fadeIn(300);
			} else {
				$('.main-header').removeClass('fixed-header');
				$('.scroll-to-top').fadeOut(300);
			}
		}
	}
	
	headerStyle();
	
	
	
	
	//Submenu Dropdown Toggle
	if($('.main-menu li.dropdown ul').length){
		$('.main-menu li.dropdown').append('<div class="dropdown-btn"></div>');
		
		//Dropdown Button
		$('.main-menu li.dropdown .dropdown-btn').on('click', function() {
			$(this).prev('ul').slideToggle(500);
		});
		
		//Disable dropdown parent link
		$('.navigation li.dropdown > a').on('click', function(e) {
			e.preventDefault();
		});
	}
	
	
	
	//Revolution Slider
	if($('.revolution-slider .tp-banner').length){

		jQuery('.revolution-slider .tp-banner').show().revolution({
		  
		  delay:5000,
		  startwidth:1920,
		  startheight:600,
		  hideThumbs:0, 
	      
		  navigationType:"bullet",
		  navigationArrows:"none",
		  navigationStyle:"preview4",
	      
		  touchenabled:"on",
		  onHoverStop:"off",
	
		  swipe_velocity: 0.7,
		  swipe_min_touches: 1,
		  swipe_max_touches: 1,
		  drag_block_vertical: false,
	
		  parallax:"mouse",
		  parallaxBgFreeze:"on",
		  parallaxLevels:[7,4,3,2,5,4,3,2,1,0],
	
		  keyboardNavigation:"off",
	
		  navigationHAlign:"center",
		  navigationVAlign:"bottom",
		  navigationHOffset:0,
		  navigationVOffset:20,

	
		  shadow:0,
		  fullWidth:"on",
		  fullScreen:"off",
	
		  spinner:"spinner4",
	
		  stopLoop:"off",
		  stopAfterLoops:-1,
		  stopAtSlide:-1,
	
		  shuffle:"off",
	
		  autoHeight:"off",
		  forceFullWidth:"on",
	
		  hideNavDelayOnMobile:1500,
		  hideBulletsOnMobile:"on",
		  hideArrowsOnMobile:"on",
		  hideThumbsUnderResolution:0,
	
		  hideSliderAtLimit:0,
		  hideCaptionAtLimit:0,
		  hideAllCaptionAtLilmit:0,
		  startWithSlide:0,
		  videoJsPath:"",
		  fullScreenOffsetContainer: ""
	  });

		
	}
	
	
	//Related Posts Carousel
	if ($('.related-posts-carousel').length) {
		$('.related-posts-carousel').owlCarousel({
			loop:true,
			margin:30,
			nav:true,
			smartSpeed: 500,
			autoplay: 5000,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:1
				},
				1024:{
					items:2
				},
				1400:{
					items:2
				}
			}
		});    		
	}
	
	
	//default page  box-product Carousel Slider
	if ($('.product-carousel').length) {
		$('.product-carousel').owlCarousel({
			loop:true,
			nav:true,
			dots: false,
			autoplayHoverPause:false,
			autoplay: 5000,
			smartSpeed: 700,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],			
			responsive:{
				0:{
					items:2
				},
				600:{
					items:3
				},
				760:{
					items:4
				},
				1024:{
					items:5
				},
				1100:{
					items:5
				}
			}
		});    		
	}
	
    /*Carausel 4 columns*/
    $(".carausel-4-columns").each(function (key, item) {
        var id = $(this).attr("id");
        var sliderID = "#" + id;
        var appendArrowsClassName = "#" + id + "-arrows";

        $(sliderID).slick({
            dots: false,
            infinite: true,
            speed: 700,
            arrows: true,
            autoplay: true,
            slidesToShow: 5,
            loop: true,
            adaptiveHeight: true,
            responsive: [
                {
                    breakpoint: 1025,
                    settings: {
                        slidesToShow: 3,
                        slidesToScroll: 3
                    }
                },
                {
                    breakpoint: 480,
                    settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                    }
                }
            ],
            prevArrow: '<span class="slider-btn slider-prev"><i class="fi-rs-arrow-small-left"></i></span>',
            nextArrow: '<span class="slider-btn slider-next"><i class="fi-rs-arrow-small-right"></i></span>',
            appendArrows: appendArrowsClassName
        });
    });
    /*Carausel 4 columns*/
 
	//Testimonials Carousel Slider
	if ($('.testimonials-carousel').length) {
		$('.testimonials-carousel').owlCarousel({
			loop:true,
			margin:60,
			nav:true,
			autoplayHoverPause:false,
			autoplay: 5000,
			smartSpeed: 700,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:1
				},
				760:{
					items:2
				},
				1024:{
					items:3
				},
				1100:{
					items:3
				}
			}
		});    		
	}
	
	// top-product-page Carousel Slider
	if ($('.toppage-product-carousel').length) {
		$('.toppage-product-carousel').owlCarousel({
			loop:true,
			nav:false,
			autoplayHoverPause:false,
			autoplay: 5000,
			smartSpeed: 700,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],
			dots: false,
			responsive:{
				0:{
					items:2
				},
				600:{
					items:3
				},
				760:{
					items:4
				},
				1024:{
					items:5
				},
				1100:{
					items:5
				}
			}
		});    		
	}
	
	// category product-page Carousel Slider
	if ($('.category-product-carousel').length) {
		$('.category-product-carousel').owlCarousel({
			loop:true,
			nav:false,
			autoplayHoverPause:false,
			autoplay: 5000,
			smartSpeed: 700,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],
			dots: false,
			responsive:{
				0:{
					items:2
				},
				600:{
					items:3
				},
				760:{
					items:4
				},
				1024:{
					items:5
				},
				1100:{
					items:5
				}
			}
		});    		
	}
	
	// three product-page Carousel Slider
	if ($('.three-product-carousel').length) {
		$('.three-product-carousel').owlCarousel({
			loop:true,
			nav:false,
			autoplayHoverPause:false,
			autoplay: 5000,
			smartSpeed: 700,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],
			dots: false,
			responsive:{
				0:{
					items:1
				},
				600:{
					items:1
				},
				760:{
					items:2
				},
				1024:{
					items:3
				},
				1100:{
					items:3
				}
			}
		});    		
	}
	
	//Sponsors Slider
	if ($('.sponsors-slider').length) {
		$('.sponsors-slider').owlCarousel({
			loop:true,
			margin:0,
			smartSpeed: 500,
			autoplay: 3000,
			navText: [ '<span class="fa fa-angle-left"></span>', '<span class="fa fa-angle-right"></span>' ],
			responsive:{
				0:{
					items:3
				},
				480:{
					items:3
				},
				600:{
					items:5
				},
				800:{
					items:5
				},
				1200:{
					items:5
				}
			}
		});    		
	}
	
	//thumbnails-image Slider
	if ($('.thumbnails-slider').length) {
		$('.thumbnails-slider').owlCarousel({
			loop:true,
			margin:0,			
			center: true,
			smartSpeed: 500,
			autoplay: 3000,
			dots: false, 
			responsive:{
				0:{
					items:2
				},
				480:{
					items:2
				},
				600:{
					items:2
				},
				800:{
					items:4
				},
				1200:{
					items:5
				}
			}
		});    		
	}

	//Accordion Box
	if($('.accordion-box').length){
		$(".accordion-box").on('click', '.accord-btn', function() {
		
			if($(this).hasClass('active')!==true){
			$('.accordion .accord-btn').removeClass('active');
			
			}
			
			if ($(this).next('.accord-content').is(':visible')){
				$(this).removeClass('active');
				$(this).next('.accord-content').slideUp(300);
			}else{
				$(this).addClass('active');
				$('.accordion .accord-content').slideUp(300);
				$(this).next('.accord-content').slideDown(300);	
			}
		});	
	}
	
	
	
	// Fact Counter
	function factCounter() {
		if($('.fact-counter').length){
			$('.fact-counter .column.animated').each(function() {
		
				var $t = $(this),
					n = $t.find(".count-text").attr("data-stop"),
					r = parseInt($t.find(".count-text").attr("data-speed"), 10);
					
				if (!$t.hasClass("counted")) {
					$t.addClass("counted");
					$({
						countNum: $t.find(".count-text").text()
					}).animate({
						countNum: n
					}, {
						duration: r,
						easing: "linear",
						step: function() {
							$t.find(".count-text").text(Math.floor(this.countNum));
						},
						complete: function() {
							$t.find(".count-text").text(this.countNum);
						}
					});
				}
				
			});
		}
	}
	
	
	//LightBox / Fancybox
	if($('.lightbox-image').length) {
		$('.lightbox-image').fancybox({
			openEffect  : 'elastic',
			closeEffect : 'elastic',
			helpers : {
				media : {}
			}
		});
	}
	
	
	//Sortable Masonary with Filters
	function enableMasonry() {
		if($('.sortable-masonry').length){
	
			var winDow = $(window);
			// Needed variables
			var $container=$('.sortable-masonry .items-container');
			var $filter=$('.sortable-masonry .filter-btns');
	
			$container.isotope({
				filter:'*',
				 masonry: {
					columnWidth : 1 
				 },
				animationOptions:{
					duration:1000,
					easing:'linear'
				}
			});
			
	
			// Isotope Filter 
			$filter.find('li').on('click', function(){
				var selector = $(this).attr('data-filter');
	
				try {
					$container.isotope({ 
						filter	: selector,
						animationOptions: {
							duration: 1000,
							easing	: 'linear',
							queue	: false
						}
					});
				} catch(err) {
	
				}
				return false;
			});
	
	
			winDow.bind('resize', function(){
				var selector = $filter.find('li.active').attr('data-filter');

				$container.isotope({ 
					filter	: selector,
					animationOptions: {
						duration: 1000,
						easing	: 'linear',
						queue	: false
					}
				});
			});
	
	
			var filterItemA	= $('.sortable-masonry .filter-btns li');
	
			filterItemA.on('click', function(){
				var $this = $(this);
				if ( !$this.hasClass('active')) {
					filterItemA.removeClass('active');
					$this.addClass('active');
				}
			});
		}
	}
	
	enableMasonry();
	
	
	
	//Gallery With Filters List
	if($('.filter-list').length){
		$('.filter-list').mixItUp({});
	}
	
	//PRODUCT With Filters List
	if($('.filter-product').length){
		$('.filter-product').mixItUp({});
	}
	
	
	//Date Picker
	  if($('.datepicker').length){
		$( ".datepicker" ).datepicker();
	  }
	
	
	//Contact Form Validation
	if($('#contact-form').length){
		$('#contact-form').validate({
			rules: {
				username: {
					required: true
				},
				email: {
					required: true,
					email: true
				},
				message: {
					required: true
				}
			}
		});
	}
	
	
	// Scroll to a Specific Div
	if($('.scroll-to-target').length){
		$(".scroll-to-target").on('click', function() {
			var HeaderHeight = $('.header-lower').height();
			var target = $(this).attr('data-target');
		   // animate
		   $('html, body').animate({
			   scrollTop: $(target).offset().top - HeaderHeight
			 }, 1000);
	
		});
	}
	
	
	// Elements Animation
	if($('.wow').length){
		var wow = new WOW(
		  {
			boxClass:     'wow',      // animated element css class (default is wow)
			animateClass: 'animated', // animation css class (default is animated)
			offset:       0,          // distance to the element when triggering the animation (default is 0)
			mobile:       false,       // trigger animations on mobile devices (default is true)
			live:         true       // act on asynchronously loaded content (default is true)
		  }
		);
		wow.init();
	}

/* ==========================================================================
   When document is Scrollig, do
   ========================================================================== */
	
	$(window).on('scroll', function() {
		headerStyle();
		factCounter();
	});
	
/* ==========================================================================
   When document is loading, do
   ========================================================================== */
	
	$(window).on('load', function() {
		handlePreloader();
		enableMasonry();
	});

	

})(window.jQuery);

	document.querySelectorAll('.title-category div').forEach((tab, index) => {
    tab.addEventListener('click', () => {
        document.querySelectorAll('.title-category div').forEach((t) => t.classList.remove('click-select'));
        tab.classList.add('click-select');
    });
});

  document.getElementById("menuToggle").addEventListener("click", function() {
        var sideNav = document.getElementById("sideNav");
        var overlay = document.getElementById("overlay");
    
        if (sideNav.classList.contains("open")) {
            sideNav.classList.remove("open");
            overlay.classList.remove("show");
        } else {
            sideNav.classList.add("open");
            overlay.classList.add("show");
        }
    });
    
    document.getElementById("closeBtn").addEventListener("click", function() {
        document.getElementById("sideNav").classList.remove("open");
        document.getElementById("overlay").classList.remove("show");
    });
    
    document.getElementById("overlay").addEventListener("click", function() {
        document.getElementById("sideNav").classList.remove("open");
        document.getElementById("overlay").classList.remove("show");
    });