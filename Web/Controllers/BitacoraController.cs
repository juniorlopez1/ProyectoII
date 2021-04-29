using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace Web.Controllers
{
    public class BitacoraController : Controller
    {
        private readonly IBitacoraWebService _service;

        public BitacoraController(IBitacoraWebService service)
        {
            _service = service;
        }

        public async Task<ActionResult> Index()
        {
            var view = await _service.Get();
            return View(view);
        }


        #region NotUse

        //public async Task<ActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = await _dataAccessProvider.GetEmployee(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Name,Address,Gender,Company,Designation")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _dataAccessProvider.Add(employee);
        //        return RedirectToAction("Index");
        //    }

        //    return View(employee);
        //}

        //public async Task<ActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = await _dataAccessProvider.GetEmployee(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address,Gender,Company,Designation")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _dataAccessProvider.Update(employee);
        //        return RedirectToAction("Index");
        //    }
        //    return View(employee);
        //}

        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Employee employee = await _dataAccessProvider.GetEmployee(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    await _dataAccessProvider.Delete(id);
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //} 

        #endregion
    }
}
