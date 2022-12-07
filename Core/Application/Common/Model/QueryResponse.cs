using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Model
{
    public interface IQueryResponse<T> where T : class, new()
    {
        int TotalCount { get; set; }
        T Result { get; set; }
        string Message { get; set; }
        bool IsSuccess { get; set; }
    }
    public sealed class QueryResponse<T>: IQueryResponse<T> where T : class, new()
    {
        public QueryResponse() { }
        public int TotalCount { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public static QueryResponse<T> CreateInstance(T result, string message, int totalCount = 1, bool isSuccess = true)
        {
            return new QueryResponse<T> { Result = result, Message = message, TotalCount = totalCount, IsSuccess = isSuccess };
        }
    }
}
