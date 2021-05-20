using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class DescriptiveResponse<T>
    {
        public T Result { get; set; }
        public int status { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }

        public DescriptiveResponse<T> success(T value)
        {
            return new DescriptiveResponse<T>()
            {
                status = 200,
                Result = value
            };
        }
        public DescriptiveResponse<T> Error(string error = "Unexpected Error")
        {
            return new DescriptiveResponse<T>()
            {
                IsError = true,
                Message = error,
                status = 500
            };
        }

        public DescriptiveResponse<T> NotFound()
        {
            return new DescriptiveResponse<T>()
            {
                IsError = true,
                Message = "Not Found",
                status = 404
            };
        }

    }
}
