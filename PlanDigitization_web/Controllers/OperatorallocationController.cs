using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static PlanDigitization_web.FilterConfig;

namespace PlanDigitization_web.Controllers
{
    public class OperatorallocationController : Controller
    {
        public readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        string Baseurl = @System.Configuration.ConfigurationManager.AppSettings["url"];

 //-------------Skill Category---------------
        public async Task<ActionResult> Skill_category(Models.Setting U)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                if (this.HasPermission("OperatorAllocation-View"))
                {
                    try
                    {
                        U.QueryType = "Get_skill_category";
                        U.Parameter1 = Session["CompanyCode"].ToString();
                        U.Parameter = Session["PlantCode"].ToString();
                        U.Parameter2 = Session["LineCode"].ToString();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Setting>("api/UserSettings/settings_details_Lines", U).Result;
                                List<Models.Skill_Category> DasList = new List<Models.Skill_Category>();
                                var data = response.Content.ReadAsStringAsync().Result;
                                DasList = JsonConvert.DeserializeObject<List<Models.Skill_Category>>(data);
                                return View(DasList);
                            }
                            else
                            {
                                TempData["message"] = "Token is not valid";
                                return RedirectToAction("Login", "Main");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Warn(e);
                        return RedirectToAction("Error_Page", "Main");
                    }
                }
                else
                {
                    return RedirectToAction("Unauth_page", "Main");
                }
                

            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }


        }

        public async Task<ActionResult> Insert_category(Models.Skill_Category S)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    S.QueryType = "Insert";
                    S.Companycode = Session["CompanyCode"].ToString();
                    S.Plantcode = Session["PlantCode"].ToString();
                    S.LineCode = Session["LineCode"].ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                        var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Skill_Category>("api/Operatorallocation/Add_Update_Category", S).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                            }
                            return RedirectToAction("Skill_category", "Operatorallocation");
                        }
                        else
                        {
                            TempData["message"] = "Token is not valid";
                            return RedirectToAction("Login", "Main");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }

            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }

        }

        [HttpPost]
        public async Task<ActionResult> Update_Category(Models.Skill_Category S)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {

                try
                {
                    S.QueryType = "Update";
                    S.Companycode = Session["CompanyCode"].ToString();
                    S.Plantcode = Session["PlantCode"].ToString();
                    S.LineCode = Session["LineCode"].ToString();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                        var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Skill_Category>("api/Operatorallocation/Add_Update_Category", S).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                            }
                            return RedirectToAction("Skill_category", "Operatorallocation");
                        }
                        else
                        {
                            TempData["message"] = "Token is not valid";
                            return RedirectToAction("Login", "Main");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }

            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }


        }

//---------------Skil Set--------------------

        public async Task<ActionResult> Skill_Set(Models.Setting U)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                if (this.HasPermission("OperatorAllocation-View"))
                {
                    try
                    {
                        U.QueryType = "Get_Skill_set";
                        U.Parameter1 = Session["CompanyCode"].ToString();
                        U.Parameter = Session["PlantCode"].ToString();
                        U.Parameter2 = Session["LineCode"].ToString();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Setting>("api/UserSettings/settings_details_Lines", U).Result;
                                List<Models.Skill_Set> DasList = new List<Models.Skill_Set>();
                                var data = response.Content.ReadAsStringAsync().Result;
                                DasList = JsonConvert.DeserializeObject<List<Models.Skill_Set>>(data);
                                return View(DasList);
                            }
                            else
                            {
                                TempData["message"] = "Token is not valid";
                                return RedirectToAction("Login", "Main");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Warn(e);
                        return RedirectToAction("Error_Page", "Main");
                    }
                }
                else
                {
                    return RedirectToAction("Unauth_page", "Main");
                }
                

            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }


        }

        public async Task<ActionResult> Insert_skillset(Models.Skill_Set S)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    S.QueryType = "Insert";
                    S.Companycode = Session["CompanyCode"].ToString();
                    S.Plantcode = Session["PlantCode"].ToString();
                    S.LineCode = Session["LineCode"].ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                        var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Skill_Set>("api/Operatorallocation/Add_Update_Skill_Set", S).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                            }
                            return RedirectToAction("Skill_Set", "Operatorallocation");
                        }
                        else
                        {
                            TempData["message"] = "Token is not valid";
                            return RedirectToAction("Login", "Main");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }

        }

        public async Task<ActionResult> Update_skillset(Models.Skill_Set S)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    S.QueryType = "Update";
                    S.Companycode = Session["CompanyCode"].ToString();
                    S.Plantcode = Session["PlantCode"].ToString();
                    S.LineCode = Session["LineCode"].ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                        var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Skill_Set>("api/Operatorallocation/Add_Update_Skill_Set", S).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                            }
                            return RedirectToAction("Skill_Set", "Operatorallocation");
                        }
                        else
                        {
                            TempData["message"] = "Token is not valid";
                            return RedirectToAction("Login", "Main");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }


            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }
        }


