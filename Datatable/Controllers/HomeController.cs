using Datatable.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Datatable.Controllers
{
    public class HomeController : Controller
    {

        demoEntities1 context = new demoEntities1();

        public ActionResult Index()
        {
            return View();
        }



        public JsonResult GetData()
        {
            NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query);

            string SEcho = nvc["SEcho"].ToString();
            string sSearch = nvc["sSearch"].ToString();
            int iDisplayStart = Convert.ToInt32(nvc["iDisplayStart"]);
            int iDisplayLength = Convert.ToInt32(nvc["iDisplayLength"]);

            //iSortCol le da su número de columna para el cual se requiere clasificación
            int iSortCol = Convert.ToInt32(nvc["iSortCol_0"]);
            //proporciona su orden de clasificación (asc/desc)
            string sortOrder = nvc["sSortDir_0"].ToString();

            //get total value count
            var Count = context.Student_details.Count();

            var Students = new List<Student_details>();

            //Search query when sSearch is not empty
            if (sSearch != "" && sSearch != null) //If there is search query
            {

                Students = context.Student_details.Where(a => a.Name.ToLower().Contains(sSearch.ToLower())
                                  || a.Email.ToLower().Contains(sSearch.ToLower())
                                  || a.Class.ToLower().Contains(sSearch.ToLower())
                                  )
                                  .ToList();

                Count = Students.Count();
                // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                Students = SortFunction(iSortCol, sortOrder, Students).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            else
            {
                //get data from database
                Students = context.Student_details //speficiy conditions if there is any using .Where(Condition)                             
                                   .ToList();

                // Call SortFunction to provide sorted Data, then Skip using iDisplayStart  
                Students = SortFunction(iSortCol, sortOrder, Students).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }

            var StudentsPaged = new SysDataTablePager<Student_details>(Students, Count, Count, SEcho);

            return Json(StudentsPaged, JsonRequestBehavior.AllowGet);


        }

        //Sorting Function
        private List<Student_details> SortFunction(int iSortCol, string sortOrder, List<Student_details> list)
        { 
     //Sorting for String columns
            if (iSortCol == 1 || iSortCol == 0 || iSortCol==2)
            {
                Func<Student_details, string> orderingFunction = (c => iSortCol == 0 ? c.Name : iSortCol == 1 ? c.Email : iSortCol == 2 ? c.Class : c.Name); // compare the sorting column

                if (sortOrder == "desc")
                {
                    list = list.OrderByDescending(orderingFunction).ToList();
}
                else
{
                     list = list.OrderBy(orderingFunction).ToList();

}
            }
           

            return list;
        }
    }
}   