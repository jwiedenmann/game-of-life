import { defineConfig } from 'vite';
import string from 'vite-plugin-string';

export default defineConfig({
  plugins: [
    string({
      include: '**/*.glsl', // or the appropriate file extension for your shaders
    }),
  ],
});