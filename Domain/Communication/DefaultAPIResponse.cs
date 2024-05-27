namespace web_store_server.Domain.Communication
{
    public class DefaultAPIResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
    }
}
