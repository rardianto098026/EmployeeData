using EmployeeData.Models;
using EmployeeData.Repository;
using ExcelLibrary.SpreadSheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using static EmployeeData.Models.MenuViewModels;

namespace EmployeeData.Controllers
{
    public class ListViewController : Controller
    {

        // GET: ListView
        public ActionResult IndexUser()
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;
            Session["controller"] = "HomeController";
            ViewBag.menu = Session["menu"];

            return View();
        }
        public ActionResult ListView(ListViewModels model, string submit, string Status, string Entity)
        {

            string url = Request.Url.OriginalString;
            Session["url"] = url;

            Entity = Request.Form["Entity"];
            Status = Request.Form["Status"];
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Session["controller"] = "ListViewController";

            #region status
            if (Status == "")
            {
                TempData["AllStatus"] = "selected = \"selected\"";
                TempData["Registered"] = "";
                TempData["Submitted"] = "";
                TempData["Approved"] = "";
                TempData["Rejected"] = "";
            }
            else if (Status == "0")
            {
                TempData["AllStatus"] = "";
                TempData["Registered"] = "";
                TempData["Submitted"] = "selected = \"selected\"";
                TempData["Approved"] = "";
                TempData["Rejected"] = "";
            }
            else if (Status == "1")
            {
                TempData["AllStatus"] = "";
                TempData["Registered"] = "";
                TempData["Submitted"] = "";
                TempData["Approved"] = "selected = \"selected\"";
                TempData["Rejected"] = "";
            }
            else if (Status == "2")
            {
                TempData["AllStatus"] = "";
                TempData["Registered"] = "";
                TempData["Submitted"] = "";
                TempData["Approved"] = "";
                TempData["Rejected"] = "selected = \"selected\"";
            }
            else if (Status == "register")
            {
                TempData["AllStatus"] = "";
                TempData["Registered"] = "selected = \"selected\"";
                TempData["Submitted"] = "";
                TempData["Approved"] = "";
                TempData["Rejected"] = "";
            }
            #endregion

            #region entity
            if (Entity == "")
            {
                TempData["AllStatus"] = "selected = \"selected\"";
                TempData["AAMI"] = "";
                TempData["AGI"] = "";
                TempData["AFI"] = "";
                TempData["AMFS"] = "";
                TempData["ASI"] = "";
                TempData["MAGI"] = "";
                TempData["AXATECH"] = "";
            }
            else if (Entity == "AAMI")
            {
                TempData["AllStatus"] = "";
                TempData["AAMI"] = "selected = \"selected\"";
                TempData["AGI"] = "";
                TempData["AFI"] = "";
                TempData["AMFS"] = "";
                TempData["ASI"] = "";
                TempData["MAGI"] = "";
                TempData["AXATECH"] = "";
            }
            else if (Entity == "AGI")
            {
                TempData["AllStatus"] = "";
                TempData["AAMI"] = "";
                TempData["AGI"] = "selected = \"selected\"";
                TempData["AFI"] = "";
                TempData["AMFS"] = "";
                TempData["ASI"] = "";
                TempData["MAGI"] = "";
                TempData["AXATECH"] = "";
            }
            else if (Entity == "AFI")
            {
                TempData["AllStatus"] = "";
                TempData["AAMI"] = "";
                TempData["AGI"] = "";
                TempData["AFI"] = "selected = \"selected\"";
                TempData["AMFS"] = "";
                TempData["ASI"] = "";
                TempData["MAGI"] = "";
                TempData["AXATECH"] = "";
            }
            else if (Entity == "AMFS")
            {
                TempData["AllStatus"] = "";
                TempData["AAMI"] = "";
                TempData["AGI"] = "";
                TempData["AFI"] = "";
                TempData["AMFS"] = "selected = \"selected\"";
                TempData["ASI"] = "";
                TempData["MAGI"] = "";
                TempData["AXATECH"] = "";
            }
            else if (Entity == "ASI")
            {
                TempData["AllStatus"] = "";
                TempData["AAMI"] = "";
                TempData["AGI"] = "";
                TempData["AFI"] = "";
                TempData["AMFS"] = "";
                TempData["ASI"] = "selected = \"selected\"";
                TempData["MAGI"] = "";
                TempData["AXATECH"] = "";
            }
            else if (Entity == "MAGI")
            {
                TempData["AllStatus"] = "";
                TempData["AAMI"] = "";
                TempData["AGI"] = "";
                TempData["AFI"] = "";
                TempData["AMFS"] = "";
                TempData["ASI"] = "";
                TempData["MAGI"] = "selected = \"selected\"";
                TempData["AXATECH"] = "";
            }
            else if (Entity == "AXATECH")
            {
                TempData["AllStatus"] = "";
                TempData["AAMI"] = "";
                TempData["AGI"] = "";
                TempData["AFI"] = "";
                TempData["AMFS"] = "";
                TempData["ASI"] = "";
                TempData["MAGI"] = "";
                TempData["AXATECH"] = "selected = \"selected\"";
            }
            #endregion

            ViewBag.menu = Session["menu"];
            //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");

            model.NIK = string.IsNullOrEmpty(model.NIK) == true ? "" : model.NIK;
            model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
            model.Department = string.IsNullOrEmpty(model.Department) == true ? "" : model.Department;
            model.Email = string.IsNullOrEmpty(model.Email) == true ? "" : model.Email;
            model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
            model.EmployeeID = string.IsNullOrEmpty(model.EmployeeID) == true ? "" : model.EmployeeID;
            model.EmployeeName = string.IsNullOrEmpty(model.EmployeeName) == true ? "" : model.EmployeeName;
            Entity = string.IsNullOrEmpty(Entity) == true ? "" : Entity;
            Status = string.IsNullOrEmpty(Status) == true ? "" : Status;
           

            if (submit == "Search")
            {
                ViewBag.menu = Session["menu"];
                //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
                Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");

                model.NIK = string.IsNullOrEmpty(model.NIK) == true ? "" : model.NIK;
                model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
                model.Department = string.IsNullOrEmpty(model.Department) == true ? "" : model.Department;
                model.Email = string.IsNullOrEmpty(model.Email) == true ? "" : model.Email;
                model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
                model.EmployeeID = string.IsNullOrEmpty(model.EmployeeID) == true ? "" : model.EmployeeID;
                model.EmployeeName = string.IsNullOrEmpty(model.EmployeeName) == true ? "" : model.EmployeeName;
                
            }
            else if (submit == "DownloadBPJS")
            {
                ViewBag.menu = Session["menu"];
                //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
                Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");

                model.NIK = string.IsNullOrEmpty(model.NIK) == true ? "" : model.NIK;
                model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
                model.Department = string.IsNullOrEmpty(model.Department) == true ? "" : model.Department;
                model.Email = string.IsNullOrEmpty(model.Email) == true ? "" : model.Email;
                model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
                model.EmployeeID = string.IsNullOrEmpty(model.EmployeeID) == true ? "" : model.EmployeeID;
                model.EmployeeName = string.IsNullOrEmpty(model.EmployeeName) == true ? "" : model.EmployeeName;

                DataTable dt = Common.ExecuteQuery("dbo.[sp_GENERATE_REPORT_Rev] '" + model.EmployeeID + "', '" + model.EmployeeName + "', '" + model.Entity + "', '"+ model.FlagID +"' ");

                if (dt.Rows.Count > 0)
                {
                    var grid = new GridView();
                    grid.DataSource = dt;
                    grid.DataBind();

                    Response.ClearContent();
                    Response.Buffer = true;
                    string filename = "UPLOAD BPJS REPORT";
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
                    Response.ContentType = "application/ms-excel";

                    Response.Charset = "";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    foreach (GridViewRow r in grid.Rows)
                    {
                        if ((r.RowType == DataControlRowType.DataRow))
                        {
                            for (int columnIndex = 0; (columnIndex
                                        <= (r.Cells.Count - 1)); columnIndex++)
                            {
                                r.Cells[columnIndex].Attributes.Add("class", "text");
                            }

                        }

                    }

                    grid.RenderControl(htw);
                    string style = "<style> .text { mso-number-format:\\@; } </style> ";
                    Response.Write(style);

                    Response.Write(sw.ToString());
                    Response.End();
                    ModelState.Clear();
                }

            }
            else if (submit == "Download")
            {
                ViewBag.menu = Session["menu"];
                //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
                Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");

                model.NIK = string.IsNullOrEmpty(model.NIK) == true ? "" : model.NIK;
                model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
                model.Department = string.IsNullOrEmpty(model.Department) == true ? "" : model.Department;
                model.Email = string.IsNullOrEmpty(model.Email) == true ? "" : model.Email;
                model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
                model.EmployeeID = string.IsNullOrEmpty(model.EmployeeID) == true ? "" : model.EmployeeID;
                model.EmployeeName = string.IsNullOrEmpty(model.EmployeeName) == true ? "" : model.EmployeeName;

                Workbook workbook = new Workbook();
                ArrayList lblproduct = new ArrayList();
                lblproduct.Add("Employee");
                //lblproduct.Add("Parent");
                lblproduct.Add("Spouse");
                lblproduct.Add("Child");


                foreach (var item in lblproduct)
                {
                    int Column;
                    Column = 1;

                    DataTable dtDCR_failed = Common.ExecuteQuery("dbo.[sp_DOWNLOAD_REPORT] '" + model.EmployeeID + "', '" + model.EmployeeName + "', '" + model.Department + "', '" + model.Entity + "', '" + item.ToString() + "' ");

                    if (dtDCR_failed.Rows.Count > 0)
                    {
                        int intcol = 0;
                        int sheetCo = 1;

                        Worksheet worksheetSu = new Worksheet(item.ToString());

                        int intcols = 0;
                        foreach (DataColumn dc in dtDCR_failed.Columns)
                        {
                            // workbook.Worksheets(sheetCo).Cells(1, Column).Value = dc.ColumnName
                            worksheetSu.Cells[0, Column - 1] = new Cell(dc.ColumnName);
                            Column = Column + 1;
                        }

                        for (int i = 0; i <= dtDCR_failed.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= dtDCR_failed.Columns.Count - 1; j++)
                            {
                                var tes = dtDCR_failed.Rows[i][j].ToString();
                                worksheetSu.Cells[i + 1, j + 0] = new Cell(dtDCR_failed.Rows[i][j].ToString());
                            }
                        }

                        for (int i = dtDCR_failed.Rows.Count + 1; i <= dtDCR_failed.Rows.Count + 100; i++)
                            worksheetSu.Cells[i, 0] = new Cell("");

                        sheetCo = sheetCo + 1;
                        workbook.Worksheets.Add(worksheetSu);
                    }
                    else
                    {
                        // oXL.Worksheets.Add(item.ToString).Cells("A1").Value = "NO DATA"
                        Worksheet worksheetSu = new Worksheet(item.ToString());
                        worksheetSu.Cells[0, 0] = new Cell("NO DATA, Please Try Again");
                        worksheetSu.Cells[2, 0] = new Cell("Copyright PT. AXA Services Indonesia " + System.Convert.ToString((DateTime.Now)));
                        for (int i = 3; i <= 101; i++)
                            worksheetSu.Cells[i, 0] = new Cell("");


                        workbook.Worksheets.Add(worksheetSu);
                    }
                }

                string filename = "Report_Employee" + ".xls";

                string report = Server.MapPath("~/Report/" + model.EmployeeID + "/" + filename);

                if (!System.IO.Directory.Exists(report))
                    System.IO.Directory.CreateDirectory(report);
                string reportName = report + filename;

                if (System.IO.File.Exists(reportName))
                    System.IO.File.Delete(reportName);

                string sAppPath = report + filename;
                string sPath = report + filename;

                workbook.Save(sAppPath);

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                Response.WriteFile(sAppPath);
                Response.Flush();
                Response.End();

            }
            else if (submit == "Back")
            {
                return RedirectToAction("ListView", "ListView");
            }
            else if (submit == "Upload")
            {
                return RedirectToAction("UploadRev", "ListView");
            }
            else if (submit == "UploadFaskes")
            {
                return RedirectToAction("Upload", "ListView");
            }

