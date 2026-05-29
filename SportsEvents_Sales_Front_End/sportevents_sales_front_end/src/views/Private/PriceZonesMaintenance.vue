<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import Swal from 'sweetalert2'
import * as bootstrap from 'bootstrap'

const token = localStorage.getItem('token')

const priceZone = ref([])

const prizeZoneForm = ref({
    idZone: 0,
    price: 0.0,
    zoneName: ''
})

let prizeZoneModal = null

onMounted(() => {
    loadPriceZone()
    const modalElement = document.getElementById('prizeZoneModal')
    prizeZoneModal = new bootstrap.Modal(modalElement)

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

const openCreateModal = () => {
    prizeZoneForm.value = {
        IdZone: 0,
        Price: 0.0,
        ZoneName: ''
    }
    prizeZoneModal.show()
}

const openEditModal = async (idGame) => {
    try {
        const response = await axios.get(
            `http://192.168.122.44:5105/zone-prices-api/zone-get-one/${idGame}`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {
            prizeZoneForm.value = response.data.dataset
            prizeZoneModal.show()
        }
    } catch (error) {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load price zone'
        })

    }

}

const savePriceZone = async () => {
    try {
        const response = await axios.post(
            'http://192.168.122.44:5105/zone-prices-api/save-zone',
            prizeZoneForm.value,
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
                text: 'Price Zone saved successfully'
            })
            prizeZoneModal.hide()
            loadPriceZone()
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Save Error',
            text: 'Could not save Price Zone'
        })
    }
}

const deletePriceZone = async (idPriceZone) => {
    const result = await Swal.fire({
        title: 'Delete Prize Zone?',
        text: 'This action cannot be undone',
        icon: 'warning',
        showCancelButton: true
    })
    if (!result.isConfirmed) {
        return
    }
    try {
        const response = await axios.get(
            `http://192.168.122.44:5105/zone-prices-api/zone-delete/${idPriceZone}`,
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
                text: 'Price Zone deleted successfully'
            })
            loadPriceZone()
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Delete Error',
            text: 'Could not delete Price Zone'
        })
    }
}
</script>
<template>
    <div class="container py-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1 class="fw-bold">
                Prize Zone Management
            </h1>
            <button class="btn btn-primary" @click="openCreateModal">
                Add Prize Zone
            </button>
        </div>
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-hover align-middle">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Price</th>
                                <th>Zone Name</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="price in priceZone" :key="price.idZone">
                                <td>
                                    {{ price.idZone }}
                                </td>
                                <td>
                                    {{ price.price }}
                                </td>
                                <td>
                                    {{ price.zoneName }}
                                </td>
                                <td>
                                    <div class="d-flex gap-2">
                                        <button class="btn btn-warning btn-sm" @click="openEditModal(price.idZone)">
                                            Edit
                                        </button>
                                        <button class="btn btn-danger btn-sm" @click="deletePriceZone(price.idZone)">
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
    <div class="modal fade" id="prizeZoneModal" tabindex="-1">
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
                                Zone Name
                            </label>
                            <input type="text" class="form-control" v-model="prizeZoneForm.zoneName">
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">
                                Price
                            </label>
                            <input type="number" class="form-control" v-model="prizeZoneForm.price">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-dismiss="modal">
                        Close
                    </button>
                    <button class="btn btn-success" @click="savePriceZone">
                        Save
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>