//----------------Machinewise--------------------


        public async Task<ActionResult> Machinewise_Skill(Models.Setting U)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                if (this.HasPermission("OperatorAllocation-View"))
                {
                    try
                    {
                        U.QueryType = "Get_machine_skill";
                        U.Parameter1 = Session["CompanyCode"].ToString();
                        U.Parameter = Session["PlantCode"].ToString();
                        U.Parameter2 = Session["LineCode"].ToString();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Setting>("api/UserSettings/settings_details_Lines", U).Result;
                                List<Models.Machinewise_Skill> DasList = new List<Models.Machinewise_Skill>();
                                var data = response.Content.ReadAsStringAsync().Result;
                                DasList = JsonConvert.DeserializeObject<List<Models.Machinewise_Skill>>(data);
                                return View(DasList);
                            }
                            else
                            {
                                TempData["message"] = "Token is not valid";
                                return RedirectToAction("Login", "Main");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Warn(e);
                        return RedirectToAction("Error_Page", "Main");
                    }
                }
                else
                {
                    return RedirectToAction("Unauth_page", "Main");
                }
                

            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }

        }

        public async Task<ActionResult> Add_Update_Machinewise_Skill(List<PlanDigitization_web.Models.Machinewise_Skill> skills, List<PlanDigitization_web.Models.Machinewise_Skill_list> Skill_list)
        {

            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        TempData["message"] = "Token is not valid";
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        foreach (var skill in skills)
                        {

                            skill.QueryType = skill.QueryType;
                            skill.Companycode = Session["CompanyCode"].ToString();
                            skill.Plantcode = Session["PlantCode"].ToString();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");

                            if (responseMessage.IsSuccessStatusCode)
                            {
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Machinewise_Skill>("api/Operatorallocation/Add_Update_Machinewise_Skill", skill).Result;
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;


                                foreach (var Skills in Skill_list)
                                {
                                    Skills.QueryType = Skills.QueryType;
                                    Skills.Companycode = Session["CompanyCode"].ToString();
                                    Skills.Plantcode = Session["PlantCode"].ToString();
                                    Skills.Machine_id = skill.Machine_id;
                                    Skills.Category_id = skill.Category_id;

                                    HttpResponseMessage response1 = client.PostAsJsonAsync<Models.Machinewise_Skill_list>("api/Operatorallocation/Add_Update_Machinewise_Skill_list", Skills).Result;
                                    if (response.IsSuccessStatusCode)
                                    {
                                        var res1 = response.Content.ReadAsStringAsync().Result;
                                        var msg1 = JsonConvert.DeserializeObject(res1);
                                        TempData["message"] = msg1;
                                    }
                                }

                            }
                            else
                            {
                                return RedirectToAction("Login", "Main");
                            }
                        }

                        //return RedirectToAction("Machinewise_Skill", "Operatorallocation");
                        return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }


         

        }

