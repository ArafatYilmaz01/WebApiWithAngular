using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public  decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
    }
}
