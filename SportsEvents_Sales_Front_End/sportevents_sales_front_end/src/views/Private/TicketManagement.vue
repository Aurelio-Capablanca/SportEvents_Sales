<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import Swal from 'sweetalert2'
import * as bootstrap from 'bootstrap'

const ticketForm = ref({
    idTicket: 0,
    idGame: 0,
    ticketPrices: []
})


let ticketModal = null

onMounted(() => {
    //loadtTickets()
    const modalElement = document.getElementById('TicketModal')
    ticketModal = new bootstrap.Modal(modalElement)

})

const openCreateModal = () => {
    ticketForm.value = {
        idTicket: 0,
        idGame: 0,
        ticketPrices: []
    }
    ticketModal.show()
}



const addPriceRow = () => {
    ticketForm.value.ticketPrices.push({
        idTicketPrice: 0,
        idPriceZone: 0,
        price: 0,
        availableSeats: 0
    })
}

const removePriceRow = (index) => {
    ticketForm.value.ticketPrices.splice(index, 1)
}

</script>
<template>
    <div class="container py-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1 class="fw-bold">
                Ticket Management
            </h1>
            <button class="btn btn-primary" @click="openCreateModal">
                Add Ticket
            </button>
        </div>
    </div>

    <div class="modal fade" id="TicketModal" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        User Form
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="form-label">
                                User Name
                            </label>
                            <input type="text" class="form-control" v-model="ticketForm.idTicket">
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">
                                Password
                            </label>
                            <input type="password" class="form-control" v-model="ticketForm.idGame">
                        </div>
                        <!-- <div class="col-md-6">
                            <label class="form-label">
                                Repeat Password
                            </label>
                            <input type="password" class="form-control" v-model="userForm.passwordRepeat">
                        </div> -->
                        <div class="col-md-12">
                            <div class="d-flex justify-content-between mb-4">
                                <h4>
                                    Ticket Prices
                                </h4>
                                <button class="btn btn-primary" @click="addPriceRow">
                                    Add Price
                                </button>
                            </div>
                            <div v-for="(price, index) in ticketForm.ticketPrices" :key="index"
                                class="border rounded p-3 mb-3">
                                <div class="row g-3">
                                    <div class="col-md-3">
                                        <label class="form-label">
                                            Zone ID
                                        </label>
                                        <input type="number" class="form-control" v-model.number="price.idPriceZone">
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">
                                            Price
                                        </label>
                                        <input type="number" step="0.01" class="form-control"
                                            v-model.number="price.price">
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">
                                            Seats
                                        </label>
                                        <input type="number" class="form-control" v-model.number="price.availableSeats">
                                    </div>
                                    <div class="col-md-3 d-flex align-items-end">

                                        <button class="btn btn-danger w-100" @click="removePriceRow(index)">
                                            Remove
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-dismiss="modal">
                        Close
                    </button>
                    <button class="btn btn-success" @click="">
                        Save
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>