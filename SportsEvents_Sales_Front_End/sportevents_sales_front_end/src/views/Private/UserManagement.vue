<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import Swal from 'sweetalert2'
import * as bootstrap from 'bootstrap'

const token = localStorage.getItem('token')

const users = ref([])

const userForm = ref({
    id: 0,
    userName: '',
    passwordHash: '',
    passwordRepeat: ''
})

let userModal = null

onMounted(() => {
    loadUsers()
    const modalElement = document.getElementById('userModal')
    userModal = new bootstrap.Modal(modalElement)

})

const loadUsers = async () => {
    try {
        const response = await axios.get(
            'http://192.168.122.44:5105/user-api/user-get-all',
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {
            users.value = response.data.dataset
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load Users'
        })
    }
}

const openCreateModal = () => {
    userForm.value = {
        id: 0,
        userName: '',
        passwordHash: '',
        passwordRepeat: ''
    }
    userModal.show()
}

const openEditModal = async (idUser) => {
    try {
        const response = await axios.get(
            `http://192.168.122.44:5105/user-api/user-get-one/${idUser}`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )
        if (response.data.status == 200) {
            userForm.value = response.data.dataset
            userForm.value.passwordHash = ''
            userForm.value.passwordRepeat = ''
            userModal.show()
        }
    } catch (error) {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load user'
        })

    }

}

const saveUser = async () => {
    try {
        const response = await axios.post(
            'http://192.168.122.44:5105/user-api/save-user',
            userForm.value,
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
                text: 'User saved successfully'
            })
            userModal.hide()
            loadUsers()
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Save Error',
            text: 'Could not save User'
        })
    }
}

const deleteUser = async (idUser) => {
    const result = await Swal.fire({
        title: 'Delete User?',
        text: 'This action cannot be undone',
        icon: 'warning',
        showCancelButton: true
    })
    if (!result.isConfirmed) {
        return
    }
    try {
        const response = await axios.get(
            `http://192.168.122.44:5105/user-api/user-delete/${idUser}`,
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
                text: 'User deleted successfully'
            })
            loadUsers()
        }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Delete Error',
            text: 'Could not delete User'
        })
    }
}
</script>
<template>
    <div class="container py-5">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1 class="fw-bold">
                User Management
            </h1>
            <button class="btn btn-primary" @click="openCreateModal">
                Add User
            </button>
        </div>
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-hover align-middle">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>UserName</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="user in users" :key="user.id">
                                <td>
                                    {{ user.id }}
                                </td>
                                <td>
                                    {{ user.userName }}
                                </td>
                                <td>
                                    <div class="d-flex gap-2">
                                        <button class="btn btn-warning btn-sm" @click="openEditModal(user.id)">
                                            Edit
                                        </button>
                                        <button class="btn btn-danger btn-sm" @click="deleteUser(user.id)">
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
    <div class="modal fade" id="userModal" tabindex="-1">
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
                            <input type="text" class="form-control" v-model="userForm.userName">
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">
                                Password
                            </label>
                            <input type="password" class="form-control" v-model="userForm.passwordHash">
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">
                                Repeat Password
                            </label>
                            <input type="password" class="form-control" v-model="userForm.passwordRepeat">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-dismiss="modal">
                        Close
                    </button>
                    <button class="btn btn-success" @click="saveUser">
                        Save
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>