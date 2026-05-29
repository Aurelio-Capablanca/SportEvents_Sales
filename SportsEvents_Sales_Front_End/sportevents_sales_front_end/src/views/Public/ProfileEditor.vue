<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import Swal from 'sweetalert2'

const token = localStorage.getItem('token')

const loading = ref(false)

const clientForm = ref({
    idclient: null,
    name: '',
    lastName: '',
    email: '',
    pass: ''
})

onMounted(() => {

    loadProfile()

})

const loadProfile = async () => {

    loading.value = true

    try {

        const response = await axios.get(
            'http://192.168.122.44:5105/client-api/get-own-details',
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )

        console.log(response.data)

        if (response.data.status == 200) {

            clientForm.value = response.data.dataset

            // Security practice:
            // never preload passwords

            clientForm.value.pass = ''

        }

    } catch (error) {

        console.error(error)

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Could not load profile'
        })

    } finally {

        loading.value = false

    }

}

const saveProfile = async () => {

    try {

        const payload = {
            ...clientForm.value
        }

        // Optional:
        // avoid sending empty password

        if (!payload.pass) {

            delete payload.pass

        }

        const response = await axios.post(
            'http://192.168.122.44:5105/client-api/save-client',

            payload,

            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        )

        console.log(response.data)

        if (response.data.status == 200) {

            Swal.fire({
                icon: 'success',
                title: 'Saved',
                text: 'Profile updated successfully'
            })

        }

    } catch (error) {

        console.error(error)

        Swal.fire({
            icon: 'error',
            title: 'Save Error',
            text: 'Could not update profile'
        })

    }

}
</script>

<template>

    <div class="container py-5">

        <div class="row justify-content-center">

            <div class="col-lg-8">

                <div class="card shadow-sm">

                    <div class="card-header">

                        <h3 class="mb-0">
                            My Profile
                        </h3>

                    </div>

                    <div class="card-body">

                        <div v-if="loading" class="text-center py-5">

                            <div class="spinner-border">

                            </div>

                        </div>

                        <div v-else>

                            <div class="row g-3">

                                <div class="col-md-6">

                                    <label class="form-label">
                                        Name
                                    </label>

                                    <input type="text" class="form-control" v-model="clientForm.name">

                                </div>

                                <div class="col-md-6">

                                    <label class="form-label">
                                        Last Name
                                    </label>

                                    <input type="text" class="form-control" v-model="clientForm.lastName">

                                </div>

                                <div class="col-md-12">

                                    <label class="form-label">
                                        Email
                                    </label>

                                    <input type="email" class="form-control" v-model="clientForm.email">

                                </div>

                                <div class="col-md-12">

                                    <label class="form-label">
                                        Password
                                    </label>

                                    <input type="password" class="form-control" v-model="clientForm.pass"
                                        placeholder="Leave empty to keep current password">

                                </div>

                            </div>

                        </div>

                    </div>

                    <div class="card-footer d-flex justify-content-end">

                        <button class="btn btn-success" @click="saveProfile">
                            Save Changes
                        </button>

                    </div>

                </div>

            </div>

        </div>

    </div>

</template>