//--------------------Operatorwise--------------------

        public async Task<ActionResult> Operatorwise_Skill(Models.Setting U)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                if (this.HasPermission("OperatorAllocation-View"))
                {
                    try
                    {
                        U.QueryType = "Get_operatorwise_skill";
                        U.Parameter1 = Session["CompanyCode"].ToString();
                        U.Parameter = Session["PlantCode"].ToString();
                        U.Parameter2 = Session["LineCode"].ToString();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Setting>("api/UserSettings/settings_details_Lines", U).Result;
                                List<Models.Operatorwise_Skill> DasList = new List<Models.Operatorwise_Skill>();
                                var data = response.Content.ReadAsStringAsync().Result;
                                DasList = JsonConvert.DeserializeObject<List<Models.Operatorwise_Skill>>(data);
                                return View(DasList);
                            }
                            else
                            {
                                TempData["message"] = "Token is not valid";
                                return RedirectToAction("Login", "Main");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Warn(e);
                        return RedirectToAction("Error_Page", "Main");
                    }
                }
                else
                {
                    return RedirectToAction("Unauth_page", "Main");
                }
                
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }
          
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="Operator_skills"></param>
        /// <param name="Operator_skill_list"></param>
        /// <returns></returns>
        public async Task<ActionResult> Add_Update_Operatorwise_Skill(List<PlanDigitization_web.Models.Operatorwise_Skill> Operator_skills, List<PlanDigitization_web.Models.Operatorwise_Skill_list> Operator_skill_list)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        foreach (var skill in Operator_skills)
                        {
                            skill.QueryType = skill.QueryType;
                            skill.Companycode = Session["CompanyCode"].ToString();
                            skill.Plantcode = Session["PlantCode"].ToString();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Operatorwise_Skill>("api/Operatorallocation/Add_Update_Operatorwise_Skill", skill).Result;
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                                foreach (var Skills in Operator_skill_list)
                                {
                                    Skills.QueryType = skill.QueryType;
                                    Skills.Companycode = Session["CompanyCode"].ToString();
                                    Skills.Plantcode = Session["PlantCode"].ToString();
                                    Skills.Operator_Id = skill.Operator_Id;
                                    Skills.Category_Id = skill.Category_Id;
                                    HttpResponseMessage response1 = client.PostAsJsonAsync<Models.Operatorwise_Skill_list>("api/Operatorallocation/Add_Update_Operatorwise_Skill_list", Skills).Result;
                                    if (response.IsSuccessStatusCode)
                                    {
                                        var res1 = response.Content.ReadAsStringAsync().Result;
                                        var msg1 = JsonConvert.DeserializeObject(res1);
                                        TempData["message"] = msg1;
                                    }
                                }
                            }
                            else
                            {
                                TempData["message"] = "Token is not valid";
                                return RedirectToAction("Login", "Main");
                            }
                        }

                        return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }
        }

