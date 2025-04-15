window.onload = function() {
    const carousel = document.querySelector('.hero-carousel');
    let index = 0;
    const images = document.querySelectorAll('.hero-image');
    const totalImages = images.length;

    function moveCarousel() {
        index = (index + 1) % totalImages;
        carousel.style.transform = `translateX(-${index * 100}%)`;
    }

    setInterval(moveCarousel, 3000);  // Change every 3 seconds
};




window.onload = function() {
    const serviceItems = document.querySelectorAll('.service-item');
    
    // Applique un délai d'animation croissant pour chaque élément
    serviceItems.forEach((item, index) => {
        item.style.animationDelay = `${index * 0.3 + 0.3}s`; // Délai croissant (commence à 0.3s)
    });
};



document.addEventListener('DOMContentLoaded', () => {
    const aboutImage = document.querySelector('.about-image');

    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                aboutImage.style.animation = 'slideInLeft 0.8s ease-out forwards';
                observer.unobserve(aboutImage); 
            }
        });
    }, {
        threshold: 0.5
    });

    observer.observe(aboutImage);
});

