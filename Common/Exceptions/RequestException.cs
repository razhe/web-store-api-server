using web_store_server.Shared.Resources;

namespace web_store_server.Common.Exceptions
{
    public class RequestException :
        Exception
    {
        public RequestException(int code, string title, IEnumerable<Error>? errors = null)
        {
            Code = code;
            Title = title;
            Errors = errors;
        }

        public int Code { get; set; }
        public string Title { get; set; } = null!;
        public IEnumerable<Error>? Errors { get; set; }

    }
}
