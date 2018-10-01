namespace Sans.CreditUnion.API.Infrastructure.Models
{
    public class ValidationError
    {
        public ValidationError(string propertyName, string error)
        {
            PropertyName = propertyName;
            Error = error;
        }

        public string PropertyName { get; set; }
        public string Error { get; set; }
    }
}
