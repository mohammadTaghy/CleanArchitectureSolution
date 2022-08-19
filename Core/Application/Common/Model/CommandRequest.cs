namespace Application.Common.Model
{
    public sealed class CommandResponse<T>
    {
        public CommandResponse(IEnumerable<string> error)
        {
            IsSuccess = false;
            Errors = error;
        }
        public CommandResponse(bool success)
        {
            IsSuccess = success;
        }
        public CommandResponse(bool success, T result)
        {
            IsSuccess = success;
            Result = result;
        }

        public T Result { get; set; }
        public bool IsSuccess { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }

}
