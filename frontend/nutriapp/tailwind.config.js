/** @type {import('tailwindcss').Config} */
export default {
  content: ['./src/**/*.{html,js,svelte,ts}'],
  theme: {
    extend: {
      screens: {
        md: '880px',
        lg: '976px',
        xl: '1440px',
      },
      colors: {
        'dark-green': '#386641',
        'dark-dark-green': '#284a2f',
        'primary-green': '#6A994E',
        'light-green': '#A7C957',
        'nutri-white': '#F2E8CF',
        'nutri-red': '#BC4749'
      },
      borderRadius: {
        'full': '15px',
        'max': '999px'
      }
  }
  },
  plugins: []
};