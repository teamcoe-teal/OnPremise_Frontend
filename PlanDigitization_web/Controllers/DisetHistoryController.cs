using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static PlanDigitization_web.FilterConfig;

namespace PlanDigitization_web.Controllers
{
    [SessionTimeout]
    public class DisetHistoryController : Controller
    {
        // GET: DisetHistory

        public readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        string Baseurl = @System.Configuration.ConfigurationManager.AppSettings["url"];

        public ActionResult DisetHistoric()
        {
            if (this.HasPermission("DisetHistoric-View"))
            {
                if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Settings_err", "Main");
                }
            }
            else
            {
                return RedirectToAction("Unauth_page", "Main");
            }

        }

        public ActionResult DisetLive()
        {
            if (this.HasPermission("DisetHistoric-View"))
            {
                if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Settings_err", "Main");
                }
            }
            else
            {
                return RedirectToAction("Unauth_page", "Main");
            }

        }

        ////Tool List
        //public async Task<ActionResult> DisetHistoric(Models.disetsetting U)
        //{

        //    if (this.HasPermission("DisetHistoric-View"))
        //    {
        //        if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
        //        {
        //            try
        //            {
        //                U.QueryType = "Diset_details";
        //                U.PlantCode = Session["PlantCode"].ToString();
        //                U.CompanyCode = Session["CompanyCode"].ToString();

        //                using (var client = new HttpClient())
        //                {
        //                    client.BaseAddress = new Uri(Baseurl);
        //                    client.DefaultRequestHeaders.Clear();
        //                    var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
        //                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
        //                    var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
        //                    if (responseMessage.IsSuccessStatusCode)
        //                    {
        //                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                        HttpResponseMessage response = client.PostAsJsonAsync<Models.disetsetting>("api/UserSettings/diset_details", U).Result;
        //                        List<Models.Diset> DasList = new List<Models.Diset>();
        //                        var data = response.Content.ReadAsStringAsync().Result;
        //                        DasList = JsonConvert.DeserializeObject<List<Models.Diset>>(data);
        //                        return View(DasList);
        //                    }
        //                    else
        //                    {
        //                        TempData["message"] = "Session Timeout";
        //                        return RedirectToAction("Login", "Main");
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Logger.Warn(e);
        //                return RedirectToAction("Error_Page", "Main");
        //            }
        //        }
        //        else
        //        {
        //            return RedirectToAction("Settings_err", "Main");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Unauth_page", "Main");
        //    }
        //}


    }
}