<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import axios from 'axios'

const route = useRoute()
const token = localStorage.getItem('token');
const event = ref(null)

onMounted(() => {
    loadEvent()
})


const loadEvent = async () => {
console.log('Loading events...')
  try {    
    const response = await axios.get(
      'http://192.168.122.44:5105/ticket-api/ticket-get-one/'+route.params.id,
      {
        headers: {
          Authorization: `Bearer ${token}`
        }
      }
    )
    console.log(response.data);
    if (response.data.status == 200) {
      event.value = response.data.dataset
    }
  } catch (error) {
    console.error('View error:', error)
  }
  console.log('events : ',event.value)
}
</script>

<template>
    <div class="container py-5" v-if="event">
        <div class="card shadow-sm">
            <img :src="event.image" class="card-img-top">
            <div class="card-body p-4">
                <h1 class="fw-bold mb-3">
                    {{ event.localTeam }}
                    vs
                    {{ event.visitorTeam }}
                </h1>
                <div class="row mb-4">
                    <div class="col-md-3">
                        <strong>Date</strong>
                        <p>{{ event.date }}</p>
                    </div>
                    <div class="col-md-3">
                        <strong>Time</strong>
                        <p>{{ event.time }}</p>
                    </div>
                    <div class="col-md-3">
                        <strong>Stadium</strong>
                        <p>{{ event.stadium }} ,  {{ event.location }}</p>
                    </div>
                    <div class="col-md-3">
                        <strong>Tournament</strong>
                        <p>{{ event.tournament }}</p>
                    </div>
                </div>
                <h3 class="mb-3">
                    Available Tickets
                </h3>
                <div v-for="ticket in event.prices" :key="ticket.type" class="border rounded p-3 mb-3">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <h5 class="mb-1">
                                {{ ticket.zonePrice }}
                            </h5>
                            <span class="text-muted">
                                ${{ ticket.price }}
                            </span>
                        </div>
                        <button class="btn btn-success">
                            Add to Cart
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>