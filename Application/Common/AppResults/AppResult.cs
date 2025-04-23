using System.Net;

namespace Application.Common.AppResults
{
    public class AppResult<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }

        public bool IsSuccess { get; private set; }
        protected AppResult() { }

        public AppResult(T data)
        {
            Data = data;
        }

        public static implicit operator T?(AppResult<T> result)
        {
            return result.Data;
        }

        public static implicit operator AppResult<T>(T value)
        {
            return new AppResult<T>(value);
        }

        /// <summary>
        /// Creates result with status <see cref="AppStatusCode.Ok"/>, equivalent <seealso cref="HttpStatusCode.OK"/><br/>
        /// Indicates that the request has succeeded
        /// </summary>
        /// <returns></returns>

        public static AppResult<T> Success(T value)
        {
            return new AppResult<T>(value) { IsSuccess = true };
        }

        /// <summary>
        /// Creates result with status <see cref="AppStatusCode.Error"/>, equivalent <seealso cref="HttpStatusCode.InternalServerError"/><br/>
        /// Indicates that the server has occur unexpected error
        /// </summary>
        /// <returns></returns>
        public static AppResult<T> Error(string message)
        {
            return new AppResult<T>() { Message = message, IsSuccess = false };
        }
    }
}
