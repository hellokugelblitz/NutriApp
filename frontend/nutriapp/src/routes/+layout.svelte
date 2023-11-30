
<script>
   //Important svelteKit stuff
   import "../app.css";
   import { onNavigate } from "$app/navigation";
   // Svelte Toast import
   import { SvelteToast, toast } from '@zerodevx/svelte-toast'

   //A little tester functions
   function sendToast(){
      toast.push('Bottoms up!')
   }
</script>

<!-- SVELTE TOAST STUFF -->
<style>
   :root {
      display: hidden;
     --toastContainerTop: auto;
     --toastContainerRight: auto;
     --toastContainerBottom: 1rem;
     --toastContainerLeft: 80vw;
   }

   :global(.custom) {
    --toastBackground: #84bb67;
    --toastBarBackground: #3c663b;
    --toastColor: black;
    --toastPadding: 8px;
  }
</style>

<!-- SIDEBAR -->
<aside id="default-sidebar" class="fixed top-0 left-0 z-40 w-64 h-screen transition-transform -translate-x-full md:translate-x-0" aria-label="Sidebar">
   <div class="h-full px-3 py-4 overflow-y-auto bg-dark-green">

      <div class="self-center content-center mt-4 mb-8">
         <svg class="mx-16" fill="#FFFFFF" width="100px" height="100px" viewBox="0 0 32 32" version="1.1" xmlns="http://www.w3.org/2000/svg">
            <title>cheese</title>
            <path d="M11.188 11.887c0.217 0.304 0.34 0.676 0.34 1.095 0 1.326-1.234 2.623-2.757 2.897s-2.757-0.58-2.757-1.906c0-0.261 0.049-0.521 0.137-0.773l-5.492 1.432v7.235c0.243-0.116 0.484-0.248 0.748-0.308 1.544-0.355 2.796 0.471 2.796 1.817s-1.253 2.731-2.796 3.086c-0.262 0.060-0.508 0.072-0.748 0.069v2.092l13.516-3.524c-0.355-0.471-0.562-1.035-0.562-1.64 0-1.654 1.54-2.996 3.441-2.996 1.863 0 3.38 1.29 3.438 2.9 0.058 0.016 0.115 0.031 0.173 0.047l10.515-2.709v-14.024l-19.993 5.211zM24.767 19.393c-1.017 0-1.841-0.718-1.841-1.604s0.824-1.604 1.841-1.604 1.841 0.718 1.841 1.604-0.824 1.604-1.841 1.604zM14.346 2.832l-1.022 0.863c1.243 0.64 1.46 1.937 0.481 2.904-0.982 0.97-2.796 1.24-4.052 0.603-0.124-0.063-0.237-0.132-0.34-0.207l-8.368 7.062 28.673-7.525c-3.207-2.792-9.45-4.136-15.372-3.699zM29.718 6.531v0 0z"></path>
         </svg>
         <span class="flex-1 whitespace-nowrap text-white font-semibold text-3xl mx-14">Nutriapp</span>
      </div>

      <ul class="space-y-1 font-extralight">
         <li>
            <a href="/" class="flex items-center p-2 rounded-lg text-white hover:bg-dark-dark-green group transition-all">          
                <!-- Dashboard SVG -->
               <svg class="flex-shrink-0 w-5 h-5 text-white transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" fill="#FFFFFF" version="1.1" viewBox="0 0 495.398 495.398">
                           <path d="M487.083,225.514l-75.08-75.08V63.704c0-15.682-12.708-28.391-28.413-28.391c-15.669,0-28.377,12.709-28.377,28.391
                              v29.941L299.31,37.74c-27.639-27.624-75.694-27.575-103.27,0.05L8.312,225.514c-11.082,11.104-11.082,29.071,0,40.158
                              c11.087,11.101,29.089,11.101,40.172,0l187.71-187.729c6.115-6.083,16.893-6.083,22.976-0.018l187.742,187.747
                              c5.567,5.551,12.825,8.312,20.081,8.312c7.271,0,14.541-2.764,20.091-8.312C498.17,254.586,498.17,236.619,487.083,225.514z"/>
                           <path d="M257.561,131.836c-5.454-5.451-14.285-5.451-19.723,0L72.712,296.913c-2.607,2.606-4.085,6.164-4.085,9.877v120.401
                              c0,28.253,22.908,51.16,51.16,51.16h81.754v-126.61h92.299v126.61h81.755c28.251,0,51.159-22.907,51.159-51.159V306.79
                              c0-3.713-1.465-7.271-4.085-9.877L257.561,131.836z"/>
               </svg>

               <span class="flex-1 ms-6 whitespace-nowrap">Dashboard</span>
            </a>
         </li>

         <li>
            <a href="/protected/food" class="flex items-center p-2 rounded-lg text-white hover:bg-dark-dark-green group transition-all">          
                <!-- Food SVG -->
                <svg class="flex-shrink-0 w-5 h-5 text-white transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" fill="#FFFFFF" viewBox="0 -3.84 122.88 122.88">
                     <path d="M29.03,100.46l20.79-25.21l9.51,12.13L41,110.69C33.98,119.61,20.99,110.21,29.03,100.46L29.03,100.46z M53.31,43.05 c1.98-6.46,1.07-11.98-6.37-20.18L28.76,1c-2.58-3.03-8.66,1.42-6.12,5.09L37.18,24c2.75,3.34-2.36,7.76-5.2,4.32L16.94,9.8 c-2.8-3.21-8.59,1.03-5.66,4.7c4.24,5.1,10.8,13.43,15.04,18.53c2.94,2.99-1.53,7.42-4.43,3.69L6.96,18.32 c-2.19-2.38-5.77-0.9-6.72,1.88c-1.02,2.97,1.49,5.14,3.2,7.34L20.1,49.06c5.17,5.99,10.95,9.54,17.67,7.53 c1.03-0.31,2.29-0.94,3.64-1.77l44.76,57.78c2.41,3.11,7.06,3.44,10.08,0.93l0.69-0.57c3.4-2.83,3.95-8,1.04-11.34L50.58,47.16 C51.96,45.62,52.97,44.16,53.31,43.05L53.31,43.05z M65.98,55.65l7.37-8.94C63.87,23.21,99-8.11,116.03,6.29 C136.72,23.8,105.97,66,84.36,55.57l-8.73,11.09L65.98,55.65L65.98,55.65z"/>
               </svg>

               <span class="flex-1 ms-6 whitespace-nowrap">Food</span>
            </a>
         </li>


         <li>
            <a href="/workout" class="flex items-center p-2 rounded-lg text-white hover:bg-dark-dark-green group transition-all"> 
               <!-- Workouts SVG -->
               <svg class="flex-shrink-0 w-5 h-5 text-white transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" fill="#FFFFFF" viewBox="0 -1.26 122.88 122.88">
                  <path class="st0" d="M8.94,102.48h85.62c3.71-3.85,8.02-14.75,12.45-25.96c1.41-3.57,2.83-7.17,4.3-10.66 c-6.43,0.02-15.18-0.61-16.52-2.15c-1.04-1.19-0.66-2.93,0.91-5.12l18.93-18.48c1.77-1.6,3.31-2.21,4.62-1.81 c1.3,0.39,2.38,1.76,3.23,4.13c0.48,7.13,0.43,14.27-0.63,21.4c-0.58,0.34-1.18,0.63-1.79,0.89c-2.01,4.48-4.01,9.56-6,14.58 c-3.41,8.63-6.75,17.09-10.52,23.18h10.39c4.92,0,8.94,4.02,8.94,8.94v0c0,4.92-4.02,8.94-8.94,8.94h-105 c-4.92,0-8.94-4.02-8.94-8.94v0C0,106.51,4.02,102.48,8.94,102.48L8.94,102.48z M36.43,36.39l-0.27-0.45l-7.06,1.01L26.54,52.6 c-0.48,2.93-3.23,4.91-6.16,4.44c-2.93-0.48-4.91-3.23-4.44-6.16l3.2-19.55c0.39-2.38,2.28-4.13,4.54-4.45l0,0l13.67-1.96 l10.11-2.21c5.76,0.19,10.06,2.61,12,7.7c0.28,0.35,0.52,0.75,0.71,1.18l5.42,12.39l17.68,1.1c2.96,0.17,5.22,2.72,5.04,5.68 c-0.17,2.96-2.72,5.22-5.68,5.05l-20.97-1.3c-2.16-0.13-3.95-1.52-4.69-3.41l-1.1-2.51l-2.25,9.79l14.84,8.74 c1.29,0.75,2.14,1.96,2.48,3.3l0.02,0l5.39,21.11c0.73,2.88-1.01,5.81-3.89,6.54c-2.88,0.73-5.81-1.01-6.54-3.89L61.1,75.19 l-1.24-0.73l-15.42-5.87l-0.83,0.46l-4.71,12.49c-0.34,0.9-0.89,1.65-1.58,2.23l0,0L19.65,98.6c-2.28,1.9-5.66,1.6-7.57-0.67 c-1.9-2.28-1.6-5.66,0.67-7.57L29.03,76.7C30.22,62.9,32.82,49.5,36.43,36.39L36.43,36.39z M57.91,0c5.64,0,10.21,4.57,10.21,10.21 c0,5.64-4.57,10.21-10.21,10.21c-5.64,0-10.21-4.57-10.21-10.21C47.7,4.57,52.27,0,57.91,0L57.91,0z"/>
               </svg>
               <span class="flex-1 ms-6 whitespace-nowrap">My Workouts</span>
            </a>
         </li> 


         <li>
            <a href="/" class="flex items-center p-2 rounded-lg text-white hover:bg-dark-dark-green group transition-all"> 
               <!-- INVITES SVG -->
               <svg class="flex-shrink-0 w-5 h-5 text-white transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
                  <path d="m17.418 3.623-.018-.008a6.713 6.713 0 0 0-2.4-.569V2h1a1 1 0 1 0 0-2h-2a1 1 0 0 0-1 1v2H9.89A6.977 6.977 0 0 1 12 8v5h-2V8A5 5 0 1 0 0 8v6a1 1 0 0 0 1 1h8v4a1 1 0 0 0 1 1h2a1 1 0 0 0 1-1v-4h6a1 1 0 0 0 1-1V8a5 5 0 0 0-2.582-4.377ZM6 12H4a1 1 0 0 1 0-2h2a1 1 0 0 1 0 2Z"/>
               </svg>

               <span class="flex-1 ms-6 whitespace-nowrap">Notifications</span>
               <span class="inline-flex items-center justify-center w-3 h-3 p-3 ms-3 text-sm font-medium bg-lime-600 bg-opacity-25 rounded-full text-lime-100">3</span>
            </a>
         </li>

         <li>
            <a href="/" class="flex items-center p-2 rounded-lg text-white hover:bg-dark-dark-green group transition-all"> 
               <!-- TEAMS SVG -->
               <svg class="flex-shrink-0 w-5 h-5 text-white transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 18">
                  <path d="M14 2a3.963 3.963 0 0 0-1.4.267 6.439 6.439 0 0 1-1.331 6.638A4 4 0 1 0 14 2Zm1 9h-1.264A6.957 6.957 0 0 1 15 15v2a2.97 2.97 0 0 1-.184 1H19a1 1 0 0 0 1-1v-1a5.006 5.006 0 0 0-5-5ZM6.5 9a4.5 4.5 0 1 0 0-9 4.5 4.5 0 0 0 0 9ZM8 10H5a5.006 5.006 0 0 0-5 5v2a1 1 0 0 0 1 1h11a1 1 0 0 0 1-1v-2a5.006 5.006 0 0 0-5-5Z"/>
               </svg>

               <span class="flex-1 ms-6 whitespace-nowrap">My Team</span>
            </a>
         </li>

      </ul>

      <ul class="space-y-2 font-extralight absolute bottom-0 mb-2">
         <li>
            <a href="/login" class="flex items-center p-2 rounded-lg text-white hover:bg-dark-dark-green group transition-all"> 
               <!-- SIGN IN SVG -->
               
                  <svg class="flex-shrink-0 w-5 h-5 text-white transition duration-7 group-hover:text-gray-900 dark:group-hover:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 18 16">
                     <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M1 8h11m0 0L8 4m4 4-4 4m4-11h3a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2h-3"/>
                  </svg>

                  <span class="flex-1 ms-6 whitespace-nowrap">Sign In</span>
               
            </a>
         </li>

         <li>
            <a href="/register" class="flex items-center p-2 rounded-lg text-white hover:bg-dark-dark-green group transition ease-in-out"> 
               <!-- SIGN UP SVG -->
               <svg class="flex-shrink-0 w-5 h-5 text-white transition duration-75 group-hover:text-gray-900 dark:group-hover:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
                  <path d="M5 5V.13a2.96 2.96 0 0 0-1.293.749L.879 3.707A2.96 2.96 0 0 0 .13 5H5Z"/>
                  <path d="M6.737 11.061a2.961 2.961 0 0 1 .81-1.515l6.117-6.116A4.839 4.839 0 0 1 16 2.141V2a1.97 1.97 0 0 0-1.933-2H7v5a2 2 0 0 1-2 2H0v11a1.969 1.969 0 0 0 1.933 2h12.134A1.97 1.97 0 0 0 16 18v-3.093l-1.546 1.546c-.413.413-.94.695-1.513.81l-3.4.679a2.947 2.947 0 0 1-1.85-.227 2.96 2.96 0 0 1-1.635-3.257l.681-3.397Z"/>
                  <path d="M8.961 16a.93.93 0 0 0 .189-.019l3.4-.679a.961.961 0 0 0 .49-.263l6.118-6.117a2.884 2.884 0 0 0-4.079-4.078l-6.117 6.117a.96.96 0 0 0-.263.491l-.679 3.4A.961.961 0 0 0 8.961 16Zm7.477-9.8a.958.958 0 0 1 .68-.281.961.961 0 0 1 .682 1.644l-.315.315-1.36-1.36.313-.318Zm-5.911 5.911 4.236-4.236 1.359 1.359-4.236 4.237-1.7.339.341-1.699Z"/>
               </svg>

               <span class="flex-1 ms-6 whitespace-nowrap">Sign Up</span>
            </a>
         </li>
      </ul>

   </div>
</aside>

<!-- Page infromation -->
<slot />

<SvelteToast />
