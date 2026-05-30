<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const loggedIn = ref(!!localStorage.getItem('token'));
const isAdmin = ref(localStorage.getItem('isAdmin') === 'true')
console.log(isAdmin.value)


const logout = () => {
  localStorage.removeItem('token')
  localStorage.removeItem('userEmail')
  localStorage.removeItem('isAdmin')
  loggedIn.value = false
  router.push('/login-client')
}
</script>
<template>
  <header v-if="loggedIn">
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark shadow">      
      <div class="container" v-if="isAdmin">
        <router-link class="navbar-brand" to="/admin">Ticket Seller</router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav me-auto">
            <li class="nav-item">
              <router-link class="nav-link" to="/admin">Home</router-link>
            </li>            
            <li class="nav-item">
              <router-link class="nav-link" to="/game-admin">Game</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/stadium-admin">Stadiums</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/zone-price-admin">Zone Prices</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/ticket-admin">Tickets</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/user-admin">User</router-link>
            </li>
          </ul>
          <div class="d-flex align-items-center">
            <span class="text-light me-3 small">Backend Lab Active</span>
            <button class="btn btn-outline-danger btn-sm" @click="logout">Logout</button>
          </div>
        </div>
      </div>
      <div class="container" v-else>
        <router-link class="navbar-brand" to="/">Ticket Seller</router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav me-auto">
            <li class="nav-item">
              <router-link class="nav-link" to="/public-dashboard">Home</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/cart">Cart</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" to="/client-profile">Profile</router-link>
            </li>
          </ul>
          <div class="d-flex align-items-center">            
            <button class="btn btn-outline-danger btn-sm" @click="logout">Logout</button>
          </div>
        </div>
      </div>
    </nav>
  </header>
</template>