<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPublic.aspx.cs" Inherits="SportEvents_Sales_Front_End.View.Public.LoginPublic" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Login - Ticket Sales</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet"/>
</head>
<body>
    <div class="container py-5 text-center">
        <h2>Iniciar Sesión</h2>
        <!-- ASP.NET Login Form will be rendered here -->
        <form id="loginForm">
            <div class="mb-3">
                <input type="text" class="form-control" id="username" placeholder="Usuario" required>
            </div>
            <div class="mb-3">
                <input type="password" class="form-control" id="password" placeholder="Contraseña" required>
            </div>
            <button type="submit" class="btn btn-primary">Login</button>
        </form>
    </div>

    <!-- Script -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
