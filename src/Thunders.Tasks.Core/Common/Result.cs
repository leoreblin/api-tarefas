using Thunders.Tasks.Core.Errors;

namespace Thunders.Tasks.Core.Common
{
    public class Result
    {
        public Result(bool success, Error error)
        {
            Success = success;
            Error = error;
        }

        public bool Success { get; set; }

        public bool Failed => !Success;

        public Error Error { get; }

        public static Result Ok() => new(true, Error.None);

        public static Result Fail(Error error) => new(false, error);

        public static Result<TValue> Ok<TValue>(TValue value) => new(value, true, Error.None);

        public static Result<TValue> Fail<TValue>(Error error) => new(default, false, error);

        public static Result<TValue> Create<TValue>(TValue? value) =>
            value is not null ? Ok(value) : Fail<TValue>(Error.None);
    }
}
