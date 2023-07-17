using System;
using System.Collections.Generic;
using System.Web.Mvc;
using static PlanDigitization_web.FilterConfig;

namespace PlanDigitization_web.Controllers
{
    [SessionTimeout]
    public class OperatorEfficiencyController : Controller
    {
        // GET: OperatorEfficiency Historic
        public ActionResult OperatorEfficiencyHistoricDashboard()
        {
            if (this.HasPermission("OperatorEfficiencyHistoric-View"))
            {
                //MachineListDisplay();
                //LineListDisplay();
                //VariantListDisplay();
                //OperatorListDisplay();
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
        //private void MachineListDisplay()
        //{
        //    try
        //    {
        //        var AvlModel = new Models.Avl_Input();
        //        AvlModel.MachineList = new List<string>();
        //        AvlModel.MachineList.Add("M1");
        //        AvlModel.MachineList.Add("M2");
        //        AvlModel.MachineList.Add("M3");
        //        TempData["MachineList"] = AvlModel.MachineList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //private void LineListDisplay()
        //{
        //    try
        //    {
        //        var AvlModel = new Models.Avl_Input();
        //        AvlModel.LineList = new List<string>();
        //        AvlModel.LineList.Add("VFOE");
        //        AvlModel.LineList.Add("L2");
        //        AvlModel.LineList.Add("L3");
        //        TempData["LineList"] = AvlModel.LineList;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void VariantListDisplay()
        //{
        //    try
        //    {
        //        var AvlModel = new Models.Avl_Input();
        //        AvlModel.VariantList = new List<string>();
        //        AvlModel.VariantList.Add("V1");
        //        AvlModel.VariantList.Add("V2");
        //        AvlModel.VariantList.Add("V3");
        //        TempData["VariantList"] = AvlModel.VariantList;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void OperatorListDisplay()
        //{
        //    try
        //    {
        //        var AvlModel = new Models.Avl_Input();
        //        AvlModel.OperatorList = new List<string>();
        //        AvlModel.OperatorList.Add("OP1");
        //        AvlModel.OperatorList.Add("OP2");
        //        AvlModel.OperatorList.Add("OP3");
        //        TempData["OperatorList"] = AvlModel.OperatorList;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //OperatorEfficiency Live

        public ActionResult OperatorEfficiencyd3()
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
        public ActionResult OperatorEfficiencyLive()
        {
            if (this.HasPermission("OperatorEfficiencyLive-View"))
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
    }
}