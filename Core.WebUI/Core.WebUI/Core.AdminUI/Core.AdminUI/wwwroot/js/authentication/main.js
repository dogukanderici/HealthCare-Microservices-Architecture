// ----------------------------------------------------
// Form Logic & Button Animation
// ----------------------------------------------------
const loginForm = document.getElementById('loginForm');
const submitBtn = document.getElementById('submitBtn');

loginForm.addEventListener('submit', (e) => {
    e.preventDefault(); // Prevent page reload

    // Add loading state
    submitBtn.classList.add('loading');
    submitBtn.disabled = true;

    // Simulate API Call / Processing time
    setTimeout(() => {
        submitBtn.classList.remove('loading');
        submitBtn.disabled = false;

        // Show a success indication (optional visual feedback)
        const btnText = submitBtn.querySelector('.btn-text');
        const originalText = btnText.innerText;
        btnText.innerText = "Success!";
        submitBtn.style.background = "linear-gradient(135deg, #10b981, #059669)"; // Emerald green

        // Reset after 2 seconds
        setTimeout(() => {
            btnText.innerText = originalText;
            submitBtn.style.background = "";
            loginForm.reset();
        }, 2000);

    }, 2000);
});

// ----------------------------------------------------
// Canvas Particle Background System
// ----------------------------------------------------
const canvas = document.getElementById('particle-canvas');
const ctx = canvas.getContext('2d');

let particlesArray;
let w = canvas.width = window.innerWidth;
let h = canvas.height = window.innerHeight;

// Resize canvas on window resize
window.addEventListener('resize', () => {
    w = canvas.width = window.innerWidth;
    h = canvas.height = window.innerHeight;
    initParticles();
});

// Create particle class
class Particle {
    constructor(x, y, directionX, directionY, size, color) {
        this.x = x;
        this.y = y;
        this.directionX = directionX;
        this.directionY = directionY;
        this.size = size;
        this.color = color;
    }

    // Method to draw individual particle
    draw() {
        ctx.beginPath();
        ctx.arc(this.x, this.y, this.size, 0, Math.PI * 2, false);
        ctx.fillStyle = this.color;
        ctx.fill();
    }

    // Check particle position, mouse position, move the particle, draw the particle
    update() {
        // Check if particle is still within canvas
        if (this.x > w || this.x < 0) {
            this.directionX = -this.directionX;
        }
        if (this.y > h || this.y < 0) {
            this.directionY = -this.directionY;
        }

        // Move particle
        this.x += this.directionX;
        this.y += this.directionY;

        // Draw particle
        this.draw();
    }
}

// Initialize particles array
function initParticles() {
    particlesArray = [];
    // Calculate number of particles based on screen size
    let numberOfParticles = (w * h) / 10000;
    // Cap the maximum particles to prevent lag
    if (numberOfParticles > 150) numberOfParticles = 150;

    for (let i = 0; i < numberOfParticles; i++) {
        let size = (Math.random() * 2) + 0.5;
        let x = (Math.random() * ((innerWidth - size * 2) - (size * 2)) + size * 2);
        let y = (Math.random() * ((innerHeight - size * 2) - (size * 2)) + size * 2);

        // Slower movement speed for a subtle, elegant feel
        let directionX = (Math.random() * 0.8) - 0.4;
        let directionY = (Math.random() * 0.8) - 0.4;

        // Color matching the slate/indigo theme with low opacity
        let color = 'rgba(99, 102, 241, 0.15)'; // Indigo

        particlesArray.push(new Particle(x, y, directionX, directionY, size, color));
    }
}

// Draw lines connecting nearby particles
function connectParticles() {
    let opacityValue = 1;
    for (let a = 0; a < particlesArray.length; a++) {
        for (let b = a; b < particlesArray.length; b++) {
            let distance = ((particlesArray[a].x - particlesArray[b].x) * (particlesArray[a].x - particlesArray[b].x))
                + ((particlesArray[a].y - particlesArray[b].y) * (particlesArray[a].y - particlesArray[b].y));

            // If close enough, draw a line
            if (distance < (w / 10) * (h / 10)) {
                opacityValue = 1 - (distance / 20000);
                ctx.strokeStyle = `rgba(148, 163, 184, ${opacityValue * 0.2})`; // Slate colored faint lines
                ctx.lineWidth = 1;
                ctx.beginPath();
                ctx.moveTo(particlesArray[a].x, particlesArray[a].y);
                ctx.lineTo(particlesArray[b].x, particlesArray[b].y);
                ctx.stroke();
            }
        }
    }
}

// Animation Loop
function animate() {
    requestAnimationFrame(animate);
    ctx.clearRect(0, 0, innerWidth, innerHeight);

    for (let i = 0; i < particlesArray.length; i++) {
        particlesArray[i].update();
    }
    connectParticles();
}

// Start background
initParticles();
animate();