<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'

const token = localStorage.getItem('token');
const cart = ref([])


onMounted(() => {
    loadCart()
})


const loadCart = async () => {
    console.log('Loading events...')
    try {
        const response = await axios.get(
            'http://192.168.122.44:5105/cart-api/get-cart',
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        console.log(response.data);
        if (response.data.status == 200) {
            cart.value = response.data.dataset
        }
    } catch (error) {
        console.error('View error:', error)
    }
}

// const total = computed(() => {
//     return cart.value.reduce((sum, item) => {
//         return sum + (item.price * item.quantity)
//     }, 0)
// })

// const removeItem = (id) => {
//     cart.value = cart.value.filter(
//         item => item.id !== id
//     )
// }
</script>

<template>
    <div class="container py-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1 class="fw-bold">
                Shopping Cart
            </h1>
            <router-link to="/events" class="btn btn-outline-primary">
                Continue Shopping
            </router-link>
        </div>
        <div class="row g-4">
            <div class="col-lg-8">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div v-for="event in cart.listTickets" :key="event.id" class="border-bottom py-3">
                            <div class="row align-items-center">
                                <div class="col-md-6">
                                    <h5 class="mb-1">
                                        {{ event.localTeam }}
                                        vs
                                        {{ event.visitorTeam }}
                                    </h5>
                                    <small class="text-muted">
                                        Ticket ID: {{ event.id }}
                                    </small>
                                </div>
                                <div class="col-md-2 text-center">

                                    <span class="badge bg-secondary">
                                        x{{ event.quantity }}
                                    </span>

                                </div>
                                <div class="col-md-2 text-center">

                                    <span class="fw-bold">
                                        ${{ event.totalPrice }}
                                    </span>

                                </div>
                                <div class="col-md-2 text-end">
                                    <button class="btn btn-outline-danger btn-sm" @click="removeItem(event.id)">
                                        Remove
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h4 class="mb-4">
                            Order Summary
                        </h4>
                        <div class="d-flex justify-content-between mb-3">
                            <span>Total</span>
                            <span class="fw-bold">
                                ${{ cart.totalPrice }}
                            </span>
                        </div>
                        <button class="btn btn-success w-100">
                            Proceed to Checkout
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>