import * as THREE from 'three';
import Stats from 'stats.js';
import WebGL from 'three/addons/capabilities/WebGL.js';
import util from 'util'
import fs from 'fs'
import path from 'path'

import vertexShader from './shaders/vertexShader.glsl';
import fragmentShader from './shaders/fragmentShader.glsl';

console.log(vertexShader)

// import vertexShader from './shaders/vertexShader.js'
// import fragmentShader from './shaders/fragmentShader.js'

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

// Define the size of the squares and the number of squares
const squareSize = 200;
const color = 0xFFFFFF;

const geometry = new THREE.PlaneGeometry(squareSize, squareSize);
// const material = new THREE.MeshBasicMaterial({ color: color });
const material = new THREE.ShaderMaterial({
    uniforms: {},
    vertexShader: vertexShader,
    fragmentShader: fragmentShader
});
const square = new THREE.Mesh(geometry, material);
square.position.set(0, 0, 0);
scene.add(square);


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