using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using Stardust.Models;

namespace Stardust.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginmodel)
        {
            if (ModelState.IsValid)
            {
                //if (loginmodel.Usuario == "stardust" && loginmodel.Contrasenia == "stardust")
                if (Utils.comprobarLogin(loginmodel.Usuario, loginmodel.Contrasenia))
                {
                    FormsAuthentication.SetAuthCookie(loginmodel.Usuario, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario y/o Constrasenia incorrectos");
                }
            }
            return View();
        }
    }
}
