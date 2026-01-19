using System;
using System.Collections.Generic;
using System.Net;

namespace mosahem.Application.Common
{
    public class Response<T>
    {
        public Response()
        {
            Errors = new Dictionary<string, List<string>>();
            StatusCode = HttpStatusCode.OK;
        }

        public Response(T? data, string? message = null) : this()
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string? message, bool succeeded = false) : this()
        {
            Succeeded = succeeded;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
