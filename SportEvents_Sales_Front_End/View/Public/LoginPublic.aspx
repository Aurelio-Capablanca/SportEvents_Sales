<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPublic.aspx.cs" Inherits="SportEvents_Sales_Front_End.View.Public.LoginPublic" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Ticket Sales Platform</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="styles.css">
</head>
<body>
    <!-- Navigation Bar -->
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container">
            <a class="navbar-brand" href="#">TicketHub</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item"><a class="nav-link active" href="#">Home</a></li>
                    <li class="nav-item"><a class="nav-link" href="#">See Tickets</a></li>
                    <%--<li class="nav-item"><a class="nav-link" href="#">Login</a></li>--%>
                    <li class="nav-item">

                        <a class="nav-link" href="#" data-bs-toggle="modal" data-bs-target="#loginModal">Login</a>

                        <%--<button class="btn btn-warning mt-4" data-bs-toggle="modal" data-bs-target="#loginModal">
                            Login
                        </button>--%>
                    </li>
                    <li class="nav-item"><a class="nav-link" href="#">Cart <span class="badge bg-primary">0</span></a></li>
                </ul>
            </div>
        </div>
    </nav>

    <!-- Main Content -->
    <div class="container mt-4">
        <h1 class="text-center mb-4">Available Tickets</h1>

        <!-- Search/Filter -->
        <div class="input-group mb-4">
            <input type="text" class="form-control" placeholder="Search tickets by name or date">
            <button class="btn btn-primary">Search</button>
        </div>

        <!-- Ticket Grid -->
        <div class="row">
            <div class="col-md-6 col-lg-4 mb-4">
                <!-- Example Ticket Card -->
                <div class="card h-100">
                    <img src="https://via.placeholder.com/300x150" class="card-img-top" alt="Match Image">
                    <div class="card-body">
                        <h5 class="card-title">Match: Barcelona vs Real Madrid</h5>
                        <p class="card-text">
                            Zone A: €12 <span class="badge bg-success">Available</span>
                            Zone B: €15 <span class="badge bg-danger">Sold Out</span>
                        </p>
                    </div>
                    <div class="card-footer">
                        <a href="#" class="btn btn-primary">See Details</a>
                    </div>
                </div>
            </div>
            <!-- Add more cards for tickets -->
        </div>
    </div>

    <!-- Login Page (Modal) -->


    <!-- Modal Login Template -->
    <div class="modal fade" id="loginModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Login</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <input type="email" class="form-control" id="email" required>
                        </div>
                        <div class="mb-3">
                            <label for="password" class="form-label">Password</label>
                            <input type="password" class="form-control" id="password" required>
                        </div>
                        <button type="button" class="btn btn-primary">Login</button>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

