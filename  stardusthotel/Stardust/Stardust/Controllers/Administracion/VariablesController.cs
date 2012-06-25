using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.IO ;
using log4net;

namespace Stardust.Controllers
{
    public class VariablesController : Controller
    {
        VariablesFacade variablesFac = new VariablesFacade();
        private static ILog log = LogManager.GetLogger(typeof(VariablesController));
        //
        // GET: /Variables/

        public ActionResult Index()
        {
            try
            {
                return View(variablesFac.getVariables());
            }
            catch (Exception e) {
                log.Error("Index(EXCEPTION): ", e);
                return View( new VariablesBean() );
            }
        }

        //
        // GET: /Variables/Edit
 
        public ActionResult Edit()
        {
            try
            {
                return View(variablesFac.getVariables());
            }
            catch (Exception e) {
                log.Error("Edit(EXCEPTION): ", e);
                return View(new VariablesBean());
            }
        }

        //
        // POST: /Variables/Edit

        [HttpPost]
        public ActionResult Edit( VariablesBean variables )
        {
            try
            {
                variablesFac.actualizarVariables(variables);
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                log.Error("Edit(EXCEPTION): ", e);
                return RedirectToAction("Index");
            }
        }

        public ActionResult UploadLogo(HttpPostedFileBase img )
        {
            try
            {
                img.SaveAs(Server.MapPath(@"~/Content/images/Logo.png"));
                return RedirectToAction("Index");
            }
            catch( Exception e ) {
                log.Error("UploadLogo(EXCEPTION): ", e);
                return RedirectToAction("Index");
            }
        }
    }
}
