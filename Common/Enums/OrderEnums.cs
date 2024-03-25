namespace web_store_server.Common.Enums
{
    public class OrderEnums
    {
        public enum Status : int
        {
            CREATED = 1,
            PENDING_PAYMENT = 2,
            SUCCESSFULL_PAYMENT = 3,
        }
    }
}
