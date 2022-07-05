using EmployeeData.Models;
using EmployeeData.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EmployeeData.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            return View(model);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string returnUrl, IndexModel model)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;
            String adPath = ConfigurationManager.AppSettings["LDAPPath"].ToString();
            String domain = ConfigurationManager.AppSettings["LDAPDomain"].ToString();
            LdapAuthentication adAuth = new LdapAuthentication(adPath);
            String LocalHostaddress = HttpContext.Request.UserHostAddress;
            String Ip_Local = LocalHostaddress.Replace(".", "").Replace("::", "").Trim();

                if (true == adAuth.IsAuthenticated(domain, model.Username, model.Password))
                {
                    Session["EmployeeNumber"] = adAuth.GetPropertyUser(domain, model.Username, model.Password);

                    DataTable dtCheck = Common.ExecuteQuery("sp_SEL_NAME_LOGIN'" + Session["EmployeeNumber"].ToString() + "'");
                    if (dtCheck.Rows.Count > 0)
                    {
                        Session["UserID"] = dtCheck.Rows[0]["Full Name"].ToString();
                        string nama = Session["UserID"].ToString();
                        string id = Session["EmployeeNumber"].ToString();
                        //Session["Department"] = dtCheck.Rows[0]["Department"].ToString();
                        Session["Email"] = dtCheck.Rows[0]["Email"].ToString();
                        Session["ShortEntity"] = dtCheck.Rows[0]["Entities"].ToString();
                        Session["LongEntity"] = dtCheck.Rows[0]["Entity"].ToString();
                        Session["NamaPhoto"] = model.Username;
                        //Session["Grade"] = Common.Decrypt(dtCheck.Rows[0]["Grade"].ToString());


                        DataTable dtlogin = Common.ExecuteQuery("sp_INSERT_LOGIN'" + model.Username + "', '" + Ip_Local + "','" + Session["EmployeeNumber"] + "'");
                        if (dtlogin.Rows.Count > 0 )
                    {
                        if (dtlogin.Rows[0][0].ToString().ToUpper() == "SUCCESS")
                        {
                            //string con = ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString;
                            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(con);
                            //Session["Database"] = builder.InitialCatalog.ToUpper().Substring(9, 3);
                            //string database = builder.InitialCatalog.ToUpper();
                            Session["Entity"] = dtlogin.Rows[0]["ENTITY"].ToString();
                            Session["Role"] = dtlogin.Rows[0]["ROLE"].ToString();
                            string role = Session["Role"].ToString();

                            return RedirectToAction("AddEmployeeData", "EmployeeData");


                        }
                    }
                }
                else
                {
                    //ViewBag.Message = "Username Not Listed in Database!";
                    TempData["Message"] = "<script>alert('Invalid Username or Password');</script>";
                    //ShowMessage("", model.Message);
                }
            }
            else
            {

                //ViewBag.Message = "Wrong Username or Password!";
                TempData["Message"] = "<script>alert('Invalid Username or Password');</script>";
            }

            return View(model);
        }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                TempData["Message"] = "<script>alert('Invalid Username or Password');</script>";
                return View(model);
            }
        }
    }
}

