namespace web_store_server.Domain.Communication
{
    public class Result<T>
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }
        public T Data { get; protected set; }

        public Result(bool success, string message, T data)
        {
            this.IsSuccess = success;
            this.Message = message ?? string.Empty;
            this.Data = data;
        }

        /// <summary>
        /// Produce un resultado fallido.
        /// </summary>
        /// <param name="message">Error message.</param>
        public Result(string message) : this(false, message, default(T))
        {
        }

        /// <summary>
        /// Produce un resultado exitoso.
        /// </summary>
        /// <param name="data">Returned data.</param>
        public Result(T data) : this(true, string.Empty, data)
        {
        }
    }
}
