using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs
{
    public class ResultDTO<T> where T: class
    {
        public T Data { get; set; }
        public bool IsSucessed { get; set; } = false;
        public string ServiceMessage { get; set; }
    }
}
