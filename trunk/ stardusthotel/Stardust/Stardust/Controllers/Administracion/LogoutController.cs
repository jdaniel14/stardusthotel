using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

using System.Web.Security;

namespace Stardust.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/

        public ActionResult Index( int id )
        {
            FormsAuthentication.SignOut();
            new UsuarioFacade().logout(id);
            return RedirectToAction("Index", "Login");
        }

    }
}
