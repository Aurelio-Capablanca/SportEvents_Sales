<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'
import Swal from 'sweetalert2'

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
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed to load cart'
        })
        router.push('/public-dashboard')
        console.error('View error:', error)
    }
}


const removeItem = async (ticket, order, price) => {
    console.log("Removing ", ticket, order, price)
    const result = await Swal.fire({
        title: 'Remove ticket?',
        text: 'This item will be removed from your cart',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, remove it'
    })
    if (result.isConfirmed) {
        console.log("Removing item")
        try {
            const response = await axios.post(
                'http://192.168.122.44:5105/cart-api/delete-one-cart',
                {
                    "IdPriceTicket": price,
                    "IdTicket": ticket,
                    "IdOrder": order
                },
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            )
            console.log(response.data);
            if (response.data.status == 200) {
                Swal.fire({
                    icon: 'success',
                    title: 'Ticket Removed',
                    text: 'Ticket Removed from Cart Successfully'
                })
            }
        } catch (error) {
            console.error('View error:', error)
        }
    }
}


const doCheckout = async () => {
    try {
        const request = await axios.get('http://192.168.122.44:5105/cart-api/checkout-cart',
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            })
        console.log(request.data);
        if (request.data.status == 200) {
            console.log(request.data.dataset);
            Swal.fire({
                icon: 'success',
                title: 'Successful Checkout!',
                text: 'Success'
            })
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed to Checkout'
        })
        console.error('Checkout error:', error)
    }
}
</script>

<template>
    <div class="container py-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1 class="fw-bold">
                Shopping Cart
            </h1>
            <router-link to="/public-dashboard" class="btn btn-outline-primary">
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
                                        Ticket ID: {{ event.idTicket }}
                                        Price ID: {{ event.idTicketPrice }}
                                        Order ID: {{ event.idOrder }}
                                    </small>
                                </div>
                                <div class="col-md-2 text-center">

                                    <span class="badge bg-secondary">
                                        x{{ event.totalBuy }}
                                    </span>

                                </div>
                                <div class="col-md-2 text-center">

                                    <span class="fw-bold">
                                        ${{ event.totalPrice }}
                                    </span>

                                </div>
                                <div class="col-md-2 text-end">
                                    <button class="btn btn-outline-danger btn-sm"
                                        @click="removeItem(event.idTicket, event.idOrder, event.idTicketPrice)">
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
                        <button class="btn btn-success w-100" @click="doCheckout()">
                            Proceed to Checkout
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>