using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Datatable.Models
{
    public class SysDataTablePager<T>
    {
        public SysDataTablePager(IEnumerable<T> items,
           int totalRecords,
           int totalDisplayRecords,
           string sEcho)
        {
            aaData = items;
            iTotalRecords = totalRecords;
            iTotalDisplayRecords = totalDisplayRecords;
            this.SEcho = sEcho;
           
        }



        public IEnumerable<T> aaData { get; set; }

        public int iTotalRecords { get; set; }

        public int iTotalDisplayRecords { get; set; }

        public string SEcho { get; set; }
    }
}
