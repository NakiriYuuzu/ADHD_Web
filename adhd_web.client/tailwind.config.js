/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  darkMode: 'media', // 'false' or 'media' or 'class'
  theme: {
    extend: {
      colors : {
        'primary': '#3a57e8',
        'modal-bg': '#151824',
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}