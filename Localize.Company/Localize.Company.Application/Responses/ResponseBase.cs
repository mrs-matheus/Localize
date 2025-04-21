using Localize.Company.Domain.Notifications;

namespace Localize.Company.Application.Responses
{
    public class ResponseBase<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public List<Notification> Errors { get; set; }

        public static ResponseBase<T> Ok(T data, string message = "Operation has been succeed")
            => new() { Success = true, Message = message, Data = data };
        public static ResponseBase<T?> Ok(string message = "Operation has been succeed")
            => new() { Success = true, Message = message, Data = default! };

        public static ResponseBase<T> Fail(List<Notification> errors, string message = "Operation Error")
            => new() { Success = false, Message = message, Errors = errors };

        public static ResponseBase<T> Fail(string errorMessage, string field = "")
            => new()
            {
                Success = false,
                Message = "Operation Error",
                Errors = new List<Notification> { new(field, errorMessage) }
            };
    }
}
