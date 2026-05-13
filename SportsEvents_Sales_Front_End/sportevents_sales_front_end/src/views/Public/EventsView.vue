<script setup>
import { ref } from 'vue'
import { onMounted } from 'vue'
import axios from 'axios'


const events = ref([]);
const token = localStorage.getItem('token');


onMounted(() => {
  fetchEvents()
});

const fetchEvents = async () => {
  console.log('Loading events...')
  try {    
    const response = await axios.get(
      'http://192.168.122.44:5105/ticket-api/ticket-get-all',
      {
        headers: {
          Authorization: `Bearer ${token}`
        }
      }
    )
    console.log(response.data);
    if (response.data.status == 200) {
      events.value = response.data.dataset
    }
  } catch (error) {
    console.error('View error:', error)
  }
  console.log('events : ',events.value)
}
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
        <div class="card-body">
          <h5 class="card-title">
            {{ event.localTeam }} vs {{ event.visitorTeam}}
          </h5>
          <p class="text-muted">
            {{ event.location }}
          </p>
          <p class="card-text">
            {{ event.location }}
          </p>
        </div>
        <div class="card-footer bg-white border-0">
          <div class="d-flex justify-content-between align-items-center">
            <span class="fw-bold">
              ${{ event.solePrice }}
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