//---------------Operator_Schedule---------------

        public async Task<ActionResult> Operator_Schedule(Models.Setting U)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                if (this.HasPermission("OperatorAllocation-View"))
                {
                    try
                    {
                        U.QueryType = "Operator_schedule_list";
                        U.Parameter1 = Session["CompanyCode"].ToString();
                        U.Parameter = Session["PlantCode"].ToString();
                        U.Parameter2 = Session["LineCode"].ToString();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Setting>("api/UserSettings/settings_details_Lines", U).Result;
                                List<Models.Operator_Schedule> DasList = new List<Models.Operator_Schedule>();
                                var data = response.Content.ReadAsStringAsync().Result;
                                DasList = JsonConvert.DeserializeObject<List<Models.Operator_Schedule>>(data);
                                return View(DasList);
                            }
                            else
                            {
                                TempData["message"] = "Token is not valid";
                                return RedirectToAction("Login", "Main");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Warn(e);
                        return RedirectToAction("Error_Page", "Main");
                    }
                }
                else
                {
                    return RedirectToAction("Unauth_page", "Main");
                }
                
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }

           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="S"></param>
        /// <param name="U"></param>
        /// <returns></returns>
        public async Task<ActionResult> Insert_Operator_Schedule(Models.Operator_Schedule S, Models.Setting U)
        {

            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {

                    S.QueryType = "Insert";
                    S.Companycode = Session["CompanyCode"].ToString();
                    S.Plantcode = Session["PlantCode"].ToString();
                    S.LineCode = Session["LineCode"].ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                        var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Operator_Schedule>("api/Operatorallocation/Add_Update_Operator_Schedule", S).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                                var Repeat = S.Repeat;
                                for (int i = 1; i <= Repeat; i++)
                                {

                                    U.QueryType = "Get_last_schedule";
                                    U.Parameter = msg.ToString();

                                    //U.Companycode = Session["CompanyCode"].ToString();
                                    //U.Plantcode = Session["PlantCode"].ToString();

                                    using (var client2 = new HttpClient())
                                    {
                                        client2.BaseAddress = new Uri(Baseurl);
                                        client2.DefaultRequestHeaders.Clear();
                                        var user2 = Session["Token"].ToString() + ':' + Session["UserName"];
                                        client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                        client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user2.ToString());
                                        var responseMessage1 = await client2.GetAsync("api/UserSettings/GetEmployee");
                                        if (responseMessage1.IsSuccessStatusCode)
                                        {
                                            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                            HttpResponseMessage response1 = client2.PostAsJsonAsync<Models.Setting>("api/UserSettings/settings_details_Lines", U).Result;
                                            List<Models.Operator_Schedule> DasList = new List<Models.Operator_Schedule>();
                                            var data = response1.Content.ReadAsStringAsync().Result;
                                            DasList = JsonConvert.DeserializeObject<List<Models.Operator_Schedule>>(data);
                                            //return View(DasList);

                                            //var Duration = DasList[0].Days;
                                            //var Repeat1 = DasList[0].Repeat;
                                            var Fromdate = DasList[0].FromDate;
                                            var Todate = DasList[0].ToDate;
                                            var Machine = DasList[0].Machine;
                                            var Opertor = DasList[0].Operator;
                                            var Shift = DasList[0].Shift;
                                            var Ref_id = DasList[0].Ref_id;

                                            S.QueryType = "Insert_Repeat";
                                            S.Days = S.Days;
                                            S.Repeat = S.Repeat;
                                            S.Machine = Machine;
                                            S.Operator = Opertor;
                                            S.Shift = Shift;
                                            S.Ref_id = Ref_id;
                                            S.FromDate = Fromdate;
                                            S.ToDate = Todate;


                                            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                            HttpResponseMessage response2 = client.PostAsJsonAsync<Models.Operator_Schedule>("api/Operatorallocation/Add_Update_Operator_Schedule", S).Result;
                                            if (response2.IsSuccessStatusCode)
                                            {
                                                var res2 = response2.Content.ReadAsStringAsync().Result;
                                                var msg2 = JsonConvert.DeserializeObject(res2);
                                                TempData["message"] = msg;
                                            }

                                        }

                                    }


                                }

                            }
                            return RedirectToAction("Operator_Schedule", "Operatorallocation");
                        }
                        else
                        {
                            TempData["message"] = "Token is not valid";
                            return RedirectToAction("Login", "Main");
                        }
                        //return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                   
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }
         

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update_Operator_Schedule(Models.Operator_Schedule S)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    S.QueryType = "Update";
                    S.Companycode = Session["CompanyCode"].ToString();
                    S.Plantcode = Session["PlantCode"].ToString();
                    S.LineCode = Session["LineCode"].ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                        var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Operator_Schedule>("api/Operatorallocation/Add_Update_Operator_Schedule", S).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                            }
                            return RedirectToAction("Operator_Schedule", "Operatorallocation");
                        }
                        else
                        {
                            TempData["message"] = "Token is not valid";
                            return RedirectToAction("Login", "Main");
                        }
                        //return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                  
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }

          

        }


//---------------Operator Allocation Slots-----------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="U"></param>
        /// <returns></returns>
        public async Task<ActionResult> Operator_Allocation_Slots(Models.Setting U)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                if (this.HasPermission("OperatorAllocation-View"))
                {
                    try
                    {
                        U.QueryType = "Operator_schedule_list";
                        U.Parameter1 = Session["CompanyCode"].ToString();
                        U.Parameter = Session["PlantCode"].ToString();
                        U.Parameter2 = Session["LineCode"].ToString();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Setting>("api/UserSettings/settings_details_Lines", U).Result;
                                List<Models.Operator_Schedule> DasList = new List<Models.Operator_Schedule>();
                                var data = response.Content.ReadAsStringAsync().Result;
                                DasList = JsonConvert.DeserializeObject<List<Models.Operator_Schedule>>(data);
                                return View(DasList);

                            }
                            else
                            {
                                TempData["message"] = "Token is not valid";
                                return RedirectToAction("Login", "Main");
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Warn(e);
                        return RedirectToAction("Error_Page", "Main");
                    }
                }
                else
                {
                    return RedirectToAction("Unauth_page", "Main");
                }
                
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }
          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult>add_Absent_Details(Models.Absent_Data S)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    S.QueryType = "Insert";
                    S.Companycode = Session["CompanyCode"].ToString();
                    S.Plantcode = Session["PlantCode"].ToString();
                    S.LineCode = Session["LineCode"].ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                        var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Absent_Data>("api/Operatorallocation/Add_Update_Absent_Details", S).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                            }
                            return RedirectToAction("Operator_Allocation_Slots", "Operatorallocation");
                        }
                        else
                        {
                            TempData["message"] = "Token is not valid";
                            return RedirectToAction("Login", "Main");
                        }

                        //return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                   
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }

           

        }

