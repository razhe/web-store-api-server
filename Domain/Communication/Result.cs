namespace web_store_server.Domain.Communication
{
    public class Result<T>
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }
        public Dictionary<int, string> Errors { get; protected set; }
        public T Data { get; protected set; }

        public Result(bool success, string message, T data, Dictionary<int, string> errors)
        {
            this.IsSuccess = success;
            this.Message = message ?? string.Empty;
            this.Data = data;
            this.Errors = errors;
        }

        /// <summary>
        /// Produce un resultado fallido.
        /// </summary>
        /// <param name="message">Error message.</param>
        public Result(string message) : this(false, message, default, default)
        {
        }


        /// <summary>
        /// Produce un resultado fallido y permite otorgar multiples mensajes de error.
        /// </summary>
        /// <param name="errors">Error messages.</param>
        public Result(Dictionary<int, string> errors) : this(false, string.Empty, default, errors)
        {
        }

        /// <summary>
        /// Produce un resultado exitoso.
        /// </summary>
        /// <param name="data">Returned data.</param>
        public Result(T data) : this(true, string.Empty, data, default)
        {
        }
    }
}