            var modelEmployee = ListEmployee(model.EmployeeID, model.EmployeeName, model.Department, Entity, Status);
            string role = Session["Role"].ToString().ToUpper();
            if (Session["Role"].ToString().ToUpper() == "SUPER ADMIN")
            {
                 modelEmployee = ListEmployee(model.EmployeeID, model.EmployeeName, model.Department, Entity, Status);
            }
            else
            {
                //modelEmployee = ListEmployee(model.EmployeeID, model.EmployeeName, model.Department, model.Entity, Status);
                modelEmployee = ListEmployee(model.EmployeeID, model.EmployeeName, model.Department, Session["ShortEntity"].ToString(), Status);
            }

            return View(modelEmployee);

        }
        [HttpGet]
        public ActionResult UploadRev()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.menu = Session["menu"];

            return View();
        }
        //public ActionResult Upload()
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    ViewBag.menu = Session["menu"];

        //    return View();

        //}
        [HttpPost]
        public ActionResult UploadRev(HttpPostedFileBase file, string submit)
        {
            string sheetName = null;

            if (submit == "Upload")
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
                                conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
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
                                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();
                                    if (sheetName.ToUpper() == "'EXPORT BPJS REPORT (3)$'")
                                    {
                                        //Read Data from First Sheet.
                                        connExcel.Open();
                                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                        odaExcel.SelectCommand = cmdExcel;
                                        odaExcel.Fill(dt);
                                        connExcel.Close();
                                    }
                                }
                            }
                        }

                        SqlConnection connX = Common.GetConnection();
                        SqlCommand cmdX = new SqlCommand("sp_INSERT_UPLOADFILE", connX);
                        cmdX.CommandType = CommandType.StoredProcedure;
                        cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                        cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                        SqlDataAdapter da = new SqlDataAdapter(cmdX);
                        DataTable dtX = new DataTable();
                        connX.Open();
                        da.Fill(dtX);

                        int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());
                        connX.Close();
                        if (sheetName.ToUpper() == "'EXPORT BPJS REPORT (3)$'")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DateTime birthdate;
                                DateTime JointDate;
                                DateTime DPLK_Join_Date;
                                int birthstring = dt.Rows[i]["BirthDate"].ToString().Length;
                                int jointstring = dt.Rows[i]["Joint Date"].ToString().Length;
                                int dplstring = dt.Rows[i]["DPLK_Join_Date"].ToString().Length;
                                //Birth
                                if (dt.Rows[i]["BirthDate"].ToString() == "" || dt.Rows[i]["BirthDate"].ToString() == null)
                                {
                                    dt.Rows[i]["BirthDate"] = "1900-01-01";
                                    birthdate = Convert.ToDateTime("1900-01-01");
                                }
                                else
                                {
                                    if (birthstring <= 10)
                                    {
                                        birthdate = DateTime.ParseExact(dt.Rows[i]["BirthDate"].ToString(), "dd/MM/yyyy", null);
                                    }
                                    else
                                    {
                                        birthdate = DateTime.ParseExact(dt.Rows[i]["BirthDate"].ToString(), "M/d/yyyy h:mm:ss tt", null);
                                    }
                                }

                                //joint
                                if (dt.Rows[i]["Joint Date"].ToString() == "" || dt.Rows[i]["Joint Date"].ToString() == null)
                                {
                                    dt.Rows[i]["Joint Date"] = "1900-01-01";
                                    JointDate = Convert.ToDateTime("1900-01-01");
                                }
                                else
                                {
                                    if (jointstring <= 10)
                                    {
                                        JointDate = DateTime.ParseExact(dt.Rows[i]["Joint Date"].ToString(), "dd/MM/yyyy", null);
                                    }
                                    else
                                    {
                                        JointDate = DateTime.ParseExact(dt.Rows[i]["Joint Date"].ToString(), "M/d/yyyy h:mm:ss tt", null);
                                    }
                                }

                                //DPLK
                                if (dt.Rows[i]["DPLK_Join_Date"].ToString() == "" || dt.Rows[i]["DPLK_Join_Date"].ToString() == null)
                                {
                                    dt.Rows[i]["DPLK_Join_Date"] = "1900-01-01";
                                    DPLK_Join_Date = Convert.ToDateTime("1900-01-01");
                                }
                                else
                                {
                                    if (dplstring <= 10)
                                    {
                                        DPLK_Join_Date = DateTime.ParseExact(dt.Rows[i]["DPLK_Join_Date"].ToString(), "dd/MM/yyyy", null);
                                    }
                                    else
                                    {
                                        DPLK_Join_Date = DateTime.ParseExact(dt.Rows[i]["DPLK_Join_Date"].ToString(), "M/d/yyyy h:mm:ss tt", null);
                                    }
                                }
                              
                                if (dt.Rows[i]["ID Person"].ToString() != "")
                                {
                                    try { 
                                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("[SP_TRN_UPLOAD_EMPLOYEE_DATA]", conn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@BPJS_KESEHATAN_ID", dt.Rows[i]["BPJS_Kesehatan_ID"].ToString());
                                            cmd.Parameters.AddWithValue("@JENISMUTASI", dt.Rows[i]["JenisMutasi"].ToString());
                                            cmd.Parameters.AddWithValue("@TGL_AKTIF_MUTASI", Common.SqlDate2(dt.Rows[i]["TglAktifBerlakuMutasi"].ToString()));
                                            cmd.Parameters.AddWithValue("@KK", dt.Rows[i]["Family Certificate No (KK)"].ToString());
                                            cmd.Parameters.AddWithValue("@NIK", dt.Rows[i]["National ID No (NIK)"].ToString());
                                            cmd.Parameters.AddWithValue("@FULL_NAME", (dt.Rows[i]["Full Name"].ToString()));
                                            cmd.Parameters.AddWithValue("@CODE_HUB_KEL", (dt.Rows[i]["KODE_HUB_KEL"].ToString()));
                                            cmd.Parameters.AddWithValue("@TMPT_LAHIR", dt.Rows[i]["TempatLahir"].ToString());
                                                
                                            cmd.Parameters.AddWithValue("@BIRTH_DATE", birthdate);
                                            cmd.Parameters.AddWithValue("@JENIS_KELAMIN", dt.Rows[i]["JenisKelamin"].ToString());
                                            cmd.Parameters.AddWithValue("@STATUS_KAWIN", dt.Rows[i]["StatusKawin"].ToString());
                                            cmd.Parameters.AddWithValue("@ALAMAT_TINGGAL", dt.Rows[i]["Alamat Tempat Tinggal"].ToString());
                                            cmd.Parameters.AddWithValue("@RT", dt.Rows[i]["RT"].ToString());
                                            cmd.Parameters.AddWithValue("@RW", (dt.Rows[i]["RW"].ToString()));
                                            cmd.Parameters.AddWithValue("@KODE_POS", dt.Rows[i]["KODE POS"].ToString());
                                            cmd.Parameters.AddWithValue("@KODE_DESA", dt.Rows[i]["Kode Desa (diisi petugas)"].ToString());
                                            cmd.Parameters.AddWithValue("@KODE_KECAMATAN", dt.Rows[i]["Kode Kecamatan (diisi petugas)"].ToString());

                                            cmd.Parameters.AddWithValue("@PROVINSI_FASKES", (dt.Rows[i]["Provinsi_Faskes"].ToString()));
                                            cmd.Parameters.AddWithValue("@CITY_FASKES", dt.Rows[i]["City Faskes TK 1"].ToString());
                                            cmd.Parameters.AddWithValue("@SUB_DISTRICT_FASKES_TK1", dt.Rows[i]["SubDistrict Faskes TK 1"].ToString());
                                            cmd.Parameters.AddWithValue("@FASKES_TK1", dt.Rows[i]["Faskes TK 1"].ToString());
                                            cmd.Parameters.AddWithValue("@KODE_FASKES_TK1", dt.Rows[i]["Kode Faskes TK 1"].ToString());
                                            cmd.Parameters.AddWithValue("@PROVINSI_FASKES_GIGI", dt.Rows[i]["Provinsi_Faskes_Gigi"].ToString());
                                            cmd.Parameters.AddWithValue("@KOTAMADYA_FASKES_GIGI", dt.Rows[i]["Kotamadya_Faskes_Gigi"].ToString());
                                            cmd.Parameters.AddWithValue("@KECAMATAN_FASKES_GIGI", dt.Rows[i]["Kecamatan_Faskes_Gigi"].ToString());

                                            cmd.Parameters.AddWithValue("@FASKES_DRG", dt.Rows[i]["Faskes drg"].ToString());
                                            cmd.Parameters.AddWithValue("@KODE_FASKES_DOK_GIGI", dt.Rows[i]["Kode Faskes Dokter Gigi"].ToString());
                                            cmd.Parameters.AddWithValue("@NOMOR_TLP_PESERTA", dt.Rows[i]["Nomor Telepon Peserta"].ToString());
                                            cmd.Parameters.AddWithValue("@EMAIL", dt.Rows[i]["Email"].ToString());
                                            cmd.Parameters.AddWithValue("@ID_PERSON", dt.Rows[i]["ID Person"].ToString());
                                            cmd.Parameters.AddWithValue("@JABATAN", dt.Rows[i]["Jabatan"].ToString());
                                            cmd.Parameters.AddWithValue("@STATUS", dt.Rows[i]["Status"].ToString());
                                            cmd.Parameters.AddWithValue("@KELAS_RAWAT", dt.Rows[i]["Kelas Rawat"].ToString());

                                            cmd.Parameters.AddWithValue("@TMT_KERJA", dt.Rows[i]["TMT Kerja (Kary_Aktif)"].ToString());
                                            cmd.Parameters.AddWithValue("@GAJI_POKOK_KARY_AKTIF", dt.Rows[i]["Gaji Pokok + Tunj_Tetap (Kary_Aktif)"].ToString());
                                            cmd.Parameters.AddWithValue("@KEWARGANEGARAAN", dt.Rows[i]["Kewarga Negaraan"].ToString());
                                            cmd.Parameters.AddWithValue("@NO_KARTU_ASURANSI", dt.Rows[i]["No_Kartu Asuransi"].ToString());
                                            cmd.Parameters.AddWithValue("@NAMA_ASURANSI", dt.Rows[i]["Nama Asuransi"].ToString());
                                            cmd.Parameters.AddWithValue("@NO_NPWP", (dt.Rows[i]["No_NPWP"].ToString()));
                                            cmd.Parameters.AddWithValue("@NO_PASSPORT", ((dt.Rows[i]["No Passport"].ToString())));
                                            if (dt.Rows[i]["KODE_HUB_KEL"].ToString() == "1")
                                            {
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS", dt.Rows[i]["Status BPJS Kesehatan"].ToString());
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_SPOUSE", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD1", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD2", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD3", "");
                                            }
                                            else if (dt.Rows[i]["KODE_HUB_KEL"].ToString() == "2")
                                            {
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_SPOUSE", dt.Rows[i]["Status BPJS Kesehatan"].ToString());
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD1","");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD2", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD3", "");
                                            }
                                            else if (dt.Rows[i]["KODE_HUB_KEL"].ToString() == "3")
                                            {
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_SPOUSE", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD1", dt.Rows[i]["Status BPJS Kesehatan"].ToString());
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD2", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD3", "");
                                            }
                                            else if (dt.Rows[i]["KODE_HUB_KEL"].ToString() == "4")
                                            {
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_SPOUSE", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD1", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD2", dt.Rows[i]["Status BPJS Kesehatan"].ToString());
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD3", "");
                                            }
                                            else if (dt.Rows[i]["KODE_HUB_KEL"].ToString() == "5")
                                            {
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_SPOUSE", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD1", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD2", "");
                                                cmd.Parameters.AddWithValue("@STATUS_BPJS_CHILD3", dt.Rows[i]["Status BPJS Kesehatan"].ToString());
                                            }



                                            cmd.Parameters.AddWithValue("@EDUCATION", dt.Rows[i]["Education"].ToString());
                                            cmd.Parameters.AddWithValue("@INSTITUTION", dt.Rows[i]["Institution Name"].ToString());
                                            cmd.Parameters.AddWithValue("@REMARK", dt.Rows[i]["Remark"].ToString());
                                            cmd.Parameters.AddWithValue("@BPJS_TENAGAKERJA_ID", dt.Rows[i]["BPJS_Ketenagakerjaan_ID"].ToString());
                                            cmd.Parameters.AddWithValue("@BPJS_PENSIUN_ID", dt.Rows[i]["BPJS_Pensiun_ID"].ToString());
                                            cmd.Parameters.AddWithValue("@JOINT_DATE", JointDate);
                                            cmd.Parameters.AddWithValue("@DPLK_NO_PESERTA", dt.Rows[i]["DPLK_NoPeserta"].ToString());
                                            cmd.Parameters.AddWithValue("@DPLK_JOINT_DATE", DPLK_Join_Date);

                                            cmd.Parameters.AddWithValue("@HEALTH_PLAN_NOPOLIS", dt.Rows[i]["Health_Plan_Membership_NoPolis"].ToString());
                                            cmd.Parameters.AddWithValue("@HEALTH_PLAN_NOPESERTA", dt.Rows[i]["Health_Plan_Membership_NoPeserta"].ToString());
                                            cmd.Parameters.AddWithValue("@HEALTH_PLAN_NOKARTU", dt.Rows[i]["Health_Plan_Membership_NoKartu"].ToString());
                                            cmd.Parameters.AddWithValue("@HEALTH_PLAN_RAWAT_INAP", dt.Rows[i]["Health_Plan_Benefit_Rawat_Inap"].ToString());
                                            cmd.Parameters.AddWithValue("@HEALTH_PLAN_RAWAT_JALAN", dt.Rows[i]["Health_Plan_Benefit_Rawat_Jalan"].ToString());
                                            cmd.Parameters.AddWithValue("@HEALTH_PLAN_PERSALINAN", dt.Rows[i]["Health_Plan_Benefit_Persalinan"].ToString());
                                            cmd.Parameters.AddWithValue("@HEALTH_PLAN_GIGI", dt.Rows[i]["Health_Plan_Benefit_Gigi"].ToString());
                                            cmd.Parameters.AddWithValue("@HEALTH_PLAN_KACAMATA", dt.Rows[i]["Health_Plan_Benefit_Kacamata"].ToString());

                                            cmd.Parameters.AddWithValue("@UPDATE_DATE", Common.SqlDate2(dt.Rows[i]["UpdatedDate"].ToString()));

                                            //cmd.Parameters.AddWithValue("@ChkSpouse", false);
                                            //cmd.Parameters.AddWithValue("@chkChild1", false);
                                            
                                            //cmd.Parameters.AddWithValue("@chkChild2", false);
                                            //cmd.Parameters.AddWithValue("@chkChild3", false);
                                            

                                            cmd.Parameters.AddWithValue("@UPLOAD_ID", UploadID);

                                            cmd.Parameters.AddWithValue("@CREATED_BY", Session["UserID"].ToString());
                                            conn.Open();
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    }
                                    catch (Exception ex)
                                    {
                                        TempData["Error"] = ex.Message;
                                        TempData["Success"] = "";
                                        ViewBag.menu = Session["menu"];
                                        return View();
                                    }
                                }
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Wrong File Name or Sheet Name";
                        }
                        if (TempData["Error"] != null)
                        {
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
            else if(submit == "Back")
            {
                return RedirectToAction("Listview", "Listview");
            }
            ViewBag.menu = Session["menu"];
            return View();
        }
        public ActionResult Upload(HttpPostedFileBase file,string ddl, string submit)
        {
            string sheetName = null;
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (submit == "Back")
            {
                return RedirectToAction("ListView", "ListView");
            }
            else if (submit == "Upload")
            {
                if (ddl == "1")
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
                                    conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
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
                                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                        connExcel.Close();
                                        if (sheetName.ToUpper() == "EMPLOYEE$")
                                        {
                                            //Read Data from First Sheet.
                                            connExcel.Open();
                                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                            odaExcel.SelectCommand = cmdExcel;
                                            odaExcel.Fill(dt);
                                            connExcel.Close();
                                        }
                                    }
                                }
                            }

                            SqlConnection connX = Common.GetConnection();
                            SqlCommand cmdX = new SqlCommand("sp_INSERT_UPLOADFILE", connX);
                            cmdX.CommandType = CommandType.StoredProcedure;
                            cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                            cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmdX);
                            DataTable dtX = new DataTable();
                            connX.Open();
                            da.Fill(dtX);

                            int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());
                            connX.Close();
                            if (sheetName.ToUpper() == "EMPLOYEE$")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["Married Date"].ToString() == "")
                                    {
                                        dt.Rows[i]["Married Date"] = "1900-01-01";
                                    }
                                    if (dt.Rows[i]["ID Employee"].ToString() != "")
                                    {
                                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                        {
                                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_EMPLOYEE_DATA", conn))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@EmployeeID", dt.Rows[i]["ID Employee"].ToString());
                                                cmd.Parameters.AddWithValue("@EmployeeName", dt.Rows[i]["Full Name"].ToString());
                                                cmd.Parameters.AddWithValue("@Entity", dt.Rows[i]["Entity"].ToString());
                                                cmd.Parameters.AddWithValue("@Department", dt.Rows[i]["Department"].ToString());
                                                cmd.Parameters.AddWithValue("@Job_Title", dt.Rows[i]["Job Tittle"].ToString());
                                                cmd.Parameters.AddWithValue("@Grade", Common.Encrypt(dt.Rows[i]["Grade"].ToString()));
                                                cmd.Parameters.AddWithValue("@Hiredate", Common.SqlDate2(dt.Rows[i]["Hiredate"].ToString()));
                                                cmd.Parameters.AddWithValue("@Spouse", "");

                                                cmd.Parameters.AddWithValue("@Reporting_To", dt.Rows[i]["Reporting To"].ToString());
                                                cmd.Parameters.AddWithValue("@Email", dt.Rows[i]["Office E-Mail"].ToString());
                                                cmd.Parameters.AddWithValue("@NIK", dt.Rows[i]["National ID No (NIK)"].ToString());
                                                cmd.Parameters.AddWithValue("@NPWP", dt.Rows[i]["Tax ID No (NPWP)"].ToString());
                                                cmd.Parameters.AddWithValue("@No_KK", dt.Rows[i]["Family Certificate No (KK)"].ToString());
                                                cmd.Parameters.AddWithValue("@Married_Date", Common.SqlDate2(dt.Rows[i]["Married Date"].ToString()));
                                                cmd.Parameters.AddWithValue("@Marital_Status", dt.Rows[i]["Marital Status"].ToString());
                                                cmd.Parameters.AddWithValue("@Gender", dt.Rows[i]["Gender"].ToString());
                                                cmd.Parameters.AddWithValue("@Place_Birth", dt.Rows[i]["Place Birth"].ToString());

                                                cmd.Parameters.AddWithValue("@Birthdate", Common.SqlDate2(dt.Rows[i]["Birthdate"].ToString()));
                                                cmd.Parameters.AddWithValue("@Nationality", dt.Rows[i]["Nationality"].ToString());
                                                cmd.Parameters.AddWithValue("@Religion", dt.Rows[i]["Religion"].ToString());
                                                cmd.Parameters.AddWithValue("@Home_Address", dt.Rows[i]["Home Address"].ToString());
                                                cmd.Parameters.AddWithValue("@City", dt.Rows[i]["City"].ToString());
                                                cmd.Parameters.AddWithValue("@KodePos", dt.Rows[i]["KodePos"].ToString());
                                                cmd.Parameters.AddWithValue("@RT", dt.Rows[i]["RT"].ToString());
                                                cmd.Parameters.AddWithValue("@RW", dt.Rows[i]["RW"].ToString());

                                                cmd.Parameters.AddWithValue("@State", dt.Rows[i]["State"].ToString());
                                                cmd.Parameters.AddWithValue("@Kotamadya", dt.Rows[i]["Kotamadya"].ToString());
                                                cmd.Parameters.AddWithValue("@Kecamatan", dt.Rows[i]["Kecamatan"].ToString());
                                                cmd.Parameters.AddWithValue("@Kelurahan", dt.Rows[i]["Kelurahan"].ToString());
                                                cmd.Parameters.AddWithValue("@Physical_Address", dt.Rows[i]["Physical Address"].ToString());
                                                cmd.Parameters.AddWithValue("@Personal_Email", dt.Rows[i]["Personal Email"].ToString());
                                                cmd.Parameters.AddWithValue("@Home_Phone", dt.Rows[i]["Home Phone"].ToString());
                                                cmd.Parameters.AddWithValue("@Handphone", dt.Rows[i]["Home Mobile Phone (Handphone)"].ToString());

                                                cmd.Parameters.AddWithValue("@Payroll_Bank", dt.Rows[i]["Payroll - Bank"].ToString());
                                                cmd.Parameters.AddWithValue("@Payroll_Branch", dt.Rows[i]["Payroll - Branch"].ToString());
                                                cmd.Parameters.AddWithValue("@Payroll_AccountNo", dt.Rows[i]["Payroll - Account No"].ToString());
                                                cmd.Parameters.AddWithValue("@Payroll_Account_Name", dt.Rows[i]["Payroll - Account Name"].ToString());
                                                cmd.Parameters.AddWithValue("@DPLK_NoPeserta", dt.Rows[i]["DPLK - No Peserta"].ToString());
                                                cmd.Parameters.AddWithValue("@DPLK_Join_Date", Common.SqlDate2(dt.Rows[i]["DPLK - Joint Date"].ToString()));
                                                cmd.Parameters.AddWithValue("@DPLK_saldo", (string.IsNullOrEmpty(dt.Rows[i]["DPLK - Saldo"].ToString())) ? Convert.ToString(0) : dt.Rows[i]["DPLK - Saldo"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPolis", dt.Rows[i]["Health Plan Membership - No Polis"].ToString());


                                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPeserta", dt.Rows[i]["Health Plan Membership - No Peserta"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoKartu", dt.Rows[i]["Health Plan Membership- No Kartu"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Inap", dt.Rows[i]["Health Plan Benefit - Rawat Inap"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Jalan", dt.Rows[i]["Health Plan Benefit - Rawat Jalan"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Persalinan", dt.Rows[i]["Health Plan Benefit - Persalinan"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Gigi", dt.Rows[i]["Health Plan Benefit - Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Kacamata", dt.Rows[i]["Health Plan Benefit - Kacamata"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Ketenagakerjaan_ID", dt.Rows[i]["BPJS Ketenagakerjaan - ID"].ToString());

                                                cmd.Parameters.AddWithValue("@BPJS_Pensiun_ID", dt.Rows[i]["BPJS Pensiun - ID"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active", dt.Rows[i]["BPJS Kesehatan - Active"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_ID", dt.Rows[i]["BPJS Kesehatan - ID"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Tingkat_Pertama", dt.Rows[i]["BPJS Kesehatan - Faskes Tingkat Pertama"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Dokter_Gigi", dt.Rows[i]["BPJS Kesehatan - Faskes Dokter Gigi"].ToString());


                                                //cmd.Parameters.AddWithValue("@ChkSpouse", false);
                                                //cmd.Parameters.AddWithValue("@chkChild1", false);

                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active_Spouse", "");
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active_Child_1", "");
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active_Child_2", "");
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active_Child_3", "");

                                                cmd.Parameters.AddWithValue("@Attach_File_Married", "");
                                                cmd.Parameters.AddWithValue("@FileName_Married", "");
                                                cmd.Parameters.AddWithValue("@Attach_File_Education", "");
                                                cmd.Parameters.AddWithValue("@FileName_Education", "");
                                                cmd.Parameters.AddWithValue("@Univ_Name", "");
                                                cmd.Parameters.AddWithValue("@Education", "");

                                                cmd.Parameters.AddWithValue("@Attach_File_Payroll", "");
                                                cmd.Parameters.AddWithValue("@FileName_Payroll", "");
                                                cmd.Parameters.AddWithValue("@BPJS_JoinDate", Common.SqlDate2(dt.Rows[i]["BPJS - JoinDate"].ToString()));
                                                cmd.Parameters.AddWithValue("@Class", dt.Rows[i]["Class"].ToString());



                                                //cmd.Parameters.AddWithValue("@chkChild2", false);
                                                //cmd.Parameters.AddWithValue("@chkChild3", false);

                                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_TK", dt.Rows[i]["Provinsi_Faskes"].ToString());
                                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_TK", dt.Rows[i]["Kotamadya_Faskes"].ToString());
                                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_TK", dt.Rows[i]["Kecamatan_Faskes"].ToString());

                                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_Gigi", dt.Rows[i]["Provinsi_Faskes_Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_Gigi", dt.Rows[i]["Kotamadya_Faskes_Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_Gigi", dt.Rows[i]["Kecamatan_Faskes_Gigi"].ToString());


                                                cmd.Parameters.AddWithValue("@UploadID", UploadID);

                                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                                                conn.Open();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Wrong File Name or Sheet Name";
                            }
                            if (TempData["Error"] != null)
                            {
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
                else if (ddl == "2")
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
                                    conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
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
                                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                        connExcel.Close();
                                        if (sheetName.ToUpper() == "CHILD$")
                                        {
                                            //Read Data from First Sheet.
                                            connExcel.Open();
                                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                            odaExcel.SelectCommand = cmdExcel;
                                            odaExcel.Fill(dt);
                                            connExcel.Close();
                                        }
                                    }
                                }
                            }

                            SqlConnection connX = Common.GetConnection();
                            SqlCommand cmdX = new SqlCommand("sp_INSERT_UPLOADFILE", connX);
                            cmdX.CommandType = CommandType.StoredProcedure;
                            cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                            cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmdX);
                            DataTable dtX = new DataTable();
                            connX.Open();
                            da.Fill(dtX);

                            int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());

                            connX.Close();
                            if (sheetName.ToUpper() == "CHILD$")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["ID Employee"].ToString() != "")
                                    {
                                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                        {
                                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_CHILD_EMPLOYEE", conn))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@EmployeeID", dt.Rows[i]["ID Employee"].ToString());
                                                cmd.Parameters.AddWithValue("@Full_Name", dt.Rows[i]["Full Name"].ToString());
                                                cmd.Parameters.AddWithValue("@Birthdate", Common.SqlDate2(dt.Rows[i]["Birthdate"].ToString()));
                                                cmd.Parameters.AddWithValue("@Gender", dt.Rows[i]["Gender"].ToString());
                                                cmd.Parameters.AddWithValue("@KK", dt.Rows[i]["Family Certificate No (KK)"].ToString());
                                                cmd.Parameters.AddWithValue("@NIK", dt.Rows[i]["National ID No (NIK)"].ToString());
                                                cmd.Parameters.AddWithValue("@Handphone", dt.Rows[i]["Home Mobile Phone (Handphone)"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPolis", dt.Rows[i]["Health Plan Membership - No Polis"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPeserta", dt.Rows[i]["Health Plan Membership - No Peserta"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoKartu", dt.Rows[i]["Health Plan Membership- No Kartu"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Inap", dt.Rows[i]["Health Plan Benefit - Rawat Inap"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Jalan", dt.Rows[i]["Health Plan Benefit - Rawat Jalan"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Persalinan", dt.Rows[i]["Health Plan Benefit - Persalinan"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Gigi", dt.Rows[i]["Health Plan Benefit - Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Kacamata", dt.Rows[i]["Health Plan Benefit - Kacamata"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_ID", dt.Rows[i]["BPJS Kesehatan - ID"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Tingkat_Pertama", dt.Rows[i]["BPJS Kesehatan - Faskes Tingkat Pertama"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Dokter_Gigi", dt.Rows[i]["BPJS Kesehatan - Faskes Dokter Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@UploadID", UploadID);
                                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                                                cmd.Parameters.AddWithValue("@idx", 0);
                                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_TK", dt.Rows[i]["Provinsi_Faskes"].ToString());
                                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_TK", dt.Rows[i]["Kotamadya_Faskes"].ToString());
                                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_TK", dt.Rows[i]["Kecamatan_Faskes"].ToString());

                                                cmd.Parameters.AddWithValue("@Class_Child", dt.Rows[i]["Class"].ToString());

                                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_Gigi", dt.Rows[i]["Provinsi_Faskes_Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_Gigi", dt.Rows[i]["Kotamadya_Faskes_Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_Gigi", dt.Rows[i]["Kecamatan_Faskes_Gigi"].ToString());

                                                conn.Open();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Wrong File Name or Sheet Name";
                            }

                            if (TempData["Error"] != null)
                            {
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
                else if (ddl == "3")
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
                                    conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
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
                                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                        connExcel.Close();
                                        if (sheetName.ToUpper() == "FASKES_TK$")
                                        {
                                            //Read Data from First Sheet.
                                            connExcel.Open();
                                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                            odaExcel.SelectCommand = cmdExcel;
                                            odaExcel.Fill(dt);
                                            connExcel.Close();
                                        }
                                    }
                                }
                            }

                            SqlConnection connX = Common.GetConnection();
                            SqlCommand cmdX = new SqlCommand("sp_INSERT_UPLOADFILE", connX);
                            cmdX.CommandType = CommandType.StoredProcedure;
                            cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                            cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmdX);
                            DataTable dtX = new DataTable();
                            connX.Open();
                            da.Fill(dtX);

                            int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());

                            connX.Close();
                            if (sheetName.ToUpper() == "FASKES_TK$")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("sp_INSERT_FASKES_TK", conn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@PROVINSI", dt.Rows[i]["PROVINSI"].ToString());
                                            cmd.Parameters.AddWithValue("@KOTAMADYA", dt.Rows[i]["KOTAMADYA"].ToString());
                                            cmd.Parameters.AddWithValue("@KECAMATAN", dt.Rows[i]["KECAMATAN"].ToString());
                                            cmd.Parameters.AddWithValue("@NAMA_FASKES", dt.Rows[i]["NAMA FASKES TINGKAT PERTAMA"].ToString());
                                            cmd.Parameters.AddWithValue("@KODE_FASKES", dt.Rows[i]["KODE FASKES TINGKAT PERTAMA"].ToString());

                                            conn.Open();
                                            cmd.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Wrong File Name or Sheet Name";
                            }

                            if (TempData["Error"] != null)
                            {
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
                else if (ddl == "4")
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
                                    conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
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
                                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                        connExcel.Close();
                                        if (sheetName.ToUpper() == "FASKES_GIGI$")
                                        {
                                            //Read Data from First Sheet.
                                            connExcel.Open();
                                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                            odaExcel.SelectCommand = cmdExcel;
                                            odaExcel.Fill(dt);
                                            connExcel.Close();
                                        }
                                    }
                                }
                            }

                            SqlConnection connX = Common.GetConnection();
                            SqlCommand cmdX = new SqlCommand("sp_INSERT_UPLOADFILE", connX);
                            cmdX.CommandType = CommandType.StoredProcedure;
                            cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                            cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmdX);
                            DataTable dtX = new DataTable();
                            connX.Open();
                            da.Fill(dtX);

                            int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());

                            connX.Close();
                            if (sheetName.ToUpper() == "FASKES_GIGI$")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("sp_INSERT_FASKES_GIGI", conn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@PROVINSI", dt.Rows[i]["PROVINSI"].ToString());
                                            cmd.Parameters.AddWithValue("@KOTAMADYA", dt.Rows[i]["KOTAMADYA"].ToString());
                                            cmd.Parameters.AddWithValue("@KECAMATAN", dt.Rows[i]["KECAMATAN"].ToString());
                                            cmd.Parameters.AddWithValue("@NAMA_FASKES", dt.Rows[i]["NAMA FASKES TINGKAT PERTAMA"].ToString());
                                            cmd.Parameters.AddWithValue("@KODE_FASKES", dt.Rows[i]["KODE FASKES GIGI"].ToString());

                                            conn.Open();
                                            cmd.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Wrong File Name or Sheet Name";
                            }
                            if (TempData["Error"] != null)
                            {
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
                else if (ddl == "5")
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
                                    conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
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
                                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                        connExcel.Close();
                                        if (sheetName.ToUpper() == "SPOUSE$")
                                        {
                                            //Read Data from First Sheet.
                                            connExcel.Open();
                                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                            odaExcel.SelectCommand = cmdExcel;
                                            odaExcel.Fill(dt);
                                            connExcel.Close();
                                        }
                                    }
                                }
                            }

                            SqlConnection connX = Common.GetConnection();
                            SqlCommand cmdX = new SqlCommand("sp_INSERT_UPLOADFILE", connX);
                            cmdX.CommandType = CommandType.StoredProcedure;
                            cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                            cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmdX);
                            DataTable dtX = new DataTable();
                            connX.Open();
                            da.Fill(dtX);

                            int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());

                            connX.Close();
                            if (sheetName.ToUpper() == "SPOUSE$")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i]["ID Employee"].ToString() != "")
                                    {
                                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                        {
                                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_SPOUSE_EMPLOYEE", conn))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@EmployeeID", dt.Rows[i]["ID Employee"].ToString());
                                                cmd.Parameters.AddWithValue("@Full_Name", dt.Rows[i]["Full Name"].ToString());
                                                cmd.Parameters.AddWithValue("@Birthdate", Common.SqlDate2(dt.Rows[i]["Birthdate"].ToString()));
                                                cmd.Parameters.AddWithValue("@Gender", dt.Rows[i]["Gender"].ToString());
                                                cmd.Parameters.AddWithValue("@KK_Spouse", dt.Rows[i]["Family Certificate No (KK)"].ToString());
                                                cmd.Parameters.AddWithValue("@NIK", dt.Rows[i]["National ID No (NIK)"].ToString());
                                                cmd.Parameters.AddWithValue("@Handphone", dt.Rows[i]["Home Mobile Phone (Handphone)"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPolis", dt.Rows[i]["Health Plan Membership - No Polis"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPeserta", dt.Rows[i]["Health Plan Membership - No Peserta"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoKartu", dt.Rows[i]["Health Plan Membership- No Kartu"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Inap", dt.Rows[i]["Health Plan Benefit - Rawat Inap"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Jalan", dt.Rows[i]["Health Plan Benefit - Rawat Jalan"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Persalinan", dt.Rows[i]["Health Plan Benefit - Persalinan"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Gigi", dt.Rows[i]["Health Plan Benefit - Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Kacamata", dt.Rows[i]["Health Plan Benefit - Kacamata"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_ID", dt.Rows[i]["BPJS Kesehatan - ID"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Tingkat_Pertama", dt.Rows[i]["BPJS Kesehatan - Faskes Tingkat Pertama"].ToString());
                                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Dokter_Gigi", dt.Rows[i]["BPJS Kesehatan - Faskes Dokter Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@UploadID", UploadID);
                                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                                                cmd.Parameters.AddWithValue("@idx", 0);
                                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_TK", dt.Rows[i]["Provinsi_Faskes"].ToString());
                                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_TK", dt.Rows[i]["Kotamadya_Faskes"].ToString());
                                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_TK", dt.Rows[i]["Kecamatan_Faskes"].ToString());

                                                cmd.Parameters.AddWithValue("@Class_Spouse", dt.Rows[i]["Class"].ToString());

                                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_Gigi", dt.Rows[i]["Provinsi_Faskes_Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_Gigi", dt.Rows[i]["Kotamadya_Faskes_Gigi"].ToString());
                                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_Gigi", dt.Rows[i]["Kecamatan_Faskes_Gigi"].ToString());

                                                conn.Open();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Wrong File Name or Sheet Name";
                            }
                            if (TempData["Error"] != null)
                            {
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
                else if (ddl == "6")
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
                                    conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
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
                                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                        connExcel.Close();
                                        if (sheetName.ToUpper() == "UNIVERSITY$")
                                        {
                                            //Read Data from First Sheet.
                                            connExcel.Open();
                                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                            odaExcel.SelectCommand = cmdExcel;
                                            odaExcel.Fill(dt);
                                            connExcel.Close();
                                        }
                                    }
                                }
                            }

                            SqlConnection connX = Common.GetConnection();
                            SqlCommand cmdX = new SqlCommand("sp_INSERT_UPLOADFILE", connX);
                            cmdX.CommandType = CommandType.StoredProcedure;
                            cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                            cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmdX);
                            DataTable dtX = new DataTable();
                            connX.Open();
                            da.Fill(dtX);

                            int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());

                            connX.Close();
                            if (sheetName.ToUpper() == "UNIVERSITY$")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("sp_INSERT_UNIVERSITY", conn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@University", dt.Rows[i]["University"].ToString());

                                            conn.Open();
                                            cmd.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Wrong File Name or Sheet Name";
                            }
                            if (TempData["Error"] != null)
                            {
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
                else if (ddl == "7")
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
                                    conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
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
                                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                        connExcel.Close();
                                        if (sheetName.ToUpper() == "SHEET1$")
                                        {
                                            //Read Data from First Sheet.
                                            connExcel.Open();
                                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                            odaExcel.SelectCommand = cmdExcel;
                                            odaExcel.Fill(dt);
                                            connExcel.Close();
                                        }
                                    }
                                }
                            }

                            SqlConnection connX = Common.GetConnection();
                            SqlCommand cmdX = new SqlCommand("sp_INSERT_UPLOADFILE", connX);
                            cmdX.CommandType = CommandType.StoredProcedure;
                            cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                            cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmdX);
                            DataTable dtX = new DataTable();
                            connX.Open();
                            da.Fill(dtX);

                            int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());

                            connX.Close();
                            if (sheetName.ToUpper() == "SHEET1$")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("SP_INSERT_KECAMATAN", conn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@NAMA_KEC", dt.Rows[i]["Nama Kecamatan"].ToString());
                                            cmd.Parameters.AddWithValue("@KODE_KEC", dt.Rows[i]["KDKEC"].ToString());
                                            cmd.Parameters.AddWithValue("@NAMA_DESA", dt.Rows[i]["NMDESA"].ToString());
                                            cmd.Parameters.AddWithValue("@KODE_DESA", dt.Rows[i]["KDDESA"].ToString());

                                            conn.Open();
                                            cmd.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Wrong File Name or Sheet Name";
                            }
                            if (TempData["Error"] != null)
                            {
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
                else if (ddl == "8")
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
                                    conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
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
                                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                        connExcel.Close();
                                        if (sheetName.ToUpper() == "'UPLOAD EMPLOYEE$'")
                                        {
                                            //Read Data from First Sheet.
                                            connExcel.Open();
                                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                            odaExcel.SelectCommand = cmdExcel;
                                            odaExcel.Fill(dt);
                                            connExcel.Close();
                                        }
                                    }
                                }
                            }

                            SqlConnection connX = Common.GetConnection();
                            SqlCommand cmdX = new SqlCommand("sp_INSERT_UPLOADFILE", connX);
                            cmdX.CommandType = CommandType.StoredProcedure;
                            cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                            cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmdX);
                            DataTable dtX = new DataTable();
                            connX.Open();
                            da.Fill(dtX);

                            int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());

                            connX.Close();
                            if (sheetName.ToUpper() == "'UPLOAD EMPLOYEE$'")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("SP_UPLOAD_FTE", conn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@EMP_ID", dt.Rows[i]["Person_Number"].ToString());
                                            cmd.Parameters.AddWithValue("@EMP_NAME", dt.Rows[i]["Name"].ToString());
                                            cmd.Parameters.AddWithValue("@EMAIL", dt.Rows[i]["Email"].ToString());
                                            cmd.Parameters.AddWithValue("@ENTITY", dt.Rows[i]["Entity"].ToString());
                                            cmd.Parameters.AddWithValue("@SHORT_ENTITY", dt.Rows[i]["Short_Entity"].ToString());
                                            cmd.Parameters.AddWithValue("@WORKER_TYPE", dt.Rows[i]["Worker_Type"].ToString());
                                            cmd.Parameters.AddWithValue("@VENDOR", dt.Rows[i]["Vendor_Outsource"].ToString());

                                            conn.Open();
                                            cmd.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Wrong File Name or Sheet Name";
                            }
                            if (TempData["Error"] != null)
                            {
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
                }
            ViewBag.menu = Session["menu"];
            return View();

        }
        public static List<ListViewModels> ListEmployee(string ID, string Name, string Dept, string Entity, string Status)
        {
            SqlConnection conn = Common.GetConnection();
            List<EmployeeData.Models.ListViewModels> model = new List<ListViewModels>();
            SqlCommand cmd = new SqlCommand("sp_Get_List_Data_Employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = Dept;
            cmd.Parameters.Add("@Entity", SqlDbType.VarChar).Value = Entity;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            conn.Open();
            da.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new ListViewModels
                {
                    No = dr["No"].ToString(),
                    EmployeeID = dr["ID"].ToString(),
                    Name = dr["Name"].ToString(),
                    Department = dr["Department"].ToString(),
                    Entity = dr["ENTITY"].ToString(),
                    Email = dr["Email"].ToString(),
                    Flag = dr["Status_Employee"].ToString(),
                    CreatedDate = (dr["CreatedDate"].ToString()),
                    ApprovalDate = (dr["ApprovalDate_Employee"].ToString()),
                    ApprovalBy = dr["ApprovalBy_Employee"].ToString()
                });
            }

            return model;
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
        public ActionResult DeleteUser(string id)
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;


            try
            {
                Common.ExecuteNonQuery("Delete From TRN_EMPLOYEE_DATA where [EmployeeID]='" + id + "'");
                TempData["message"] = "<script>alert('Delete succesfully');</script>";
                return RedirectToAction("ListView");
            }
            catch (Exception)
            {
                TempData["message"] = "<script>alert('Delete unsuccesfully');</script>";
                return RedirectToAction("ListView");
            }
        }
        // [HttpGet]
        public ActionResult DDL()
        {
            ListViewModels model = new ListViewModels();
            DataTable dt;
            dt = Common.ExecuteQuery("dbo.[sp_SEL_DepartmentID]");
            if (dt.Rows.Count > 0)
            {
                model.DepartmentID = DDLDept();

            }
            return Json(new SelectList(model.DepartmentID, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        private static List<SelectListItem> DDLDept()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_SEL_DepartmentID";
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
                            Text = dr["Department"].ToString(),
                            Value = dr["Department"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
    }

}