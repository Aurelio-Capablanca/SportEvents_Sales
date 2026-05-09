<script setup>
// import HelloWorld from './components/HelloWorld.vue'
// import TheWelcome from './components/TheWelcome.vue'
import { ref } from 'vue'
import Login from './components/Login.vue';

const loggedIn = ref(!!localStorage.getItem('token'));
const handleLoginSuccess = (data) => {
  loggedIn.value = true;
  console.log('Logged as ', data.email, '!')
}
</script>

<!-- <template>
  <div v-if="!loggedIn">
    <Login @login-success="handleLoginSuccess" />
  </div>
  <div v-else>
    <h1>Welcome to the Backend Lab Dashboard</h1>
    <button @click="loggedIn = false; localStorage.removeItem('token')">Logout</button>
  </div>
</template> -->
<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
    <div class="container">
      <router-link class="navbar-brand" to="/">My Project</router-link>
      <div class="navbar-nav">
        <router-link class="nav-link" to="/">Home</router-link>
        <router-link class="nav-link" to="/admin">Admin</router-link>
      </div>
    </div>
  </nav>

  <!-- This is where the magic happens -->
  <router-view />
</template>


<!-- <style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style> -->
