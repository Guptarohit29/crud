using rohit_mvc.dbcontext;
using rohit_mvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace rohit_mvc.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            myDBEntities db = new myDBEntities();
            List<modelclass> empobj = new List<modelclass>();
            var res = db.myTables.ToList();
            foreach (var item in res)
            {
                empobj.Add(new modelclass
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Address = item.Address

                }
                    );
            }
            return View(empobj);
        }
        [HttpGet]
        public ActionResult Addemployee()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Addemployee(modelclass empobj)
        {
            myDBEntities db = new myDBEntities();
            myTable tbl = new myTable();
            tbl.Id = empobj.Id;
            tbl.Name = empobj.Name;
            tbl.Email = empobj.Email;
            tbl.Address = empobj.Address;
            if (empobj.Id == 0)
            {
                db.myTables.Add(tbl);
                db.SaveChanges();
            }
            else
            {
                db.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(int Id)
        {
            myDBEntities db = new myDBEntities();
            modelclass empobj = new modelclass();

            var edititem = db.myTables.Where(m => m.Id == Id).First();
            empobj.Id = edititem.Id;
            empobj.Name = edititem.Name;
            empobj.Email = edititem.Email;
            empobj.Address = edititem.Address;
            ViewBag.Id = edititem.Id;
            return View("Addemployee", empobj);
        }
        public ActionResult Delete(int Id)
        {
            myDBEntities db = new myDBEntities();
            var del = db.myTables.Where(m => m.Id == Id).First();
            db.myTables.Remove(del);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}