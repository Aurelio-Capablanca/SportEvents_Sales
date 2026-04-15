namespace SportEvents_Sales_Back_End.Model.ModelDomain
{
    public class GeneralResponse<T>
    {
        public string? Message { get; set; } = null;
        public T? Dataset { get; set; } = default;
        public int? Status { get; set; } = null;
        public string? Error { get; set; } = null;


    }
}
