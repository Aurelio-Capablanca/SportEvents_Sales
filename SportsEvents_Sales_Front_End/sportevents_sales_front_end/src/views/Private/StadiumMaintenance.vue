<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import Swal from 'sweetalert2'
import * as bootstrap from 'bootstrap'

const token = localStorage.getItem('token')

const stadiums = ref([])

const stadiumForm = ref({
    idStadium: 0,
    name: '',
    location: '',
    capacity: 0
})

let stadiumModal = null

onMounted(() => {
    loadStadiums()
    const modalElement = document.getElementById('stadiumModal')
    stadiumModal = new bootstrap.Modal(modalElement)
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
        console.log(response.data)
        if (response.data.status == 200) {
            stadiums.value = response.data.dataset
        }
    } catch (error) {
        console.error(error)
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load stadiums'
        })

    }
}

const openCreateModal = () => {

    stadiumForm.value = {
        idStadium: 0,
        name: '',
        location: '',
        capacity: 0
    }

    stadiumModal.show()

}

const openEditModal = async (idStadium) => {

    try {

        const response = await axios.get(
            `http://192.168.122.44:5105/stadium-api/stadium-get-one/${idStadium}`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )

        if (response.data.status == 200) {

            stadiumForm.value = response.data.dataset

            stadiumModal.show()

        }

    } catch (error) {

        console.error(error)

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load stadium'
        })

    }

}

const saveStadium = async () => {
    try {
        const response = await axios.post(
            'http://192.168.122.44:5105/stadium-api/save-stadium',

            stadiumForm.value,

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
                text: 'Stadium saved successfully'
            })
            stadiumModal.hide()
            loadStadiums()
        }
    } catch (error) {
        console.error(error)
        Swal.fire({
            icon: 'error',
            title: 'Save Error',
            text: 'Could not save stadium'
        })
    }
}

const deleteStadium = async (idStadium) => {
    const result = await Swal.fire({
        title: 'Delete stadium?',
        text: 'This action cannot be undone',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Delete'
    })
    if (!result.isConfirmed) {
        return
    }
    try {
        const response = await axios.get(
            `http://192.168.122.44:5105/stadium-api/stadium-delete/${idStadium}`,
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
                text: 'Stadium deleted successfully'
            })
            loadStadiums()
        }
    } catch (error) {
        console.error(error)
        Swal.fire({
            icon: 'error',
            title: 'Delete Error',
            text: 'Could not delete stadium'
        })
    }
}
</script>

<template>

    <div class="container py-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1 class="fw-bold">
                Stadium Management
            </h1>
            <button class="btn btn-primary" @click="openCreateModal">
                Add Stadium
            </button>
        </div>
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-hover align-middle">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Name</th>
                                <th>Location</th>
                                <th>Capacity</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="stadium in stadiums" :key="stadium.idStadium">
                                <td>
                                    {{ stadium.idStadium }}
                                </td>
                                <td>
                                    {{ stadium.name }}
                                </td>
                                <td>
                                    {{ stadium.location }}
                                </td>
                                <td>
                                    {{ stadium.capacity }}
                                </td>
                                <td>
                                    <div class="d-flex gap-2">
                                        <button class="btn btn-warning btn-sm"
                                            @click="openEditModal(stadium.idStadium)">
                                            Edit
                                        </button>
                                        <button class="btn btn-danger btn-sm" @click="deleteStadium(stadium.idStadium)">
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
    <div class="modal fade" id="stadiumModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        Stadium Form
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label">
                            Stadium Name
                        </label>
                        <input type="text" class="form-control" v-model="stadiumForm.name">
                    </div>
                    <div class="mb-3">
                        <label class="form-label">
                            Location
                        </label>
                        <input type="text" class="form-control" v-model="stadiumForm.location">
                    </div>
                    <div class="mb-3">
                        <label class="form-label">
                            Capacity
                        </label>
                        <input type="number" class="form-control" v-model.number="stadiumForm.capacity">
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-dismiss="modal">
                        Close
                    </button>
                    <button class="btn btn-success" @click="saveStadium">
                        Save
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>