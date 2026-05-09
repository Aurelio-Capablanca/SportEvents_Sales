using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.Model.ModelDomain.Request;
using SportEvents_Sales_Back_End.Model.ModelDomain.Response;

namespace SportEvents_Sales_Back_End.Domain.Business
{

    /*
     + SaveCartAsync(CartReq model) -> Task<GeneralResponse<String>> 


    //+  -> Task<GeneralResponse<List<>>> 

    
    /* accept other args via generics and use Pattern Matching on it */

    //+ 
    //+ 

    //+ CartCheckOut(Integer IdCart) -> Task<GeneralResponse<OrderDTO>>     

    public class CartLogic
    {

        private DbContext _dbContext;

        public CartLogic(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<GeneralResponse<CartResponse>> SaveCartAsync(CartRequest request)
        {

            /*
                DB -> {
                INSERT TO Order, (if ID doesn't exists)
                INSERT TO Ticket_Orders (add always, since this concats with Order)
                UPDATE TICKET (total_tickets =-1) 
                }
            */
            return null;
        }

        public Task<GeneralResponse<List<CartResponse>>> ReadCartAsync(String EmailClient)
        {
            return null;
        }


        //+ ReadClientByArgument(T id) -> Task<GeneralResponse<DomainModel>>

        public Task<GeneralResponse<String>> DeleteFromCartAsync(CartRequest request)
        {
            /*
                DB -> {
                Update Ticket (total_tickets =+ 1) Where id_ticket = @1
                Update Order (sum all remnan Tickets) where idOrder = idCart
                }
            */
            return null;
        }

        public Task<GeneralResponse<String>> DeleteAllCartAsync(int IdCart)
        {
            /*
                DB -> {
                Update Ticket (total_tickets =+ 1) Where id_ticket = @1
                And id_ticket = @n ...
                }
            */
            return null;
        }        



    }
}
