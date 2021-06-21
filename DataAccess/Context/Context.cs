using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Timers;

namespace WebApi.DataAccess
{
    public static class Context
    {
        public static List<Product> Products = new List<Product>();
        public static List<Order> Orders = new List<Order>();
        public static List<Campaign> Campaigns = new List<Campaign>();
        public static DateTime timer = DateTime.Now.Date;

        
    }
}

