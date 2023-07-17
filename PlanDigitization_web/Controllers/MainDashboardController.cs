using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanDigitization_web.Controllers
{
    public class MainDashboardController : Controller
    {

        // GET: MainDashboard
        public ActionResult MainDashboard()
        {
            string _browserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent + "~" + Request.ServerVariables["REMOTE_ADDR"];
            string _sessionValue = Convert.ToString(Session["UserId"]) + "^" + DateTime.Now.Ticks + "^" + _browserInfo + "^" + System.Guid.NewGuid();
            byte[] _encodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_sessionValue);
            string _encryptedString = System.Convert.ToBase64String(_encodeAsBytes);
            Session["encryptedSession"] = _encryptedString;

            return View();
        }
    }
}