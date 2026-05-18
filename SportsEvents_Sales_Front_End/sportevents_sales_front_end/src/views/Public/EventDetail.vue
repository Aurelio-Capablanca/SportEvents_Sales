<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import axios from 'axios'

const route = useRoute()
const token = localStorage.getItem('token');
const event = ref(null)
const seatAdd = ref({})

onMounted(() => {
    loadEvent()
})


const loadEvent = async () => {
    console.log('Loading events...')
    try {
        const response = await axios.get(
            'http://192.168.122.44:5105/ticket-api/ticket-get-one/' + route.params.id,
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
    //console.log('events : ', event.value)
}


/*
"Tickets": [
        {
            "IdTicket": 7,
            "IdPriceTicket" : 4,
            "AvailableSeats": 600,
            "InCartTickets": 5
        }
*/

const addToCart = async (ticket, price, availableSeats) => {
    console.log("inside!", ticket)
    console.log(seatAdd.value)
    const quantity =
        seatAdd.value[price] || 1
    console.log(quantity, price, availableSeats)
    const tickets = []
    tickets.push({
        IdTicket: ticket,
        IdPriceTicket: price,
        AvailableSeats: availableSeats,
        InCartTickets: quantity
    })
    console.log(tickets)
    try {
        const request = await axios.post('http://192.168.122.44:5105/cart-api/save-cart',
            {
                Tickets: tickets
            },
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            })
        console.log(request.data);
        if (request.data.status == 200) {
            console.log(request.data.dataset);
        }
    } catch (error) {
        console.error('Cart error:', error)
    }

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
                        <p>{{ event.stadium }} , {{ event.location }}</p>
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
                        <input type="number" min="1" class="form-control" style="width: 100px"
                            v-model.number="seatAdd[ticket.id]">
                        <button class="btn btn-success" @click="addToCart(event.idTicket, ticket.id, ticket.availableSeats)">
                            Add to Cart
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>