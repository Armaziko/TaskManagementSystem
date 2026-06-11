namespace TaskManagementSystem.Backend.Application.Models
{
    /// <summary>
    /// Represents a result of an operation.
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; } = new();
        public OperationStatus OperationStatus { get; set; } 
        public Result(bool isSuccess, string? message, OperationStatus status)
        {
            this.IsSuccess = isSuccess;
            this.Messages.Add(string.IsNullOrWhiteSpace(message) ? string.Empty : message);
            this.OperationStatus = status;
        }
        public Result(bool isSuccess, List<string> messages, OperationStatus status)
        {
            this.IsSuccess = isSuccess;
            this.Messages.AddRange(messages);
            this.OperationStatus = status;
        }
        public static Result Success() => new Result(true, "Operation was a success.", OperationStatus.SUCCESS);
        public static Result BadRequest() => new Result(false, "Operation resulted in a bad request code.", OperationStatus.BAD_REQUEST);
        public static Result NotFound() => new Result(false, "Operation resulted in a not found code.", OperationStatus.NOT_FOUND);
        public static Result ValidationFailed(List<string> messages) => new Result(false, messages, OperationStatus.VALIDATION_FAILED);
        public static Result ValidationFailed(string message) => new Result(false, message, OperationStatus.VALIDATION_FAILED);
        public static Result InternalError() => new Result(false, "Operation resulted in an internal error code.", (OperationStatus.INTERNAL_ERROR));
        public static Result Failure() => new Result(false, "Operation was a failure.", OperationStatus.FAILURE);
    }
    /// <summary>
    /// Represents a result of an operation that returns a value.
    /// </summary>
    public class Result<TValue>
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; } = new();
        public OperationStatus OperationStatus { get; set; }
        public TValue? Value { get; set; } = default(TValue);
        public Result(bool isSuccess, string? message, OperationStatus status)
        {
            this.IsSuccess = isSuccess;
            this.Messages.Add(string.IsNullOrWhiteSpace(message) ? string.Empty : message);
            this.OperationStatus = status;
        }
        public Result(bool isSuccess, string? message, TValue value, OperationStatus status)
        {
            this.IsSuccess = isSuccess;
            this.Messages.Add(string.IsNullOrWhiteSpace(message) ? string.Empty : message);
            this.OperationStatus = status;
            this.Value = value;
        }
        public Result(bool isSuccess, List<string> messages, OperationStatus status)
        {
            this.IsSuccess = isSuccess;
            this.Messages.AddRange(messages);
            this.OperationStatus = status;
        }
        public Result(bool isSuccess, List<string> messages, TValue value, OperationStatus status)
        {
            this.IsSuccess = isSuccess;
            this.Messages.AddRange(messages);
            this.OperationStatus = status;
            this.Value = value;
        }
        public static Result<T> Success<T>(T value) => new Result<T>(true, "Operation was a success.", value, OperationStatus.SUCCESS);
        public static Result<T> Failure<T>() => new Result<T>(false, "Operation was a failure.", OperationStatus.FAILURE);
        public static Result<T> BadRequest<T>() => new Result<T>(false, "Operation resulted in a bad request code.", OperationStatus.BAD_REQUEST);
        public static Result<T> ValidationFailed<T>(List<string> messages) => new Result<T>(false, messages, OperationStatus.VALIDATION_FAILED);
        public static Result<T> ValidationFailed<T>(string message) => new Result<T>(false, message, OperationStatus.VALIDATION_FAILED);
        public static Result<T> NotFound<T>() => new Result<T>(false, "Operation resulted in a not found code.", OperationStatus.NOT_FOUND);
        public static Result<T> InternalError<T>() => new Result<T>(false, "Operation resulted in an internal error code.", (OperationStatus.INTERNAL_ERROR));
    }
}
