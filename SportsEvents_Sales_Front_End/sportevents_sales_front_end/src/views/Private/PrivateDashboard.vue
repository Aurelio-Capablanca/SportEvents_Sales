<script setup>
import EventsView from '@/views/Public/EventsView.vue';
import Events from '@/views/Public/EventsView.vue';

</script>

<template>
  <div class="page-wrapper">
    <div class="container">
      <div class="row justify-content-center align-items-center min-vh-100">
        <div class="col-md-8 col-lg-6">
          <div class="card border-0 shadow-lg">
            <div class="card-body p-5 text-center">
              <h1 class="display-5 fw-bold text-info">
                Private View
              </h1>
              <p class="lead text-muted">
                This is accessible to admins.
              </p>
              <hr class="my-4">
              <router-link to="/login-client" class="btn btn-primary btn-lg px-5">
                Go to Login
              </router-link>
            </div>
          </div>
        </div>      
      </div>
    </div>
  </div>
</template>