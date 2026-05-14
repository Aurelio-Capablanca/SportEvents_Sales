<script setup>
import { ref, computed } from 'vue'

const cart = ref([
    {
        id: 1,
        title: 'Rock Festival',
        quantity: 2,
        price: 50
    },
    {
        id: 2,
        title: 'Jazz Night',
        quantity: 1,
        price: 35
    }
])

const total = computed(() => {
    return cart.value.reduce((sum, item) => {
        return sum + (item.price * item.quantity)
    }, 0)
})

const removeItem = (id) => {
    cart.value = cart.value.filter(
        item => item.id !== id
    )
}
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
                        <div v-for="item in cart" :key="item.id" class="border-bottom py-3">
                            <div class="row align-items-center">
                                <div class="col-md-6">
                                    <h5 class="mb-1">
                                        {{ item.title }}
                                    </h5>
                                    <small class="text-muted">
                                        Ticket ID: {{ item.id }}
                                    </small>
                                </div>
                                <div class="col-md-2 text-center">

                                    <span class="badge bg-secondary">
                                        x{{ item.quantity }}
                                    </span>

                                </div>
                                <div class="col-md-2 text-center">

                                    <span class="fw-bold">
                                        ${{ item.price }}
                                    </span>

                                </div>
                                <div class="col-md-2 text-end">
                                    <button class="btn btn-outline-danger btn-sm" @click="removeItem(item.id)">
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
                                ${{ total }}
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