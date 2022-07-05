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
using static EmployeeData.Models.MenuViewModels;

namespace EmployeeData.Controllers
{
    public class ListMasterFaskesController : Controller
    {
        // GET: ListMasterFaskes
        public ActionResult ListMasterFaskes(ListMasterFaskesModels model, string submit)
        {

            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.menu = Session["menu"];
            //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");

            Session["controller"] = "ListMasterFaskesController";

            model.Faskes_GIGI = DDLMasterFaskesList();
            model.Provinsi = DDLSTATE_FASKES();
            model.KotaMadya = DDLKOTAMADYA_FASKES_ALL();
            model.Kecamatan = DDLKECAMATAN_FASKES_ALL();
            model.NamaFaskes = DDL_FASKES_TK1();

            model.Faskes_GIGI_ID = string.IsNullOrEmpty(model.Faskes_GIGI_ID) == true ? "" : model.Faskes_GIGI_ID;
            model.Provinsi_ID = string.IsNullOrEmpty(model.Provinsi_ID) == true ? "" : model.Provinsi_ID;
            model.KotaMadya_ID = string.IsNullOrEmpty(model.KotaMadya_ID) == true ? "" : model.KotaMadya_ID;
            model.Kecamatan_ID = string.IsNullOrEmpty(model.Kecamatan_ID) == true ? "" : model.Kecamatan_ID;
            model.NamaFaskes_ID = string.IsNullOrEmpty(model.NamaFaskes_ID) == true ? "" : model.NamaFaskes_ID;
            model.GetFaskes = GetFaskesList(model.Faskes_GIGI_ID,model.Provinsi_ID,model.KotaMadya_ID,model.Kecamatan_ID,model.NamaFaskes_ID);


            model.KotaMadya = DDLKOTAMADYA_FASKES_BY_FASKES(model.Faskes_GIGI_ID, model.Provinsi_ID);
            model.Kecamatan = DDLKECAMATAN_FASKES_BY_KOTAMADYA(model.Faskes_GIGI_ID,model.Provinsi_ID,model.KotaMadya_ID);
            model.NamaFaskes = DDL_FASKES_BY_KECAMATAN_TK1(model.Faskes_GIGI_ID, model.Provinsi_ID, model.KotaMadya_ID,model.Kecamatan_ID);

            //if (submit == "Upload")
            //{
            //   return RedirectToAction("Upload", "ListView");
            //}
            //if (submit == "Search" || string.IsNullOrEmpty(submit) == true)
            //{
            //    if (model.Faskes_GIGI_ID == "")
            //    {

            //    }
            //}

            return View(model);
        }
        [HttpGet]
        public ActionResult EditFaskes(ListMasterFaskesModels model,string idx, string Faskes,string Prov,string Kota, string Kec, string TypeFaskes)
        {


            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.menu = Session["menu"];
            //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");

            Session["controller"] = "ListMasterFaskesController";

            string Nama = null;
            string ID = null;
            string EmplID = null;
            string Names = null;

            if (Session["Role"].ToString().ToUpper() == "ADMIN" || Session["Role"].ToString().ToUpper() == "SUPER ADMIN")
            {
                ID = string.IsNullOrEmpty(EmplID) == true ? Session["EmployeeNumber"].ToString() : EmplID;
                Nama = string.IsNullOrEmpty(Names) == true ? Session["UserID"].ToString() : Names;
            }
            else
            {
                ID = Session["EmployeeNumber"].ToString();
                Nama = Session["UserID"].ToString();
            }

            model.idx = idx;
            model.Faskes_GIGI = DDLMasterFaskesList();
            model.Provinsi = DDLSTATE_FASKES();
            model.KotaMadya = DDLKOTAMADYA_FASKES_ALL();
            model.Kecamatan = DDLKECAMATAN_FASKES_ALL();
            model.NamaFaskes = DDL_FASKES_TK1();

            model.Faskes_GIGI_ID = TypeFaskes.ToString();
            model.Provinsi_ID = Prov.ToString();

            model.KotaMadya = DDLKOTAMADYA_FASKES_BY_FASKES(model.Faskes_GIGI_ID, model.Provinsi_ID);
            model.KotaMadya_ID = Kota.ToString();

            model.Kecamatan = DDLKECAMATAN_FASKES_BY_KOTAMADYA(model.Faskes_GIGI_ID, model.Provinsi_ID, model.KotaMadya_ID);
            model.Kecamatan_ID = Kec.ToString();

            model.NamaFaskes = DDL_FASKES_BY_KECAMATAN_TK1(model.Faskes_GIGI_ID, model.Provinsi_ID, model.KotaMadya_ID, model.Kecamatan_ID);
            model.NamaFaskes_ID = Faskes.ToString();

            return View(model);
        }
        [HttpPost]
        public ActionResult EditFaskes(ListMasterFaskesModels model,string submit)
        {
            if (submit == "Upload")
            {
                return RedirectToAction("Upload", "ListView");
            }

            string url = Request.Url.OriginalString;
            Session["url"] = url;
            model.LINK = url;

            try
            { 
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                ViewBag.menu = Session["menu"];
                //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
                Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");

                Session["controller"] = "ListMasterFaskesController";

                string Nama = null;
                string ID = null;
                string EmplID = null;
                string Names = null;

                if (Session["Role"].ToString().ToUpper() == "ADMIN" || Session["Role"].ToString().ToUpper() == "SUPER ADMIN")
                {
                    ID = string.IsNullOrEmpty(EmplID) == true ? Session["EmployeeNumber"].ToString() : EmplID;
                    Nama = string.IsNullOrEmpty(Names) == true ? Session["UserID"].ToString() : Names;
                }
                else
                {
                    ID = Session["EmployeeNumber"].ToString();
                    Nama = Session["UserID"].ToString();
                }

                if (submit == "Submit")
                {
                    if (model.Faskes_GIGI_ID == "Faskes TK 1")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_UPDATE_FASKES_TK", conn))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Provinsi", model.Provinsi_ID);
                                cmd.Parameters.AddWithValue("@KOTAMADYA", model.KotaMadya_ID);
                                cmd.Parameters.AddWithValue("@KECAMATAN", model.Kecamatan_ID);
                                cmd.Parameters.AddWithValue("@NAMA_FASKES", model.NamaFaskes_ID);
                                cmd.Parameters.AddWithValue("@IDX", model.idx);

                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    else
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_UPDATE_FASKES_GIGI", conn))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Provinsi", model.Provinsi_ID);
                                cmd.Parameters.AddWithValue("@KOTAMADYA", model.KotaMadya_ID);
                                cmd.Parameters.AddWithValue("@KECAMATAN", model.Kecamatan_ID);
                                cmd.Parameters.AddWithValue("@NAMA_FASKES", model.NamaFaskes_ID);
                                cmd.Parameters.AddWithValue("@IDX", model.idx);

                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }


