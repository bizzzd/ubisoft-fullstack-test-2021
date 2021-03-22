using System.Collections.Generic;
using System.Linq;

namespace AcmeGames.Application.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Failure(params string[] errors)
        {
            return new Result(false, errors);
        }
    }

    public class Result<T> : Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors, T data) : base(succeeded, errors)
        {
            Data = data;
        }

        public T Data { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, new string[] { }, data);
        }

        public static new Result<T> Failure(params string[] errors)
        {
            return new Result<T>(false, errors, default);
        }
    }
}
