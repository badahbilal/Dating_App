using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    // The 'ApiException' class represents API-related exceptions with status code, message, and details.

    public class ApiException
    {
        // Constructor to initialize the ApiException with status code, message, and details.
        public ApiException(int statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        // Gets or sets the HTTP status code associated with the exception.
        public int StatusCode { get; set; }

        // Gets or sets the human-readable error message.
        public string Message { get; set; }

        // Gets or sets additional details about the error.
        public string Details { get; set; }
    }

}