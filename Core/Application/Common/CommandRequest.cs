namespace Application.Common
{
    public sealed class CommandRequest<T>
    {
        public CommandRequest(IEnumerable<CustomError> error)
        {
            IsSuccess = false;
            Error = error;
        }

        public CommandRequest(bool success,T result)
        {
            IsSuccess = success;
            Result = result;
        }
        
        public T Result { get; set; }
        public bool IsSuccess { get; set; }

        public IEnumerable<CustomError> Error { get; set; }
    }
    public class CustomError
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