//------------------List of Absent----------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="U"></param>
        /// <returns></returns>
        public async Task<ActionResult> Operator_Absent_Details(Models.Setting U)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                if (this.HasPermission("OperatorAllocation-View"))
                {
                    try
                    {
                        U.QueryType = "Get_absent";
                        U.Parameter1 = Session["CompanyCode"].ToString();
                        U.Parameter = Session["PlantCode"].ToString();
                        U.Parameter2 = Session["LineCode"].ToString();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Setting>("api/UserSettings/settings_details_Lines", U).Result;
                                List<Models.Absent_Data> DasList = new List<Models.Absent_Data>();
                                var data = response.Content.ReadAsStringAsync().Result;
                                DasList = JsonConvert.DeserializeObject<List<Models.Absent_Data>>(data);
                                return View(DasList);
                            }
                            else
                            {
                                TempData["message"] = "Token is not valid";
                                return RedirectToAction("Login", "Main");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Warn(e);
                        return RedirectToAction("Error_Page", "Main");
                    }
                }
                else
                {
                    return RedirectToAction("Unauth_page", "Main");
                }
                
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult>Update_Absent_Details(Models.Absent_Data S)
        {

            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    S.QueryType = "Update";
                    S.Companycode = Session["CompanyCode"].ToString();
                    S.Plantcode = Session["PlantCode"].ToString();
                    S.LineCode = Session["LineCode"].ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                        var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Absent_Data>("api/Operatorallocation/Add_Update_Absent_Details", S).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                            }
                            return RedirectToAction("Operator_Absent_Details", "Operatorallocation");
                        }
                        else
                        {
                            TempData["message"] = "Token is not valid";
                            return RedirectToAction("Login", "Main");
                        }
                        //return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                  
                }
                catch (Exception e)
                {
                    Logger.Warn(e);
                    return RedirectToAction("Error_Page", "Main");
                }
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }
           

        }

//--------------Skill wise Eligibility module-------------

       /// <summary>
       /// 
       /// </summary>
       /// <param name="U"></param>
       /// <returns></returns>
       /// 

        public ActionResult Skill_Wise_Report()
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                if (this.HasPermission("OperatorAllocation-View"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Unauth_page", "Main");
                }
            }
            else
            {
                return RedirectToAction("Settings_err", "Main");
            }
            
            
        }

        //public async Task<ActionResult>Skill_Wise_Report(Models.Setting U)
        //{
        //    try
        //    {
        //        U.QueryType = "";
        //        U.Parameter1 = Session["CompanyCode"].ToString();
        //        U.Parameter = Session["PlantCode"].ToString();
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(Baseurl);
        //            client.DefaultRequestHeaders.Clear();
        //            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
        //            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                HttpResponseMessage response = client.PostAsJsonAsync<Models.Setting>("api/UserSettings/settings_details_Lines", U).Result;
        //                List<Models.Skill_Wise_Report> DasList = new List<Models.Skill_Wise_Report>();
        //                var data = response.Content.ReadAsStringAsync().Result;
        //                DasList = JsonConvert.DeserializeObject<List<Models.Skill_Wise_Report>>(data);
        //                return View(DasList);
        //            }
        //            else
        //            {
        //                TempData["message"] = "Token is not valid";
        //                return RedirectToAction("Login", "Main");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Warn(e);
        //        return RedirectToAction("Error_Page", "Main");
        //    }
        //}

    }
}