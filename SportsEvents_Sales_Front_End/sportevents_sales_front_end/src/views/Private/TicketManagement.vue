<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import Swal from 'sweetalert2'
import * as bootstrap from 'bootstrap'


const games = ref([])
const tickets = ref([])
const priceZone = ref([])
const ticketForm = ref({
    idTicket: 0,
    idGame: 0,
    prices: []
})

const token = localStorage.getItem('token')
let ticketModal = null

onMounted(() => {
    loadGames()
    loadTickets()
    loadPriceZone()
    const modalElement = document.getElementById('TicketModal')
    ticketModal = new bootstrap.Modal(modalElement)

})


const loadPriceZone = async () => {
    try {
        const response = await axios.get(
            'http://192.168.122.44:5105/zone-prices-api/zone-get-all',
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {
            console.log(response.data.dataset)
            priceZone.value = response.data.dataset
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load PriceZone'
        })
    }
}

const loadGames = async () => {
    try {
        const response = await axios.get(
            'http://192.168.122.44:5105/game-api/game-get-all',
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {
            games.value = response.data.dataset
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load games'
        })
    }
}


const loadTickets = async () => {
    try {
        const response = await axios.get(
            'http://192.168.122.44:5105/ticket-api/ticket-get-all',
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {
            tickets.value = response.data.dataset
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load tickets'
        })
    }
}


const openCreateModal = () => {
    ticketForm.value = {
        idTicket: 0,
        idGame: 0,
        prices: []
    }
    ticketModal.show()
}



const addPriceRow = () => {
    ticketForm.value.prices.push({
        id: 0,
        idPriceZone: 0,
        prices: 0,
        availableSeats: 0
    })
}

const removePriceRow = (index) => {
    ticketForm.value.prices.splice(index, 1)
}


const openEditModal = async (idTicket) => {
    try {
        const response = await axios.get(
            `http://192.168.122.44:5105/ticket-api/ticket-get-one/${idTicket}`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {
            console.log(response.data.dataset)
            ticketForm.value = response.data.dataset            
            ticketModal.show()
        }
    } catch (error) {

        console.log(error)
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load ticket'
        })

    }

}

const saveTickets = async () => {
    console.log(ticketForm)
    try {
        const response = await axios.post(
            'http://192.168.122.44:5105/ticket-api/save-ticket',
            ticketForm.value,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )        
        if (response.data.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Saved',
                text: 'Ticket and prices saved successfully'
            })
            ticketModal.hide()
            loadTickets()
        }
    } catch (error) {        
        console.log(error)
        Swal.fire({
            icon: 'error',
            title: 'Save Error',
            text: 'Ticket and prices not saved'
        })
    }
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
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-hover align-middle">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Local</th>
                                <th>Visitor</th>
                                <th>Tournament</th>
                                <th>Date</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="ticket in tickets" :key="ticket.idTicket">
                                <td>
                                    {{ ticket.idTicket }}
                                </td>
                                <td>
                                    {{ ticket.localTeam }}
                                </td>
                                <td>
                                    {{ ticket.visitorTeam }}
                                </td>
                                <td>
                                    {{ ticket.tournament }}
                                </td>
                                <td>
                                    {{ ticket.date }} - {{ ticket.time }}
                                </td>
                                <!-- <td>
                                    <span class="badge" :class="game.status ? 'bg-success' : 'bg-danger'">
                                        {{ game.status ? 'Active' : 'Inactive' }}
                                    </span>
                                </td> -->
                                <td>
                                    <div class="d-flex gap-2">
                                        <button class="btn btn-warning btn-sm" @click="openEditModal(ticket.idTicket)">
                                            Edit
                                        </button>
                                        <button class="btn btn-danger btn-sm" @click="deleteGame(ticket.idTicket)">
                                            Delete
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="TicketModal" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        Ticket Form
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="form-label">
                                Game
                            </label>
                            <select class="form-select" v-model.number="ticketForm.idGame">
                                <option :value="0">
                                    Select Game
                                </option>
                                <option v-for="game in games" :key="game.idGame" :value="game.idGame">
                                    {{ game.visitorTeam }} vs {{ game.localTeam }} at {{ game.timeGame }}
                                </option>
                            </select>
                        </div>
                        <div class="col-md-12">
                            <div class="d-flex justify-content-between mb-4">
                                <h4>
                                    Ticket Prices
                                </h4>
                                <button class="btn btn-primary" @click="addPriceRow">
                                    Add Price
                                </button>
                            </div>
                            <div v-for="(price, index) in ticketForm.prices" :key="index"
                                class="border rounded p-3 mb-3">
                                <div class="row g-3">
                                    <div class="col-md-3">
                                        <label class="form-label">
                                            Zone ID
                                        </label>
                                        <!-- v-model.number="price.idPriceZone" --> 
                                        <select class="form-select" v-model.number="price.idPriceZone">
                                            <option :value="0">
                                                Select Price Zone
                                            </option>
                                            <option v-for="priceZones in priceZone" :key="priceZones.idZone"
                                                :value="priceZones.idZone">
                                                {{ priceZones.zoneName }}
                                            </option>
                                        </select>
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
                    <button class="btn btn-success" @click="saveTickets">
                        Save
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>