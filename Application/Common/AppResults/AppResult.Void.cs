using System.Net;

namespace Application.Common.AppResults
{
    public class AppResult : AppResult<AppResult>
    {
        public AppResult() { }

        /// <summary>
        /// Creates result with status <see cref="AppStatusCode.Ok"/>, equivalent <seealso cref="HttpStatusCode.OK"/><br/>
        /// Indicates that the request has succeeded
        /// </summary>
        /// <returns></returns>
        public static AppResult Success()
        {
            return new AppResult();
        }

        /// <summary>
        /// Creates result with status <see cref="AppStatusCode.Ok"/>, equivalent <seealso cref="HttpStatusCode.OK"/><br/> 
        /// Indicates that the request has succeeded
        /// </summary>
        /// <returns></returns>
        public static AppResult<T> Success<T>(T value)
        {
            return new AppResult<T>(value);
        }

        /// <summary>
        /// Creates result with status <see cref="AppStatusCode.Error"/>, equivalent <seealso cref="HttpStatusCode.InternalServerError"/><br/>
        /// Indicates that the server has occur unexpected error
        /// </summary>
        /// <returns></returns>
        public new static AppResult Error(string message)
        {
            return new AppResult() { Message = message };
        }
    }
}
