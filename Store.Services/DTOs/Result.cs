namespace MLGStore.Services.DTOs
{
    public abstract class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
    }

    public class Result<T> : Result
    {
        public T Data { get; private set; }

        public static Result<T> CreateResult(T data, string msg = "")
        {
            return new Result<T>()
            {
                Data = data,
                Message = msg,
                Success = true
            };
        }

        public static Result<T> CreateErrorResult(string msg)
        {
            return new Result<T>()
            {
                Data = default,
                Message = msg,
                Success = false
            };
        }

        public static Result<T> CreateExceptionResult(Exception ex)
        {
            return new Result<T>()
            {
                Data = default,
                Message = ex.Message,
                Success = false
            };
        }

    }
}
