<%@ Page Title="" Language="C#" MasterPageFile="~/View/Public/PublicMaster.Master" AutoEventWireup="true" CodeBehind="PublicDashboard.aspx.cs" Inherits="SportEvents_Sales_Front_End.View.Public.PublicDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
</asp:Content>
