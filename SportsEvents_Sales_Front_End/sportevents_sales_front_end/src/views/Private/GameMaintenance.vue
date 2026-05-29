<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import Swal from 'sweetalert2'
import * as bootstrap from 'bootstrap'

const token = localStorage.getItem('token')

const games = ref([])
const stadiums = ref([])



const gameForm = ref({
    idGame: 0,
    localTeam: '',
    visitorTeam: '',
    timeGame: '',
    idStadium: 0,
    tournament: '',
    status: true
})

let gameModal = null

onMounted(() => {
    loadGames()
    loadStadiums()
    const modalElement = document.getElementById('gameModal')
    gameModal = new bootstrap.Modal(modalElement)

})


const loadStadiums = async () => {
    try {
        const response = await axios.get(
            'http://192.168.122.44:5105/stadium-api/stadium-get-all',
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {
            stadiums.value = response.data.dataset
        }
    } catch (error) {
        console.error(error)
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

const openCreateModal = () => {
    gameForm.value = {
        idGame: 0,
        localTeam: '',
        visitorTeam: '',
        timeGame: '',
        idStadium: 0,
        tournament: '',
        status: true
    }
    gameModal.show()
}

const openEditModal = async (idGame) => {
    try {
        const response = await axios.get(
            `http://192.168.122.44:5105/game-api/game-get-one/${idGame}`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {
            gameForm.value = response.data.dataset
            gameModal.show()
        }
    } catch (error) {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load game'
        })

    }

}

const saveGame = async () => {
    try {
        const response = await axios.post(
            'http://192.168.122.44:5105/game-api/save-game',
            gameForm.value,
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
                text: 'Game saved successfully'
            })
            gameModal.hide()
            loadGames()
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Save Error',
            text: 'Could not save game'
        })
    }
}

const deleteGame = async (idGame) => {
    const result = await Swal.fire({
        title: 'Delete game?',
        text: 'This action cannot be undone',
        icon: 'warning',
        showCancelButton: true
    })
    if (!result.isConfirmed) {
        return
    }
    try {
        const response = await axios.get(
            `http://192.168.122.44:5105/game-api/game-delete/${idGame}`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {

            Swal.fire({
                icon: 'success',
                title: 'Deleted',
                text: 'Game deleted successfully'
            })

            loadGames()

        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Delete Error',
            text: 'Could not delete game'
        })
    }
}
</script>
<template>
    <div class="container py-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1 class="fw-bold">
                Game Management
            </h1>
            <button class="btn btn-primary" @click="openCreateModal">
                Add Game
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
                                <th>Status</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="game in games" :key="game.idGame">
                                <td>
                                    {{ game.idGame }}
                                </td>
                                <td>
                                    {{ game.localTeam }}
                                </td>
                                <td>
                                    {{ game.visitorTeam }}
                                </td>
                                <td>
                                    {{ game.tournament }}
                                </td>
                                <td>
                                    {{ game.timeGame }}
                                </td>
                                <td>
                                    <span class="badge" :class="game.status ? 'bg-success' : 'bg-danger'">
                                        {{ game.status ? 'Active' : 'Inactive' }}
                                    </span>
                                </td>
                                <td>
                                    <div class="d-flex gap-2">
                                        <button class="btn btn-warning btn-sm" @click="openEditModal(game.idGame)">
                                            Edit
                                        </button>
                                        <button class="btn btn-danger btn-sm" @click="deleteGame(game.idGame)">
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
    <div class="modal fade" id="gameModal" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        Game Form
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="form-label">
                                Local Team
                            </label>
                            <input type="text" class="form-control" v-model="gameForm.localTeam">
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">
                                Visitor Team
                            </label>
                            <input type="text" class="form-control" v-model="gameForm.visitorTeam">
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">
                                Tournament
                            </label>
                            <input type="text" class="form-control" v-model="gameForm.tournament">
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">
                                Date
                            </label>
                            <input type="datetime-local" class="form-control" v-model="gameForm.timeGame">
                        </div>
                        <!-- new way -->
                        <div class="col-md-6">
                            <label class="form-label">
                                Stadium
                            </label>
                            <select class="form-select" v-model.number="gameForm.idStadium">
                                <option :value="0">
                                    Select Stadium
                                </option>
                                <option v-for="stadium in stadiums" :key="stadium.idStadium" :value="stadium.idStadium">
                                    {{ stadium.name }}
                                </option>
                            </select>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">
                                Status
                            </label>
                            <select class="form-select" v-model="gameForm.status">
                                <option :value="true">
                                    Active
                                </option>
                                <option :value="false">
                                    Inactive
                                </option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-dismiss="modal">
                        Close
                    </button>
                    <button class="btn btn-success" @click="saveGame">
                        Save
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>