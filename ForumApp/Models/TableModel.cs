using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumApp.Models
{
    public class TableModel<T>
    {
        public string code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }

        public List<T> data { get; set; }
    }
}