using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EADProject.Models;

namespace EADProject.Controllers
{
    public class HospitalManagementController : Controller
    {
        //
        // GET: /HospitalManagement/
        DRecordsDataContext dc = new DRecordsDataContext();
        public ActionResult RegisterDoctors()
        {
            var r = dc.DRecords.Select(rec => rec.Specialist).Distinct();
            return View(r.ToList());
        }

        public ActionResult AddDoctor()
        {
            DRecord mr = new DRecord();

            mr.Name = Request["name"];
            string ar = Request["specialist"];
            if (ar == "Others")
                mr.Specialist = Request["other"];
            else
                mr.Specialist = ar;
            mr.HospitalName = Request["hName"];
            mr.Fee = Request["fee"];
            mr.Location = Request["location"];
            dc.DRecords.InsertOnSubmit(mr);
            dc.SubmitChanges();
            return RedirectToAction("RegisterDoctors");
        }

        public ActionResult DoctorsDetails()
        {
            var r = dc.DRecords.Select(rec => rec);
            ViewBag.record = r;
            return View();
        }

        public ActionResult SearchDetails()
        {
            var r = dc.DRecords.Select(rec => rec.Specialist).Distinct();
            return View(r.ToList());
        }

        public ActionResult viewDetails()
        {
            string spec = Request["SpecialistIn"];
            if (spec != "all")
            {
                var r = dc.DRecords.Where(rec => rec.Specialist == spec);
                ViewBag.record = r;
                return View(r.ToList());
            }
            else
            {
                var r = dc.DRecords.Select(rec => rec);
                ViewBag.record = r;
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            ViewBag.data = id;
            var r = dc.DRecords.First(rec => rec.Id == id);
            dc.DRecords.DeleteOnSubmit(r);
            dc.SubmitChanges();

            return RedirectToAction("SearchDetails");
        }

        public ActionResult Edit(int id)
        {
            Session["Id"] = id;
            var val = dc.DRecords.First(e => e.Id == id);
            ViewBag.data = dc.DRecords.Select(rec => rec.Specialist).Distinct();
            return View(val);
        }

        public ActionResult Update()
        {
            DRecord mr = new DRecord();
            var r = dc.DRecords.First(rec => rec.Id == Convert.ToInt32(Session["Id"]));
            r.Name = Request["name"];
            string ar = Request["specialist"];
            if (ar == "Others")
                r.Specialist = Request["other"];
            else
                r.Specialist = ar;
            r.HospitalName = Request["hName"];
            r.Fee = Request["fee"];
            r.Location = Request["location"];
            dc.SubmitChanges();
          
            return RedirectToAction("SearchDetails");
        }        
	}
}