<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateCRUD.aspx.cs" Inherits="SportEvents_Sales_Front_End.View.Private.TemplateCRUD" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Admin Ticket Management - CRUD</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .table-responsive {
            overflow-x: auto;
            margin-bottom: 2rem;
        }
        .btn-action {
            padding: 0.25rem 0.75rem;
            font-size: 0.875rem;
        }
        .modal-form {
            max-width: 600px;
            margin: auto;
        }
    </style>
</head>
<body>
    <div class="container py-4">
        <!-- Header -->
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1>Ticket Management</h1>
            <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#ticketModal">
                Add New Ticket
            </button>
        </div>

        <!-- Table with CRUD Actions -->
        <div class="table-responsive">
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>Code Ticket</th>
                        <th>Zone</th>
                        <th>Price</th>
                        <th>Status</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <!-- Sample data (will be replaced by fetched data) -->
                    <tr>
                        <td>TICKET-001</td>
                        <td>Zone A</td>
                        <td>\$12.00</td>
                        <td><span class="badge bg-success">Available</span></td>
                        <td>
                            <button class="btn btn-sm btn-warning btn-action" data-bs-toggle="modal" data-bs-target="#editModal" data-ticket-id="TICKET-001">Edit</button>
                            <button class="btn btn-sm btn-danger btn-action">Delete</button>
                        </td>
                    </tr>
                    <tr>
                        <td>TICKET-002</td>
                        <td>Zone B</td>
                        <td>\$15.00</td>
                        <td><span class="badge bg-danger">Sold Out</span></td>
                        <td>
                            <button class="btn btn-sm btn-warning btn-action" data-bs-toggle="modal" data-bs-target="#editModal" data-ticket-id="TICKET-002">Edit</button>
                            <button class="btn btn-sm btn-danger btn-action">Delete</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!-- Add New Ticket Modal -->
        <div class="modal fade" id="ticketModal" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-form" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add New Ticket</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <form id="ticketForm">
                            <div class="mb-3">
                                <label for="ticketCode" class="form-label">Ticket Code</label>
                                <input type="text" class="form-control" id="ticketCode" placeholder="Enter ticket code" required>
                            </div>
                            <div class="mb-3">
                                <label for="ticketZone" class="form-label">Zone</label>
                                <input type="text" class="form-control" id="ticketZone" placeholder="Zone name" required>
                            </div>
                            <div class="mb-3">
                                <label for="ticketPrice" class="form-label">Price</label>
                                <input type="number" class="form-control" id="ticketPrice" placeholder="Price" step="0.01" required>
                            </div>
                            <div class="mb-3">
                                <label for="ticketStatus" class="form-label">Status</label>
                                <select class="form-select" id="ticketStatus" required>
                                    <option value="Available">Available</option>
                                    <option value="Sold Out">Sold Out</option>
                                </select>
                            </div>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" id="submitForm">Save Changes</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Edit Ticket Modal -->
        <div class="modal fade" id="editModal" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-form" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Edit Ticket</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <form id="updateTicketForm">
                            <input type="hidden" id="ticketId">
                            <div class="mb-3">
                                <label for="ticketCodeEdit" class="form-label">Ticket Code</label>
                                <input type="text" class="form-control" id="ticketCodeEdit" readonly>
                            </div>
                            <div class="mb-3">
                                <label for="ticketZoneEdit" class="form-label">Zone</label>
                                <input type="text" class="form-control" id="ticketZoneEdit" required>
                            </div>
                            <div class="mb-3">
                                <label for="ticketPriceEdit" class="form-label">Price</label>
                                <input type="number" class="form-control" id="ticketPriceEdit" step="0.01" required>
                            </div>
                            <div class="mb-3">
                                <label for="ticketStatusEdit" class="form-label">Status</label>
                                <select class="form-select" id="ticketStatusEdit">
                                    <option value="Available">Available</option>
                                    <option value="Sold Out">Sold Out</option>
                                </select>
                            </div>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" id="updateForm">Update</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Script to handle form submission -->
        <script>
            // Fetch data from API (replace with your backend logic)
            // Example: Fetch tickets from ASP.NET
            // fetch('/api/tickets')
            //     .then(response => response.json())
            //     .then(data => {
            //         const tableBody = document.querySelector('#tableBody');
            //         data.forEach(ticket => {
            //             const row = document.createElement('tr');
            //             // Populate table rows with data...
            //             tableBody.appendChild(row);
            //         });
            //     });

            // Event handlers for CRUD operations
            document.addEventListener('DOMContentLoaded', function () {
                // Set up event listeners for Edit button clicks
                document.querySelectorAll('.btn-warning').forEach(button => {
                    button.addEventListener('click', function () {
                        const ticketId = this.getAttribute('data-ticket-id');
                        const row = this.closest('tr');
                        const data = row.cells[0].textContent;

                        // Set modal form values
                        document.getElementById('ticketId').value = data;
                        document.getElementById('ticketCodeEdit').value = row.c
