using System.Collections.Generic;

namespace MCSABackend.Utilities
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } // Indicates whether the operation was successful.
        public string Message { get; set; } // A general message regarding the request (e.g., success or error description).
        public T Data { get; set; } // The actual data, which is generic to handle various types.
        public List<string> Errors { get; set; } // Any list of errors if applicable (e.g., validation errors).
        public int StatusCode { get; set; } // The HTTP status code.

        // Default constructor
        public ApiResponse()
        {
            Errors = new List<string>();
        }

        // Parameterized constructor to set properties easily
        public ApiResponse(bool success, string message, T data = default, List<string> errors = null, int statusCode = 200)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors ?? new List<string>();
            StatusCode = statusCode;
        }
    }

}
