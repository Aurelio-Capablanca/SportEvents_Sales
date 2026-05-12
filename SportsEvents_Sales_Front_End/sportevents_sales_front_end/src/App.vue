<script setup>
import { ref } from 'vue'
import Navbar from './components/Navbar.vue';


const loggedIn = ref(!!localStorage.getItem('token'));

const handleLoginSuccess = (data) => {
  loggedIn.value = true;
}

</script>

<template>
  <Navbar />
  <main class="py-4">
    <router-view @login-success="handleLoginSuccess" />
  </main>
</template>
