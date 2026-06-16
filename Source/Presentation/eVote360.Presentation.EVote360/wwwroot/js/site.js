// eVote360 - Common JavaScript

function evNavigateError(direction) {
    const slides = document.querySelectorAll('.ev-error-slide');
    const currentText = document.getElementById('evErrorCurrent');
    const prevBtn = document.getElementById('evErrorPrev');
    const nextBtn = document.getElementById('evErrorNext');
    
    let activeIndex = Array.from(slides).findIndex(s => s.classList.contains('active'));
    let nextIndex = activeIndex + direction;

    if (nextIndex >= 0 && nextIndex < slides.length) {
        slides[activeIndex].classList.remove('active');
        slides[nextIndex].classList.add('active');
        
        currentText.innerText = nextIndex + 1;
        
        prevBtn.disabled = nextIndex === 0;
        nextBtn.disabled = nextIndex === slides.length - 1;
    }
}

// Auto-hide notifications after 5 seconds
document.addEventListener('DOMContentLoaded', () => {
    const notification = document.getElementById('evTempNotification');
    if (notification) {
        setTimeout(() => {
            notification.style.opacity = '0';
            notification.style.transition = 'opacity 0.5s ease';
            setTimeout(() => notification.remove(), 500);
        }, 5000);
    }
});
