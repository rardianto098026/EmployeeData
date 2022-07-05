﻿using EmployeeData.Models;
using EmployeeData.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.OleDb;
using static EmployeeData.Models.MenuViewModels;


namespace EmployeeData.Controllers
{
    public class UserMatrixController : Controller
    {
        // GET: UserMatrixModels
        public ActionResult Pending(UserMatrixModels model, string submit)
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (submit == "Search" || string.IsNullOrEmpty(submit) == true)
            {

                ViewBag.menu = Session["menu"];
                Session["controller"] = "UserMatrixController";
                model.NIK = string.IsNullOrEmpty(model.NIK) == true ? "" : model.NIK;
                model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
                model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
                model.Role = string.IsNullOrEmpty(model.Role) == true ? "" : model.Role;

            }
            else if (submit == "Upload")
            {
                return RedirectToAction("Upload", "UserMatrixModels");
            }

            var modelEmployee = GetEmployeeData(model.NIK, model.Name, model.Entity, model.Role);
            return View(modelEmployee);
        }
        [HttpGet]
        public ActionResult Edit(string id, UserMatrixModels model, string Role)
        {


            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            model.NIK = string.IsNullOrEmpty(model.NIK) == true ? "" : model.NIK;
            model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
            model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;

            if (model.NIK == "")
            {
                model.NIK = Session["NIK"].ToString();
                model.Name = Session["Name"].ToString();
                model.Entity = Session["Enitity"].ToString();
            }
            else
            {
                Session["NIK"] = model.NIK;
                Session["Name"] = model.Name;
                Session["Enitity"] = model.Entity;
            }



            #region Role
            if (Role == "1")
            {
                TempData["RoleSA"] = "selected = \"selected\"";
                TempData["RoleA"] = "";
            }
            else if (Role == "2")
            {
                TempData["RoleSA"] = "";
                TempData["RoleA"] = "selected = \"selected\"";
            }

            #endregion

            List<SelectListItem> listMenuName = new List<SelectListItem>();
            listMenuName.Add(new SelectListItem { });
            DataTable dt = Common.ExecuteQuery("select * from MST_MENU where IsParent=1 order by IDMenu");
            foreach (DataRow dr in dt.Rows)
            {
                listMenuName.Add(new SelectListItem { Text = dr["MenuName"].ToString(), Value = dr["IDMenu"].ToString() });
            }
            model.MenuList = new SelectList(listMenuName, "Value", "Text");

            List<SelectListItem> listChildMenu = new List<SelectListItem>();
            listChildMenu.Add(new SelectListItem { });
            model.ChildMenuList = new SelectList(listChildMenu, "Value", "Text");

            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            ViewBag.menu = Session["menu"];

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserMatrixModels model, string submit, string Role)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (submit == "Back")
            {
                return RedirectToAction("Pending", "UserMatrix");
            }
            else if (submit == "Save")
            {
                #region Role
                if (Role == "1")
                {
                    TempData["RoleSA"] = "selected = \"selected\"";
                    TempData["RoleA"] = "";
                }
                else if (Role == "2")
                {
                    TempData["RoleSA"] = "";
                    TempData["RoleA"] = "selected = \"selected\"";
                }

                #endregion
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Insert_Previlege", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RoleID", model.Role);
                        cmd.Parameters.AddWithValue("@MenuID", model.MenuName);
                        cmd.Parameters.AddWithValue("@NIK", Session["NIK"].ToString());
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_Insert_Previlege", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RoleID", model.Role);
                        cmd.Parameters.AddWithValue("@MenuID", model.ChildMenu);
                        cmd.Parameters.AddWithValue("@NIK", Session["NIK"].ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            List<SelectListItem> listMenuName = new List<SelectListItem>();
            listMenuName.Add(new SelectListItem { });
            DataTable dt = Common.ExecuteQuery("select * from MST_MENU where IsParent=1 order by IDMenu");
            foreach (DataRow dr in dt.Rows)
            {
                listMenuName.Add(new SelectListItem { Text = dr["MenuName"].ToString(), Value = dr["IDMenu"].ToString() });
            }
            model.MenuList = new SelectList(listMenuName, "Value", "Text");

            List<SelectListItem> listChildMenu = new List<SelectListItem>();
            listChildMenu.Add(new SelectListItem { });
            DataTable dtChild = Common.ExecuteQuery("select * from MST_MENU where ParentID='" + model.MenuName + "' order by IDMenu");
            foreach (DataRow dr in dtChild.Rows)
            {
                listChildMenu.Add(new SelectListItem { Text = dr["MenuName"].ToString(), Value = dr["IDMenu"].ToString() });
            }
            model.ChildMenuList = new SelectList(listChildMenu, "Value", "Text");

            //ViewBag.menu = Session["menu"];
            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            ViewBag.menu = Session["menu"];

            model.NIK = Session["NIK"].ToString();
            model.Name = Session["Name"].ToString();
            model.Entity = Session["Enitity"].ToString();
            TempData["success"] = "Save Data Successfully";
            return View(model);

        }
        public ActionResult DeleteUser(string id)
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;


            try
            {
                Common.ExecuteNonQuery("Delete From MST_Employee where [EmployeeID]='" + id + "'");
                TempData["message"] = "<script>alert('Delete succesfully');</script>";
                return RedirectToAction("Pending", "UserMatrix");
            }
            catch (Exception)
            {
                TempData["message"] = "<script>alert('Delete unsuccesfully');</script>";
                return RedirectToAction("Pending", "UserMatrix");
            }
        }
        [HttpGet]
        public ActionResult Upload()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.menu = Session["menu"];

            return View();

        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string submit)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (submit == "Back")
            {
                return RedirectToAction("Pending", "UserMatrix");
            }
            else if (submit == "Upload")
            {
                Helper helper = new Helper();
                try
                {
                    string filePath = string.Empty;
                    if (file != null)
                    {
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        filePath = path + Path.GetFileName(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        file.SaveAs(filePath);

                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                break;
                            case ".xlsx": //Excel 07 and above.
                                conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                break;
                        }

                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath);

                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();

                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();
                                }
                            }
                        }


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                            {
                                using (SqlCommand cmd = new SqlCommand("sp_Insert_Employee", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@PersonNumber", dt.Rows[i]["Person Number"].ToString());
                                    cmd.Parameters.AddWithValue("@FullName", dt.Rows[i]["Full Name"].ToString());
                                    cmd.Parameters.AddWithValue("@PrimaryEMail", dt.Rows[i]["Primary E-Mail"].ToString());
                                    cmd.Parameters.AddWithValue("@Enitity", dt.Rows[i]["Entity"].ToString());
                                    cmd.Parameters.AddWithValue("@Entities", dt.Rows[i]["Entities"].ToString());
                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                        }

                        if (TempData["Error"] != null)
                        {
                            string message = "";
                            TempData["Error"] = message;
                            TempData["Success"] = "";
                        }
                        else
                        {
                            string message = "File uploaded succesfully.";
                            TempData["Success"] = message;
                            TempData["Error"] = "";
                        }

                    }
                    else
                    {
                        string message = "File upload can't be empty.";
                        TempData["Error"] = message;
                        TempData["Success"] = "";
                    }

                    ViewBag.menu = Session["menu"];
                    return View();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    TempData["Success"] = "";
                    ViewBag.menu = Session["menu"];
                    return View();
                }
            }

            ViewBag.menu = Session["menu"];

            return View();

        }
        public DataSet Get_SubMenu(string ParentID)

        {

            SqlCommand com = new SqlCommand("exec [sp_Get_SubMenu] '" + Session["EmployeeNumber"].ToString() + "',@ParentID", Common.GetConnection());

            com.Parameters.AddWithValue("@ParentID", ParentID);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }

        public void get_Submenu(string catid)

        {

            DataSet ds = Get_SubMenu(catid);

            List<SubMenu> submenulist = new List<SubMenu>();

            foreach (DataRow dr in ds.Tables[0].Rows)

            {

                submenulist.Add(new SubMenu
                {

                    MenuID = dr["MenuID"].ToString(),

                    MenuName = dr["MenuName"].ToString(),

                    ActionName = dr["ActionName"].ToString(),

                    ControllerName = dr["ControllerName"].ToString()

                });

            }

            Session["submenu"] = submenulist;

        }

        public static List<UserMatrixModels> GetEmployeeData(string ID, string Name, string entity, string role)
        {
            SqlConnection conn = Common.GetConnection();
            List<EmployeeData.Models.UserMatrixModels> model = new List<UserMatrixModels>();
            SqlCommand cmd = new SqlCommand("sp_Get_Employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@Entity", SqlDbType.VarChar).Value = entity;
            cmd.Parameters.Add("@Role", SqlDbType.VarChar).Value = role;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            conn.Open();
            da.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new UserMatrixModels
                {
                    No = dr["No"].ToString(),
                    NIK = dr["NIK"].ToString(),
                    Name = dr["Name"].ToString(),
                    Email = dr["Email"].ToString(),
                    Entity = dr["Entity"].ToString(),
                    Role = dr["Role"].ToString()
                });
            }

            return model;
        }

        public JsonResult GetChildMenu(string MenuName)
        {
            DataTable dt = Common.ExecuteQuery("select * from MST_MENU where IsParent=0 and ParentID='" + MenuName + "'");
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SelectListItem { Text = dr["MenuName"].ToString(), Value = dr["IDMenu"].ToString() });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailMenu()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<DetailMenuModel> model = new List<DetailMenuModel>();
            string nik = Session["NIK"].ToString();
            DataTable dt = Common.ExecuteQuery("dbo.sp_Get_List_Menu '" + nik + "'");
            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new DetailMenuModel
                {
                    Entity = dr["ShortEntity"].ToString(),
                    ID_Employee = dr["EmployeeID"].ToString(),
                    Menu = dr["MenuName"].ToString(),
                    Name = dr["EmployeeName"].ToString(),
                    No = dr["NO"].ToString(),
                    Role = dr["ROLE"].ToString()
                });
            }
            return PartialView("DetailMenu", model);
        }

        public ActionResult DeleteMenu(string menu, UserMatrixModels model)
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;
            int NIK = Convert.ToInt32(Session["NIK"].ToString());

            try
            {
                Common.ExecuteNonQuery("dbo.sp_Delete_Menu '" + NIK + "','" + menu + "'");
                TempData["messageDelete"] = "<script>alert('Delete succesfully');</script>";
                model.NIK = Session["NIK"].ToString();
                model.Name = Session["Name"].ToString();
                model.Entity = Session["Enitity"].ToString();

                //return RedirectToAction(url);
                //return View(model);
                return RedirectToAction("Edit", "UserMatrix");

            }
            catch (Exception)
            {
                TempData["messageDelete"] = "<script>alert('Delete unsuccesfully');</script>";
                return RedirectToAction("Edit", "UserMatrix");
            }



        }
        public DataSet Get_Menu()

        {

            SqlCommand com = new SqlCommand("exec [sp_Get_Menu_Parent] '" + Session["EmployeeNumber"] + "'", Common.GetConnection());

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);


            return ds;

        }
    }
}