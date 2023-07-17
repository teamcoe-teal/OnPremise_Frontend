using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlanDigitization_web.Controllers
{
    public class DiagnosticsController : Controller
    {
        public readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        string Baseurl = @System.Configuration.ConfigurationManager.AppSettings["url"];

        // GET: Diagnostics

        public async Task<ActionResult> DiagnosticsSetting(Models.Setting A)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                if (this.HasPermission("DiagnosticSetting-View"))
                {
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            A.QueryType = "Diagnostic_details";
                            A.Parameter1 = Session["CompanyCode"].ToString();
                            A.Parameter = Session["PlantCode"].ToString();
                            A.Parameter2 = Session["LineCode"].ToString();

                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();

                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                                HttpResponseMessage response = client.PostAsJsonAsync<Models.Setting>("api/Diagnostics/Diagnostics_details", A).Result;
                                List<Models.Diagnostics_settings> DasList = new List<Models.Diagnostics_settings>();
                                var data = response.Content.ReadAsStringAsync().Result;
                                DasList = JsonConvert.DeserializeObject<List<Models.Diagnostics_settings>>(data);
                                return View(DasList);
                            }
                            else
                            {
                                TempData["message"] = "Session Timeout";
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


        //[HttpPost]
        //public async Task<ActionResult> upload_details(HttpPostedFileBase postedFile)
        //{

        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
                    
        //            client.BaseAddress = new Uri(Baseurl);
        //            client.DefaultRequestHeaders.Clear();

        //            client.BaseAddress = new Uri(Baseurl);
        //            client.DefaultRequestHeaders.Clear();
        //            var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
        //            var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());


        //                string data = "";
        //                if (postedFile != null)
        //                {
        //                    if (ModelState.IsValid)
        //                    {
        //                        int AlreadyCount = 0;
        //                        int ResultCount = 0;
        //                        string filename = postedFile.FileName;
        //                        if (filename.EndsWith(".xlsx") || filename.EndsWith(".xls") || filename.EndsWith(".csv"))
        //                        {
        //                            string dt = DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss") + "_";
        //                            string path = Server.MapPath("~/upload_excel/" + dt + postedFile.FileName);
        //                            postedFile.SaveAs(path);
        //                            string excelConnectionString = @"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + path + "';Extended Properties='Excel 12.0 Xml;IMEX=1'";
        //                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);

        //                            //Sheet Name
        //                            excelConnection.Open();
        //                            string tableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
        //                            excelConnection.Close();
        //                            //End

        //                            OleDbCommand cmd = new OleDbCommand("Select * from [" + tableName + "]", excelConnection);

        //                            excelConnection.Open();

        //                            OleDbDataReader dReader;
        //                            dReader = cmd.ExecuteReader();

        //                            if (dReader.HasRows)
        //                            {

        //                                while (dReader.Read())
        //                                {
        //                                    if (dReader["DeviceID"] != null && dReader["DeviceID"].ToString() != "")
        //                                    {
        //                                        var input = new
        //                                        {

        //                                            CompanyCode = Session["CompanyCode"].ToString(),
        //                                            PlantCode = Session["PlantCode"].ToString(),
        //                                            LineCode = Session["LineCode"].ToString(),
        //                                            DeviceID = dReader["DeviceID"],
        //                                            make = dReader["Make"],
        //                                            gateway = dReader["Gateway"],
        //                                            DeviceRef = dReader["DeviceRef"],
        //                                            ioserver = dReader["IOServer"],
        //                                            partnumber = dReader["Partnumber"],
        //                                            installed = dReader["Installed"],
        //                                            connectedto = dReader["Connectedto"],
        //                                            macaddress = dReader["Macaddress"],
        //                                            remarks = dReader["Remarks"],
        //                                        };
        //                                        string inputJson = (new JavaScriptSerializer()).Serialize(input);

        //                                        HttpResponseMessage response = client.PostAsJsonAsync<string>("api/Upload/upload_diagnosticDetails", inputJson).Result;
        //                                        List<Models.Diagnostics_settings> DasList = new List<Models.Diagnostics_settings>();
        //                                        var data1 = response.Content.ReadAsStringAsync().Result;
        //                                        DasList = JsonConvert.DeserializeObject<List<Models.Diagnostics_settings>>(data1);
        //                                        string response1 = (new JavaScriptSerializer()).Deserialize<string>(data1);
        //                                        if (response1 == "1")
        //                                        {
        //                                            ResultCount = ResultCount + 1;
        //                                        }
        //                                        else if (response1 == "-1")
        //                                        {
        //                                            AlreadyCount = AlreadyCount + 1;
        //                                        }
        //                                    }
                                            
        //                                }
        //                                data = ResultCount.ToString() + ' ' + "Record(s) are Inserted Successfully. " + AlreadyCount.ToString() + " Record(s) are Already Existing.";
        //                                TempData["message"] = data;
        //                                dReader.Close();
        //                                //if ((System.IO.File.Exists(path)))
        //                                //{
        //                                //    System.IO.File.Delete(path);
        //                                //}
                                        
        //                            }
        //                            return RedirectToAction("DiagnosticsSetting", "Diagnostics");
        //                        }
        //                        else
        //                        {
        //                            data = "Only Excel file format is allowed";
        //                            TempData["message"] = data;
        //                            //  return RedirectToAction("Upload", "Upload");
        //                            return RedirectToAction("DiagnosticsSetting", "Diagnostics");
        //                        }
        //                    }
        //                    return RedirectToAction("DiagnosticsSetting", "Diagnostics");
        //                }
        //                else
        //                {
        //                    if (postedFile == null)
        //                    {
        //                        data = "Please choose Excel file";
        //                    }
        //                    TempData["message"] = data;
        //                    return RedirectToAction("DiagnosticsSetting", "Diagnostics");
        //                }

                        
        //            }
        //            else
        //            {
        //                TempData["message"] = "Session Timeout";
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

        //public ActionResult Samplesheet()
        //{
        //    try
        //    {
        //        string path = "~/samples/sample_sheet.xlsx";
        //        return File(path, "application/vnd.ms-excel", "Diagnostics_details.xlsx");
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Warn(e);
        //        return RedirectToAction("Error_Page", "Main");
        //    }
        //}


        [HttpPost]
        public async Task<ActionResult> Save_Diagnostics(Models.Diagnostics_settings A)
        {

            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    A.QueryType = "Insert";
                    A.CompanyCode = Session["CompanyCode"].ToString();
                    A.PlantCode = Session["PlantCode"].ToString();
                    A.LineCode = Session["LineCode"].ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();

                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        var user1 = Session["Token"].ToString() + ':' + Session["UserName"];
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                        var responseMessage = await client.GetAsync("api/UserSettings/GetEmployee");
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Diagnostics_settings>("api/Diagnostics/Add_Update_delete_Diagnostic", A).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                            }
                            return RedirectToAction("DiagnosticsSetting", "Diagnostics");
                        }
                        else
                        {
                            TempData["message"] = "Session Timeout";
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
        public async Task<ActionResult> Update_Diagnostics(Models.Diagnostics_settings A)
        {
            if (Session["CompanyCode"] != null && Session["PlantCode"] != null && Session["LineCode"] != null)
            {
                try
                {
                    A.QueryType = "Update";
                    A.CompanyCode = Session["CompanyCode"].ToString();
                    A.PlantCode = Session["PlantCode"].ToString();
                    A.LineCode = Session["LineCode"].ToString();

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
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user1.ToString());
                            HttpResponseMessage response = client.PostAsJsonAsync<Models.Diagnostics_settings>("api/Diagnostics/Add_Update_delete_Diagnostic", A).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var res = response.Content.ReadAsStringAsync().Result;
                                var msg = JsonConvert.DeserializeObject(res);
                                TempData["message"] = msg;
                            }
                            return RedirectToAction("DiagnosticsSetting", "Diagnostics");
                        }
                        else
                        {
                            TempData["message"] = "Session Timeout";
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

        
        public ActionResult DiagnosticsHistory()
        {
            if (this.HasPermission("DiagnosticHistoric-View"))
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
                return RedirectToAction("Settings_err", "Main");
            }

        }

        public ActionResult DiagnosticsLive()
        {
            if (this.HasPermission("DiagnosticLive-View"))
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
                return RedirectToAction("Settings_err", "Main");
            }
        }
    }
}