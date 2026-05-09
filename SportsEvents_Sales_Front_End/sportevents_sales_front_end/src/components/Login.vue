<script setup>
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

const router = useRouter()
const emit = defineEmits(['login-success'])

const email = ref('')              // User's email input
const password = ref('')           // User's password input
const message = ref('')            // Success/error message
const messageType = ref('error')   // 'success' or 'error'
const loading = ref(false)         // Shows loading state


const isValidEmail = (email) => {
    const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    return regexEmail.test(email)
}

const login = async () => {
    // Step 1: Validate inputs
    if (!email.value || !password.value) {
        message.value = 'Please enter email and password'
        messageType.value = 'error'
        return  // Stop here if empty
    }

    // Step 2: Validate email format
    if (!isValidEmail(email.value)) {
        message.value = 'Please enter a valid email'
        messageType.value = 'error'
        return  // Stop here if invalid
    }

    // Step 3: Show loading state
    loading.value = true
    message.value = ''

    try {
        const response = await axios.post('http://192.168.122.44:5105/auth/do-login', {
            User: email.value,
            Password: password.value,
            IsAdmin: false
        })
        console.log(response.data)
        if (response.data.status == 200) {
            // Store the token in browser storage            
            localStorage.setItem('token', response.data.dataset)
            localStorage.setItem('userEmail', email.value)
            // Show success message
            message.value = 'Login successful!'
            messageType.value = 'success'
            // Clear form
            email.value = ''
            password.value = ''
            const userEmail = email.value
            setTimeout(() => {
                emit('login-success', { email: userEmail })
            }, 1000)
            router.push('/public-dashboard')
        } else {
            // Server said login failed
            message.value = response.data.message || 'Login failed'
            messageType.value = 'error'
        }
    } catch (error) {
        console.error('Login error:', error)
        message.value = 'Error: ' + (error.response?.data?.message || error.message /*|| 'Network error'*/)
        messageType.value = 'error'
    } finally {
        loading.value = false
    }
}

</script>
<template>

    <div id="layoutAuthentication">
        <div id="layoutAuthentication_content">
            <main>
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <div class="card shadow-lg border-0 rounded-lg mt-5">
                                <div class="card-header">
                                    <h3 class="text-center font-weight-light my-4">Login</h3>
                                </div>
                                <div class="card-body">
                                    <form @submit.prevent="login">
                                        <div class="form-floating mb-3">
                                            <input class="form-control" v-model="email" id="email" type="email"
                                                placeholder="name@example.com" :disabled="loading" required />
                                            <label for="email">Email address</label>
                                        </div>
                                        <div class="form-floating mb-3">
                                            <input v-model="password" class="form-control" id="password" type="password"
                                                placeholder="••••••••" :disabled="loading" required />
                                            <label for="password">Password</label>
                                        </div>
                                        <div class="form-check mb-3">
                                            <input class="form-check-input" id="inputRememberPassword" type="checkbox"
                                                value="" />
                                            <label class="form-check-label" for="inputRememberPassword">Remember
                                                Password</label>
                                        </div>
                                        <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                                            <!-- <a class="btn btn-primary" href="index.html">Login</a> -->
                                            <button type="submit" :disabled="loading" class="btn btn-primary w-100">
                                                {{ loading ? 'Logging in...' : 'Login' }}
                                            </button>
                                            <div v-if="message"
                                                :class="'alert alert-' + (messageType === 'success' ? 'success' : 'danger')"
                                                class="mt-3">
                                                {{ message }}
                                            </div>
                                            <a class="small" href="password.html">Forgot Password?</a>
                                        </div>
                                    </form>
                                </div>
                                <div class="card-footer text-center py-3">
                                    <div class="small"><a href="register.html">Need an account? Sign up!</a></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </div>
        <div id="layoutAuthentication_footer">
            <footer class="py-4 bg-light mt-auto">
                <div class="container-fluid px-4">
                    <div class="d-flex align-items-center justify-content-between small">
                        <div class="text-muted">Copyright &copy; Your Website 2023</div>
                        <div>
                            <a href="#">Privacy Policy</a>
                            &middot;
                            <a href="#">Terms &amp; Conditions</a>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    </div>
</template>