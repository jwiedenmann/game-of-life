import * as THREE from 'three';
import Stats from 'stats.js';
import WebGL from 'three/addons/capabilities/WebGL.js';

var stats = new Stats();
stats.showPanel(1);
document.body.appendChild(stats.dom);

const scene = new THREE.Scene();
// const camera = new THREE.PerspectiveCamera(90, window.innerWidth / window.innerHeight, 0.1, 1000);
// camera.position.z = 5;
const zoom = 150
const camera = new THREE.OrthographicCamera(window.innerWidth / -zoom, window.innerWidth / zoom, window.innerHeight / zoom, window.innerHeight / -zoom, -500, 1000);

// Create the renderer
const renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// Your 2D array
const array2D = [
    [1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1,],
    [0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,],
    [1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1,],
    [0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,],
    [1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1,],
    [0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,],
    [1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1,],
    [0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,],
    [1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1,],
    [0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,],
    [1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1,],
];

// const array2D = [
//     [1, 0, 1],
//     [0, 1, 0],
//     [1, 0, 1]
// ];


// Create the geometry and material for the squares
const geometry = new THREE.PlaneGeometry(1, 1);
const material = new THREE.MeshBasicMaterial({ color: 0x00ff00, side: THREE.DoubleSide });

// Loop through the 2D array
for (let i = 0; i < array2D.length; i++) {
    for (let j = 0; j < array2D[i].length; j++) {

        const square = new THREE.Mesh(geometry, material);
        square.position.x = i - parseInt(array2D.length / 2);
        console.log(square.position.x)
        square.position.y = j - parseInt(array2D[i].length / 2);

        // Add the square to the scene
        scene.add(square);
    }
}


function animate() {

    stats.begin();

    // monitored code goes here
    renderer.render(scene, camera);

    stats.end();

    requestAnimationFrame(animate);

}

if (WebGL.isWebGLAvailable()) {
    animate();
} else {
    const warning = WebGL.getWebGLErrorMessage();
    document.body.appendChild(warning);
}