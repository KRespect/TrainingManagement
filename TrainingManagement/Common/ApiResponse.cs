using System.Text.Json.Serialization;

namespace TrainingManagement.Common
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("code")]
        public int Code { get; set; } = 200;

        [JsonPropertyName("msg")]
        public string Message { get; set; } = "success";

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> { Data = data };
        }

        public static ApiResponse<T> Error(string message, int code = 400)
        {
            return new ApiResponse<T> { Code = code, Message = message };
        }
    }

    public class ApiResponse : ApiResponse<object>
    {
    }
}