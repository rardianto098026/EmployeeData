using EmployeeData.Models;
using EmployeeData.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace EmployeeData.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult IndexUser()
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "HomeController";

            ViewBag.menu = Session["menu"];

            DataTable dt = Common.ExecuteQuery("dbo.[sp_GetDATA_Employee_Detail] '" + Session["EmployeeNumber"].ToString() + "', '" + Session["UserID"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {

                //TempData["SuccessRequest"] = "Submit Request successfully.";
                TempData["messageRequest"] = "<script>alert('Your data has not been registered by HRD, Please Contact Your HR Services');</script>";

            }


            return View();

        }
      
        public DataSet Get_Menu()

        {

            SqlCommand com = new SqlCommand("exec [sp_Get_Menu_Parent] '" + Session["EmployeeNumber"] + "'", Common.GetConnection());

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);


            return ds;

        }

        public DataSet Get_SubMenu(string ParentID)

        {

            SqlCommand com = new SqlCommand("exec [sp_Get_SubMenu] '" + Session["EmployeeNumber"] + "',@ParentID", Common.GetConnection());

            com.Parameters.AddWithValue("@ParentID", ParentID);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }

        public void get_Submenu(string catid)

        {

            DataSet ds = Get_SubMenu(catid);

            List<EmployeeData.Models.MenuViewModels.SubMenu> submenulist = new List<EmployeeData.Models.MenuViewModels.SubMenu>();

            foreach (DataRow dr in ds.Tables[0].Rows)

            {

                submenulist.Add(new EmployeeData.Models.MenuViewModels.SubMenu
                {

                    MenuID = dr["MenuID"].ToString(),

                    MenuName = dr["MenuName"].ToString(),

                    ActionName = dr["ActionName"].ToString(),

                    ControllerName = dr["ControllerName"].ToString()

                });

            }

            Session["submenu"] = submenulist;

        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Login");
        }
       

    }
}