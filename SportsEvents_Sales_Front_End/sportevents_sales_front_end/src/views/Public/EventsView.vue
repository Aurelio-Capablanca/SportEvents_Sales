<script setup>
import { ref } from 'vue'

const events = ref([
  {
    id: 1,
    title: 'Rock Concert',
    quantity: 2,
    date: "10/10/2040",
    price: 40
  },
  {
    id: 2,
    title: 'Jazz Festival',
    quantity: 1,
    date: "10/10/2040",
    price: 25
  }
])
</script>


<template>
    <div class="mb-4">
      <h1 class="fw-bold">
        Upcoming Games
      </h1>
      <p class="text-muted">
        Browse available Soccer Games
      </p>
    </div>
    <div class="row g-4">
      <div class="col-md-6 col-lg-4" v-for="event in events" :key="event.id">
        <div class="card h-100 shadow-sm">
          <img :src="event.image" class="card-img-top" alt="">
          <div class="card-body">
            <h5 class="card-title">
              {{ event.title }}
            </h5>
            <p class="text-muted">
              {{ event.date }}
            </p>
            <p class="card-text">
              {{ event.description }}
            </p>
          </div>
          <div class="card-footer bg-white border-0">
            <div class="d-flex justify-content-between align-items-center">
              <span class="fw-bold">
                ${{ event.price }}
              </span>
              <router-link :to="'/events/' + event.id" class="btn btn-primary">
                View
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>  
</template>
