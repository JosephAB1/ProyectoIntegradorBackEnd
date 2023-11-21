using System;
namespace PRY.DataAcces.Bases
{
    public class BaseResponse<T>
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public object InnerException { get; set; }
    }
}

