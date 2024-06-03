import * as THREE from 'three';
import Stats from 'stats.js';
import WebGL from 'three/addons/capabilities/WebGL.js';

var stats = new Stats();
stats.showPanel(1);
document.body.appendChild(stats.dom);

// Setup scene, camera, and renderer
const scene = new THREE.Scene();
const camera = new THREE.OrthographicCamera(window.innerWidth / -2, window.innerWidth / 2, window.innerHeight / 2, window.innerHeight / -2, 1, 1000);
const renderer = new THREE.WebGLRenderer({ antialias: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// Adjust camera position
camera.position.z = 5;

// Function to create a square
function createSquare(x, y, size, color) {
    const geometry = new THREE.PlaneGeometry(size, size);
    const material = new THREE.MeshBasicMaterial({ color: color });
    const square = new THREE.Mesh(geometry, material);
    square.position.set(x, y, 0);
    return square;
}

// Define the size of the squares and the number of squares
const squareSize = 20;
const columns = Math.floor(window.innerWidth / squareSize);
const rows = Math.floor(window.innerHeight / squareSize);

// Calculate border size
const horizontalBorder = (window.innerWidth - columns * squareSize) / 2;
const verticalBorder = (window.innerHeight - rows * squareSize) / 2;
const borderColor = 0x0; // Grey color

// Create the grid of squares
for (let i = 0; i < columns; i++) {
    for (let j = 0; j < rows; j++) {
        const color = (i + j) % 2 === 0 ? 0x000000 : 0xFFFFFF;
        const x = i * squareSize - window.innerWidth / 2 + squareSize / 2 + horizontalBorder;
        const y = j * squareSize - window.innerHeight / 2 + squareSize / 2 + verticalBorder;
        const square = createSquare(x, y, squareSize, color);
        scene.add(square);
    }
}

// Create border planes
const borderGeometryH = new THREE.PlaneGeometry(window.innerWidth, verticalBorder * 2);
const borderGeometryV = new THREE.PlaneGeometry(horizontalBorder * 2, window.innerHeight);

// Top border
const topBorder = new THREE.Mesh(borderGeometryH, new THREE.MeshBasicMaterial({ color: borderColor }));
topBorder.position.set(0, window.innerHeight / 2 - verticalBorder / 2, 0);
scene.add(topBorder);

// Bottom border
const bottomBorder = new THREE.Mesh(borderGeometryH, new THREE.MeshBasicMaterial({ color: borderColor }));
bottomBorder.position.set(0, -window.innerHeight / 2 + verticalBorder / 2, 0);
scene.add(bottomBorder);

// Left border
const leftBorder = new THREE.Mesh(borderGeometryV, new THREE.MeshBasicMaterial({ color: borderColor }));
leftBorder.position.set(-window.innerWidth / 2 + horizontalBorder / 2, 0, 0);
scene.add(leftBorder);

// Right border
const rightBorder = new THREE.Mesh(borderGeometryV, new THREE.MeshBasicMaterial({ color: borderColor }));
rightBorder.position.set(window.innerWidth / 2 - horizontalBorder / 2, 0, 0);
scene.add(rightBorder);

// Handle window resize
// window.addEventListener('resize', () => {
//     const aspect = window.innerWidth / window.innerHeight;
//     camera.left = -window.innerWidth / 2;
//     camera.right = window.innerWidth / 2;
//     camera.top = window.innerHeight / 2;
//     camera.bottom = -window.innerHeight / 2;
//     camera.updateProjectionMatrix();
//     renderer.setSize(window.innerWidth, window.innerHeight);
//     // Reload the page to reinitialize the scene
//     location.reload();
// });

// Render the scene
function animate() {
    stats.begin();
    renderer.render(scene, camera);
    stats.end();

    requestAnimationFrame(animate);
}

if (WebGL.isWebGLAvailable()) {
    console.log("starting animation...")
    animate();
} else {
    const warning = WebGL.getWebGLErrorMessage();
    document.body.appendChild(warning);
}