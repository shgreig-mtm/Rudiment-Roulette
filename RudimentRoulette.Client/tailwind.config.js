/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'drum-dark': '#1a1a1a',
        'drum-slate': '#2d3748',
        'drum-gray': '#4a5568',
        'drum-orange': '#ed8936',
        'drum-orange-light': '#f6ad55',
      },
    },
  },
  plugins: [],
}
