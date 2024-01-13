namespace web_store_server.Shared.Resources
{
    public class Error
    {
        public Error(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string PropertyName { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
    }
}
