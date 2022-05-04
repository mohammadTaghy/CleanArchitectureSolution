using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public struct Result
    {
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;
        public string Msg { get; }
        
        
        private Result(bool isFailure, string msg)
        {
            Msg = msg;
            IsFailure = isFailure;
        }

        public static Result Ok()
        {
           return new Result(false, string.Empty);
        }

        public static Result Ok(string msg)
        {
           return new Result(false, msg);
        }

        public static Result Fail()
        {
            return new Result(true, string.Empty);
        }

        public static Result Fail(string errorMsg)
        {
            return new Result(true, errorMsg);
        }
    }

    public struct Result<T>
    {
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;
        public string Error { get; }
        public T Value { get; }

        private Result(bool isFailure, string error, T value)
        {
            Error = error;
            IsFailure = isFailure;
            Value = value;
        }
      

        public static Result<T> Ok(T value)
        {
            return new Result<T>(false, string.Empty, value);
        }

        public static Result<T> Fail(T value)
        {
            return new Result<T>(true, string.Empty, value);
        }

        public static Result<T> Fail(string errorMsg, T value)
        {
            return new Result<T>(true, errorMsg, value);
        }

    }
}
