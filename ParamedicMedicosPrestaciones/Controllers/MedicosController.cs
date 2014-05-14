using ParamedicMedicosPrestaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParamedicMedicosPrestaciones.Controllers
{
    public class MedicosController : Controller
    {
        //
        // GET: /Medicos/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetGuardias()
        {

            List<Guardia> guardias = new List<Guardia>();
            Guardia guardia1 = new Guardia(1, "FIJA", 20, "07:00", "20:00", 13, 3, 4, 1, 5, 2, 400.32);
            Guardia guardia2 = new Guardia(2, "VARIADA", 14, "07:00", "20:00", 13, 3, 4, 1, 5, 2, 400.32);
            Guardia guardia3 = new Guardia(3, "FIJA", 11, "07:00", "20:00", 13, 3, 4, 1, 5, 2, 400.32);
            guardias.Add(guardia1);
            guardias.Add(guardia2);
            guardias.Add(guardia3);

            return Json(guardias, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Medicos/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Medicos/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Medicos/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Medicos/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Medicos/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Medicos/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Medicos/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
