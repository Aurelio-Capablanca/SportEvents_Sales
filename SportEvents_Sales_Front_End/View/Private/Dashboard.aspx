<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SportEvents_Sales_Front_End.View.Private.Dashboard" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Admin Dashboard - TicketHub</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="styles.css">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</head>
<body>
    <!-- Sidebar -->
    <div class="container-fluid">
        <!-- Sidebar -->
        <div class="row">
            <div class="col-md-2 bg-dark text-white d-flex flex-column align-items-center border-right">
                <h5>Admin Panel</h5>
                <ul class="nav flex-column w-100">
                    <li class="nav-item"><a class="nav-link active" href="#" data-bs-toggle="tooltip" title="Ticket Management">📋 Tickets</a></li>
                    <li class="nav-item"><a class="nav-link" href="#" data-bs-toggle="tooltip" title="Zone Pricing">💰 Zone Pricing</a></li>
                    <li class="nav-item"><a class="nav-link" href="#" data-bs-toggle="tooltip" title="Event Matches">📅 Matches</a></li>
                    <li class="nav-item"><a class="nav-link" href="#" data-bs-toggle="tooltip" title="User Management">👤 Users</a></li>
                    <li class="nav-item"><a class="nav-link" href="#" data-bs-toggle="tooltip" title="Sales Stats">📈 Stats</a></li>
                </ul>
            </div>

            <!-- Main Content -->
            <div class="col-md-10">
                <!-- Header -->
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h1>Dashboard</h1>
                    <div>
                        <select class="form-select form-select-sm me-2">
                            <option>Today</option>
                            <option>Week</option>
                            <option>Month</option>
                        </select>
                        <button class="btn btn-sm btn-outline-secondary">Refresh</button>
                    </div>
                </div>

                <!-- Stats Cards -->
                <div class="row mb-4">
                    <div class="col-md-3 mb-4">
                        <div class="card h-100">
                            <div class="card-body">
                                <h5 class="card-title text-center">Total Tickets Sold</h5>
                                <p class="card-text text-center">📊 1,234</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 mb-4">
                        <div class="card h-100">
                            <div class="card-body">
                                <h5 class="card-title text-center">Available Tickets</h5>
                                <p class="card-text text-center">✅ 529</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 mb-4">
                        <div class="card h-100">
                            <div class="card-body">
                                <h5 class="card-title text-center">Tickets Sold Today</h5>
                                <p class="card-text text-center">📈 432</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 mb-4">
                        <div class="card h-100">
                            <div class="card-body">
                                <h5 class="card-title text-center">Admin Users</h5>
                                <p class="card-text text-center">⚙️ 2</p>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Active Tickets Table -->
                <div class="row mb-4">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header">
                                <h5>Active Tickets</h5>
                                <button class="btn btn-sm btn-outline-primary">Add New</button>
                            </div>
                            <div class="card-body">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th>Code</th>
                                            <th>Zone</th>
                                            <th>Price</th>
                                            <th>Status</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>TICKET001</td>
                                            <td>Zone A</td>
                                            <td>$12.00</td>
                                            <td><span class="badge bg-success">Available</span></td>
                                            <td>
                                                <button class="btn btn-sm btn-outline-warning">Edit</button>
                                                <button class="btn btn-sm btn-outline-danger">Delete</button>
                                            </td>
                                        </tr>
                                        <!-- Add more rows -->
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Add Event Match Modal -->
    <div class="modal fade" id="eventMatchModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add New Event Match</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="mb-3">
                            <label class="form-label">Match Name</label>
                            <input type="text" class="form-control" placeholder="Enter match name">
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Date & Time</label>
                            <input type="datetime-local" class="form-control">
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Venue</label>
                            <select class="form-select">
                                <!-- Options -->
                            </select>
                        </div>
                        <button type="button" class="btn btn-primary">Add Match</button>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <!-- Edit Zone Price Modal -->
    <div class="modal fade" id="editZonePriceModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Zone Price</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="mb-3">
                            <label class="form-label">Zone Name</label>
                            <input type="text" class="form-control" disabled>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">New Price</label>
                            <input type="number" class="form-control" required>
                        </div>
                        <button type="button" class="btn btn-primary">Update Price</button>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <!-- Add Admin Modal -->
    <div class="modal fade" id="addAdminModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add New Admin User</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="mb-3">
                            <label class="form-label">Username</label>
                            <input type="text" class="form-control" required>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Password</label>
                            <input type="password" class="form-control" required>
                        </div>
                        <button type="button" class="btn btn-primary">Create User</button>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <!-- Script to Initialize Modal Popups -->
    <script>
        // Initialize modals
        document.addEventListener('DOMContentLoaded', function() {
            // Open the modal with the "Add New" button click
            document.querySelector('.row > div:nth-child(2) > div:nth-child(1) > button').addEventListener('click', function() {
                $('#eventMatchModal').modal('show');
            });

            // Edit Zone Price modal
            document.querySelector('.card > thead > tr > th:nth-child(4) > button:nth-child(1)').addEventListener('click', function() {
                $('#editZonePriceModal').modal('show');
            });
        });
    </script>
</body>
</html>