                TempData["messageRequest"] = "<script>alert('Save Data Success.');</script>";
                    return Redirect(model.LINK + "?idx="+ model.idx +"&Faskes=" + model.NamaFaskes_ID + "&Prov=" + model.Provinsi_ID + "&Kota=" + model.KotaMadya_ID + "&Kec=" + model.Kecamatan_ID + "&TypeFaskes="+model.Faskes_GIGI_ID+"");

            }
            catch (Exception ex)
            {
                TempData["ErrorRequest"] = ex.ToString();
                return View(model);
            }
        }
        private static List<ListMasterFaskesModels> GetFaskesList(string Faskes_GIGI,string Provinsi,string Kotamadya, string Kecamatan, string NamaFaskes)
        {
            List<ListMasterFaskesModels> model = new List<ListMasterFaskesModels>();
            SqlConnection connX = Common.GetConnection();
            SqlCommand cmdX = new SqlCommand("sp_GET_FASKES_LIST", connX);
            cmdX.CommandType = CommandType.StoredProcedure;
            cmdX.Parameters.Add("@FASKES", SqlDbType.VarChar).Value = Faskes_GIGI;
            cmdX.Parameters.Add("@Provinsi", SqlDbType.VarChar).Value = Provinsi;
            cmdX.Parameters.Add("@KOTAMADYA", SqlDbType.VarChar).Value = Kotamadya;
            cmdX.Parameters.Add("@KECAMATAN", SqlDbType.VarChar).Value = Kecamatan;
            cmdX.Parameters.Add("@NAMA_FASKES", SqlDbType.VarChar).Value = NamaFaskes;
            
            SqlDataAdapter da = new SqlDataAdapter(cmdX);
            DataTable dtX = new DataTable();
            connX.Open();
            da.Fill(dtX);
            connX.Close();

            foreach (DataRow dr in dtX.Rows)
            {
                model.Add(
                    new ListMasterFaskesModels
                    {
                        idx =  dr["IDX"].ToString(),
                        No = dr["No"].ToString(),
                        Provinsi_ID = dr["Provinsi"].ToString(),
                        KotaMadya_ID = dr["KOTAMADYA"].ToString(),
                        Kecamatan_ID = dr["KECAMATAN"].ToString(),
                        NamaFaskes_ID = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString()
                    }
                    );
            }
            return (model);
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
        private static List<SelectListItem> DDLSTATE()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_Provinsi";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Provinsi"].ToString(),
                            Value = dr["Provinsi"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLSTATE_FASKES()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_Provinsi_FASKES";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Provinsi"].ToString(),
                            Value = dr["Provinsi"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLSTATE_GIGI()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_Provinsi_GIGI";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Provinsi"].ToString(),
                            Value = dr["Provinsi"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        
        private static List<SelectListItem> DDL_FASKES_TK1()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_FASKES";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString(),
                            Value = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_FASKES_BY_KECAMATAN_TK1(string FAKSES, string Provinsi, string Kotamadya,string Kecamatan)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_FASKES_BY_KECAMATAN_LIST'" + FAKSES + "', '" + Provinsi + "','" + Kotamadya + "','" + Kecamatan + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString(),
                            Value = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }


        private static List<SelectListItem> DDL_FASKES_GIGI()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_FASKES_GIGI";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString(),
                            Value = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        public static List<SelectListItem> DDLMasterFaskesList()
        {
            var list = new List<SelectListItem>
                {
                    new SelectListItem {Text="Faskes TK 1", Value="Faskes TK 1"},
                    new SelectListItem {Text="Faskes dr Gigi",Value="Faskes dr Gigi" },
                };
            return list;
        }
        public JsonResult GetListMasterFaskes(string id)
        {
            ListMasterFaskesModels model = new ListMasterFaskesModels();
            model.Provinsi = DDLProvMasterFaskes(id);
            return Json(new SelectList(model.Provinsi, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLProvMasterFaskes(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "";
            if (id == "Faskes TK 1")
            {

                query = "SELECT DISTINCT Provinsi FROM [MST_Faskes_TK1] ORDER BY Provinsi";
            }
            else
            {

                query = "SELECT DISTINCT Provinsi FROM [MST_Faskes_Gigi] ORDER BY Provinsi";
            }

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Provinsi"].ToString(),
                            Value = dr["Provinsi"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }


        public JsonResult getKotaMadyaList(string id,string idx)
        {
            ListMasterFaskesModels model = new ListMasterFaskesModels();
            model.KotaMadya = DDLKotamadyaByProv(id,idx);
            return Json(new SelectList(model.KotaMadya, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKotamadyaByProv(string id, string idx)
        {
            string query = "";
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            if (idx == "Faskes TK 1")
            {

                query = "SELECT DISTINCT KOTAMADYA FROM [MST_Faskes_TK1] WHERE Provinsi = '" + id + "' ORDER BY KOTAMADYA";
            }
            else
            {

                query = "SELECT DISTINCT KOTAMADYA FROM [MST_Faskes_Gigi] WHERE Provinsi = '" + id + "' ORDER BY KOTAMADYA";
            }

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        public JsonResult getKotaMadyaList_FASKES(string id)
        {
            ListMasterFaskesModels model = new ListMasterFaskesModels();
            model.KotaMadya = DDLKotamadyaByProv_FASKES(id);
            return Json(new SelectList(model.KotaMadya, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKotamadyaByProv_FASKES(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KOTAMADYA FROM [MST_Faskes_TK1] WHERE Provinsi = '" + id + "' ORDER BY KOTAMADYA";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        public JsonResult getKotaMadyaList_GIGI(string id)
        {
            ListMasterFaskesModels model = new ListMasterFaskesModels();
            model.KotaMadya = DDLKotamadyaByProv_GIGI(id);
            return Json(new SelectList(model.KotaMadya, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKotamadyaByProv_GIGI(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KOTAMADYA FROM [MST_Faskes_Gigi] WHERE Provinsi = '" + id + "' ORDER BY KOTAMADYA";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }


        public JsonResult getKecamatanList(string id, string idx, string idx2)
        {
            ListMasterFaskesModels model = new ListMasterFaskesModels();
            model.Kecamatan = DDLKecamatanByKotamadya(id, idx,idx2);
            return Json(new SelectList(model.Kecamatan, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKecamatanByKotamadya(string id, string idx, string idx2)
        {
            string query = "";
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();

            if (idx2 == "Faskes TK 1")
            {

                query = "SELECT DISTINCT KECAMATAN FROM [MST_Faskes_TK1] WHERE KOTAMADYA = '" + id + "' and Provinsi = '" + idx + "' ORDER BY KECAMATAN";
            }
            else
            {

                query = "SELECT DISTINCT KECAMATAN FROM [MST_Faskes_Gigi] WHERE KOTAMADYA = '" + id + "'  and Provinsi = '" + idx + "' ORDER BY KECAMATAN";
            }

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        public JsonResult getKecamatanList_FASKES(string id, string idx)
        {
            ListMasterFaskesModels model = new ListMasterFaskesModels();
            model.Kecamatan = DDLKecamatanByKotamadya_FASKES(id, idx);
            return Json(new SelectList(model.Kecamatan, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKecamatanByKotamadya_FASKES(string id, string idx)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KECAMATAN FROM [MST_Faskes_TK1] WHERE KOTAMADYA = '" + id + "' and Provinsi = '" + idx + "' ORDER BY KECAMATAN";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        public JsonResult getKecamatanList_GIGI(string id, string idx)
        {
            ListMasterFaskesModels model = new ListMasterFaskesModels();
            model.Kecamatan = DDLKecamatanByKotamadya_GIGI(id, idx);
            return Json(new SelectList(model.Kecamatan, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKecamatanByKotamadya_GIGI(string id, string idx)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KECAMATAN FROM [MST_Faskes_Gigi] WHERE KOTAMADYA = '" + id + "' and Provinsi = '" + idx + "' ORDER BY KECAMATAN";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        public JsonResult getKelurahanList(string id, string idx, string idx2,string idx3)
        {
            ListMasterFaskesModels model = new ListMasterFaskesModels();
            model.NamaFaskes = DDLKelurahanByKecamatan(id, idx, idx2,idx3);
            return Json(new SelectList(model.NamaFaskes, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKelurahanByKecamatan(string id, string idx, string idx2,string idx3)
        {
            string query = "";
               SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();

            if (idx3 == "Faskes TK 1")
            {

                query = "SELECT DISTINCT NAMA_FASKES_TINGKAT_PERTAMA FROM [MST_Faskes_TK1] WHERE Provinsi = '" + idx + "' AND KOTAMADYA = '" + idx2 + "' AND KECAMATAN = '" + id + "'  ORDER BY NAMA_FASKES_TINGKAT_PERTAMA";
            }
            else
            {

                query = "SELECT DISTINCT NAMA_FASKES_TINGKAT_PERTAMA FROM [MST_Faskes_Gigi] WHERE Provinsi = '" + idx + "' AND KOTAMADYA = '" + idx2 + "' AND KECAMATAN = '" + id + "'  ORDER BY NAMA_FASKES_TINGKAT_PERTAMA";
            }
            
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KELURAHAN"].ToString(),
                            Value = dr["KELURAHAN"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        

        private static List<SelectListItem> DDLKOTAMADYA_ALL()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_KOTAMADYA";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKOTAMADYA_FASKES_ALL()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_KOTAMADYA_FASKES";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKOTAMADYA_FASKES_BY_FASKES(string Faskes,string Provinsi)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_KOTAMADYA_FASKES_BY_TYPE_FASKES '" + Faskes + "', '" + Provinsi + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Provinsi"].ToString(),
                            Value = dr["Provinsi"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKOTAMADYA_GIGI_ALL()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_KOTAMADYA_GIGI";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKOTAMADYABYSTATE(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_KOTAMADYA '" + id + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKOTAMADYABYSTATE_FASKES(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_KOTAMADYA_FASKES '" + id + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDLKOTAMADYABYSTATE_GIGI(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_KOTAMADYA_GIGI '" + id + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKECAMATAN(string Kotamadya, string State)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_KECAMATAN '" + Kotamadya + "', '" + State + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDLKECAMATAN_FASKES(string Kotamadya, string State)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_KECAMATAN_FASKES '" + Kotamadya + "', '" + State + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKECAMATAN_GIGI(string Kotamadya, string State)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_KECAMATAN_GIGI '" + Kotamadya + "', '" + State + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKECAMATAN_ALL()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_KECAMATAN";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKECAMATAN_FASKES_ALL()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_KECAMATAN_FASKES";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        public JsonResult getFASKES_LIST(string id, string idx, string idx2,string idx3)
        {
            ListMasterFaskesModels model = new ListMasterFaskesModels();
            model.NamaFaskes = DDLFASKESBYKECAMATAN(id, idx, idx2,idx3);
            return Json(new SelectList(model.NamaFaskes, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLFASKESBYKECAMATAN(string id, string idx, string idx2,string idx3)
        {
            string query = "";
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            if (idx3 == "Faskes TK 1")
            { 
                query = "SELECT DISTINCT NAMA_FASKES_TINGKAT_PERTAMA  FROM [MST_Faskes_TK1] WHERE Provinsi = '" + idx + "' AND KOTAMADYA = '" + idx2 + "' AND KECAMATAN = '" + id + "' ORDER BY NAMA_FASKES_TINGKAT_PERTAMA";
            }
            else
            {
                query = "SELECT DISTINCT NAMA_FASKES_TINGKAT_PERTAMA  FROM [MST_Faskes_Gigi] WHERE Provinsi = '" + idx + "' AND KOTAMADYA = '" + idx2 + "' AND KECAMATAN = '" + id + "' ORDER BY NAMA_FASKES_TINGKAT_PERTAMA";
            }

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString(),
                            Value = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKECAMATAN_FASKES_BY_KOTAMADYA(string FAKSES, string Provinsi, string Kotamadya)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_KECAMATAN_FASKES_BY_KOTAMDYA'" + FAKSES + "', '" + Provinsi + "','" + Kotamadya + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = " - Please select - ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKECAMATAN_GIGI_ALL()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_KECAMATAN_GIGI";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KECAMATAN"].ToString(),
                            Value = dr["KECAMATAN"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKELURAHAN(string Kecamatan, string Provinsi, string Kotamadya)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_KELURAHAN '" + Kecamatan + "', '" + Provinsi + "','" + Kotamadya + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KELURAHAN"].ToString(),
                            Value = dr["KELURAHAN"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLFASKES(string Kecamatan, string Provinsi, string Kotamadya)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_FASKES_BY_KECAMATAN '" + Kecamatan + "', '" + Provinsi + "','" + Kotamadya + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString(),
                            Value = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLGIGI(string Kecamatan, string Provinsi, string Kotamadya)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_GIGI_BY_KECAMATAN '" + Kecamatan + "', '" + Provinsi + "','" + Kotamadya + "'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString(),
                            Value = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKELURAHAN_ALL()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_KELURAHAN";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KELURAHAN"].ToString(),
                            Value = dr["KELURAHAN"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        public ActionResult DeleteChild(string idx)
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;


            try
            {
                Common.ExecuteNonQuery("Delete From TRN_Child_Employee where [IDX]='" + idx + "'");
                TempData["message"] = "<script>alert('Delete succesfully');</script>";
                return RedirectToAction("AddEmployeeData");
            }
            catch (Exception)
            {
                TempData["message"] = "<script>alert('Delete unsuccesfully');</script>";
                return RedirectToAction("AddEmployeeData");
            }
        }
        public ActionResult DeleteFaskes(string Faskes,string Prov, string Kota, string Kec, string TypeFaskes)
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (TypeFaskes == "Faskes TK 1")
            {
                try
                {
                    Common.ExecuteNonQuery("Delete From MST_Faskes_TK1 where [NAMA_FASKES_TINGKAT_PERTAMA]='" + Faskes + "' AND Provinsi = '" + Prov + "' AND KOTAMADYA = '" + Kota + "' AND KECAMATAN = '" + Kec + "' ");
                    TempData["message"] = "<script>alert('Delete succesfully');</script>";
                    return RedirectToAction("ListMasterFaskes");
                }
                catch (Exception)
                {
                    TempData["message"] = "<script>alert('Delete unsuccesfully');</script>";
                    return RedirectToAction("ListMasterFaskes");
                }
            }
            else
            {
                try
                {
                    Common.ExecuteNonQuery("Delete From MST_Faskes_Gigi where [NAMA_FASKES_TINGKAT_PERTAMA]='" + Faskes + "' AND Provinsi = '" + Prov + "' AND KOTAMADYA = '" + Kota + "' AND KECAMATAN = '" + Kec + "' ");
                    TempData["message"] = "<script>alert('Delete succesfully');</script>";
                    return RedirectToAction("ListMasterFaskes");
                }
                catch (Exception)
                {
                    TempData["message"] = "<script>alert('Delete unsuccesfully');</script>";
                    return RedirectToAction("ListMasterFaskes");
                }
            }
        }


    }
}