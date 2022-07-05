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
using static EmployeeData.Models.ListViewModels;
using static EmployeeData.Models.MenuViewModels;
using static EmployeeData.Models.EmployeeDataModels;

namespace EmployeeData.Controllers
{
    public class ListApproveController : Controller
    {

        // GET: ListApprove
        public ActionResult IndexUser()
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;
            Session["controller"] = "HomeController";
            ViewBag.menu = Session["menu"];

            return View();
        }
        public ActionResult ListApprove(ListApproveModels model, string submit)
        {

            string url = Request.Url.OriginalString;
            Session["url"] = url;
            ViewBag.menu = Session["menu"];
            Session["controller"] = "ListApproveController";

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Session["controller"] = "ListApproveController";
            if (submit == "Search" || string.IsNullOrEmpty(submit) == true)
            {

                //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
                Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");

                model.NIK = string.IsNullOrEmpty(model.NIK) == true ? "" : model.NIK;
                model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
                model.Department = string.IsNullOrEmpty(model.Department) == true ? "" : model.Department;
                model.Email = string.IsNullOrEmpty(model.Email) == true ? "" : model.Email;
                model.EntityID = string.IsNullOrEmpty(model.EntityID) == true ? "" : model.EntityID;
                model.EmployeeID = string.IsNullOrEmpty(model.EmployeeID) == true ? "" : model.EmployeeID;
                model.EmployeeName = string.IsNullOrEmpty(model.EmployeeName) == true ? "" : model.EmployeeName;
            }
            else if (submit == "Back")
            {
                return RedirectToAction("ListApprove", "ListApprove");
            }


            var modelEmployee = ListEmployee(model.EmployeeID, model.EmployeeName, Session["LongEntity"].ToString(), Session["Role"].ToString());
            return View(modelEmployee);

        }
        [HttpGet]
        public ActionResult Upload()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.menu = Session["menu"];

            return View();

        }


        public ActionResult Edit(ListApproveModels model, string ID)
        {
            string EmplID = Request.QueryString["NIK"];
            string Names = Request.QueryString["Name"];
            string Department = Request.QueryString["Department"];

            String PathUAT = ConfigurationManager.AppSettings["PathUAT"].ToString();
            String PathProd = ConfigurationManager.AppSettings["PathProd"].ToString();

            string url = Request.Url.OriginalString;
            Session["url"] = url;
            model.LINK = url;
            //ViewBag.menu = Session["menu"];
            //Session["controller"] = "ListApproveController";

            

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.menu = Session["menu"];
            Session["controller"] = "ListApproveController";

            model.Department1 = DDLDept1();
            model.Entity = DDLEntity();
            model.JobTitle = DDLJobTitle();
            model.Grade = DDLGrade();
            model.MaritalStatus_ID = DDLMarital();
            model.Education_ID = DDLEdu();
            model.Univ_Name_ID = DDLUNIVERSITY();
            model.Gender = DDLSex();
            model.PlaceBirth = DDLPB();
            model.Nationality = DDLNATIONALITY();
            model.Religion = DDLRELIGION();
            model.State = DDLSTATE();
            model.State_ID = string.IsNullOrEmpty(model.State_ID) ? "" : model.State_ID;
            model.Kotamadya = DDLKOTAMADYA_ALL();
            model.Kecamatan = DDLKECAMATAN_ALL();
            model.Kelurahan = DDLKELURAHAN_ALL();

            model.Gender_Spouse = DDLSex();
            //model.Provinsi_FASKES = DDLSTATE_FASKES();
            //model.Kotamadya_FASKES = DDLKOTAMADYA_FASKES_ALL();
            //model.Kecamatan_FASKES = DDLKECAMATAN_FASKES_ALL();
            //model.BPJS_Kesehatan_Faskes_Tingkat_Pertama = DDL_FASKES_TK1();

            //model.Provinsi_GIGI = DDLSTATE_GIGI();
            //model.Kotamadya_GIGI = DDLKOTAMADYA_GIGI_ALL();
            //model.Kecamatan_GIGI = DDLKECAMATAN_GIGI_ALL();
            //model.BPJS_Kesehatan_Faskes_Dokter_Gigi = DDL_FASKES_GIGI();

            //// Spouse
            //model.Provinsi_FASKES_Spouse = DDLSTATE_FASKES();
            //model.Kotamadya_FASKES_Spouse = DDLKOTAMADYA_FASKES_ALL();
            //model.Kecamatan_FASKES_Spouse = DDLKECAMATAN_FASKES_ALL();
            //model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse = DDL_FASKES_TK1();

            //model.Provinsi_GIGI_Spouse = DDLSTATE_GIGI();
            //model.Kotamadya_GIGI_Spouse = DDLKOTAMADYA_GIGI_ALL();
            //model.Kecamatan_GIGI_Spouse = DDLKECAMATAN_GIGI_ALL();
            //model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse = DDL_FASKES_GIGI();

            //// Child
            //model.Provinsi_FASKES_Child_1 = DDLSTATE_FASKES();
            //model.Kotamadya_FASKES_Child_1 = DDLKOTAMADYA_FASKES_ALL();
            //model.Kecamatan_FASKES_Child_1 = DDLKECAMATAN_FASKES_ALL();
            //model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_1 = DDL_FASKES_TK1();

            //model.Provinsi_GIGI_Child_1 = DDLSTATE_GIGI();
            //model.Kotamadya_GIGI_Child_1 = DDLKOTAMADYA_GIGI_ALL();
            //model.Kecamatan_GIGI_Child_1 = DDLKECAMATAN_GIGI_ALL();
            //model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_1 = DDL_FASKES_GIGI();

            //model.Provinsi_FASKES_Child_2 = DDLSTATE_FASKES();
            //model.Kotamadya_FASKES_Child_2 = DDLKOTAMADYA_FASKES_ALL();
            //model.Kecamatan_FASKES_Child_2 = DDLKECAMATAN_FASKES_ALL();
            //model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_2 = DDL_FASKES_TK1();

            //model.Provinsi_GIGI_Child_2 = DDLSTATE_GIGI();
            //model.Kotamadya_GIGI_Child_2 = DDLKOTAMADYA_GIGI_ALL();
            //model.Kecamatan_GIGI_Child_2 = DDLKECAMATAN_GIGI_ALL();
            //model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_2 = DDL_FASKES_GIGI();

            //model.Provinsi_FASKES_Child_3 = DDLSTATE_FASKES();
            //model.Kotamadya_FASKES_Child_3 = DDLKOTAMADYA_FASKES_ALL();
            //model.Kecamatan_FASKES_Child_3 = DDLKECAMATAN_FASKES_ALL();
            //model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_3 = DDL_FASKES_TK1();

            //model.Provinsi_GIGI_Child_3 = DDLSTATE_GIGI();
            //model.Kotamadya_GIGI_Child_3 = DDLKOTAMADYA_GIGI_ALL();
            //model.Kecamatan_GIGI_Child_3 = DDLKECAMATAN_GIGI_ALL();
            //model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_3 = DDL_FASKES_GIGI();

            model.Gender_Parent1 = DDLSex();
            model.Gender_Parent2 = DDLSex();

            //model.Kecamatanddl = DDLKecamatanList("");
            //model.Desaddl = DDLDesaList(model.Kecamatan_ID);

            //model.Kecamatanddl_Spouse = DDLKecamatanList("");
            //model.Desaddl_Spouse = DDLDesaList(model.KecamatanID_Spouse);

            //model.Kecamatanddl_Ch1 = DDLKecamatanList("");
            //model.Desaddl_Ch1 = DDLDesaList(model.KecamatanID_Ch1);

            //model.Kecamatanddl_Ch2 = DDLKecamatanList("");
            //model.Desaddl_Ch2 = DDLDesaList(model.KecamatanID_Ch2);

            //model.Kecamatanddl_Ch3 = DDLKecamatanList("");
            //model.Desaddl_Ch3 = DDLDesaList(model.KecamatanID_Ch3);
            // ADD ON SPOUSE //

            // JENIS MUTASI SPOUSE
            model.JENISMUTASI_SPOUSE_ID = DDL_JENISMUTASI_SPOUSE();
            var JENISMUTASI_SPOUSE = DDL_JENISMUTASI_SPOUSE();
            List<SelectListItem> JENISMUTASI_SPOUSElistItem = new List<SelectListItem>();
            foreach (var item in JENISMUTASI_SPOUSE)
            {
                JENISMUTASI_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.JENISMUTASI_SPOUSE_ID = new SelectList(JENISMUTASI_SPOUSElistItem, "Value", "Text");
            // END JENIS MUTASI SPOUSE

            // FAMILY RELATIONSHIP SPOUSE
            model.FAMILYRELATIONSHIP_SPOUSE_ID = DDL_FAMILYRELATIONSHIP_SPOUSE();
            var FAMILYRELATIONSHIP_SPOUSE = DDL_FAMILYRELATIONSHIP_SPOUSE();
            List<SelectListItem> FAMILYRELATIONSHIP_SPOUSElistItem = new List<SelectListItem>();
            foreach (var item in FAMILYRELATIONSHIP_SPOUSE)
            {
                FAMILYRELATIONSHIP_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.FAMILYRELATIONSHIP_SPOUSE_ID = new SelectList(FAMILYRELATIONSHIP_SPOUSElistItem, "Value", "Text");
            // END FAMILY RELATIONSHIP SPOUSE

            //GENDER SPOUSE
            model.Gender_Spouse = DDL_GENDER_SPOUSE();
            var GENDER_SPOUSE = DDL_GENDER_SPOUSE();
            List<SelectListItem> GENDER_SPOUSElistItem = new List<SelectListItem>();
            foreach (var item in GENDER_SPOUSE)
            {
                GENDER_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.Gender_Spouse = new SelectList(GENDER_SPOUSElistItem, "Value", "Text");
            // END GENDER SPOUSE

            //MARITAL STATUS SPOUSE
            model.MARITALSTATUS_SPOUSE_ID = DDL_MARITALSTATUS_SPOUSE();
            var MARITALSTATUS_SPOUSE = DDL_MARITALSTATUS_SPOUSE();
            List<SelectListItem> MARITALSTATUS_SPOUSElistItem = new List<SelectListItem>();
            foreach (var item in MARITALSTATUS_SPOUSE)
            {
                MARITALSTATUS_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.MARITALSTATUS_SPOUSE_ID = new SelectList(MARITALSTATUS_SPOUSElistItem, "Value", "Text");
            //END MARITAL STATUS SPOUSE

            //NATIONALITY CITIZENSHIP SPOUSE
            model.CITIZENSHIP_SPOUSE_ID = DDL_CITIZENSHIP_SPOUSE();
            var CITIZENSHIP_SPOUSE = DDL_CITIZENSHIP_SPOUSE();
            List<SelectListItem> CITIZENSHIP_SPOUSElistItem = new List<SelectListItem>();
            foreach (var item in CITIZENSHIP_SPOUSE)
            {
                CITIZENSHIP_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.CITIZENSHIP_SPOUSE_ID = new SelectList(CITIZENSHIP_SPOUSElistItem, "Value", "Text");
            // END NATIONALITY CITIZENSHIP SPOUSE

            model.Provinsi_FASKES_Spouse = DDLSTATE_FASKES();
            model.Kotamadya_FASKES_Spouse = DDLKOTAMADYA_FASKES_ALL();
            model.Kecamatan_FASKES_Spouse = DDLKECAMATAN_FASKES_ALL();
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse = DDL_FASKES_TK1();

            model.Provinsi_GIGI_Spouse = DDLSTATE_GIGI();
            model.Kotamadya_GIGI_Spouse = DDLKOTAMADYA_GIGI_ALL();
            model.Kecamatan_GIGI_Spouse = DDLKECAMATAN_GIGI_ALL();
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse = DDL_FASKES_GIGI();
            // END ADD ON SPOUSE

            // ADD ON CHILD 1

            // jenis mutasi CHILD 1
            model.JENISMUTASI_CHILD1_ID = DDL_JENISMUTASICHILD1();
            var JENISMUTASI_CHILD1 = DDL_JENISMUTASICHILD1();
            List<SelectListItem> JENISMUTASI_CHILD1listItem = new List<SelectListItem>();
            foreach (var item in JENISMUTASI_CHILD1)
            {
                JENISMUTASI_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.JENISMUTASI_CHILD1_ID = new SelectList(JENISMUTASI_CHILD1listItem, "Value", "Text");
            // end jenis mutasi 

            // FAMILY RELATIONSHIP CODE CHILD 1
            model.FAMILYRELATIONSHIP_CHILD1_ID = DDL_FAMILYRELATIONSHIP_CHILD1();
            var FAMILYRELATIONSHIP_CHILD1 = DDL_FAMILYRELATIONSHIP_CHILD1();
            List<SelectListItem> FAMILYRELATIONSHIP_CHILD1listItem = new List<SelectListItem>();
            foreach (var item in FAMILYRELATIONSHIP_CHILD1)
            {
                FAMILYRELATIONSHIP_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.FAMILYRELATIONSHIP_CHILD1_ID = new SelectList(FAMILYRELATIONSHIP_CHILD1listItem, "Value", "Text");
            // END FAMILY RELATIONSHIP CODE

            // ADD GENDER CHILD 1
            model.GENDER_CHILD1_ID = DDL_GENDER_CHILD1();
            var GENDER_CHILD1 = DDL_GENDER_CHILD1();
            List<SelectListItem> GENDER_CHILD1listItem = new List<SelectListItem>();
            foreach (var item in GENDER_CHILD1)
            {
                GENDER_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.GENDER_CHILD1_ID = new SelectList(GENDER_CHILD1listItem, "Value", "Text");
            //END ADD GENDER CHILD 1

            // ADD MARITAL STATUS CHILD 1
            model.MARITALSTATUS_CHILD1_ID = DDL_MARITALSTATUS_CHILD1();
            var MARITALSTATUS_CHILD1 = DDL_MARITALSTATUS_CHILD1();
            List<SelectListItem> MARITALSTATUS_CHILD1listItem = new List<SelectListItem>();
            foreach (var item in MARITALSTATUS_CHILD1)
            {
                MARITALSTATUS_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.MARITALSTATUS_CHILD1_ID = new SelectList(MARITALSTATUS_CHILD1listItem, "Value", "Text");
            // END MARITAL STATUS 

            // NATIONALITY CITIZENSHIP CHILD 1
            model.CITIZENSHIP_CHILD1_ID = DDL_CITIZENSHIP_CHILD1();
            var CITIZENSHIP_CHILD1 = DDL_CITIZENSHIP_CHILD1();
            List<SelectListItem> CITIZENSHIP_CHILD1listItem = new List<SelectListItem>();
            foreach (var item in CITIZENSHIP_CHILD1)
            {
                CITIZENSHIP_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.CITIZENSHIP_CHILD1_ID = new SelectList(CITIZENSHIP_CHILD1listItem, "Value", "Text");
            // END NATIONALITY CITIZENSHIP

            model.Provinsi_FASKES_Child_1 = DDLSTATE_FASKES();
            model.Kotamadya_FASKES_Child_1 = DDLKOTAMADYA_FASKES_ALL();
            model.Kecamatan_FASKES_Child_1 = DDLKECAMATAN_FASKES_ALL();
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_1 = DDL_FASKES_TK1();

            model.Provinsi_GIGI_Child_1 = DDLSTATE_GIGI();
            model.Kotamadya_GIGI_Child_1 = DDLKOTAMADYA_GIGI_ALL();
            model.Kecamatan_GIGI_Child_1 = DDLKECAMATAN_GIGI_ALL();
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_1 = DDL_FASKES_GIGI();

            // END ADD ON CHILD 1

            // ADD ON CHILD 2 //
            // jenis mutasi CHILD 2
            model.JENISMUTASI_CHILD2_ID = DDL_JENISMUTASICHILD2();
            var JENISMUTASI_CHILD2 = DDL_JENISMUTASICHILD2();
            List<SelectListItem> JENISMUTASI_CHILD2listItem = new List<SelectListItem>();
            foreach (var item in JENISMUTASI_CHILD2)
            {
                JENISMUTASI_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.JENISMUTASI_CHILD2_ID = new SelectList(JENISMUTASI_CHILD2listItem, "Value", "Text");
            // end jenis mutasi 

            // FAMILY RELATIONSHIP CODE CHILD 2
            model.FAMILYRELATIONSHIP_CHILD2_ID = DDL_FAMILYRELATIONSHIP_CHILD2();
            var FAMILYRELATIONSHIP_CHILD2 = DDL_FAMILYRELATIONSHIP_CHILD2();
            List<SelectListItem> FAMILYRELATIONSHIP_CHILD2listItem = new List<SelectListItem>();
            foreach (var item in FAMILYRELATIONSHIP_CHILD2)
            {
                FAMILYRELATIONSHIP_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.FAMILYRELATIONSHIP_CHILD2_ID = new SelectList(FAMILYRELATIONSHIP_CHILD2listItem, "Value", "Text");
            // END FAMILY RELATIONSHIP CODE

            // ADD GENDER CHILD 2
            model.GENDER_CHILD2_ID = DDL_GENDER_CHILD2();
            var GENDER_CHILD2 = DDL_GENDER_CHILD2();
            List<SelectListItem> GENDER_CHILD2listItem = new List<SelectListItem>();
            foreach (var item in GENDER_CHILD2)
            {
                GENDER_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.GENDER_CHILD2_ID = new SelectList(GENDER_CHILD2listItem, "Value", "Text");
            //END ADD GENDER CHILD 2

            // ADD MARITAL STATUS CHILD 2
            model.MARITALSTATUS_CHILD2_ID = DDL_MARITALSTATUS_CHILD2();
            var MARITALSTATUS_CHILD2 = DDL_MARITALSTATUS_CHILD2();
            List<SelectListItem> MARITALSTATUS_CHILD2listItem = new List<SelectListItem>();
            foreach (var item in MARITALSTATUS_CHILD2)
            {
                MARITALSTATUS_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.MARITALSTATUS_CHILD2_ID = new SelectList(MARITALSTATUS_CHILD2listItem, "Value", "Text");
            // END MARITAL STATUS 

            // NATIONALITY CITIZENSHIP CHILD 2
            model.CITIZENSHIP_CHILD2_ID = DDL_CITIZENSHIP_CHILD2();
            var CITIZENSHIP_CHILD2 = DDL_CITIZENSHIP_CHILD2();
            List<SelectListItem> CITIZENSHIP_CHILD2listItem = new List<SelectListItem>();
            foreach (var item in CITIZENSHIP_CHILD2)
            {
                CITIZENSHIP_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.CITIZENSHIP_CHILD2_ID = new SelectList(CITIZENSHIP_CHILD2listItem, "Value", "Text");
            // END NATIONALITY CITIZENSHIP

            model.Provinsi_FASKES_Child_2 = DDLSTATE_FASKES();
            model.Kotamadya_FASKES_Child_2 = DDLKOTAMADYA_FASKES_ALL();
            model.Kecamatan_FASKES_Child_2 = DDLKECAMATAN_FASKES_ALL();
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_2 = DDL_FASKES_TK1();

            model.Provinsi_GIGI_Child_2 = DDLSTATE_GIGI();
            model.Kotamadya_GIGI_Child_2 = DDLKOTAMADYA_GIGI_ALL();
            model.Kecamatan_GIGI_Child_2 = DDLKECAMATAN_GIGI_ALL();
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_2 = DDL_FASKES_GIGI();
            // END ADD ON CHILD 2 //



            // ADD ON CHILD 3 //
            // jenis mutasi CHILD 2
            model.JENISMUTASI_CHILD3_ID = DDL_JENISMUTASICHILD3();
            var JENISMUTASI_CHILD3 = DDL_JENISMUTASICHILD3();
            List<SelectListItem> JENISMUTASI_CHILD3listItem = new List<SelectListItem>();
            foreach (var item in JENISMUTASI_CHILD3)
            {
                JENISMUTASI_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.JENISMUTASI_CHILD3_ID = new SelectList(JENISMUTASI_CHILD3listItem, "Value", "Text");
            // end jenis mutasi 

            // FAMILY RELATIONSHIP CODE CHILD 3
            model.FAMILYRELATIONSHIP_CHILD3_ID = DDL_FAMILYRELATIONSHIP_CHILD3();
            var FAMILYRELATIONSHIP_CHILD3 = DDL_FAMILYRELATIONSHIP_CHILD3();
            List<SelectListItem> FAMILYRELATIONSHIP_CHILD3listItem = new List<SelectListItem>();
            foreach (var item in FAMILYRELATIONSHIP_CHILD3)
            {
                FAMILYRELATIONSHIP_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.FAMILYRELATIONSHIP_CHILD3_ID = new SelectList(FAMILYRELATIONSHIP_CHILD3listItem, "Value", "Text");
            // END FAMILY RELATIONSHIP CODE

            // ADD GENDER CHILD 3
            model.GENDER_CHILD3_ID = DDL_GENDER_CHILD3();
            var GENDER_CHILD3 = DDL_GENDER_CHILD3();
            List<SelectListItem> GENDER_CHILD3listItem = new List<SelectListItem>();
            foreach (var item in GENDER_CHILD3)
            {
                GENDER_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.GENDER_CHILD3_ID = new SelectList(GENDER_CHILD3listItem, "Value", "Text");
            //END ADD GENDER CHILD 3

            // ADD MARITAL STATUS CHILD 3
            model.MARITALSTATUS_CHILD3_ID = DDL_MARITALSTATUS_CHILD3();
            var MARITALSTATUS_CHILD3 = DDL_MARITALSTATUS_CHILD3();
            List<SelectListItem> MARITALSTATUS_CHILD3listItem = new List<SelectListItem>();
            foreach (var item in MARITALSTATUS_CHILD3)
            {
                MARITALSTATUS_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.MARITALSTATUS_CHILD3_ID = new SelectList(MARITALSTATUS_CHILD3listItem, "Value", "Text");
            // END MARITAL STATUS 

            // NATIONALITY CITIZENSHIP CHILD 3
            model.CITIZENSHIP_CHILD3_ID = DDL_CITIZENSHIP_CHILD3();
            var CITIZENSHIP_CHILD3 = DDL_CITIZENSHIP_CHILD3();
            List<SelectListItem> CITIZENSHIP_CHILD3listItem = new List<SelectListItem>();
            foreach (var item in CITIZENSHIP_CHILD3)
            {
                CITIZENSHIP_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.CITIZENSHIP_CHILD3_ID = new SelectList(CITIZENSHIP_CHILD3listItem, "Value", "Text");
            // END NATIONALITY CITIZENSHIP
            model.Provinsi_FASKES_Child_3 = DDLSTATE_FASKES();
            model.Kotamadya_FASKES_Child_3 = DDLKOTAMADYA_FASKES_ALL();
            model.Kecamatan_FASKES_Child_3 = DDLKECAMATAN_FASKES_ALL();
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_3 = DDL_FASKES_TK1();

            model.Provinsi_GIGI_Child_3 = DDLSTATE_GIGI();
            model.Kotamadya_GIGI_Child_3 = DDLKOTAMADYA_GIGI_ALL();
            model.Kecamatan_GIGI_Child_3 = DDLKECAMATAN_GIGI_ALL();
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_3 = DDL_FASKES_GIGI();

            //END ADD ON CHILD 3 //
            model.Gender_Parent1 = DDLSex();
            model.Gender_Parent2 = DDLSex();

            // jenis mutasi EMPLOYEE
            model.JenisMutasiDDL = DDL_JENISMUTASI();
            var jenismutasi = DDL_JENISMUTASI();
            List<SelectListItem> JenisMutasilistItem = new List<SelectListItem>();
            foreach (var item in jenismutasi)
            {
                JenisMutasilistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.jenismutasiddl = new SelectList(JenisMutasilistItem, "Value", "Text");
            // end jenis mutasi EMPLOYEE

            // kode hubkel EMPLOYEE
            model.KodeHubKelDDL = DDL_KODEHUBKEL();
            var kodehubkel = DDL_KODEHUBKEL();
            List<SelectListItem> KodeHubkellistItem = new List<SelectListItem>();
            foreach (var item in kodehubkel)
            {
                KodeHubkellistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.kodehubkelddl = new SelectList(KodeHubkellistItem, "Value", "Text");
            // end kode hubkel EMPLOYEE

            // jenis kelamin EMPLOYEE
            model.JenisKelaminDDL = DDL_JENISKELAMIN();
            var jeniskelamin = DDL_JENISKELAMIN();
            List<SelectListItem> jeniskelaminlistItem = new List<SelectListItem>();
            foreach (var item in jeniskelamin)
            {
                jeniskelaminlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.jeniskelaminddl = new SelectList(jeniskelaminlistItem, "Value", "Text");
            // end jeniskelamin EMPLOYEE

            // status kawin EMPLOYEE
            model.StatusKawinDDL = DDL_STATUSKAWIN();
            var statuskawin = DDL_STATUSKAWIN();
            List<SelectListItem> statuskawinlistItem = new List<SelectListItem>();
            foreach (var item in statuskawin)
            {
                statuskawinlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.statuskawinddl = new SelectList(statuskawinlistItem, "Value", "Text");
            // END STATUS KAWIN EMPLOYEE


            // STATUS EMPLOYEE
            model.STATUSDDL = DDL_STATUS();
            var status = DDL_STATUS();
            List<SelectListItem> statuslistItem = new List<SelectListItem>();
            foreach (var item in status)
            {
                statuslistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.STATUSDDL = new SelectList(statuslistItem, "Value", "Text");
            // end STATUS EMPLOYEE

            // KEWARGANEGARAAN EMPLOYEE
            model.KEWARGANEGARAANDDL = DDL_KEWARGANEGARAAN();
            var KEWARGANEGARAANDDL = DDL_KEWARGANEGARAAN();
            List<SelectListItem> KEWARGANEGARAANlistItem = new List<SelectListItem>();
            foreach (var item in KEWARGANEGARAANDDL)
            {
                KEWARGANEGARAANlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.KEWARGANEGARAANDDL = new SelectList(KEWARGANEGARAANlistItem, "Value", "Text");
            // end KEWARGANEGARAAN EMPLOYEE



            string Nama = null;

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

            DataTable dtxxx = Common.ExecuteQuery("dbo.[sp_TMP_REV_GetDATA_Employee_Detail] '" + ID + "', '" + Nama + "'");
            if (dtxxx.Rows.Count > 0)
            {
                model.idx_rev = dtxxx.Rows[0]["idx"].ToString();
                model.IDPERSON = dtxxx.Rows[0]["Employee_ID"].ToString();
                model.JenisMutasi = dtxxx.Rows[0]["Jenis_Mutasi"].ToString();
                model.TglAktifBerlakuMutasi = dtxxx.Rows[0]["TglAktifBerlakuMutasi"].ToString();
                model.KodeHubKel = dtxxx.Rows[0]["Kode_HubKel"].ToString();
                model.TempatLahir = dtxxx.Rows[0]["Tempat_Lahir"].ToString();
                model.JenisKelamin = dtxxx.Rows[0]["Jenis_Kelamin"].ToString();
                model.StatusKawin = dtxxx.Rows[0]["Status_Kawin"].ToString();
                model.AlamatTempatTinggal = dtxxx.Rows[0]["Alamat_TempatTinggal"].ToString();
                model.RT2 = dtxxx.Rows[0]["RT"].ToString();
                model.RW2 = dtxxx.Rows[0]["RW"].ToString();
                model.KodePosAddn = dtxxx.Rows[0]["Kode_Pos"].ToString();
                model.kodekecamatan = dtxxx.Rows[0]["Kode_Kecamatan"].ToString();
                model.kodedesa  = dtxxx.Rows[0]["Kode_Desa"].ToString();
                model.FaskesTKICode = dtxxx.Rows[0]["Faskes_Tk_Code"].ToString();
                model.FASKESDRGCODE = dtxxx.Rows[0]["Faskes_drg_Code"].ToString();
                model.NOMORPESERTA = dtxxx.Rows[0]["No_TelpPeserta"].ToString();
                model.EMAILS = dtxxx.Rows[0]["Email"].ToString();
                model.IDPERSON = dtxxx.Rows[0]["ID_Person"].ToString();
                model.STATUS = dtxxx.Rows[0]["Status"].ToString();
                model.TMTKERJA = dtxxx.Rows[0]["TMT_Kerja"].ToString();
                model.KEWARGANEGARAAN = dtxxx.Rows[0]["Kewarganegaraan"].ToString();
                model.NOMORKARTU = dtxxx.Rows[0]["No_KartuAsuransi"].ToString();
                model.NAMAASURANSI = dtxxx.Rows[0]["Nama_Asuransi"].ToString();
                model.NOMORPASSPORT = dtxxx.Rows[0]["No_Passport"].ToString();
                model.NPWP2 = dtxxx.Rows[0]["NPWP"].ToString();
                model.fixedsalary = dtxxx.Rows[0]["FIXEDSALARY"].ToString();
                model.jobtittle = dtxxx.Rows[0]["JobTitle"].ToString();
                model.Provinsi_FASKES_ID = dtxxx.Rows[0]["Provinsi_Faskes_TK"].ToString();
                model.Kotamadya_FASKES_ID = dtxxx.Rows[0]["Kotamadya_Faskes_TK"].ToString();
                model.Kecamatan_FASKES_ID = dtxxx.Rows[0]["Kecamatan_Faskes_TK"].ToString();
                model.Provinsi_GIGI_ID = dtxxx.Rows[0]["Provinsi_Faskes_Gigi"].ToString();
                model.Kotamadya_GIGI_ID = dtxxx.Rows[0]["Kotamadya_Faskes_Gigi"].ToString();
                model.Kecamatan_GIGI_ID = dtxxx.Rows[0]["Kecamatan_Faskes_Gigi"].ToString();
            }

            DataTable dtxxxSPOUSE = Common.ExecuteQuery("dbo.[sp_TMP_REV_GetDATA_Employee_Detail_SPOUSE] '" + ID + "', '" + Nama + "'");
            if (dtxxxSPOUSE.Rows.Count > 0)
            {
                model.idx_rev_spouse = dtxxxSPOUSE.Rows[0]["idx"].ToString();
                model.EMPLID_SPOUSE = dtxxxSPOUSE.Rows[0]["Employee_ID"].ToString();
                model.JENISMUTASI_SPOUSE = dtxxxSPOUSE.Rows[0]["Jenis_Mutasi"].ToString();
                model.TglAktifBerlakuMutasi_SPOUSE = dtxxxSPOUSE.Rows[0]["TglAktifBerlakuMutasi"].ToString();
                model.FAMILYRELATIONSHIP_SPOUSE = dtxxxSPOUSE.Rows[0]["Kode_HubKel"].ToString();
                model.TEMPATLAHIR_SPOUSE = dtxxxSPOUSE.Rows[0]["Tempat_Lahir"].ToString();
                model.Gender_Spouse_ID = dtxxxSPOUSE.Rows[0]["Jenis_Kelamin"].ToString();
                model.MARITALSTATUS_SPOUSE = dtxxxSPOUSE.Rows[0]["Status_Kawin"].ToString();
                model.ADDRESS_SPOUSE = dtxxxSPOUSE.Rows[0]["Alamat_TempatTinggal"].ToString();
                model.RT_SPOUSE = dtxxxSPOUSE.Rows[0]["RT"].ToString();
                model.RW_SPOUSE = dtxxxSPOUSE.Rows[0]["RW"].ToString();
                model.POSTALCODE_SPOUSE = dtxxxSPOUSE.Rows[0]["Kode_Pos"].ToString();
                model.KECAMATANCODE_SPOUSE = dtxxxSPOUSE.Rows[0]["Kode_Kecamatan"].ToString();
                model.KELURAHANCODE_SPOUSE = dtxxxSPOUSE.Rows[0]["Kode_Desa"].ToString();
                model.FASKESCODETKI_SPOUSE = dtxxxSPOUSE.Rows[0]["Faskes_Tk_Code"].ToString();
                model.FASKESDRGCODE_SPOUSE = dtxxxSPOUSE.Rows[0]["Faskes_drg_Code"].ToString();
                model.MOBILEPHONE_SPOUSE = dtxxxSPOUSE.Rows[0]["No_TelpPeserta"].ToString();
                model.EMAIL_SPOUSE = dtxxxSPOUSE.Rows[0]["Email"].ToString();
                model.JOBTITTLE_SPOUSE = dtxxxSPOUSE.Rows[0]["JobTitle"].ToString();
                model.EMPLID_SPOUSE = dtxxxSPOUSE.Rows[0]["ID_Person"].ToString();
                model.EMPLSTATUS_SPOUSE = dtxxxSPOUSE.Rows[0]["Status"].ToString();
                model.TMTKERJA_SPOUSE = dtxxxSPOUSE.Rows[0]["TMT_Kerja"].ToString();
                model.CITIZENSHIP_SPOUSE = dtxxxSPOUSE.Rows[0]["Kewarganegaraan"].ToString();
                model.FIXEDSALARY_SPOUSE = dtxxxSPOUSE.Rows[0]["FIXEDSALARY"].ToString();
                model.INSURANCECARD_SPOUSE = dtxxxSPOUSE.Rows[0]["No_KartuAsuransi"].ToString();
                model.INSURANCENAME_SPOUSE = dtxxxSPOUSE.Rows[0]["Nama_Asuransi"].ToString();
                model.NPWP_SPOUSE = dtxxxSPOUSE.Rows[0]["NPWP"].ToString();
                model.PASSPORT_SPOUSE = dtxxxSPOUSE.Rows[0]["No_Passport"].ToString();
                model.Provinsi_FASKES_Spouse_ID = dtxxxSPOUSE.Rows[0]["Provinsi_Faskes_TK"].ToString();
                model.Kotamadya_FASKES_Spouse_ID = dtxxxSPOUSE.Rows[0]["Kotamadya_Faskes_TK"].ToString();
                model.Kecamatan_FASKES_Spouse_ID = dtxxxSPOUSE.Rows[0]["Kecamatan_Faskes_TK"].ToString();
                model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse_ID = dtxxxSPOUSE.Rows[0]["Nama_Faskes_TK1"].ToString();
                model.Provinsi_GIGI_Spouse_ID = dtxxxSPOUSE.Rows[0]["Provinsi_Faskes_Gigi"].ToString();
                model.Kotamadya_GIGI_Spouse_ID = dtxxxSPOUSE.Rows[0]["Kotamadya_Faskes_Gigi"].ToString();
                model.Kecamatan_GIGI_Spouse_ID = dtxxxSPOUSE.Rows[0]["Kecamatan_Faskes_Gigi"].ToString();
                model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse_ID = dtxxxSPOUSE.Rows[0]["Nama_Faskes_Gigi"].ToString();
                //Tambahan 5 September
                model.KecamatanID_Spouse = dtxxxSPOUSE.Rows[0]["Nama_Kec"].ToString();
                model.DesaID_Spouse = dtxxxSPOUSE.Rows[0]["Nama_Desa"].ToString();
            }

            DataTable dtxxxChild = Common.ExecuteQuery("dbo.[sp_TMP_REV_GetDATA_Employee_Detail_Child1] '" + ID + "', '" + Nama + "'");
            if (dtxxxChild.Rows.Count > 0)
            {
                model.idx_rev_child_1 = dtxxxChild.Rows[0]["idx"].ToString();
                model.JENISMUTASI_CHILD1 = dtxxxChild.Rows[0]["Jenis_Mutasi"].ToString();
                model.TglAktifBerlakuMutasi_CHILD1 = dtxxxChild.Rows[0]["TglAktifBerlakuMutasi"].ToString();
                model.FAMILYRELATIONSHIP_CHILD1 = dtxxxChild.Rows[0]["Kode_HubKel"].ToString();
                model.TEMPATLAHIR_CHILD1 = dtxxxChild.Rows[0]["Tempat_Lahir"].ToString();
                model.GENDER_CHILD1 = dtxxxChild.Rows[0]["Jenis_Kelamin"].ToString();
                model.MARITALSTATUS_CHILD1 = dtxxxChild.Rows[0]["Status_Kawin"].ToString();
                model.ADDRESS_CHILD1 = dtxxxChild.Rows[0]["Alamat_TempatTinggal"].ToString();
                model.RT_CHILD1 = dtxxxChild.Rows[0]["RT"].ToString();
                model.RW_CHILD1 = dtxxxChild.Rows[0]["RW2"].ToString();
                model.POSTALCODE_CHILD1 = dtxxxChild.Rows[0]["Kode_Pos"].ToString();
                model.KECAMATANCODE_CHILD1 = dtxxxChild.Rows[0]["Kode_Kecamatan"].ToString();
                model.KELURAHANCODE_CHILD1 = dtxxxChild.Rows[0]["Kode_Desa"].ToString();
                model.FASKESCODETKI_CHILD1 = dtxxxChild.Rows[0]["Kode_Faskes_Tk1"].ToString();
                model.FASKESDRGCODE_CHILD1 = dtxxxChild.Rows[0]["Kode_Faskes_DokterGigi"].ToString();
                model.MOBILEPHONE_CHILD1 = dtxxxChild.Rows[0]["No_TelpPeserta"].ToString();
                model.EMAIL_CHILD1 = dtxxxChild.Rows[0]["Email"].ToString();
                model.EMPLSTATUS_CHILD1 = dtxxxChild.Rows[0]["Status"].ToString();
                model.TMTKERJA_CHILD1 = dtxxxChild.Rows[0]["TMT_Kerja"].ToString();
                model.CITIZENSHIP_CHILD1 = dtxxxChild.Rows[0]["Kewarganegaraan"].ToString();
                model.INSURANCECARD_CHILD1 = dtxxxChild.Rows[0]["No_KartuAsuransi"].ToString();
                model.INSURANCENAME_CHILD1 = dtxxxChild.Rows[0]["Nama_Asuransi"].ToString();
                model.PASSPORT_CHILD1 = dtxxxChild.Rows[0]["No_Passport"].ToString();
                model.NPWP_CHILD1 = dtxxxChild.Rows[0]["NPWP"].ToString();
                model.FIXEDSALARY_CHILD1 = dtxxxChild.Rows[0]["FIXEDSALARY"].ToString();
                model.JOBTITTLE_CHILD1 = dtxxxChild.Rows[0]["JobTitle"].ToString();
                model.EMPLID_CHILD1 = dtxxxChild.Rows[0]["ID_Person2"].ToString();
                model.Provinsi_FASKES_ID_child_1 = dtxxxChild.Rows[0]["Provinsi_Faskes_TK"].ToString();
                model.Kotamadya_FASKES_ID_child_1 = dtxxxChild.Rows[0]["Kotamadya_Faskes_TK"].ToString();
                model.Kecamatan_FASKES_ID_child_1 = dtxxxChild.Rows[0]["Kecamatan_Faskes_TK"].ToString();
                model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 = dtxxxChild.Rows[0]["Nama_Faskes_TK1"].ToString();
                model.Provinsi_GIGI_ID_child_1 = dtxxxChild.Rows[0]["Provinsi_Faskes_Gigi"].ToString();
                model.Kotamadya_GIGI_ID_child_1 = dtxxxChild.Rows[0]["Kotamadya_Faskes_Gigi"].ToString();
                model.Kecamatan_GIGI_ID_child_1 = dtxxxChild.Rows[0]["Kecamatan_Faskes_Gigi"].ToString();
                model.FASKESDRGCODE_CHILD1 = dtxxxChild.Rows[0]["Nama_Faskes_Gigi"].ToString();

                //Tambahan 5 September
                model.KecamatanID_Ch1 = dtxxxChild.Rows[0]["Nama_Kec"].ToString();
                model.DesaID_Ch1 = dtxxxChild.Rows[0]["Nama_Desa"].ToString();
            }

            DataTable dtxxxChild2 = Common.ExecuteQuery("dbo.[sp_TMP_REV_GetDATA_Employee_Detail_Child2] '" + ID + "', '" + Nama + "'");
            if (dtxxxChild2.Rows.Count > 0)
            {
                model.idx_rev_child_2 = dtxxxChild2.Rows[0]["idx"].ToString();
                model.JENISMUTASI_CHILD2 = dtxxxChild2.Rows[0]["Jenis_Mutasi"].ToString();
                model.TglAktifBerlakuMutasi_CHILD2 = dtxxxChild2.Rows[0]["TglAktifBerlakuMutasi"].ToString();
                model.FAMILYRELATIONSHIP_CHILD2 = dtxxxChild2.Rows[0]["Kode_HubKel"].ToString();
                model.TEMPATLAHIR_CHILD2 = dtxxxChild2.Rows[0]["Tempat_Lahir"].ToString();
                model.GENDER_CHILD2 = dtxxxChild2.Rows[0]["Jenis_Kelamin"].ToString();
                model.MARITALSTATUS_CHILD2 = dtxxxChild2.Rows[0]["Status_Kawin"].ToString();
                model.ADDRESS_CHILD2 = dtxxxChild2.Rows[0]["Alamat_TempatTinggal"].ToString();
                model.RT_CHILD2 = dtxxxChild2.Rows[0]["RT"].ToString();
                model.RW_CHILD2 = dtxxxChild2.Rows[0]["RW"].ToString();
                model.POSTALCODE_CHILD2 = dtxxxChild2.Rows[0]["Kode_Pos"].ToString();
                model.KECAMATANCODE_CHILD2 = dtxxxChild2.Rows[0]["Kode_Kecamatan"].ToString();
                model.KELURAHANCODE_CHILD2 = dtxxxChild2.Rows[0]["Kode_Desa"].ToString();
                model.FASKESCODETKI_CHILD2 = dtxxxChild2.Rows[0]["Kode_Faskes_Tk1"].ToString();
                model.FASKESDRGCODE_CHILD2 = dtxxxChild2.Rows[0]["Kode_Faskes_DokterGigi"].ToString();
                model.MOBILEPHONE_CHILD2 = dtxxxChild2.Rows[0]["No_TelpPeserta"].ToString();
                model.EMAIL_CHILD2 = dtxxxChild2.Rows[0]["Email"].ToString();
                model.EMPLSTATUS_CHILD2 = dtxxxChild2.Rows[0]["Status"].ToString();
                model.TMTKERJA_CHILD2 = dtxxxChild2.Rows[0]["TMT_Kerja"].ToString();
                model.CITIZENSHIP_CHILD2 = dtxxxChild2.Rows[0]["Kewarganegaraan"].ToString();
                model.INSURANCECARD_CHILD2 = dtxxxChild2.Rows[0]["No_KartuAsuransi"].ToString();
                model.INSURANCENAME_CHILD2 = dtxxxChild2.Rows[0]["Nama_Asuransi"].ToString();
                model.PASSPORT_CHILD2 = dtxxxChild2.Rows[0]["No_Passport"].ToString();
                model.NPWP_CHILD2 = dtxxxChild2.Rows[0]["NPWP"].ToString();
                model.FIXEDSALARY_CHILD2 = dtxxxChild2.Rows[0]["FIXEDSALARY"].ToString();
                model.JOBTITTLE_CHILD2 = dtxxxChild2.Rows[0]["JobTitle"].ToString();
                model.EMPLID_CHILD2 = dtxxxChild2.Rows[0]["ID_Person2"].ToString();
                model.Provinsi_FASKES_ID_child_2 = dtxxxChild2.Rows[0]["Provinsi_Faskes_TK"].ToString();
                model.Kotamadya_FASKES_ID_child_2 = dtxxxChild2.Rows[0]["Kotamadya_Faskes_TK"].ToString();
                model.Kecamatan_FASKES_ID_child_2 = dtxxxChild2.Rows[0]["Kecamatan_Faskes_TK"].ToString();
                model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 = dtxxxChild2.Rows[0]["Nama_Faskes_TK1"].ToString();
                model.Provinsi_GIGI_ID_child_2 = dtxxxChild2.Rows[0]["Provinsi_Faskes_Gigi"].ToString();
                model.Kotamadya_GIGI_ID_child_2 = dtxxxChild2.Rows[0]["Kotamadya_Faskes_Gigi"].ToString();
                model.Kecamatan_GIGI_ID_child_2 = dtxxxChild2.Rows[0]["Kecamatan_Faskes_Gigi"].ToString();
                model.FASKESDRGCODE_CHILD2 = dtxxxChild2.Rows[0]["Nama_Faskes_Gigi"].ToString();

                //Tambahan 5 September
                model.KecamatanID_Ch2 = dtxxxChild2.Rows[0]["Nama_Kec"].ToString();
                model.DesaID_Ch2 = dtxxxChild2.Rows[0]["Nama_Desa"].ToString();
            }

            DataTable dtxxxChild3 = Common.ExecuteQuery("dbo.[sp_TMP_REV_GetDATA_Employee_Detail_Child3] '" + ID + "', '" + Nama + "'");
            if (dtxxxChild3.Rows.Count > 0)
            {
                model.idx_rev_child_3 = dtxxxChild3.Rows[0]["idx"].ToString();
                model.JENISMUTASI_CHILD3 = dtxxxChild3.Rows[0]["Jenis_Mutasi"].ToString();
                model.TglAktifBerlakuMutasi_CHILD3 = dtxxxChild3.Rows[0]["TglAktifBerlakuMutasi"].ToString();
                model.FAMILYRELATIONSHIP_CHILD3 = dtxxxChild3.Rows[0]["Kode_HubKel"].ToString();
                model.TEMPATLAHIR_CHILD3 = dtxxxChild3.Rows[0]["Tempat_Lahir"].ToString();
                model.GENDER_CHILD3 = dtxxxChild3.Rows[0]["Jenis_Kelamin"].ToString();
                model.MARITALSTATUS_CHILD3 = dtxxxChild3.Rows[0]["Status_Kawin"].ToString();
                model.ADDRESS_CHILD3 = dtxxxChild3.Rows[0]["Alamat_TempatTinggal"].ToString();
                model.RT_CHILD3 = dtxxxChild3.Rows[0]["RT"].ToString();
                model.RW_CHILD3 = dtxxxChild3.Rows[0]["RW"].ToString();
                model.POSTALCODE_CHILD3 = dtxxxChild3.Rows[0]["Kode_Pos"].ToString();
                model.KECAMATANCODE_CHILD3 = dtxxxChild3.Rows[0]["Kode_Kecamatan"].ToString();
                model.KELURAHANCODE_CHILD3 = dtxxxChild3.Rows[0]["Kode_Desa"].ToString();
                model.FASKESCODETKI_CHILD3 = dtxxxChild3.Rows[0]["Kode_Faskes_Tk1"].ToString();
                model.FASKESDRGCODE_CHILD3 = dtxxxChild3.Rows[0]["Kode_Faskes_DokterGigi"].ToString();
                model.MOBILEPHONE_CHILD3 = dtxxxChild3.Rows[0]["No_TelpPeserta"].ToString();
                model.EMAIL_CHILD3 = dtxxxChild3.Rows[0]["Email"].ToString();
                model.EMPLSTATUS_CHILD3 = dtxxxChild3.Rows[0]["Status"].ToString();
                model.TMTKERJA_CHILD3 = dtxxxChild3.Rows[0]["TMT_Kerja"].ToString();
                model.CITIZENSHIP_CHILD3 = dtxxxChild3.Rows[0]["Kewarganegaraan"].ToString();
                model.INSURANCECARD_CHILD3 = dtxxxChild3.Rows[0]["No_KartuAsuransi"].ToString();
                model.INSURANCENAME_CHILD3 = dtxxxChild3.Rows[0]["Nama_Asuransi"].ToString();
                model.PASSPORT_CHILD3 = dtxxxChild3.Rows[0]["No_Passport"].ToString();
                model.NPWP_CHILD3 = dtxxxChild3.Rows[0]["NPWP"].ToString();
                model.FIXEDSALARY_CHILD3 = dtxxxChild3.Rows[0]["FIXEDSALARY"].ToString();
                model.JOBTITTLE_CHILD3 = dtxxxChild3.Rows[0]["JobTitle"].ToString();
                model.EMPLID_CHILD3 = dtxxxChild3.Rows[0]["ID_Person2"].ToString();
                model.Provinsi_FASKES_ID_child_3 = dtxxxChild3.Rows[0]["Provinsi_Faskes_TK"].ToString();
                model.Kotamadya_FASKES_ID_child_3 = dtxxxChild3.Rows[0]["Kotamadya_Faskes_TK"].ToString();
                model.Kecamatan_FASKES_ID_child_3 = dtxxxChild3.Rows[0]["Kecamatan_Faskes_TK"].ToString();
                model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 = dtxxxChild3.Rows[0]["Nama_Faskes_TK1"].ToString();
                model.Provinsi_GIGI_ID_child_3 = dtxxxChild3.Rows[0]["Provinsi_Faskes_Gigi"].ToString();
                model.Kotamadya_GIGI_ID_child_3 = dtxxxChild3.Rows[0]["Kotamadya_Faskes_Gigi"].ToString();
                model.Kecamatan_GIGI_ID_child_3 = dtxxxChild3.Rows[0]["Kecamatan_Faskes_Gigi"].ToString();
                model.FASKESDRGCODE_CHILD3 = dtxxxChild3.Rows[0]["Nama_Faskes_Gigi"].ToString();

                //Tambahan 5 September
                model.KecamatanID_Ch3 = dtxxxChild3.Rows[0]["Nama_Kec"].ToString();
                model.DesaID_Ch3 = dtxxxChild3.Rows[0]["Nama_Desa"].ToString();
            }
            //Masih 5 september
            model.Provinsi_FASKES = DDLSTATE_FASKES();
            model.Kotamadya_FASKES = DDLKOTAMADYA_FASKES_BYPROV(model.Provinsi_FASKES_ID);
            model.Kecamatan_FASKES = DDLKECAMATAN_FASKES_BYKOTA(model.Provinsi_FASKES_ID, model.Kotamadya_FASKES_ID);
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama = DDL_FASKES_TK1_BYKEC(model.Provinsi_FASKES_ID, model.Kotamadya_FASKES_ID, model.Kecamatan_FASKES_ID);

            model.Provinsi_GIGI = DDLSTATE_GIGI();
            model.Kotamadya_GIGI = DDLKOTAMADYA_GIGI_BYPROV(model.Provinsi_GIGI_ID);
            model.Kecamatan_GIGI = DDLKECAMATAN_GIGI_BYKOT(model.Provinsi_GIGI_ID, model.Kotamadya_GIGI_ID);
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi = DDL_FASKES_GIGI_APPROVAL(model.Provinsi_GIGI_ID, model.Kotamadya_GIGI_ID, model.Kecamatan_GIGI_ID);

            // Spouse
            model.Provinsi_FASKES_Spouse = DDLSTATE_FASKES();
            model.Kotamadya_FASKES_Spouse = DDLKOTAMADYA_FASKES_BYPROV(model.Provinsi_FASKES_Spouse_ID);
            model.Kecamatan_FASKES_Spouse = DDLKECAMATAN_FASKES_BYKOTA(model.Provinsi_FASKES_Spouse_ID, model.Kotamadya_FASKES_Spouse_ID);
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse = DDL_FASKES_GIGI_APPROVAL(model.Provinsi_FASKES_Spouse_ID, model.Kotamadya_FASKES_Spouse_ID, model.KecamatanID_Spouse); ;

            model.Provinsi_GIGI_Spouse = DDLSTATE_GIGI();
            model.Kotamadya_GIGI_Spouse = DDLKOTAMADYA_GIGI_BYPROV(model.Provinsi_GIGI_Spouse_ID);
            model.Kecamatan_GIGI_Spouse = DDLKECAMATAN_GIGI_BYKOT(model.Provinsi_GIGI_Spouse_ID, model.Kotamadya_GIGI_Spouse_ID);
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse = DDL_FASKES_GIGI_APPROVAL(model.Provinsi_GIGI_Spouse_ID, model.Kotamadya_GIGI_Spouse_ID, model.Kecamatan_GIGI_Spouse_ID);

            // Child
            model.Provinsi_FASKES_Child_1 = DDLSTATE_FASKES();
            model.Kotamadya_FASKES_Child_1 = DDLKOTAMADYA_FASKES_BYPROV(model.Provinsi_FASKES_ID_child_1);
            model.Kecamatan_FASKES_Child_1 = DDLKECAMATAN_FASKES_BYKOTA(model.Provinsi_FASKES_ID_child_1, model.Kotamadya_FASKES_ID_child_1);
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_1 = DDL_FASKES_GIGI_APPROVAL(model.Provinsi_FASKES_ID_child_1, model.Kotamadya_FASKES_ID_child_1, model.KecamatanID_Ch1); ;

            model.Provinsi_GIGI_Child_1 = DDLSTATE_GIGI();
            model.Kotamadya_GIGI_Child_1 = DDLKOTAMADYA_GIGI_BYPROV(model.Provinsi_GIGI_ID_child_1);
            model.Kecamatan_GIGI_Child_1 = DDLKECAMATAN_GIGI_BYKOT(model.Provinsi_GIGI_ID_child_1, model.Kotamadya_GIGI_ID_child_1);
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_1 = DDL_FASKES_GIGI_APPROVAL(model.Provinsi_GIGI_ID_child_1, model.Kotamadya_GIGI_ID_child_1, model.Kecamatan_GIGI_ID_child_1);

            model.Provinsi_FASKES_Child_2 = DDLSTATE_FASKES();
            model.Kotamadya_FASKES_Child_2 = DDLKOTAMADYA_FASKES_BYPROV(model.Provinsi_FASKES_ID_child_2);
            model.Kecamatan_FASKES_Child_2 = DDLKECAMATAN_FASKES_BYKOTA(model.Provinsi_FASKES_ID_child_2, model.Kotamadya_FASKES_ID_child_2);
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_2 = DDL_FASKES_GIGI_APPROVAL(model.Provinsi_FASKES_ID_child_2, model.Kotamadya_FASKES_ID_child_2, model.KecamatanID_Ch2); ;

            model.Provinsi_GIGI_Child_2 = DDLSTATE_GIGI();
            model.Kotamadya_GIGI_Child_2 = DDLKOTAMADYA_GIGI_BYPROV(model.Provinsi_GIGI_ID_child_2);
            model.Kecamatan_GIGI_Child_2 = DDLKECAMATAN_GIGI_BYKOT(model.Provinsi_GIGI_ID_child_2, model.Kotamadya_GIGI_ID_child_2);
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_2 = DDL_FASKES_GIGI_APPROVAL(model.Provinsi_GIGI_ID_child_2, model.Kotamadya_GIGI_ID_child_2, model.Kecamatan_GIGI_ID_child_2);

            model.Provinsi_FASKES_Child_3 = DDLSTATE_FASKES();
            model.Kotamadya_FASKES_Child_3 = DDLKOTAMADYA_FASKES_BYPROV(model.Provinsi_FASKES_ID_child_3);
            model.Kecamatan_FASKES_Child_3 = DDLKECAMATAN_FASKES_BYKOTA(model.Provinsi_FASKES_ID_child_3, model.Kotamadya_FASKES_ID_child_3);
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_3 = DDL_FASKES_GIGI_APPROVAL(model.Provinsi_FASKES_ID_child_3, model.Kotamadya_FASKES_ID_child_3, model.KecamatanID_Ch3); ;

            model.Provinsi_GIGI_Child_3 = DDLSTATE_GIGI();
            model.Kotamadya_GIGI_Child_3 = DDLKOTAMADYA_FASKES_BYPROV(model.Provinsi_FASKES_ID_child_2);
            model.Kecamatan_GIGI_Child_3 = DDLKECAMATAN_GIGI_BYKOT(model.Provinsi_GIGI_ID_child_3, model.Kotamadya_GIGI_ID_child_3);
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_3 = DDL_FASKES_GIGI_APPROVAL(model.Provinsi_GIGI_ID_child_3, model.Kotamadya_GIGI_ID_child_3, model.Kecamatan_GIGI_ID_child_3);

            //KECAMATAN
            model.Kecamatanddl = DDLKecamatanList("");
            model.Desaddl = DDLDesaList(model.Kecamatan_ID);

            model.Kecamatanddl_Spouse = DDLKecamatanList("");
            model.Desaddl_Spouse = DDLDesaList(model.KecamatanID_Spouse);

            model.Kecamatanddl_Ch1 = DDLKecamatanList("");
            model.Desaddl_Ch1 = DDLDesaList(model.KecamatanID_Ch1);

            model.Kecamatanddl_Ch2 = DDLKecamatanList("");
            model.Desaddl_Ch2 = DDLDesaList(model.KecamatanID_Ch2);

            model.Kecamatanddl_Ch3 = DDLKecamatanList("");
            model.Desaddl_Ch3 = DDLDesaList(model.KecamatanID_Ch3);
            DataTable dt = Common.ExecuteQuery("dbo.[sp_TMP_GetDATA_Employee_Detail] '" + ID + "', '" + Nama + "'");
            if (dt.Rows.Count > 0)
            {
                model.idx = dt.Rows[0]["idx"].ToString();
                model.EmployeeID = dt.Rows[0]["EmployeeID"].ToString();
                model.Gender_ID = dt.Rows[0]["Gender"].ToString();
                model.EmployeeName = dt.Rows[0]["EmployeeName"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.DepartmentID1 = dt.Rows[0]["Department"].ToString();
                model.EntityID = dt.Rows[0]["Entity"].ToString();
                model.JobTitleID = dt.Rows[0]["Job_Title"].ToString();
                //model.GradeID = Common.Decrypt(dt.Rows[0]["Grade"].ToString());
                model.GradeID = (dt.Rows[0]["Grade"].ToString());
                //model.HiredDate = (dt.Rows[0]["Hiredate"].ToString()).ToString();
                model.GradeID = (dt.Rows[0]["Grade"].ToString());

                model.ReportingTo = dt.Rows[0]["Reporting_To"].ToString();
                model.KTP = dt.Rows[0]["NIK"].ToString();
                model.NPWP = dt.Rows[0]["NPWP"].ToString();
                model.KK = dt.Rows[0]["No_KK"].ToString();
                model.KK_Spouse = dt.Rows[0]["KK_Spouse"].ToString();
                model.MarriedDate = dt.Rows[0]["Married_Date"].ToString();
                model.MaritalStatus = dt.Rows[0]["Marital_Status"].ToString();
                //model.Education = dt.Rows[0]["Education"].ToString();
                model.Spouse = dt.Rows[0]["Spouse"].ToString();
                model.PlaceBirth_ID = dt.Rows[0]["Place_Birth"].ToString();
                model.Birthdate = dt.Rows[0]["Birthdate"].ToString();
                model.Nationality_ID = dt.Rows[0]["Nationality"].ToString();
                model.Religion_ID = dt.Rows[0]["RELIGION"].ToString();
                model.Home_Address = dt.Rows[0]["Home_Address"].ToString();
                model.City_ID = dt.Rows[0]["City"].ToString();
                model.Kodepos = dt.Rows[0]["Kodepos"].ToString();
                model.RT = dt.Rows[0]["RT"].ToString();
                model.RW = dt.Rows[0]["RW"].ToString();
                model.State_ID = dt.Rows[0]["State"].ToString();
                model.Kotamadya_ID = dt.Rows[0]["KOTAMADYA"].ToString();
                model.Kecamatan_ID = dt.Rows[0]["KECAMATAN"].ToString();
                model.Kelurahan_ID = dt.Rows[0]["Kelurahan"].ToString();
                model.Physical_Address = dt.Rows[0]["Physical_Address"].ToString();
                model.Personal_Email = dt.Rows[0]["Personal_Email"].ToString();
                model.Home_Phone = dt.Rows[0]["Home_Phone"].ToString();
                model.Handphone = dt.Rows[0]["Handphone"].ToString();
                model.Payroll_Bank = dt.Rows[0]["Payroll_Bank"].ToString();
                model.Payroll_Branch = dt.Rows[0]["Payroll_Branch"].ToString();
                model.Payroll_Account_No = dt.Rows[0]["Payroll_AccountNo"].ToString();
                model.Payroll_Accoun_Name = dt.Rows[0]["Payroll_Account_Name"].ToString();
                model.DPLK_No_Peserta = dt.Rows[0]["DPLK_NoPeserta"].ToString();
                model.DPLK_Joint_Date = dt.Rows[0]["DPLK_Join_Date"].ToString();
                model.DPLK_Saldo = dt.Rows[0]["DPLK_Saldo"].ToString();
                model.Health_Plan_Membership_No_Polis = dt.Rows[0]["Health_Plan_Membership_NoPolis"].ToString();
                model.Health_Plan_Membership_No_Peserta = dt.Rows[0]["Health_Plan_Membership_NoPeserta"].ToString();
                model.Health_Plan_Membership_No_Kartu = dt.Rows[0]["Health_Plan_Membership_NoKartu"].ToString();
                model.Health_Plan_Benefit_Rawat_Inap = dt.Rows[0]["Health_Plan_Benefit_Rawat_Inap"].ToString();
                model.Health_Plan_Benefit_Rawat_Jalan = dt.Rows[0]["Health_Plan_Benefit_Rawat_Jalan"].ToString();
                model.Health_Plan_Benefit_Persalinan = dt.Rows[0]["Health_Plan_Benefit_Persalinan"].ToString();
                model.Health_Plan_Benefit_Gigi = dt.Rows[0]["Health_Plan_Benefit_Gigi"].ToString();
                model.Health_Plan_Benefit_Kacamata = dt.Rows[0]["Health_Plan_Benefit_Kacamata"].ToString();
                model.BPJS_Ketenagakerjaan_ID = dt.Rows[0]["BPJS_Ketenagakerjaan_ID"].ToString();
                model.BPJS_Pensiun_ID = dt.Rows[0]["BPJS_Pensiun_ID"].ToString();

                model.BPJS_JoinDate = dt.Rows[0]["BPJS_JoinDate"].ToString();

                model.Class = dt.Rows[0]["Class"].ToString();
                model.Class_Spouse = dt.Rows[0]["Class_Spouse"].ToString();


                model.BPJS_Kesehatan_Active = dt.Rows[0]["BPJS_Kesehatan_Active"].ToString();
                model.BPJS_Kesehatan_Active_Spouse = dt.Rows[0]["BPJS_Kesehatan_Active_Spouse"].ToString();
                model.BPJS_Kesehatan_Active_Child_1 = dt.Rows[0]["BPJS_Kesehatan_Active_Child_1"].ToString();
                model.BPJS_Kesehatan_Active_Child_2 = dt.Rows[0]["BPJS_Kesehatan_Active_Child_2"].ToString();
                model.BPJS_Kesehatan_Active_Child_3 = dt.Rows[0]["BPJS_Kesehatan_Active_Child_3"].ToString();

                model.BPJS_Kesehatan_ID = dt.Rows[0]["BPJS_Kesehatan_ID"].ToString();
                model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID = dt.Rows[0]["BPJS_Kesehatan_Faskes_Tingkat_Pertama"].ToString();
                model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID = dt.Rows[0]["BPJS_Kesehatan_Faskes_Dokter_Gigi"].ToString();
                model.Attach_File_Married = dt.Rows[0]["Attach_File_Married"].ToString();
                model.Attach_File_Education = dt.Rows[0]["Attach_File_Education"].ToString();
                model.FileName_Married = dt.Rows[0]["FileName_Married"].ToString();
                model.Attach_File_Payroll = dt.Rows[0]["Attach_File_Payroll"].ToString();
                model.Univ_Name = dt.Rows[0]["Univ_Name"].ToString();
                model.Education = dt.Rows[0]["Education"].ToString();
                model.FileName_Education = dt.Rows[0]["FileName_Education"].ToString();
                model.FileName_Payroll = dt.Rows[0]["FileName_Payroll"].ToString();
                model.Remark = dt.Rows[0]["Remark"].ToString();


                Session["FileName_Marriedx"] = dt.Rows[0]["FileName_Married"].ToString();
                Session["FileName_Educationx"] = dt.Rows[0]["FileName_Education"].ToString();
                Session["FilePath_Marriedx"] = dt.Rows[0]["Attach_File_Married"].ToString();
                Session["FilePath_Educationx"] = dt.Rows[0]["Attach_File_Education"].ToString();
                Session["EntityEmpl"] = dt.Rows[0]["Entity"].ToString();


                Session["FileName_Payrollx"] = dt.Rows[0]["FileName_Payroll"].ToString();
                Session["FilePath_Payrollx"] = dt.Rows[0]["Attach_File_Payroll"].ToString();

                model.Full_Name_Spouse = dt.Rows[0]["Full_Name"].ToString();
                model.Birthdate_Spouse = dt.Rows[0]["Birthdate_SPOUSE"].ToString();
                model.Gender_Spouse_ID = dt.Rows[0]["Gender_Spouse"].ToString();
                model.NIK_Spouse = dt.Rows[0]["NIK_Spouse"].ToString();
                model.Handphone_Spouse = dt.Rows[0]["Handphone_Spouse"].ToString();
                model.Health_Plan_Membership_No_Polis_Spouse = dt.Rows[0]["Health_Plan_Membership_NoPolis_Spouse"].ToString();
                model.Health_Plan_Membership_No_Peserta_Spouse = dt.Rows[0]["Health_Plan_Membership_NoPeserta_Spouse"].ToString();
                model.Health_Plan_Membership_No_Kartu_Spouse = dt.Rows[0]["Health_Plan_Membership_NoKartu_Spouse"].ToString();
                model.Health_Plan_Benefit_Rawat_Inap_Spouse = dt.Rows[0]["Health_Plan_Benefit_Rawat_Inap_Spouse"].ToString();
                model.Health_Plan_Benefit_Rawat_Jalan_Spouse = dt.Rows[0]["Health_Plan_Benefit_Rawat_Jalan_Spouse"].ToString();
                model.Health_Plan_Benefit_Persalinan_Spouse = dt.Rows[0]["Health_Plan_Benefit_Persalinan_Spouse"].ToString();
                model.Health_Plan_Benefit_Gigi_Spouse = dt.Rows[0]["Health_Plan_Benefit_Gigi_Spouse"].ToString();
                model.Health_Plan_Benefit_Kacamata_Spouse = dt.Rows[0]["Health_Plan_Benefit_Kacamata_Spouse"].ToString();
                model.BPJS_Kesehatan_Spouse_ID = dt.Rows[0]["BPJS_Kesehatan_ID_SPOUSE"].ToString();
                model.idx_spouse = dt.Rows[0]["idx_Spouse"].ToString();

                model.idx_parent1 = dt.Rows[0]["IDX_PARENT"].ToString();
                model.Full_Name_Parent1 = dt.Rows[0]["Full_Name_Parent1"].ToString();
                model.Birthdate_Parent1 = dt.Rows[0]["Birthdate_Parent1"].ToString();
                model.Gender_ID_Parent1 = dt.Rows[0]["Gender_Parent1"].ToString();
                model.Handphone_Parent1 = dt.Rows[0]["Handphone_Parent1"].ToString();

                model.idx_parent2 = dt.Rows[0]["IDX_PARENT"].ToString();
                model.Full_Name_Parent2 = dt.Rows[0]["Full_Name_Parent2"].ToString();
                model.Birthdate_Parent2 = dt.Rows[0]["Birthdate_Parent2"].ToString();
                model.Gender_ID_Parent2 = dt.Rows[0]["Gender_Parent2"].ToString();
                model.Handphone_Parent2 = dt.Rows[0]["Handphone_Parent2"].ToString();

                model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse_ID = dt.Rows[0]["BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse"].ToString();

                model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse_ID = dt.Rows[0]["BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse"].ToString();

                //var PhotoJPG = ("~/Images/Foto/" + model.EntityID + "/" + model.EmployeeID + " - " + model.EmployeeName + ".jpg");
                //if (System.IO.File.Exists(PhotoJPG))
                //{
                //    model.ImageURL = PhotoJPG;
                //}
                //else
                //{
                //    model.ImageURL = ("~/Images/Photo");
                //}

                //var PhotoJPG = ("~/Images/Foto/" + "Employee" + "/" + model.EmployeeID + ".jpg");
                //if (System.IO.File.Exists(PhotoJPG))
                //{
                //    model.ImageURL = PhotoJPG;
                //}
                //var PhotoPNG = "~/Images/Foto/" + model.EntityID + "/" + model.EmployeeID + ".png".ToUpper();
                //if (System.IO.File.Exists(PhotoPNG))
                //{
                //    model.ImageURL = PhotoPNG;
                //}
                //var PhotoJPEG = "~/Images/Foto/" + model.EntityID + "/" + model.EmployeeID + ".jpeg".ToUpper();
                //if (System.IO.File.Exists(PhotoJPEG))
                //{
                //    model.ImageURL = PhotoJPEG;
                //}
                //model.ImageURL = PhotoJPG;

                var PhotoJPG = ("~/Images/Foto/" + "Employee" + "/" + model.EmployeeID + " - " + model.EmployeeName + ".jpg");

                if (System.IO.File.Exists(PhotoJPG))
                {
                    model.ImageURL = PhotoJPG;
                }
                //else
                //{
                //    model.ImageURL = ("~/Images/Photo");
                //}
                model.ImageURL = PhotoJPG;

                //MarriedFile
                var Authopp = PathProd;
                var FileMarried = "/Images/Married" + "/" + model.EntityID + "/" + model.EmployeeID + "_Married" + ".pdf".ToUpper();
                //var FileMarried = Server.MapPath("Images\\");
                Session["Autho"] = PathProd.Replace("http://wrbmdtapp01/", "");
                Session["FileNameMarried"] = model.EmployeeID + "_Married" + ".pdf".ToUpper();

                if (System.IO.File.Exists(FileMarried))
                {
                    model.Attach_File_Married = FileMarried;
                }
                model.Attach_File_Married = Authopp + FileMarried;

                //EducationFile
                var FileEdu = "/Images/Education" + model.EntityID + "/" + model.EmployeeID + "/" + "_Education" + ".pdf".ToUpper();
                Session["Autho"] = PathProd.Replace("http://wrbmdtapp01/", "");
                Session["FileNameEducation"] = model.EmployeeID + "_Education" + ".pdf".ToUpper();

                if (System.IO.File.Exists(FileEdu))
                {
                    model.Attach_File_Education = FileEdu;
                }
                model.Attach_File_Education = FileEdu;

                //PayrollFile
                var FilePay = "/Images/Payroll" + Session["ShortEntity"] + "/" + model.EmployeeID + "/" + "_Payroll" + ".pdf".ToUpper();
                Session["Autho"] = Authopp;
                Session["FileNamePayroll"] = model.EmployeeID + "_Payroll" + ".pdf".ToUpper();

                if (System.IO.File.Exists(FilePay))
                {
                    model.Attach_File_Payroll = FilePay;
                }
                model.Attach_File_Payroll = FilePay;
                
                if (model.FaskesTKICode != "")
                {
                    DataTable dt2 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_FASKES_BY_NAMA_FASKES] '" + model.FaskesTKICode + "'");
                    if (dt2.Rows.Count > 0)
                    {
                        //model.Provinsi_FASKES_ID = dt2.Rows[0]["PROVINSI"].ToString();
                        //model.Kotamadya_FASKES_ID = dt2.Rows[0]["KOTAMADYA"].ToString();
                        //model.Kecamatan_FASKES_ID = dt2.Rows[0]["KECAMATAN"].ToString();
                        //model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID = dt2.Rows[0]["NAMA_FASKES_TINGKAT_PERTAMA"].ToString();
                    }
                }
                

                if (model.FASKESDRGCODE != "")
                {
                    DataTable dt3 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_GIGI_BY_NAMA_FASKES] '" + model.FASKESDRGCODE + "'");
                    if (dt3.Rows.Count > 0)
                    {
                        //model.Provinsi_GIGI_ID = dt3.Rows[0]["PROVINSI"].ToString();
                        //model.Kotamadya_GIGI_ID = dt3.Rows[0]["KOTAMADYA"].ToString();
                        //model.Kecamatan_GIGI_ID = dt3.Rows[0]["KECAMATAN"].ToString();
                        //model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID = dt3.Rows[0]["NAMA_FASKES_TINGKAT_PERTAMA"].ToString();
                    }
                }
                

                //spouse
                if (model.FASKESCODETKI_SPOUSE != "")
                {
                    DataTable dt2 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_FASKES_BY_NAMA_FASKES] '" + model.FASKESCODETKI_SPOUSE + "'");
                    if (dt2.Rows.Count > 0)
                    {
                        //model.Provinsi_FASKES_Spouse_ID = dt2.Rows[0]["PROVINSI"].ToString();
                        //model.Kotamadya_FASKES_Spouse_ID = dt2.Rows[0]["KOTAMADYA"].ToString();
                        //model.Kecamatan_FASKES_Spouse_ID = dt2.Rows[0]["KECAMATAN"].ToString();
                    }
                }


                if (model.FASKESDRGCODE_SPOUSE != "")
                {
                    DataTable dt3 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_GIGI_BY_NAMA_FASKES] '" + model.FASKESDRGCODE_SPOUSE + "'");
                    if (dt3.Rows.Count > 0)
                    {
                        //model.Provinsi_GIGI_Spouse_ID = dt3.Rows[0]["PROVINSI"].ToString();
                        //model.Kotamadya_GIGI_Spouse_ID = dt3.Rows[0]["KOTAMADYA"].ToString();
                        //model.Kecamatan_GIGI_Spouse_ID = dt3.Rows[0]["KECAMATAN"].ToString();
                    }
                }

                if (model.Sequence_child_1 != "")
                {
                    DataTable dtCh1 = Common.ExecuteQuery("dbo.[sp_TMP_GET_CHILD_DATA_DETAIL_CHILD_1] '" + model.EmployeeID + "'");
                    if (dtCh1.Rows.Count > 0)
                    {
                        model.idx_child_1 = dtCh1.Rows[0]["idx"].ToString();
                        model.Sequence_child_1 = dtCh1.Rows[0]["Child_Sequence"].ToString();
                        model.KK_Child_1 = dtCh1.Rows[0]["KK"].ToString();
                        model.NIK_child_1 = dtCh1.Rows[0]["NIK"].ToString();
                        model.EmployeeID_child_1 = dtCh1.Rows[0]["EmployeeID"].ToString();
                        model.EmployeeName_child_1 = dtCh1.Rows[0]["Full_Name"].ToString();
                        model.Birthdate_child_1 = dtCh1.Rows[0]["Birthdate"].ToString();
                        model.Gender_ID_child_1 = dtCh1.Rows[0]["Gender"].ToString();
                        model.Handphone_child_1 = dtCh1.Rows[0]["Handphone"].ToString();
                        
                        model.Class_Child_1 = dtCh1.Rows[0]["Class_Child"].ToString();

                        model.Health_Plan_Membership_No_Polis_child_1 = dtCh1.Rows[0]["Health_Plan_Membership_NoPolis"].ToString();
                        model.Health_Plan_Membership_No_Peserta_child_1 = dtCh1.Rows[0]["Health_Plan_Membership_NoPeserta"].ToString();
                        model.Health_Plan_Membership_No_Kartu_child_1 = dtCh1.Rows[0]["Health_Plan_Membership_NoKartu"].ToString();
                        model.Health_Plan_Benefit_Rawat_Inap_child_1 = dtCh1.Rows[0]["Health_Plan_Benefit_Rawat_Inap"].ToString();
                        model.Health_Plan_Benefit_Rawat_Jalan_child_1 = dtCh1.Rows[0]["Health_Plan_Benefit_Rawat_Jalan"].ToString();
                        model.Health_Plan_Benefit_Persalinan_child_1 = dtCh1.Rows[0]["Health_Plan_Benefit_Persalinan"].ToString();
                        model.Health_Plan_Benefit_Gigi_child_1 = dtCh1.Rows[0]["Health_Plan_Benefit_Gigi"].ToString();
                        model.Health_Plan_Benefit_Kacamata_child_1 = dtCh1.Rows[0]["Health_Plan_Benefit_Kacamata"].ToString();

                        model.BPJS_Kesehatan_ID_child_1 = dtCh1.Rows[0]["BPJS_Kesehatan_ID"].ToString();
                        //model.Provinsi_FASKES_ID_child_1 = dtCh2.Rows[0]["Provinsi_Faskes_TK"].ToString();
                        //model.Kotamadya_FASKES_ID_child_1 = dtCh2.Rows[0]["Kotamadya_Faskes_TK"].ToString();
                        //model.Kecamatan_FASKES_ID_child_1 = dtCh2.Rows[0]["Kecamatan_Faskes_TK"].ToString();
                        model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 = dtCh1.Rows[0]["BPJS_Kesehatan_Faskes_Tingkat_Pertama"].ToString();

                        //model.Provinsi_GIGI_ID_child_1 = dtCh2.Rows[0]["Provinsi_Faskes_Gigi"].ToString();
                        //model.Kotamadya_GIGI_ID_child_1 = dtCh2.Rows[0]["Kotamadya_Faskes_Gigi"].ToString();
                        //model.Kecamatan_GIGI_ID_child_1 = dtCh2.Rows[0]["Kecamatan_Faskes_Gigi"].ToString();
                        model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 = dtCh1.Rows[0]["BPJS_Kesehatan_Faskes_Dokter_Gigi"].ToString();
                    }
                }

                if (model.Sequence_child_2 != "")
                {
                    DataTable dtCh2 = Common.ExecuteQuery("dbo.[sp_TMP_GET_CHILD_DATA_DETAIL_CHILD_2] '" + model.EmployeeID + "'");
                    if (dtCh2.Rows.Count > 0)
                    {
                        model.idx_child_2 = dtCh2.Rows[0]["idx"].ToString();
                        model.Sequence_child_2 = dtCh2.Rows[0]["Child_Sequence"].ToString();
                        model.KK_Child_2 = dtCh2.Rows[0]["KK"].ToString();
                        model.NIK_child_2 = dtCh2.Rows[0]["NIK"].ToString();
                        model.EmployeeID_child_2 = dtCh2.Rows[0]["EmployeeID"].ToString();
                        model.EmployeeName_child_2 = dtCh2.Rows[0]["Full_Name"].ToString();
                        model.Birthdate_child_2 = dtCh2.Rows[0]["Birthdate"].ToString();
                        model.Gender_ID_child_2 = dtCh2.Rows[0]["Gender"].ToString();
                        model.Handphone_child_2 = dtCh2.Rows[0]["Handphone"].ToString();

                        model.Class_Child_2 = dtCh2.Rows[0]["Class_Child"].ToString();

                        model.Health_Plan_Membership_No_Polis_child_2 = dtCh2.Rows[0]["Health_Plan_Membership_NoPolis"].ToString();
                        model.Health_Plan_Membership_No_Peserta_child_2 = dtCh2.Rows[0]["Health_Plan_Membership_NoPeserta"].ToString();
                        model.Health_Plan_Membership_No_Kartu_child_2 = dtCh2.Rows[0]["Health_Plan_Membership_NoKartu"].ToString();
                        model.Health_Plan_Benefit_Rawat_Inap_child_2 = dtCh2.Rows[0]["Health_Plan_Benefit_Rawat_Inap"].ToString();
                        model.Health_Plan_Benefit_Rawat_Jalan_child_2 = dtCh2.Rows[0]["Health_Plan_Benefit_Rawat_Jalan"].ToString();
                        model.Health_Plan_Benefit_Persalinan_child_2 = dtCh2.Rows[0]["Health_Plan_Benefit_Persalinan"].ToString();
                        model.Health_Plan_Benefit_Gigi_child_2 = dtCh2.Rows[0]["Health_Plan_Benefit_Gigi"].ToString();
                        model.Health_Plan_Benefit_Kacamata_child_2 = dtCh2.Rows[0]["Health_Plan_Benefit_Kacamata"].ToString();

                        model.BPJS_Kesehatan_ID_child_2 = dtCh2.Rows[0]["BPJS_Kesehatan_ID"].ToString();

                        //model.Provinsi_FASKES_ID_child_2 = dtCh2.Rows[0]["Provinsi_Faskes_TK"].ToString();
                        //model.Kotamadya_FASKES_ID_child_2 = dtCh2.Rows[0]["Kotamadya_Faskes_TK"].ToString();
                        //model.Kecamatan_FASKES_ID_child_2 = dtCh2.Rows[0]["Kecamatan_Faskes_TK"].ToString();
                        model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 = dtCh2.Rows[0]["BPJS_Kesehatan_Faskes_Tingkat_Pertama"].ToString();

                        //model.Provinsi_GIGI_ID_child_2 = dtCh2.Rows[0]["Provinsi_Faskes_Gigi"].ToString();
                        //model.Kotamadya_GIGI_ID_child_2 = dtCh2.Rows[0]["Kotamadya_Faskes_Gigi"].ToString();
                        //model.Kecamatan_GIGI_ID_child_2 = dtCh2.Rows[0]["Kecamatan_Faskes_Gigi"].ToString();
                        model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 = dtCh2.Rows[0]["BPJS_Kesehatan_Faskes_Dokter_Gigi"].ToString();
                    }
                }

                if (model.Sequence_child_3 != "")
                {
                    DataTable dtCh3 = Common.ExecuteQuery("dbo.[sp_TMP_GET_CHILD_DATA_DETAIL_CHILD_3] '" + model.EmployeeID + "'");
                    if (dtCh3.Rows.Count > 0)
                    {
                        model.idx_child_3 = dtCh3.Rows[0]["idx"].ToString();
                        model.Sequence_child_3 = dtCh3.Rows[0]["Child_Sequence"].ToString();
                        model.KK_Child_3 = dtCh3.Rows[0]["KK"].ToString();
                        model.NIK_child_3 = dtCh3.Rows[0]["NIK"].ToString();
                        model.EmployeeID_child_3 = dtCh3.Rows[0]["EmployeeID"].ToString();
                        model.EmployeeName_child_3 = dtCh3.Rows[0]["Full_Name"].ToString();
                        model.Birthdate_child_3 = dtCh3.Rows[0]["Birthdate"].ToString();
                        model.Gender_ID_child_3 = dtCh3.Rows[0]["Gender"].ToString();
                        model.Handphone_child_3 = dtCh3.Rows[0]["Handphone"].ToString();

                        model.Class_Child_3 = dtCh3.Rows[0]["Class_Child"].ToString();

                        model.Health_Plan_Membership_No_Polis_child_3 = dtCh3.Rows[0]["Health_Plan_Membership_NoPolis"].ToString();
                        model.Health_Plan_Membership_No_Peserta_child_3 = dtCh3.Rows[0]["Health_Plan_Membership_NoPeserta"].ToString();
                        model.Health_Plan_Membership_No_Kartu_child_3 = dtCh3.Rows[0]["Health_Plan_Membership_NoKartu"].ToString();
                        model.Health_Plan_Benefit_Rawat_Inap_child_3 = dtCh3.Rows[0]["Health_Plan_Benefit_Rawat_Inap"].ToString();
                        model.Health_Plan_Benefit_Rawat_Jalan_child_3 = dtCh3.Rows[0]["Health_Plan_Benefit_Rawat_Jalan"].ToString();
                        model.Health_Plan_Benefit_Persalinan_child_3 = dtCh3.Rows[0]["Health_Plan_Benefit_Persalinan"].ToString();
                        model.Health_Plan_Benefit_Gigi_child_3 = dtCh3.Rows[0]["Health_Plan_Benefit_Gigi"].ToString();
                        model.Health_Plan_Benefit_Kacamata_child_3 = dtCh3.Rows[0]["Health_Plan_Benefit_Kacamata"].ToString();

                        model.BPJS_Kesehatan_ID_child_3 = dtCh3.Rows[0]["BPJS_Kesehatan_ID"].ToString();

                        //model.Provinsi_FASKES_ID_child_3 = dtCh3.Rows[0]["Provinsi_Faskes_TK"].ToString();
                        //model.Kotamadya_FASKES_ID_child_3 = dtCh3.Rows[0]["Kotamadya_Faskes_TK"].ToString();
                        //model.Kecamatan_FASKES_ID_child_3 = dtCh3.Rows[0]["Kecamatan_Faskes_TK"].ToString();
                        model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 = dtCh3.Rows[0]["BPJS_Kesehatan_Faskes_Tingkat_Pertama"].ToString();

                        //model.Provinsi_GIGI_ID_child_3 = dtCh3.Rows[0]["Provinsi_Faskes_Gigi"].ToString();
                        //model.Kotamadya_GIGI_ID_child_3 = dtCh3.Rows[0]["Kotamadya_Faskes_Gigi"].ToString();
                        //model.Kecamatan_GIGI_ID_child_3 = dtCh3.Rows[0]["Kecamatan_Faskes_Gigi"].ToString();
                        model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 = dtCh3.Rows[0]["BPJS_Kesehatan_Faskes_Dokter_Gigi"].ToString();
                    }
                }


                //CHILD
                if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 != "")
                {
                    DataTable dtf01 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_FASKES_BY_NAMA_FASKES] '" + model.FASKESCODETKI_CHILD1 + "'");
                    if (dtf01.Rows.Count > 0)
                    {
                        model.Provinsi_FASKES_ID_child_1 = dtf01.Rows[0]["PROVINSI"].ToString();
                        model.Kotamadya_FASKES_ID_child_1 = dtf01.Rows[0]["KOTAMADYA"].ToString();
                        model.Kecamatan_FASKES_ID_child_1 = dtf01.Rows[0]["KECAMATAN"].ToString();
                    }
                }

                if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 != "")
                {
                    DataTable dtf02 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_FASKES_BY_NAMA_FASKES] '" + model.FASKESCODETKI_CHILD2 + "'");
                    if (dtf02.Rows.Count > 0)
                    {
                        model.Provinsi_FASKES_ID_child_2 = dtf02.Rows[0]["PROVINSI"].ToString();
                        model.Kotamadya_FASKES_ID_child_2 = dtf02.Rows[0]["KOTAMADYA"].ToString();
                        model.Kecamatan_FASKES_ID_child_2 = dtf02.Rows[0]["KECAMATAN"].ToString();
                    }
                }

                if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 != "")
                {
                    DataTable dtf03 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_FASKES_BY_NAMA_FASKES] '" + model.FASKESCODETKI_CHILD3 + "'");
                    if (dtf03.Rows.Count > 0)
                    {
                        model.Provinsi_FASKES_ID_child_3 = dtf03.Rows[0]["PROVINSI"].ToString();
                        model.Kotamadya_FASKES_ID_child_3 = dtf03.Rows[0]["KOTAMADYA"].ToString();
                        model.Kecamatan_FASKES_ID_child_3 = dtf03.Rows[0]["KECAMATAN"].ToString();
                    }
                }

                if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 != "")
                {
                    DataTable dtf001 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_GIGI_BY_NAMA_FASKES] '" + model.FASKESDRGCODE_CHILD1 + "'");
                    if (dtf001.Rows.Count > 0)
                    {
                        model.Provinsi_GIGI_ID_child_1 = dtf001.Rows[0]["PROVINSI"].ToString();
                        model.Kotamadya_GIGI_ID_child_1 = dtf001.Rows[0]["KOTAMADYA"].ToString();
                        model.Kecamatan_GIGI_ID_child_1 = dtf001.Rows[0]["KECAMATAN"].ToString();
                    }
                }

                if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 != "")
                {
                    DataTable dtf002 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_GIGI_BY_NAMA_FASKES] '" + model.FASKESDRGCODE_CHILD2 + "'");
                    if (dtf002.Rows.Count > 0)
                    {
                        model.Provinsi_GIGI_ID_child_2 = dtf002.Rows[0]["PROVINSI"].ToString();
                        model.Kotamadya_GIGI_ID_child_2 = dtf002.Rows[0]["KOTAMADYA"].ToString();
                        model.Kecamatan_GIGI_ID_child_2 = dtf002.Rows[0]["KECAMATAN"].ToString();
                    }
                }

                if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 != "")
                {
                    DataTable dtf003 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_GIGI_BY_NAMA_FASKES] '" + model.FASKESDRGCODE_CHILD3 + "'");
                    if (dtf003.Rows.Count > 0)
                    {
                        model.Provinsi_GIGI_ID_child_3 = dtf003.Rows[0]["PROVINSI"].ToString();
                        model.Kotamadya_GIGI_ID_child_3 = dtf003.Rows[0]["KOTAMADYA"].ToString();
                        model.Kecamatan_GIGI_ID_child_3 = dtf003.Rows[0]["KECAMATAN"].ToString();
                    }
                }


                // CHILD
                // FASKES
                if (model.Provinsi_FASKES_ID_child_1 != "")
                {
                    model.Kotamadya_FASKES_Child_1 = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_ID_child_1);
                }
                if (model.Kotamadya_FASKES_ID_child_1 != "")
                {
                    model.Kecamatan_FASKES_Child_1 = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_ID_child_1, model.Provinsi_FASKES_ID_child_1);
                }
                if (model.Kecamatan_FASKES_ID_child_1 != "")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_1 = DDLFASKES(model.Kecamatan_FASKES_ID_child_1, model.Provinsi_FASKES_ID_child_1, model.Kotamadya_FASKES_ID_child_1);
                }

                //

                if (model.Provinsi_FASKES_ID_child_2 != "")
                {
                    model.Kotamadya_FASKES_Child_2 = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_ID_child_2);
                }
                if (model.Kotamadya_FASKES_ID_child_2 != "")
                {
                    model.Kecamatan_FASKES_Child_2 = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_ID_child_2, model.Provinsi_FASKES_ID_child_2);
                }
                if (model.Kecamatan_FASKES_ID_child_2 != "")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_2 = DDLFASKES(model.Kecamatan_FASKES_ID_child_2, model.Provinsi_FASKES_ID_child_2, model.Kotamadya_FASKES_ID_child_2);
                }

                //

                if (model.Provinsi_FASKES_ID_child_3 != "")
                {
                    model.Kotamadya_FASKES_Child_3 = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_ID_child_3);
                }
                if (model.Kotamadya_FASKES_ID_child_3 != "")
                {
                    model.Kecamatan_FASKES_Child_3 = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_ID_child_3, model.Provinsi_FASKES_ID_child_3);
                }
                if (model.Kecamatan_FASKES_ID_child_3 != "")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_3 = DDLFASKES(model.Kecamatan_FASKES_ID_child_3, model.Provinsi_FASKES_ID_child_3, model.Kotamadya_FASKES_ID_child_3);
                }

                // END FASKES

                //GIGI
                if (model.Provinsi_GIGI_ID_child_1 != "")
                {
                    model.Kotamadya_GIGI_Child_1 = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_ID_child_1);
                }
                if (model.Kotamadya_GIGI_ID_child_1 != "")
                {
                    model.Kecamatan_GIGI_Child_1 = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_ID_child_1, model.Provinsi_GIGI_ID_child_1);
                }
                if (model.Kecamatan_GIGI_ID_child_1 != "")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_1 = DDLGIGI(model.Kecamatan_GIGI_ID_child_1, model.Provinsi_GIGI_ID_child_1, model.Kotamadya_GIGI_ID_child_1);
                }

                //

                if (model.Provinsi_GIGI_ID_child_2 != "")
                {
                    model.Kotamadya_GIGI_Child_2 = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_ID_child_2);
                }
                if (model.Kotamadya_GIGI_ID_child_2 != "")
                {
                    model.Kecamatan_GIGI_Child_2 = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_ID_child_2, model.Provinsi_GIGI_ID_child_2);
                }
                if (model.Kecamatan_GIGI_ID_child_2 != "")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_2 = DDLGIGI(model.Kecamatan_GIGI_ID_child_2, model.Provinsi_GIGI_ID_child_2, model.Kotamadya_GIGI_ID_child_2);
                }

                //

                if (model.Provinsi_GIGI_ID_child_3 != "")
                {
                    model.Kotamadya_GIGI_Child_3 = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_ID_child_3);
                }
                if (model.Kotamadya_GIGI_ID_child_3 != "")
                {
                    model.Kecamatan_GIGI_Child_3 = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_ID_child_3, model.Provinsi_GIGI_ID_child_3);
                }
                if (model.Kecamatan_GIGI_ID_child_3 != "")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_3 = DDLGIGI(model.Kecamatan_GIGI_ID_child_3, model.Provinsi_GIGI_ID_child_3, model.Kotamadya_GIGI_ID_child_3);
                }

                //END GIGI



                // FASKES ALL
                if (model.Provinsi_FASKES_ID != "")
                {
                    model.Kotamadya_FASKES = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_ID);
                }

                if (model.Kotamadya_FASKES_ID != "")
                {
                    model.Kecamatan_FASKES = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_ID, model.Provinsi_FASKES_ID);
                }
                if (model.Kecamatan_FASKES_ID != "")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama = DDLFASKES(model.Kecamatan_FASKES_ID, model.Provinsi_FASKES_ID, model.Kotamadya_FASKES_ID);
                }
                // END FASKES

                //GIGI
                if (model.Provinsi_GIGI_ID != "")
                {
                    model.Kotamadya_GIGI = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_ID);
                }

                if (model.Kotamadya_GIGI_ID != "")
                {
                    model.Kecamatan_GIGI = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_ID, model.Provinsi_GIGI_ID);
                }
                if (model.Kecamatan_GIGI_ID != "")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi = DDLGIGI(model.Kecamatan_GIGI_ID, model.Provinsi_GIGI_ID, model.Kotamadya_GIGI_ID);
                }
                // END GIGI
                if (model.State_ID != "")
                {
                    model.Kotamadya = DDLKOTAMADYABYSTATE(model.State_ID);
                }


                if (model.State_ID != "")
                {
                    model.Kotamadya = DDLKOTAMADYABYSTATE(model.State_ID);
                }
                if (model.Kotamadya_ID != "")
                {
                    model.Kecamatan = DDLKECAMATAN(model.Kotamadya_ID, model.State_ID);
                }
                if (model.Kecamatan_ID != "")
                {
                    model.Kelurahan = DDLKELURAHAN(model.Kecamatan_ID, model.State_ID, model.Kotamadya_ID);
                }


                // Spouse
                // FASKES
                if (model.Provinsi_FASKES_Spouse_ID != "")
                {
                    model.Kotamadya_FASKES_Spouse = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_Spouse_ID);
                }

                if (model.Kotamadya_FASKES_Spouse_ID != "")
                {
                    model.Kecamatan_FASKES_Spouse = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_Spouse_ID, model.Provinsi_FASKES_Spouse_ID);
                }
                if (model.Kecamatan_FASKES_Spouse_ID != "")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse = DDLFASKES(model.Kecamatan_FASKES_Spouse_ID, model.Provinsi_FASKES_Spouse_ID, model.Kotamadya_FASKES_Spouse_ID);
                }
                // END FASKES

                //GIGI
                if (model.Provinsi_GIGI_Spouse_ID != "")
                {
                    model.Kotamadya_GIGI_Spouse = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_Spouse_ID);
                }

                if (model.Kotamadya_GIGI_Spouse_ID != "")
                {
                    model.Kecamatan_GIGI_Spouse = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_Spouse_ID, model.Provinsi_GIGI_Spouse_ID);
                }
                if (model.Kecamatan_GIGI_Spouse_ID != "")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse = DDLGIGI(model.Kecamatan_GIGI_Spouse_ID, model.Provinsi_GIGI_Spouse_ID, model.Kotamadya_GIGI_Spouse_ID);
                }
                // END GIGI

                //BPJS ACTIVE
                //if (model.BPJS_Kesehatan_ID == "" || model.BPJS_Kesehatan_Spouse_ID == null)
                //{
                //    model.BPJS_Kesehatan_Active = "Not Registered";
                //}
                //else
                //{
                //    model.BPJS_Kesehatan_Active = "Registered";
                //}

                //if (model.BPJS_Kesehatan_Spouse_ID == "" || model.BPJS_Kesehatan_Spouse_ID == null)
                //{
                //    model.BPJS_Kesehatan_Active_Spouse = "Not Registered";
                //}
                //else
                //{
                //    model.BPJS_Kesehatan_Active_Spouse = "Registered";
                //}

                //if (model.BPJS_Kesehatan_ID_child_1 == "" || model.BPJS_Kesehatan_ID_child_1 == null)
                //{
                //    model.BPJS_Kesehatan_Active_Child_1 = "Not Registered";
                //}
                //else
                //{
                //    model.BPJS_Kesehatan_Active_Child_1 = "Registered";
                //}

                //if (model.BPJS_Kesehatan_ID_child_2 == "" || model.BPJS_Kesehatan_ID_child_2 == null)
                //{
                //    model.BPJS_Kesehatan_Active_Child_2 = "Not Registered";
                //}
                //else
                //{
                //    model.BPJS_Kesehatan_Active_Child_2 = "Registered";
                //}

                //if (model.BPJS_Kesehatan_ID_child_3 == "" || model.BPJS_Kesehatan_ID_child_3 == null)
                //{
                //    model.BPJS_Kesehatan_Active_Child_3 = "Not Registered";
                //}
                //else
                //{
                //    model.BPJS_Kesehatan_Active_Child_3 = "Registered";
                //}

                if (model.Birthdate_Parent1 == "01-01-1900")
                {
                    model.Birthdate_Parent1 = "";
                }
                if (model.Birthdate_Parent2 == "01-01-1900")
                {
                    model.Birthdate_Parent2 = "";
                }
                if (model.BPJS_JoinDate == "01-01-1900")
                {
                    model.BPJS_JoinDate = "";
                }
                if (model.DPLK_Joint_Date == "01-01-1900")
                {
                    model.DPLK_Joint_Date = "";
                }
                if (model.Birthdate_child_1 == "01-01-1900")
                {
                    model.Birthdate_child_1 = "";
                }
                if (model.Birthdate_child_2 == "01-01-1900")
                {
                    model.Birthdate_child_2 = "";
                }
                if (model.Birthdate_child_3 == "01-01-1900")
                {
                    model.Birthdate_child_3 = "";
                }
                if (model.Birthdate_Spouse == "01-01-1900")
                {
                    model.Birthdate_Spouse = "";
                }
                if (model.Birthdate == "01-01-1900")
                {
                    model.Birthdate = "";
                }

                //LinkAttachment
                string pathmove2_1 = Server.MapPath("~/Images/Education/Waiting/" + model.EntityID + "/" + ID + "_Education.PDF");

                if (System.IO.File.Exists(pathmove2_1))
                {
                    model.LinkAttachment = "Waiting";
                }
                else
                {
                    model.LinkAttachment = "Available";
                }
                //EndLinkAttachment

            }
            else
            {
                return RedirectToAction("ListApprove", "ListApprove");
            }
            return View(model);
        }
        
        public string GetDateInYYYYMMDD(string dt)
        {
            if (dt == "1900-01-01")
            {
                return dt;
            }
            string[] stringSeparators = new string[] { "-" };
            string[] str = dt.Split('-');

            string tempdt = string.Empty;
            for (int i = 2; i >= 0; i += -1)
                tempdt += str[i] + "-";
            tempdt = tempdt.Substring(0, 10);
            return tempdt;
        }

        [HttpPost]
        public ActionResult Edit(ListApproveModels model, string Submit, string Reject, string id, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                string filename_EDU = string.Empty;
                string filename_Pay = string.Empty;
                string FilePathEdu = string.Empty;
                string FilePathPay = string.Empty;
                string Photo = string.Empty;
                string FilePathMaried = string.Empty;
                string filename_Married = string.Empty;
                string url = Request.Url.OriginalString;
                Session["url"] = url;
                model.LINK = url;
                ViewBag.menu = Session["menu"];
                Session["controller"] = "ListApproveController";
                string ID = null;
                string Nama = null;

                ID = string.IsNullOrEmpty(ID) == true ? Session["EmployeeNumber"].ToString() : ID;
                Nama = string.IsNullOrEmpty(Nama) == true ? Session["UserID"].ToString() : Nama;
                if (model.HiredDate == "")
                {
                    model.Birthdate_Parent1 = "1900-01-01";
                }

                //All
                if (model.idx == null)
                {
                    model.idx = "";
                }
                if (model.EmployeeID == null)
                {
                    model.EmployeeID = "";
                }
                if (model.Gender_ID == null)
                {
                    model.Gender_ID = "";
                }
                if (model.EmployeeName == null)
                {
                    model.EmployeeName = "";
                }
                if (model.Email == null)
                {
                    model.Email = "";
                }
                if (model.DepartmentID1 == null)
                {
                    model.DepartmentID1 = "";
                }
                if (model.EntityID == null)
                {
                    model.EntityID = "";
                }
                if (model.JobTitleID == null)
                {
                    model.JobTitleID = "";
                }
                if (model.GradeID == null)
                {
                    model.GradeID = "";
                }
                if (model.ReportingTo == null)
                {
                    model.ReportingTo = "";
                }
                if (model.KTP == null)
                {
                    model.KTP = "";
                }
                if (model.NPWP == null)
                {
                    model.NPWP = "";
                }
                if (model.KK == null)
                {
                    model.KK = "";
                }
                if (model.KK_Spouse == null)
                {
                    model.KK_Spouse = "";
                }
                if (model.MaritalStatus == null)
                {
                    model.MaritalStatus = "";
                }
                if (model.Spouse == null)
                {
                    model.Spouse = "";
                }
                if (model.PlaceBirth_ID == null)
                {
                    model.PlaceBirth_ID = "";
                }
                if (model.Nationality_ID == null)
                {
                    model.Nationality_ID = "";
                }
                if (model.Religion_ID == null)
                {
                    model.Religion_ID = "";
                }
                if (model.Home_Address == null)
                {
                    model.Home_Address = "";
                }
                if (model.City_ID == null)
                {
                    model.City_ID = "";
                }
                if (model.Kodepos == null)
                {
                    model.Kodepos = "";
                }
                if (model.RT == null)
                {
                    model.RT = "";
                }
                if (model.RW == null)
                {
                    model.RW = "";
                }
                if (model.State_ID == null)
                {
                    model.State_ID = "";
                }
                if (model.Kotamadya_ID == null)
                {
                    model.Kotamadya_ID = "";
                }
                if (model.Kecamatan_ID == null)
                {
                    model.Kecamatan_ID = "";
                }
                if (model.Kelurahan_ID == null)
                {
                    model.Kelurahan_ID = "";
                }
                if (model.Physical_Address == null)
                {
                    model.Physical_Address = "";
                }
                if (model.Personal_Email == null)
                {
                    model.Personal_Email = "";
                }
                if (model.Home_Phone == null)
                {
                    model.Home_Phone = "";
                }
                if (model.Handphone == null)
                {
                    model.Handphone = "";
                }
                if (model.Payroll_Bank == null)
                {
                    model.Payroll_Bank = "";
                }
                if (model.Payroll_Branch == null)
                {
                    model.Payroll_Branch = "";
                }
                if (model.Payroll_Account_No == null)
                {
                    model.Payroll_Account_No = "";
                }
                if (model.Payroll_Accoun_Name == null)
                {
                    model.Payroll_Accoun_Name = "";
                }
                if (model.DPLK_No_Peserta == null)
                {
                    model.DPLK_No_Peserta = "";
                }
                if (model.DPLK_Saldo == null && model.DPLK_Saldo == "")
                {
                    model.DPLK_Saldo = "0";
                }
                if (model.Health_Plan_Membership_No_Polis == null)
                {
                    model.Health_Plan_Membership_No_Polis = "";
                }
                if (model.Health_Plan_Membership_No_Peserta == null)
                {
                    model.Health_Plan_Membership_No_Peserta = "";
                }
                if (model.Health_Plan_Membership_No_Kartu == null)
                {
                    model.Health_Plan_Membership_No_Kartu = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Inap == null)
                {
                    model.Health_Plan_Benefit_Rawat_Inap = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Jalan == null)
                {
                    model.Health_Plan_Benefit_Rawat_Jalan = "";
                }
                if (model.Health_Plan_Benefit_Persalinan == null)
                {
                    model.Health_Plan_Benefit_Persalinan = "";
                }
                if (model.Health_Plan_Benefit_Gigi == null)
                {
                    model.Health_Plan_Benefit_Gigi = "";
                }
                if (model.Health_Plan_Benefit_Kacamata == null)
                {
                    model.Health_Plan_Benefit_Kacamata = "";
                }
                if (model.BPJS_Ketenagakerjaan_ID == null)
                {
                    model.BPJS_Ketenagakerjaan_ID = "";
                }
                if (model.BPJS_Pensiun_ID == null)
                {
                    model.BPJS_Pensiun_ID = "";
                }
                if (model.BPJS_Kesehatan_Active == null)
                {
                    model.BPJS_Kesehatan_Active = "";
                }
                if (model.BPJS_Kesehatan_Active_Spouse == null)
                {
                    model.BPJS_Kesehatan_Active_Spouse = "";
                }
                if (model.BPJS_Kesehatan_Active_Child_1 == null)
                {
                    model.BPJS_Kesehatan_Active_Child_1 = "";
                }
                if (model.BPJS_Kesehatan_Active_Child_2 == null)
                {
                    model.BPJS_Kesehatan_Active_Child_2 = "";
                }
                if (model.BPJS_Kesehatan_Active_Child_3 == null)
                {
                    model.BPJS_Kesehatan_Active_Child_3 = "";
                }
                if (model.BPJS_Kesehatan_ID == null)
                {
                    model.BPJS_Kesehatan_ID = "";
                }
                if (model.Attach_File_Married == null)
                {
                    model.Attach_File_Married = "";
                }
                if (model.Attach_File_Education == null)
                {
                    model.Attach_File_Education = "";
                }
                if (model.Attach_File_Payroll == null)
                {
                    model.Attach_File_Payroll = "";
                }
                if (model.FileName_Married == null)
                {
                    model.FileName_Married = "";
                }
                if (model.Univ_Name == null)
                {
                    model.Univ_Name = "";
                }
                if (model.Education == null)
                {
                    model.Education = "";
                }
                if (model.FileName_Education == null)
                {
                    model.FileName_Education = "";
                }
                if (model.FileName_Payroll == null)
                {
                    model.FileName_Payroll = "";
                }
                if (model.Remark == null)
                {
                    model.Remark = "";
                }
                if (model.Class == null)
                {
                    model.Class = "";
                }
                if (model.Class_Spouse == null)
                {
                    model.Class_Spouse = "";
                }
                if (model.Full_Name_Spouse == null)
                {
                    model.Full_Name_Spouse = "";
                }
                if (model.Gender_Spouse_ID == null)
                {
                    model.Gender_Spouse_ID = "";
                }
                if (model.NIK_Spouse == null)
                {
                    model.NIK_Spouse = "";
                }
                if (model.Handphone_Spouse == null)
                {
                    model.Handphone_Spouse = "";
                }
                if (model.Health_Plan_Membership_No_Polis_Spouse == null)
                {
                    model.Health_Plan_Membership_No_Polis_Spouse = "";
                }
                if (model.Health_Plan_Membership_No_Peserta_Spouse == null)
                {
                    model.Health_Plan_Membership_No_Peserta_Spouse = "";
                }
                if (model.Health_Plan_Membership_No_Kartu_Spouse == null)
                {
                    model.Health_Plan_Membership_No_Kartu_Spouse = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Inap_Spouse == null)
                {
                    model.Health_Plan_Benefit_Rawat_Inap_Spouse = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Jalan_Spouse == null)
                {
                    model.Health_Plan_Benefit_Rawat_Jalan_Spouse = "";
                }
                if (model.Health_Plan_Benefit_Persalinan_Spouse == null)
                {
                    model.Health_Plan_Benefit_Persalinan_Spouse = "";
                }
                if (model.Health_Plan_Benefit_Gigi_Spouse == null)
                {
                    model.Health_Plan_Benefit_Gigi_Spouse = "";
                }
                if (model.Health_Plan_Benefit_Kacamata_Spouse == null)
                {
                    model.Health_Plan_Benefit_Kacamata_Spouse = "";
                }
                if (model.BPJS_Kesehatan_Spouse_ID == null)
                {
                    model.BPJS_Kesehatan_Spouse_ID = "";
                }
                if (model.idx_spouse == null)
                {
                    model.idx_spouse = "";
                }
                if (model.idx_parent1 == null)
                {
                    model.idx_parent1 = "";
                }
                if (model.Full_Name_Parent1 == null)
                {
                    model.Full_Name_Parent1 = "";
                }
                if (model.Gender_ID_Parent1 == null)
                {
                    model.Gender_ID_Parent1 = "";
                }
                if (model.Handphone_Parent1 == null)
                {
                    model.Handphone_Parent1 = "";
                }
                if (model.idx_parent2 == null)
                {
                    model.idx_parent2 = "";
                }
                if (model.Full_Name_Parent2 == null)
                {
                    model.Full_Name_Parent2 = "";
                }
                if (model.Gender_ID_Parent2 == null)
                {
                    model.Gender_ID_Parent2 = "";
                }
                if (model.Handphone_Parent2 == null)
                {
                    model.Handphone_Parent2 = "";
                }
                if (model.Sequence_child_1 == null)
                {
                    model.Sequence_child_1 = "";
                }
                if (model.KK_Child_1 == null)
                {
                    model.KK_Child_1 = "";
                }
                if (model.NIK_child_1 == null)
                {
                    model.NIK_child_1 = "";
                }
                if (model.EmployeeID_child_1 == null)
                {
                    model.EmployeeID_child_1 = "";
                }
                if (model.EmployeeName_child_1 == null)
                {
                    model.EmployeeName_child_1 = "";
                }
                if (model.Gender_ID_child_1 == null)
                {
                    model.Gender_ID_child_1 = "";
                }
                if (model.Handphone_child_1 == null)
                {
                    model.Handphone_child_1 = "";
                }
                if (model.Health_Plan_Membership_No_Polis_child_1 == null)
                {
                    model.Health_Plan_Membership_No_Polis_child_1 = "";
                }
                if (model.Health_Plan_Membership_No_Peserta_child_1 == null)
                {
                    model.Health_Plan_Membership_No_Peserta_child_1 = "";
                }
                if (model.Health_Plan_Membership_No_Kartu_child_1 == null)
                {
                    model.Health_Plan_Membership_No_Kartu_child_1 = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Inap_child_1 == null)
                {
                    model.Health_Plan_Benefit_Rawat_Inap_child_1 = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Jalan_child_1 == null)
                {
                    model.Health_Plan_Benefit_Rawat_Jalan_child_1 = "";
                }
                if (model.Health_Plan_Benefit_Persalinan_child_1 == null)
                {
                    model.Health_Plan_Benefit_Persalinan_child_1 = "";
                }
                if (model.Health_Plan_Benefit_Gigi_child_1 == null)
                {
                    model.Health_Plan_Benefit_Gigi_child_1 = "";
                }
                if (model.Health_Plan_Benefit_Kacamata_child_1 == null)
                {
                    model.Health_Plan_Benefit_Kacamata_child_1 = "";
                }
                if (model.BPJS_Kesehatan_ID_child_1 == null)
                {
                    model.BPJS_Kesehatan_ID_child_1 = "";
                }
                if (model.Class_Child_2 == null)
                {
                    model.Class_Child_2 = "";
                }
                if (model.Sequence_child_3 == null)
                {
                    model.Sequence_child_3 = "";
                }
                if (model.KK_Child_3 == null)
                {
                    model.KK_Child_3 = "";
                }
                if (model.NIK_child_3 == null)
                {
                    model.NIK_child_3 = "";
                }
                if (model.EmployeeID_child_3 == null)
                {
                    model.EmployeeID_child_3 = "";
                }
                if (model.EmployeeName_child_3 == null)
                {
                    model.EmployeeName_child_3 = "";
                }
                if (model.Gender_ID_child_3 == null)
                {
                    model.Gender_ID_child_3 = "";
                }
                if (model.Handphone_child_3 == null)
                {
                    model.Handphone_child_3 = "";
                }
                if (model.Health_Plan_Membership_No_Polis_child_3 == null)
                {
                    model.Health_Plan_Membership_No_Polis_child_3 = "";
                }
                if (model.Health_Plan_Membership_No_Peserta_child_3 == null)
                {
                    model.Health_Plan_Membership_No_Peserta_child_3 = "";
                }
                if (model.Health_Plan_Membership_No_Kartu_child_3 == null)
                {
                    model.Health_Plan_Membership_No_Kartu_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Inap_child_3 == null)
                {
                    model.Health_Plan_Benefit_Rawat_Inap_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Jalan_child_3 == null)
                {
                    model.Health_Plan_Benefit_Rawat_Jalan_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Persalinan_child_3 == null)
                {
                    model.Health_Plan_Benefit_Persalinan_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Gigi_child_3 == null)
                {
                    model.Health_Plan_Benefit_Gigi_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Kacamata_child_3 == null)
                {
                    model.Health_Plan_Benefit_Kacamata_child_3 = "";
                }
                if (model.BPJS_Kesehatan_ID_child_3 == null)
                {
                    model.BPJS_Kesehatan_ID_child_3 = "";
                }
                if (model.Class_Child_3 == null)
                {
                    model.Class_Child_3 = "";
                }
                if (model.Sequence_child_3 == null)
                {
                    model.Sequence_child_3 = "";
                }
                if (model.KK_Child_3 == null)
                {
                    model.KK_Child_3 = "";
                }
                if (model.NIK_child_3 == null)
                {
                    model.NIK_child_3 = "";
                }
                if (model.EmployeeID_child_3 == null)
                {
                    model.EmployeeID_child_3 = "";
                }
                if (model.EmployeeName_child_3 == null)
                {
                    model.EmployeeName_child_3 = "";
                }
                if (model.Gender_ID_child_3 == null)
                {
                    model.Gender_ID_child_3 = "";
                }
                if (model.Handphone_child_3 == null)
                {
                    model.Handphone_child_3 = "";
                }
                if (model.Health_Plan_Membership_No_Polis_child_3 == null)
                {
                    model.Health_Plan_Membership_No_Polis_child_3 = "";
                }
                if (model.Health_Plan_Membership_No_Peserta_child_3 == null)
                {
                    model.Health_Plan_Membership_No_Peserta_child_3 = "";
                }
                if (model.Health_Plan_Membership_No_Kartu_child_3 == null)
                {
                    model.Health_Plan_Membership_No_Kartu_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Inap_child_3 == null)
                {
                    model.Health_Plan_Benefit_Rawat_Inap_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Rawat_Jalan_child_3 == null)
                {
                    model.Health_Plan_Benefit_Rawat_Jalan_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Persalinan_child_3 == null)
                {
                    model.Health_Plan_Benefit_Persalinan_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Gigi_child_3 == null)
                {
                    model.Health_Plan_Benefit_Gigi_child_3 = "";
                }
                if (model.Health_Plan_Benefit_Kacamata_child_3 == null)
                {
                    model.Health_Plan_Benefit_Kacamata_child_3 = "";
                }
                if (model.BPJS_Kesehatan_ID_child_3 == null)
                {
                    model.BPJS_Kesehatan_ID_child_3 = "";
                }
                if (model.Class_Child_3 == null)
                {
                    model.Class_Child_3 = "";
                }
                if (model.idx_child_1 == null)
                {
                    model.idx_child_1 = "10000";
                }
                if (model.idx_child_2 == null)
                {
                    model.idx_child_2 = "20000";
                }
                if (model.idx_child_3 == null)
                {
                    model.idx_child_3 = "30000";
                }

                if (model.MarriedDate == null)
                {
                    model.MarriedDate = "1900-01-01";
                }

                if (model.Univ_Name != "--- OTHERS ---")
                {
                    model.Remark = "";
                }

                if (model.Birthdate_Spouse == null)
                {
                    model.Birthdate_Spouse = "1900-01-01";
                }

                if (model.Provinsi_FASKES_ID == "--- Choose ---" || model.Provinsi_FASKES_ID == "- Please Select -")
                {
                    model.Provinsi_FASKES_ID = "";
                }
                if (model.Kotamadya_FASKES_ID == "--- Choose ---" || model.Kotamadya_FASKES_ID == "- Please Select -")
                {
                    model.Kotamadya_FASKES_ID = "";
                }
                if (model.Kecamatan_FASKES_ID == "--- Choose ---" || model.Kecamatan_FASKES_ID == "- Please Select -")
                {
                    model.Kecamatan_FASKES_ID = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID = "";
                }
                if (model.Provinsi_GIGI_ID == "--- Choose ---" || model.Provinsi_GIGI_ID == "- Please Select -")
                {
                    model.Provinsi_GIGI_ID = "";
                }
                if (model.Kotamadya_GIGI_ID == "--- Choose ---" || model.Kotamadya_GIGI_ID == "- Please Select -")
                {
                    model.Kotamadya_GIGI_ID = "";
                }
                if (model.Kecamatan_GIGI_ID == "--- Choose ---" || model.Kecamatan_GIGI_ID == "- Please Select -")
                {
                    model.Kecamatan_GIGI_ID = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID = "";
                }

                if (model.Provinsi_FASKES_Spouse_ID == "--- Choose ---" || model.Provinsi_FASKES_Spouse_ID == "- Please Select -")
                {
                    model.Provinsi_FASKES_Spouse_ID = "";
                }
                if (model.Kotamadya_FASKES_Spouse_ID == "--- Choose ---" || model.Kotamadya_FASKES_Spouse_ID == "- Please Select -")
                {
                    model.Kotamadya_FASKES_Spouse_ID = "";
                }
                if (model.Kecamatan_FASKES_Spouse_ID == "--- Choose ---" || model.Kecamatan_FASKES_Spouse_ID == "- Please Select -")
                {
                    model.Kecamatan_FASKES_Spouse_ID = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse_ID == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse_ID == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse_ID = "";
                }
                if (model.Provinsi_GIGI_Spouse_ID == "--- Choose ---" || model.Provinsi_GIGI_Spouse_ID == "- Please Select -")
                {
                    model.Provinsi_GIGI_Spouse_ID = "";
                }
                if (model.Kotamadya_GIGI_Spouse_ID == "--- Choose ---" || model.Kotamadya_GIGI_Spouse_ID == "- Please Select -")
                {
                    model.Kotamadya_GIGI_Spouse_ID = "";
                }
                if (model.Kecamatan_GIGI_Spouse_ID == "--- Choose ---" || model.Kecamatan_GIGI_Spouse_ID == "- Please Select -")
                {
                    model.Kecamatan_GIGI_Spouse_ID = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse_ID == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse_ID == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse_ID = "";
                }

                if (model.Provinsi_FASKES_ID_child_1 == "--- Choose ---" || model.Provinsi_FASKES_ID_child_1 == "- Please Select -")
                {
                    model.Provinsi_FASKES_ID_child_1 = "";
                }
                if (model.Kotamadya_FASKES_ID_child_1 == "--- Choose ---" || model.Kotamadya_FASKES_ID_child_1 == "- Please Select -")
                {
                    model.Kotamadya_FASKES_ID_child_1 = "";
                }
                if (model.Kecamatan_FASKES_ID_child_1 == "--- Choose ---" || model.Kecamatan_FASKES_ID_child_1 == "- Please Select -")
                {
                    model.Kecamatan_FASKES_ID_child_1 = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 = "";
                }
                if (model.Provinsi_GIGI_ID_child_1 == "--- Choose ---" || model.Provinsi_GIGI_ID_child_1 == "- Please Select -")
                {
                    model.Provinsi_GIGI_ID_child_1 = "";
                }
                if (model.Kotamadya_GIGI_ID_child_1 == "--- Choose ---" || model.Kotamadya_GIGI_ID_child_1 == "- Please Select -")
                {
                    model.Kotamadya_GIGI_ID_child_1 = "";
                }
                if (model.Kecamatan_GIGI_ID_child_1 == "--- Choose ---" || model.Kecamatan_GIGI_ID_child_1 == "- Please Select -")
                {
                    model.Kecamatan_GIGI_ID_child_1 = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 = "";
                }

                if (model.Provinsi_FASKES_ID_child_2 == "--- Choose ---" || model.Provinsi_FASKES_ID_child_2 == "- Please Select -")
                {
                    model.Provinsi_FASKES_ID_child_2 = "";
                }
                if (model.Kotamadya_FASKES_ID_child_2 == "--- Choose ---" || model.Kotamadya_FASKES_ID_child_2 == "- Please Select -")
                {
                    model.Kotamadya_FASKES_ID_child_2 = "";
                }
                if (model.Kecamatan_FASKES_ID_child_2 == "--- Choose ---" || model.Kecamatan_FASKES_ID_child_2 == "- Please Select -")
                {
                    model.Kecamatan_FASKES_ID_child_2 = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 = "";
                }
                if (model.Provinsi_GIGI_ID_child_2 == "--- Choose ---" || model.Provinsi_GIGI_ID_child_2 == "- Please Select -")
                {
                    model.Provinsi_GIGI_ID_child_2 = "";
                }
                if (model.Kotamadya_GIGI_ID_child_2 == "--- Choose ---" || model.Kotamadya_GIGI_ID_child_2 == "- Please Select -")
                {
                    model.Kotamadya_GIGI_ID_child_2 = "";
                }
                if (model.Kecamatan_GIGI_ID_child_2 == "--- Choose ---" || model.Kecamatan_GIGI_ID_child_2 == "- Please Select -")
                {
                    model.Kecamatan_GIGI_ID_child_2 = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 = "";
                }

                if (model.Provinsi_FASKES_ID_child_3 == "--- Choose ---" || model.Provinsi_FASKES_ID_child_3 == "- Please Select -")
                {
                    model.Provinsi_FASKES_ID_child_3 = "";
                }
                if (model.Kotamadya_FASKES_ID_child_3 == "--- Choose ---" || model.Kotamadya_FASKES_ID_child_3 == "- Please Select -")
                {
                    model.Kotamadya_FASKES_ID_child_3 = "";
                }
                if (model.Kecamatan_FASKES_ID_child_3 == "--- Choose ---" || model.Kecamatan_FASKES_ID_child_3 == "- Please Select -")
                {
                    model.Kecamatan_FASKES_ID_child_3 = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 = "";
                }
                if (model.Provinsi_GIGI_ID_child_3 == "--- Choose ---" || model.Provinsi_GIGI_ID_child_3 == "- Please Select -")
                {
                    model.Provinsi_GIGI_ID_child_3 = "";
                }
                if (model.Kotamadya_GIGI_ID_child_3 == "--- Choose ---" || model.Kotamadya_GIGI_ID_child_3 == "- Please Select -")
                {
                    model.Kotamadya_GIGI_ID_child_3 = "";
                }
                if (model.Kecamatan_GIGI_ID_child_3 == "--- Choose ---" || model.Kecamatan_GIGI_ID_child_3 == "- Please Select -")
                {
                    model.Kecamatan_GIGI_ID_child_3 = "";
                }
                if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 == "--- Choose ---" || model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 == "- Please Select -")
                {
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 = "";
                }

                if (model.Birthdate_Parent1 == "")
                {
                    model.Birthdate_Parent1 = "1900-01-01";
                }

                if (model.Birthdate_Parent1 == null)
                {
                    model.Birthdate_Parent1 = "1900-01-01";
                }

                if (model.Birthdate_Parent2 == "")
                {
                    model.Birthdate_Parent2 = "1900-01-01";
                }

                if (model.Birthdate_Parent2 == null)
                {
                    model.Birthdate_Parent2 = "1900-01-01";
                }
                if (model.Birthdate_Parent1 == null)
                {
                    model.Birthdate_Parent1 = "1900-01-01";
                }
                if (model.Birthdate_Parent2 == null)
                {
                    model.Birthdate_Parent2 = "1900-01-01";
                }
                if (model.BPJS_JoinDate == null)
                {
                    model.BPJS_JoinDate = "1900-01-01";
                }
                if (model.DPLK_Joint_Date == null)
                {
                    model.DPLK_Joint_Date = "1900-01-01";
                }
                if (model.Birthdate_child_1 == null)
                {
                    model.Birthdate_child_1 = "1900-01-01";
                }
                if (model.Birthdate_child_2 == null)
                {
                    model.Birthdate_child_2 = "1900-01-01";
                }
                if (model.Birthdate_child_3 == null)
                {
                    model.Birthdate_child_3 = "1900-01-01";
                }
                if (model.Birthdate_Spouse == null)
                {
                    model.Birthdate_Spouse = "1900-01-01";
                }
                if (model.Birthdate == null)
                {
                    model.Birthdate = "1900-01-01";
                }
                if (model.Remark == null)
                {
                    model.Remark = "";
                }

                if (model.Birthdate_child_1 == null)
                {
                    model.Birthdate_child_1 = "1900-01-01";
                }

                if (model.Birthdate_child_2 == null)
                {
                    model.Birthdate_child_2 = "1900-01-01";
                }

                if (model.Birthdate_child_3 == null)
                {
                    model.Birthdate_child_3 = "1900-01-01";
                }
                //sequence
                if (model.Sequence_child_1 == null)
                {
                    model.Sequence_child_1 = "1";
                }
                if (model.Sequence_child_2 == null)
                {
                    model.Sequence_child_2 = "2";
                }
                if (model.Sequence_child_3 == null)
                {
                    model.Sequence_child_3 = "3";
                }
                //emplID
                if (model.EmployeeID_child_1 == null)
                {
                    model.EmployeeID_child_1 = model.EmployeeID;
                }
                if (model.EmployeeID_child_2 == null)
                {
                    model.EmployeeID_child_2 = model.EmployeeID;
                }
                if (model.EmployeeID_child_3 == null)
                {
                    model.EmployeeID_child_3 = model.EmployeeID;
                }

                //Name
                if (model.EmployeeName_child_1 == null)
                {
                    model.EmployeeName_child_1 = "";
                }
                if (model.EmployeeName_child_2 == null)
                {
                    model.EmployeeName_child_2 = "";
                }
                if (model.EmployeeName_child_3 == null)
                {
                    model.EmployeeName_child_3 = "";
                }


                //ActiveBPJS
                if (model.BPJS_Kesehatan_Active == "Not Registered")
                {
                    model.BPJS_Kesehatan_Active = "";
                }
                if (model.BPJS_Kesehatan_Active_Child_1 == "Not Registered")
                {
                    model.BPJS_Kesehatan_Active_Child_1 = "";
                }
                if (model.BPJS_Kesehatan_Active_Child_2 == "Not Registered")
                {
                    model.BPJS_Kesehatan_Active_Child_2 = "";
                }
                if (model.BPJS_Kesehatan_Active_Child_3 == "Not Registered")
                {
                    model.BPJS_Kesehatan_Active_Child_3 = "";
                }
                if (model.BPJS_Kesehatan_Active_Spouse == "Not Registered")
                {
                    model.BPJS_Kesehatan_Active_Spouse = "";
                }


                if (Submit == "Submit")
                {
                    // photo
                    Helper helper = new Helper();
                    try
                    {
                        string filePath = string.Empty;
                        if (file != null)
                        {
                            string path = Server.MapPath("~/Images/Foto/" + model.EntityID + "/" + model.EmployeeID + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }


                            filePath = path + model.EmployeeID + ".JPG";
                            string filePathPNG = path + model.EmployeeID + ".PNG";
                            string filePathJPEG = path + model.EmployeeID + ".JPEG";

                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            if (System.IO.File.Exists(filePathPNG))
                            {
                                System.IO.File.Delete(filePathPNG);
                            }
                            if (System.IO.File.Exists(filePathJPEG))
                            {
                                System.IO.File.Delete(filePathJPEG);
                            }
                            string extension = Path.GetExtension(file.FileName);
                            Photo = Path.GetExtension(file.FileName);
                            //if (extension.ToUpper() == ".JPG" || extension.ToUpper() == ".PNG" || extension.ToUpper() == ".JPEG")
                            if (extension.ToUpper() == ".JPG")
                            {
                                file.SaveAs(filePath);
                            }
                        }
                        //else
                        //{
                        //    Photo = Session["Photo"].ToString();
                        //}
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorRequest"] = ex.ToString();
                        return View(model);
                    }
                    // End Photo
                    //var PhotoJPG = ("~/Images/Foto/" + "Employee" + "/" + model.EmployeeID + ".jpg");
                    var PhotoJPG = ("~/Images/Foto/" + "Employee" + "/" + model.EmployeeID + " - " + model.EmployeeName + ".jpg");

                    if (System.IO.File.Exists(PhotoJPG))
                    {
                        model.ImageURL = PhotoJPG;
                    }
                    //else
                    //{
                    //    model.ImageURL = ("~/Images/Photo");
                    //}
                    model.ImageURL = PhotoJPG;

                    //var PhotoJPG = ("~/Images/Foto/" + "Employee" + "/" + model.EmployeeID + ".jpg");
                    //if (System.IO.File.Exists(PhotoJPG))
                    //{
                    //    model.ImageURL = PhotoJPG;
                    //}
                    //var PhotoPNG = "~/Images/Foto/" + model.EntityID + "/" + model.EmployeeID + ".png".ToUpper();
                    //if (System.IO.File.Exists(PhotoPNG))
                    //{
                    //    model.ImageURL = PhotoPNG;
                    //}
                    //var PhotoJPEG = "~/Images/Foto/" + model.EntityID + "/" + model.EmployeeID + ".jpeg".ToUpper();
                    //if (System.IO.File.Exists(PhotoJPEG))
                    //{
                    //    model.ImageURL = PhotoJPEG;
                    //}
                    //model.ImageURL = PhotoJPG;


                    model.Department1 = DDLDept1();
                    model.Entity = DDLEntity();
                    model.JobTitle = DDLJobTitle();
                    model.Grade = DDLGrade();
                    model.MaritalStatus_ID = DDLMarital();
                    model.Education_ID = DDLEdu();
                    model.Univ_Name_ID = DDLUNIVERSITY();
                    model.Gender = DDLSex();
                    model.PlaceBirth = DDLPB();
                    model.Nationality = DDLNATIONALITY();
                    model.Religion = DDLRELIGION();
                    model.State = DDLSTATE();
                    model.State_ID = string.IsNullOrEmpty(model.State_ID) ? "" : model.State_ID;
                    model.Kotamadya = DDLKOTAMADYA_ALL();
                    model.Kecamatan = DDLKECAMATAN_ALL();
                    model.Kelurahan = DDLKELURAHAN_ALL();

                    model.Gender_Spouse = DDLSex();
                    model.Provinsi_FASKES = DDLSTATE_FASKES();
                    model.Kotamadya_FASKES = DDLKOTAMADYA_FASKES_ALL();
                    model.Kecamatan_FASKES = DDLKECAMATAN_FASKES_ALL();
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama = DDL_FASKES_TK1();

                    model.Provinsi_GIGI = DDLSTATE_GIGI();
                    model.Kotamadya_GIGI = DDLKOTAMADYA_GIGI_ALL();
                    model.Kecamatan_GIGI = DDLKECAMATAN_GIGI_ALL();
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi = DDL_FASKES_GIGI();

                    // Spouse
                    model.Provinsi_FASKES_Spouse = DDLSTATE_FASKES();
                    model.Kotamadya_FASKES_Spouse = DDLKOTAMADYA_FASKES_ALL();
                    model.Kecamatan_FASKES_Spouse = DDLKECAMATAN_FASKES_ALL();
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse = DDL_FASKES_TK1();

                    model.Provinsi_GIGI_Spouse = DDLSTATE_GIGI();
                    model.Kotamadya_GIGI_Spouse = DDLKOTAMADYA_GIGI_ALL();
                    model.Kecamatan_GIGI_Spouse = DDLKECAMATAN_GIGI_ALL();
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse = DDL_FASKES_GIGI();

                    // Child
                    model.Provinsi_FASKES_Child_1 = DDLSTATE_FASKES();
                    model.Kotamadya_FASKES_Child_1 = DDLKOTAMADYA_FASKES_ALL();
                    model.Kecamatan_FASKES_Child_1 = DDLKECAMATAN_FASKES_ALL();
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_1 = DDL_FASKES_TK1();

                    model.Provinsi_GIGI_Child_1 = DDLSTATE_GIGI();
                    model.Kotamadya_GIGI_Child_1 = DDLKOTAMADYA_GIGI_ALL();
                    model.Kecamatan_GIGI_Child_1 = DDLKECAMATAN_GIGI_ALL();
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_1 = DDL_FASKES_GIGI();

                    model.Provinsi_FASKES_Child_2 = DDLSTATE_FASKES();
                    model.Kotamadya_FASKES_Child_2 = DDLKOTAMADYA_FASKES_ALL();
                    model.Kecamatan_FASKES_Child_2 = DDLKECAMATAN_FASKES_ALL();
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_2 = DDL_FASKES_TK1();

                    model.Provinsi_GIGI_Child_2 = DDLSTATE_GIGI();
                    model.Kotamadya_GIGI_Child_2 = DDLKOTAMADYA_GIGI_ALL();
                    model.Kecamatan_GIGI_Child_2 = DDLKECAMATAN_GIGI_ALL();
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_2 = DDL_FASKES_GIGI();

                    model.Provinsi_FASKES_Child_3 = DDLSTATE_FASKES();
                    model.Kotamadya_FASKES_Child_3 = DDLKOTAMADYA_FASKES_ALL();
                    model.Kecamatan_FASKES_Child_3 = DDLKECAMATAN_FASKES_ALL();
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_3 = DDL_FASKES_TK1();

                    model.Provinsi_GIGI_Child_3 = DDLSTATE_GIGI();
                    model.Kotamadya_GIGI_Child_3 = DDLKOTAMADYA_GIGI_ALL();
                    model.Kecamatan_GIGI_Child_3 = DDLKECAMATAN_GIGI_ALL();
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_3 = DDL_FASKES_GIGI();

                    model.Gender_Parent1 = DDLSex();
                    model.Gender_Parent2 = DDLSex();

                    // ADD ON SPOUSE //

                    // JENIS MUTASI SPOUSE
                    model.JENISMUTASI_SPOUSE_ID = DDL_JENISMUTASI_SPOUSE();
                    var JENISMUTASI_SPOUSE = DDL_JENISMUTASI_SPOUSE();
                    List<SelectListItem> JENISMUTASI_SPOUSElistItem = new List<SelectListItem>();
                    foreach (var item in JENISMUTASI_SPOUSE)
                    {
                        JENISMUTASI_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.JENISMUTASI_SPOUSE_ID = new SelectList(JENISMUTASI_SPOUSElistItem, "Value", "Text");
                    // END JENIS MUTASI SPOUSE

                    // FAMILY RELATIONSHIP SPOUSE
                    model.FAMILYRELATIONSHIP_SPOUSE_ID = DDL_FAMILYRELATIONSHIP_SPOUSE();
                    var FAMILYRELATIONSHIP_SPOUSE = DDL_FAMILYRELATIONSHIP_SPOUSE();
                    List<SelectListItem> FAMILYRELATIONSHIP_SPOUSElistItem = new List<SelectListItem>();
                    foreach (var item in FAMILYRELATIONSHIP_SPOUSE)
                    {
                        FAMILYRELATIONSHIP_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.FAMILYRELATIONSHIP_SPOUSE_ID = new SelectList(FAMILYRELATIONSHIP_SPOUSElistItem, "Value", "Text");
                    // END FAMILY RELATIONSHIP SPOUSE

                    //GENDER SPOUSE
                    model.Gender_Spouse = DDL_GENDER_SPOUSE();
                    var GENDER_SPOUSE = DDL_GENDER_SPOUSE();
                    List<SelectListItem> GENDER_SPOUSElistItem = new List<SelectListItem>();
                    foreach (var item in GENDER_SPOUSE)
                    {
                        GENDER_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.Gender_Spouse = new SelectList(GENDER_SPOUSElistItem, "Value", "Text");
                    // END GENDER SPOUSE

                    //MARITAL STATUS SPOUSE
                    model.MARITALSTATUS_SPOUSE_ID = DDL_MARITALSTATUS_SPOUSE();
                    var MARITALSTATUS_SPOUSE = DDL_MARITALSTATUS_SPOUSE();
                    List<SelectListItem> MARITALSTATUS_SPOUSElistItem = new List<SelectListItem>();
                    foreach (var item in MARITALSTATUS_SPOUSE)
                    {
                        MARITALSTATUS_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.MARITALSTATUS_SPOUSE_ID = new SelectList(MARITALSTATUS_SPOUSElistItem, "Value", "Text");
                    //END MARITAL STATUS SPOUSE

                    //NATIONALITY CITIZENSHIP SPOUSE
                    model.CITIZENSHIP_SPOUSE_ID = DDL_CITIZENSHIP_SPOUSE();
                    var CITIZENSHIP_SPOUSE = DDL_CITIZENSHIP_SPOUSE();
                    List<SelectListItem> CITIZENSHIP_SPOUSElistItem = new List<SelectListItem>();
                    foreach (var item in CITIZENSHIP_SPOUSE)
                    {
                        CITIZENSHIP_SPOUSElistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.CITIZENSHIP_SPOUSE_ID = new SelectList(CITIZENSHIP_SPOUSElistItem, "Value", "Text");
                    // END NATIONALITY CITIZENSHIP SPOUSE

                    model.Provinsi_FASKES_Spouse = DDLSTATE_FASKES();
                    model.Kotamadya_FASKES_Spouse = DDLKOTAMADYA_FASKES_ALL();
                    model.Kecamatan_FASKES_Spouse = DDLKECAMATAN_FASKES_ALL();
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse = DDL_FASKES_TK1();

                    model.Provinsi_GIGI_Spouse = DDLSTATE_GIGI();
                    model.Kotamadya_GIGI_Spouse = DDLKOTAMADYA_GIGI_ALL();
                    model.Kecamatan_GIGI_Spouse = DDLKECAMATAN_GIGI_ALL();
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse = DDL_FASKES_GIGI();
                    // END ADD ON SPOUSE

                    // ADD ON CHILD 1

                    // jenis mutasi CHILD 1
                    model.JENISMUTASI_CHILD1_ID = DDL_JENISMUTASICHILD1();
                    var JENISMUTASI_CHILD1 = DDL_JENISMUTASICHILD1();
                    List<SelectListItem> JENISMUTASI_CHILD1listItem = new List<SelectListItem>();
                    foreach (var item in JENISMUTASI_CHILD1)
                    {
                        JENISMUTASI_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.JENISMUTASI_CHILD1_ID = new SelectList(JENISMUTASI_CHILD1listItem, "Value", "Text");
                    // end jenis mutasi 

                    // FAMILY RELATIONSHIP CODE CHILD 1
                    model.FAMILYRELATIONSHIP_CHILD1_ID = DDL_FAMILYRELATIONSHIP_CHILD1();
                    var FAMILYRELATIONSHIP_CHILD1 = DDL_FAMILYRELATIONSHIP_CHILD1();
                    List<SelectListItem> FAMILYRELATIONSHIP_CHILD1listItem = new List<SelectListItem>();
                    foreach (var item in FAMILYRELATIONSHIP_CHILD1)
                    {
                        FAMILYRELATIONSHIP_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.FAMILYRELATIONSHIP_CHILD1_ID = new SelectList(FAMILYRELATIONSHIP_CHILD1listItem, "Value", "Text");
                    // END FAMILY RELATIONSHIP CODE

                    // ADD GENDER CHILD 1
                    model.GENDER_CHILD1_ID = DDL_GENDER_CHILD1();
                    var GENDER_CHILD1 = DDL_GENDER_CHILD1();
                    List<SelectListItem> GENDER_CHILD1listItem = new List<SelectListItem>();
                    foreach (var item in GENDER_CHILD1)
                    {
                        GENDER_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.GENDER_CHILD1_ID = new SelectList(GENDER_CHILD1listItem, "Value", "Text");
                    //END ADD GENDER CHILD 1

                    // ADD MARITAL STATUS CHILD 1
                    model.MARITALSTATUS_CHILD1_ID = DDL_MARITALSTATUS_CHILD1();
                    var MARITALSTATUS_CHILD1 = DDL_MARITALSTATUS_CHILD1();
                    List<SelectListItem> MARITALSTATUS_CHILD1listItem = new List<SelectListItem>();
                    foreach (var item in MARITALSTATUS_CHILD1)
                    {
                        MARITALSTATUS_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.MARITALSTATUS_CHILD1_ID = new SelectList(MARITALSTATUS_CHILD1listItem, "Value", "Text");
                    // END MARITAL STATUS 

                    // NATIONALITY CITIZENSHIP CHILD 1
                    model.CITIZENSHIP_CHILD1_ID = DDL_CITIZENSHIP_CHILD1();
                    var CITIZENSHIP_CHILD1 = DDL_CITIZENSHIP_CHILD1();
                    List<SelectListItem> CITIZENSHIP_CHILD1listItem = new List<SelectListItem>();
                    foreach (var item in CITIZENSHIP_CHILD1)
                    {
                        CITIZENSHIP_CHILD1listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.CITIZENSHIP_CHILD1_ID = new SelectList(CITIZENSHIP_CHILD1listItem, "Value", "Text");
                    // END NATIONALITY CITIZENSHIP

                    model.Provinsi_FASKES_Child_1 = DDLSTATE_FASKES();
                    model.Kotamadya_FASKES_Child_1 = DDLKOTAMADYA_FASKES_ALL();
                    model.Kecamatan_FASKES_Child_1 = DDLKECAMATAN_FASKES_ALL();
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_1 = DDL_FASKES_TK1();

                    model.Provinsi_GIGI_Child_1 = DDLSTATE_GIGI();
                    model.Kotamadya_GIGI_Child_1 = DDLKOTAMADYA_GIGI_ALL();
                    model.Kecamatan_GIGI_Child_1 = DDLKECAMATAN_GIGI_ALL();
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_1 = DDL_FASKES_GIGI();

                    // END ADD ON CHILD 1

                    // ADD ON CHILD 2 //
                    // jenis mutasi CHILD 2
                    model.JENISMUTASI_CHILD2_ID = DDL_JENISMUTASICHILD2();
                    var JENISMUTASI_CHILD2 = DDL_JENISMUTASICHILD2();
                    List<SelectListItem> JENISMUTASI_CHILD2listItem = new List<SelectListItem>();
                    foreach (var item in JENISMUTASI_CHILD2)
                    {
                        JENISMUTASI_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.JENISMUTASI_CHILD2_ID = new SelectList(JENISMUTASI_CHILD2listItem, "Value", "Text");
                    // end jenis mutasi 

                    // FAMILY RELATIONSHIP CODE CHILD 2
                    model.FAMILYRELATIONSHIP_CHILD2_ID = DDL_FAMILYRELATIONSHIP_CHILD2();
                    var FAMILYRELATIONSHIP_CHILD2 = DDL_FAMILYRELATIONSHIP_CHILD2();
                    List<SelectListItem> FAMILYRELATIONSHIP_CHILD2listItem = new List<SelectListItem>();
                    foreach (var item in FAMILYRELATIONSHIP_CHILD2)
                    {
                        FAMILYRELATIONSHIP_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.FAMILYRELATIONSHIP_CHILD2_ID = new SelectList(FAMILYRELATIONSHIP_CHILD2listItem, "Value", "Text");
                    // END FAMILY RELATIONSHIP CODE

                    // ADD GENDER CHILD 2
                    model.GENDER_CHILD2_ID = DDL_GENDER_CHILD2();
                    var GENDER_CHILD2 = DDL_GENDER_CHILD2();
                    List<SelectListItem> GENDER_CHILD2listItem = new List<SelectListItem>();
                    foreach (var item in GENDER_CHILD2)
                    {
                        GENDER_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.GENDER_CHILD2_ID = new SelectList(GENDER_CHILD2listItem, "Value", "Text");
                    //END ADD GENDER CHILD 2

                    // ADD MARITAL STATUS CHILD 2
                    model.MARITALSTATUS_CHILD2_ID = DDL_MARITALSTATUS_CHILD2();
                    var MARITALSTATUS_CHILD2 = DDL_MARITALSTATUS_CHILD2();
                    List<SelectListItem> MARITALSTATUS_CHILD2listItem = new List<SelectListItem>();
                    foreach (var item in MARITALSTATUS_CHILD2)
                    {
                        MARITALSTATUS_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.MARITALSTATUS_CHILD2_ID = new SelectList(MARITALSTATUS_CHILD2listItem, "Value", "Text");
                    // END MARITAL STATUS 

                    // NATIONALITY CITIZENSHIP CHILD 2
                    model.CITIZENSHIP_CHILD2_ID = DDL_CITIZENSHIP_CHILD2();
                    var CITIZENSHIP_CHILD2 = DDL_CITIZENSHIP_CHILD2();
                    List<SelectListItem> CITIZENSHIP_CHILD2listItem = new List<SelectListItem>();
                    foreach (var item in CITIZENSHIP_CHILD2)
                    {
                        CITIZENSHIP_CHILD2listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.CITIZENSHIP_CHILD2_ID = new SelectList(CITIZENSHIP_CHILD2listItem, "Value", "Text");
                    // END NATIONALITY CITIZENSHIP

                    model.Provinsi_FASKES_Child_2 = DDLSTATE_FASKES();
                    model.Kotamadya_FASKES_Child_2 = DDLKOTAMADYA_FASKES_ALL();
                    model.Kecamatan_FASKES_Child_2 = DDLKECAMATAN_FASKES_ALL();
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_2 = DDL_FASKES_TK1();

                    model.Provinsi_GIGI_Child_2 = DDLSTATE_GIGI();
                    model.Kotamadya_GIGI_Child_2 = DDLKOTAMADYA_GIGI_ALL();
                    model.Kecamatan_GIGI_Child_2 = DDLKECAMATAN_GIGI_ALL();
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_2 = DDL_FASKES_GIGI();
                    // END ADD ON CHILD 2 //



                    // ADD ON CHILD 3 //
                    // jenis mutasi CHILD 2
                    model.JENISMUTASI_CHILD3_ID = DDL_JENISMUTASICHILD3();
                    var JENISMUTASI_CHILD3 = DDL_JENISMUTASICHILD3();
                    List<SelectListItem> JENISMUTASI_CHILD3listItem = new List<SelectListItem>();
                    foreach (var item in JENISMUTASI_CHILD3)
                    {
                        JENISMUTASI_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.JENISMUTASI_CHILD3_ID = new SelectList(JENISMUTASI_CHILD3listItem, "Value", "Text");
                    // end jenis mutasi 

                    // FAMILY RELATIONSHIP CODE CHILD 3
                    model.FAMILYRELATIONSHIP_CHILD3_ID = DDL_FAMILYRELATIONSHIP_CHILD3();
                    var FAMILYRELATIONSHIP_CHILD3 = DDL_FAMILYRELATIONSHIP_CHILD3();
                    List<SelectListItem> FAMILYRELATIONSHIP_CHILD3listItem = new List<SelectListItem>();
                    foreach (var item in FAMILYRELATIONSHIP_CHILD3)
                    {
                        FAMILYRELATIONSHIP_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.FAMILYRELATIONSHIP_CHILD3_ID = new SelectList(FAMILYRELATIONSHIP_CHILD3listItem, "Value", "Text");
                    // END FAMILY RELATIONSHIP CODE

                    // ADD GENDER CHILD 3
                    model.GENDER_CHILD3_ID = DDL_GENDER_CHILD3();
                    var GENDER_CHILD3 = DDL_GENDER_CHILD3();
                    List<SelectListItem> GENDER_CHILD3listItem = new List<SelectListItem>();
                    foreach (var item in GENDER_CHILD3)
                    {
                        GENDER_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.GENDER_CHILD3_ID = new SelectList(GENDER_CHILD3listItem, "Value", "Text");
                    //END ADD GENDER CHILD 3

                    // ADD MARITAL STATUS CHILD 3
                    model.MARITALSTATUS_CHILD3_ID = DDL_MARITALSTATUS_CHILD3();
                    var MARITALSTATUS_CHILD3 = DDL_MARITALSTATUS_CHILD3();
                    List<SelectListItem> MARITALSTATUS_CHILD3listItem = new List<SelectListItem>();
                    foreach (var item in MARITALSTATUS_CHILD3)
                    {
                        MARITALSTATUS_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.MARITALSTATUS_CHILD3_ID = new SelectList(MARITALSTATUS_CHILD3listItem, "Value", "Text");
                    // END MARITAL STATUS 

                    // NATIONALITY CITIZENSHIP CHILD 3
                    model.CITIZENSHIP_CHILD3_ID = DDL_CITIZENSHIP_CHILD3();
                    var CITIZENSHIP_CHILD3 = DDL_CITIZENSHIP_CHILD3();
                    List<SelectListItem> CITIZENSHIP_CHILD3listItem = new List<SelectListItem>();
                    foreach (var item in CITIZENSHIP_CHILD3)
                    {
                        CITIZENSHIP_CHILD3listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.CITIZENSHIP_CHILD3_ID = new SelectList(CITIZENSHIP_CHILD3listItem, "Value", "Text");
                    // END NATIONALITY CITIZENSHIP
                    model.Provinsi_FASKES_Child_3 = DDLSTATE_FASKES();
                    model.Kotamadya_FASKES_Child_3 = DDLKOTAMADYA_FASKES_ALL();
                    model.Kecamatan_FASKES_Child_3 = DDLKECAMATAN_FASKES_ALL();
                    model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_3 = DDL_FASKES_TK1();

                    model.Provinsi_GIGI_Child_3 = DDLSTATE_GIGI();
                    model.Kotamadya_GIGI_Child_3 = DDLKOTAMADYA_GIGI_ALL();
                    model.Kecamatan_GIGI_Child_3 = DDLKECAMATAN_GIGI_ALL();
                    model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_3 = DDL_FASKES_GIGI();

                    //END ADD ON CHILD 3 //
                    model.Gender_Parent1 = DDLSex();
                    model.Gender_Parent2 = DDLSex();

                    // jenis mutasi EMPLOYEE
                    model.JenisMutasiDDL = DDL_JENISMUTASI();
                    var jenismutasi = DDL_JENISMUTASI();
                    List<SelectListItem> JenisMutasilistItem = new List<SelectListItem>();
                    foreach (var item in jenismutasi)
                    {
                        JenisMutasilistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.jenismutasiddl = new SelectList(JenisMutasilistItem, "Value", "Text");
                    // end jenis mutasi EMPLOYEE

                    // kode hubkel EMPLOYEE
                    model.KodeHubKelDDL = DDL_KODEHUBKEL();
                    var kodehubkel = DDL_KODEHUBKEL();
                    List<SelectListItem> KodeHubkellistItem = new List<SelectListItem>();
                    foreach (var item in kodehubkel)
                    {
                        KodeHubkellistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.kodehubkelddl = new SelectList(KodeHubkellistItem, "Value", "Text");
                    // end kode hubkel EMPLOYEE

                    // jenis kelamin EMPLOYEE
                    model.JenisKelaminDDL = DDL_JENISKELAMIN();
                    var jeniskelamin = DDL_JENISKELAMIN();
                    List<SelectListItem> jeniskelaminlistItem = new List<SelectListItem>();
                    foreach (var item in jeniskelamin)
                    {
                        jeniskelaminlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.jeniskelaminddl = new SelectList(jeniskelaminlistItem, "Value", "Text");
                    // end jeniskelamin EMPLOYEE

                    // status kawin EMPLOYEE
                    model.StatusKawinDDL = DDL_STATUSKAWIN();
                    var statuskawin = DDL_STATUSKAWIN();
                    List<SelectListItem> statuskawinlistItem = new List<SelectListItem>();
                    foreach (var item in statuskawin)
                    {
                        statuskawinlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.statuskawinddl = new SelectList(statuskawinlistItem, "Value", "Text");
                    // END STATUS KAWIN EMPLOYEE


                    // STATUS EMPLOYEE
                    model.STATUSDDL = DDL_STATUS();
                    var status = DDL_STATUS();
                    List<SelectListItem> statuslistItem = new List<SelectListItem>();
                    foreach (var item in status)
                    {
                        statuslistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.STATUSDDL = new SelectList(statuslistItem, "Value", "Text");
                    // end STATUS EMPLOYEE

                    // KEWARGANEGARAAN EMPLOYEE
                    model.KEWARGANEGARAANDDL = DDL_KEWARGANEGARAAN();
                    var KEWARGANEGARAANDDL = DDL_KEWARGANEGARAAN();
                    List<SelectListItem> KEWARGANEGARAANlistItem = new List<SelectListItem>();
                    foreach (var item in KEWARGANEGARAANDDL)
                    {
                        KEWARGANEGARAANlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                    }
                    ViewBag.KEWARGANEGARAANDDL = new SelectList(KEWARGANEGARAANlistItem, "Value", "Text");
                    // end KEWARGANEGARAAN EMPLOYEE


                    if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 != "")
                    {
                        DataTable dt01 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_FASKES_BY_NAMA_FASKES] '" + model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 + "'");
                        if (dt01.Rows.Count > 0)
                        {
                            model.Provinsi_FASKES_ID_child_1 = dt01.Rows[0]["PROVINSI"].ToString();
                            model.Kotamadya_FASKES_ID_child_1 = dt01.Rows[0]["KOTAMADYA"].ToString();
                            model.Kecamatan_FASKES_ID_child_1 = dt01.Rows[0]["KECAMATAN"].ToString();
                        }
                    }

                    if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 != "")
                    {
                        DataTable dt02 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_FASKES_BY_NAMA_FASKES] '" + model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 + "'");
                        if (dt02.Rows.Count > 0)
                        {
                            model.Provinsi_FASKES_ID_child_2 = dt02.Rows[0]["PROVINSI"].ToString();
                            model.Kotamadya_FASKES_ID_child_2 = dt02.Rows[0]["KOTAMADYA"].ToString();
                            model.Kecamatan_FASKES_ID_child_2 = dt02.Rows[0]["KECAMATAN"].ToString();
                        }
                    }

                    if (model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 != "")
                    {
                        DataTable dt03 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_FASKES_BY_NAMA_FASKES] '" + model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 + "'");
                        if (dt03.Rows.Count > 0)
                        {
                            model.Provinsi_FASKES_ID_child_3 = dt03.Rows[0]["PROVINSI"].ToString();
                            model.Kotamadya_FASKES_ID_child_3 = dt03.Rows[0]["KOTAMADYA"].ToString();
                            model.Kecamatan_FASKES_ID_child_3 = dt03.Rows[0]["KECAMATAN"].ToString();
                        }
                    }

                    if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 != "")
                    {
                        DataTable dt001 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_GIGI_BY_NAMA_FASKES] '" + model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 + "'");
                        if (dt001.Rows.Count > 0)
                        {
                            model.Provinsi_GIGI_ID_child_1 = dt001.Rows[0]["PROVINSI"].ToString();
                            model.Kotamadya_GIGI_ID_child_1 = dt001.Rows[0]["KOTAMADYA"].ToString();
                            model.Kecamatan_GIGI_ID_child_1 = dt001.Rows[0]["KECAMATAN"].ToString();
                        }
                    }

                    if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 != "")
                    {
                        DataTable dt002 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_GIGI_BY_NAMA_FASKES] '" + model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 + "'");
                        if (dt002.Rows.Count > 0)
                        {
                            model.Provinsi_GIGI_ID_child_2 = dt002.Rows[0]["PROVINSI"].ToString();
                            model.Kotamadya_GIGI_ID_child_2 = dt002.Rows[0]["KOTAMADYA"].ToString();
                            model.Kecamatan_GIGI_ID_child_2 = dt002.Rows[0]["KECAMATAN"].ToString();
                        }
                    }

                    if (model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 != "")
                    {
                        DataTable dt003 = Common.ExecuteQuery("dbo.[sp_GET_DETAIL_GIGI_BY_NAMA_FASKES] '" + model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 + "'");
                        if (dt003.Rows.Count > 0)
                        {
                            model.Provinsi_GIGI_ID_child_3 = dt003.Rows[0]["PROVINSI"].ToString();
                            model.Kotamadya_GIGI_ID_child_3 = dt003.Rows[0]["KOTAMADYA"].ToString();
                            model.Kecamatan_GIGI_ID_child_3 = dt003.Rows[0]["KECAMATAN"].ToString();
                        }
                    }


                    // CHILD
                    // FASKES
                    if (model.Provinsi_FASKES_ID_child_1 != "")
                    {
                        model.Kotamadya_FASKES_Child_1 = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_ID_child_1);
                    }
                    if (model.Kotamadya_FASKES_ID_child_1 != "")
                    {
                        model.Kecamatan_FASKES_Child_1 = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_ID_child_1, model.Provinsi_FASKES_ID_child_1);
                    }
                    if (model.Kecamatan_FASKES_ID_child_1 != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_1 = DDLFASKES(model.Kecamatan_FASKES_ID_child_1, model.Provinsi_FASKES_ID_child_1, model.Kotamadya_FASKES_ID_child_1);
                    }

                    //

                    if (model.Provinsi_FASKES_ID_child_2 != "")
                    {
                        model.Kotamadya_FASKES_Child_2 = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_ID_child_2);
                    }
                    if (model.Kotamadya_FASKES_ID_child_2 != "")
                    {
                        model.Kecamatan_FASKES_Child_2 = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_ID_child_2, model.Provinsi_FASKES_ID_child_2);
                    }
                    if (model.Kecamatan_FASKES_ID_child_2 != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_2 = DDLFASKES(model.Kecamatan_FASKES_ID_child_2, model.Provinsi_FASKES_ID_child_2, model.Kotamadya_FASKES_ID_child_2);
                    }

                    //

                    if (model.Provinsi_FASKES_ID_child_3 != "")
                    {
                        model.Kotamadya_FASKES_Child_3 = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_ID_child_3);
                    }
                    if (model.Kotamadya_FASKES_ID_child_3 != "")
                    {
                        model.Kecamatan_FASKES_Child_3 = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_ID_child_3, model.Provinsi_FASKES_ID_child_3);
                    }
                    if (model.Kecamatan_FASKES_ID_child_3 != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_3 = DDLFASKES(model.Kecamatan_FASKES_ID_child_3, model.Provinsi_FASKES_ID_child_3, model.Kotamadya_FASKES_ID_child_3);
                    }

                    // END FASKES

                    //GIGI
                    if (model.Provinsi_GIGI_ID_child_1 != "")
                    {
                        model.Kotamadya_GIGI_Child_1 = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_ID_child_1);
                    }
                    if (model.Kotamadya_GIGI_ID_child_1 != "")
                    {
                        model.Kecamatan_GIGI_Child_1 = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_ID_child_1, model.Provinsi_GIGI_ID_child_1);
                    }
                    if (model.Kecamatan_GIGI_ID_child_1 != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_1 = DDLGIGI(model.Kecamatan_GIGI_ID_child_1, model.Provinsi_GIGI_ID_child_1, model.Kotamadya_GIGI_ID_child_1);
                    }

                    //

                    if (model.Provinsi_GIGI_ID_child_2 != "")
                    {
                        model.Kotamadya_GIGI_Child_2 = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_ID_child_2);
                    }
                    if (model.Kotamadya_GIGI_ID_child_2 != "")
                    {
                        model.Kecamatan_GIGI_Child_2 = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_ID_child_2, model.Provinsi_GIGI_ID_child_2);
                    }
                    if (model.Kecamatan_GIGI_ID_child_2 != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_2 = DDLGIGI(model.Kecamatan_GIGI_ID_child_2, model.Provinsi_GIGI_ID_child_2, model.Kotamadya_GIGI_ID_child_2);
                    }

                    //

                    if (model.Provinsi_GIGI_ID_child_3 != "")
                    {
                        model.Kotamadya_GIGI_Child_3 = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_ID_child_3);
                    }
                    if (model.Kotamadya_GIGI_ID_child_3 != "")
                    {
                        model.Kecamatan_GIGI_Child_3 = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_ID_child_3, model.Provinsi_GIGI_ID_child_3);
                    }
                    if (model.Kecamatan_GIGI_ID_child_3 != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_3 = DDLGIGI(model.Kecamatan_GIGI_ID_child_3, model.Provinsi_GIGI_ID_child_3, model.Kotamadya_GIGI_ID_child_3);
                    }

                    //END GIGI



                    // FASKES ALL
                    if (model.Provinsi_FASKES_ID != "")
                    {
                        model.Kotamadya_FASKES = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_ID);
                    }

                    if (model.Kotamadya_FASKES_ID != "")
                    {
                        model.Kecamatan_FASKES = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_ID, model.Provinsi_FASKES_ID);
                    }
                    if (model.Kecamatan_FASKES_ID != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Tingkat_Pertama = DDLFASKES(model.Kecamatan_FASKES_ID, model.Provinsi_FASKES_ID, model.Kotamadya_FASKES_ID);
                    }
                    // END FASKES

                    //GIGI
                    if (model.Provinsi_GIGI_ID != "")
                    {
                        model.Kotamadya_GIGI = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_ID);
                    }

                    if (model.Kotamadya_GIGI_ID != "")
                    {
                        model.Kecamatan_GIGI = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_ID, model.Provinsi_GIGI_ID);
                    }
                    if (model.Kecamatan_GIGI_ID != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Dokter_Gigi = DDLGIGI(model.Kecamatan_GIGI_ID, model.Provinsi_GIGI_ID, model.Kotamadya_GIGI_ID);
                    }
                    // END GIGI
                    if (model.State_ID != "")
                    {
                        model.Kotamadya = DDLKOTAMADYABYSTATE(model.State_ID);
                    }


                    if (model.State_ID != "")
                    {
                        model.Kotamadya = DDLKOTAMADYABYSTATE(model.State_ID);
                    }
                    if (model.Kotamadya_ID != "")
                    {
                        model.Kecamatan = DDLKECAMATAN(model.Kotamadya_ID, model.State_ID);
                    }
                    if (model.Kecamatan_ID != "")
                    {
                        model.Kelurahan = DDLKELURAHAN(model.Kecamatan_ID, model.State_ID, model.Kotamadya_ID);
                    }


                    // Spouse
                    // FASKES
                    if (model.Provinsi_FASKES_Spouse_ID != "")
                    {
                        model.Kotamadya_FASKES_Spouse = DDLKOTAMADYABYSTATE_FASKES(model.Provinsi_FASKES_Spouse_ID);
                    }

                    if (model.Kotamadya_FASKES_Spouse_ID != "")
                    {
                        model.Kecamatan_FASKES_Spouse = DDLKECAMATAN_FASKES(model.Kotamadya_FASKES_Spouse_ID, model.Provinsi_FASKES_Spouse_ID);
                    }
                    if (model.Kecamatan_FASKES_Spouse_ID != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse = DDLFASKES(model.Kecamatan_FASKES_Spouse_ID, model.Provinsi_FASKES_Spouse_ID, model.Kotamadya_FASKES_Spouse_ID);
                    }
                    // END FASKES

                    //GIGI
                    if (model.Provinsi_GIGI_Spouse_ID != "")
                    {
                        model.Kotamadya_GIGI_Spouse = DDLKOTAMADYABYSTATE_GIGI(model.Provinsi_GIGI_Spouse_ID);
                    }

                    if (model.Kotamadya_GIGI_Spouse_ID != "")
                    {
                        model.Kecamatan_GIGI_Spouse = DDLKECAMATAN_GIGI(model.Kotamadya_GIGI_Spouse_ID, model.Provinsi_GIGI_Spouse_ID);
                    }
                    if (model.Kecamatan_GIGI_Spouse_ID != "")
                    {
                        model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse = DDLGIGI(model.Kecamatan_GIGI_Spouse_ID, model.Provinsi_GIGI_Spouse_ID, model.Kotamadya_GIGI_Spouse_ID);
                    }
                    // END GIGI

                    //BPJS ACTIVE
                    //if (model.BPJS_Kesehatan_Active == "")
                    //{
                    //    model.BPJS_Kesehatan_Active = "Not Registered";
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_1 == "")
                    //{
                    //    model.BPJS_Kesehatan_Active_Child_1 = "Not Registered";
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_2 == "")
                    //{
                    //    model.BPJS_Kesehatan_Active_Child_2 = "Not Registered";
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_3 == "")
                    //{
                    //    model.BPJS_Kesehatan_Active_Child_3 = "Not Registered";
                    //}
                    //if (model.BPJS_Kesehatan_Active_Spouse == "")
                    //{
                    //    model.BPJS_Kesehatan_Active_Spouse = "Not Registered";
                    //}

                    if (model.Education == "- Please Select -")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Education field.');</script>";
                        //return RedirectToAction("ListView", "ListView");
                        return View(model);
                        //return Redirect("Edit" + "?NIK=" + model.EmployeeID + "&Name=" + model.EmployeeName + "&Department=" + model.DepartmentID);
                    }
                    if ((model.Univ_Name == "- Please Select -" || model.Univ_Name == "" || model.Univ_Name == null) && (model.Education == "D3" || model.Education == "D2" || model.Education == "D1" || model.Education == "s3" || model.Education == "S2" || model.Education == "s1"))
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Institution field.');</script>";
                        //return RedirectToAction("ListView", "ListView");
                        return View(model);
                        //return Redirect("Edit" + "?NIK=" + model.EmployeeID + "&Name=" + model.EmployeeName + "&Department=" + model.DepartmentID);
                    }

                    //if (model.State_ID == "--- Choose ---" || model.State_ID == "- Please Select -")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Provinsi field.');</script>";
                    //    return View(model);
                    //}

                    //if (model.Kotamadya_ID == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya field.');</script>";
                    //    return View(model);
                    //}

                    //if (model.Kecamatan_ID == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan field.');</script>";
                    //    return View(model);
                    //}

                    //if (model.Kelurahan_ID == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kelurahan field.');</script>";
                    //    return View(model);
                    //}

                    //if (model.Handphone == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Handphone field.');</script>";
                    //    return View(model);
                    //}

                    //if (model.Home_Address == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Home Address field.');</script>";
                    //    return View(model);
                    //}

                    //if (model.City_ID == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the City field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.Kodepos == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kode Pos field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.RT == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the RT field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.RW == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the RW field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.Home_Phone == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Home Phone field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.KK == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the No.KK field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.NPWP == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the NPWP field.');</script>";
                    //    return View(model);
                    //}
                    if (model.Education == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Education field.');</script>";
                        return View(model);
                    }
                    //if (model.Univ_Name == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Institution field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.Univ_Name == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Institution field.');</script>";
                    //    return View(model);
                    //}

                    ////MARRIED

                    //if (model.MaritalStatus == "Married" && model.Full_Name_Spouse == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Name Spouse field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.MaritalStatus == "Married" && model.Birthdate_Spouse == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Brithdate Spouse field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.MaritalStatus == "Married" && model.Gender_Spouse_ID == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Gender Spouse field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.MaritalStatus == "Married" && model.NIK_Spouse == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the NIK Spouse field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.MaritalStatus == "Married" && model.Handphone_Spouse == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Handphone Spouse field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.MaritalStatus == "Married" && model.MarriedDate == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Married Date field.');</script>";
                    //    return View(model);
                    //}

                    ////SINGLE

                    //if (model.MaritalStatus == "Single" && (model.Full_Name_Parent1 == null || model.Full_Name_Parent2 == null))
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Name Parent field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.MaritalStatus == "Single" && (model.Birthdate_Parent1 == null || model.Birthdate_Parent2 == null))
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Brithdate Parent field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.MaritalStatus == "Single" && (model.Gender_ID_Parent1 == null || model.Gender_ID_Parent2 == null))
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Gender Parent field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.MaritalStatus == "Single" && (model.Handphone_Parent1 == null || model.Handphone_Parent2 == null))
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Handphone Parent field.');</script>";
                    //    return View(model);
                    //}


                    //BPJSActive Employee
                    if (model.BPJS_Kesehatan_Active == "Registered" && model.Provinsi_FASKES_ID == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active == "Registered" && model.Kotamadya_FASKES_ID == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active == "Registered" && model.Kecamatan_FASKES_ID == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active == "Registered" && model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    //if (model.BPJS_Kesehatan_Active == "Registered" && model.Provinsi_GIGI_ID == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active == "Registered" && model.Kotamadya_GIGI_ID == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active == "Registered" && model.Kecamatan_GIGI_ID == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active == "Registered" && model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //BPJSActive Spouse
                    if (model.BPJS_Kesehatan_Active_Spouse == "Registered" && model.Provinsi_FASKES_Spouse_ID == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Spouse == "Registered" && model.Kotamadya_FASKES_Spouse_ID == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Spouse == "Registered" && model.Kecamatan_FASKES_Spouse_ID == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Spouse == "Registered" && model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse_ID == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    //if (model.BPJS_Kesehatan_Active_Spouse == "Registered" && model.Provinsi_GIGI_Spouse_ID == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Spouse == "Registered" && model.Kotamadya_GIGI_Spouse_ID == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Spouse == "Registered" && model.Kecamatan_GIGI_Spouse_ID == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Spouse == "Registered" && model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse_ID == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //BPJSActive Child

                    if (model.BPJS_Kesehatan_Active_Child_1 == "Registered" && model.Provinsi_FASKES_ID_child_1 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Child_1 == "Registered" && model.Kotamadya_FASKES_ID_child_1 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Child_1 == "Registered" && model.Kecamatan_FASKES_ID_child_1 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Child_1 == "Registered" && model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    //if (model.BPJS_Kesehatan_Active_Child_1 == "Registered" && model.Provinsi_GIGI_ID_child_1 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_1 == "Registered" && model.Kotamadya_GIGI_ID_child_1 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_1 == "Registered" && model.Kecamatan_GIGI_ID_child_1 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_1 == "Registered" && model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    if (model.BPJS_Kesehatan_Active_Child_2 == "Registered" && model.Provinsi_FASKES_ID_child_2 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Child_2 == "Registered" && model.Kotamadya_FASKES_ID_child_2 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Child_2 == "Registered" && model.Kecamatan_FASKES_ID_child_2 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Child_2 == "Registered" && model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    //if (model.BPJS_Kesehatan_Active_Child_2 == "Registered" && model.Provinsi_GIGI_ID_child_2 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_2 == "Registered" && model.Kotamadya_GIGI_ID_child_2 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_2 == "Registered" && model.Kecamatan_GIGI_ID_child_2 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_2 == "Registered" && model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    if (model.BPJS_Kesehatan_Active_Child_3 == "Registered" && model.Provinsi_FASKES_ID_child_3 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Child_3 == "Registered" && model.Kotamadya_FASKES_ID_child_3 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Child_3 == "Registered" && model.Kecamatan_FASKES_ID_child_3 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    if (model.BPJS_Kesehatan_Active_Child_3 == "Registered" && model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 == "")
                    {
                        TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Tingkat Pertama field.');</script>";
                        return View(model);
                    }
                    //if (model.BPJS_Kesehatan_Active_Child_3 == "Registered" && model.Provinsi_GIGI_ID_child_3 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Provinsi Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_3 == "Registered" && model.Kotamadya_GIGI_ID_child_3 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kotamadya Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_3 == "Registered" && model.Kecamatan_GIGI_ID_child_3 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the Kecamatan Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    //if (model.BPJS_Kesehatan_Active_Child_3 == "Registered" && model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 == "")
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please complete the BPJS Faskes Gigi field.');</script>";
                    //    return View(model);
                    //}
                    /////////////////////////////////////////////////////////////////////////////////////////////
                    //FileMarried
                    Helper helperMarried = new Helper();
                    try
                    {
                        string filePath = string.Empty;
                        if (file1 != null)
                        {
                            string path = Server.MapPath("~/Images/Married/" + model.EntityID + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }


                            filePath = path + model.EmployeeID + "_Married.PDF";
                            FilePathMaried = filePath;

                            int fileSize = file1.ContentLength;
                            if (fileSize > (1 * 1000000))
                            {
                                TempData["messageRequest"] = "<script>alert('Size Maximum File 1 Mb');</script>";
                                return View(model);
                            }
                            else
                            {
                                if (System.IO.File.Exists(filePath))
                                {
                                    System.IO.File.Delete(filePath);
                                }

                                string extension = Path.GetExtension(file1.FileName);
                                filename_Married = Path.GetFileName(file1.FileName);
                                //if (extension.ToUpper() == ".JPG" || extension.ToUpper()  == ".PNG" || extension.ToUpper() == ".JPEG")
                                if (extension.ToUpper() == ".PDF")
                                {
                                    file1.SaveAs(filePath);
                                }
                            }
                        }
                        else
                        {
                            filename_Married = Session["FileName_Marriedx"].ToString();
                            FilePathMaried = Session["FilePath_Marriedx"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorRequest"] = ex.ToString();
                        return View(model);
                    }
                    //End FileMarried
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    //FileEdu
                    Helper helperEdu = new Helper();
                    try
                    {
                        string filePath = string.Empty;

                        if (file2 != null)
                        {
                            string path = Server.MapPath("~/Images/Education/" + model.EntityID + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }


                            filePath = path + model.EmployeeID + "_Education.PDF";
                            FilePathEdu = filePath;

                            int fileSize = file2.ContentLength;
                            if (fileSize > (1 * 1000000))
                            {
                                TempData["messageRequest"] = "<script>alert('Size Maximum File 1 Mb');</script>";
                                return View(model);
                            }
                            else
                            {
                                if (System.IO.File.Exists(filePath))
                                {
                                    System.IO.File.Delete(filePath);
                                }

                                string extension = Path.GetExtension(file2.FileName);

                                filename_EDU = Path.GetFileName(file2.FileName);
                                //if (extension.ToUpper() == ".JPG" || extension.ToUpper()  == ".PNG" || extension.ToUpper() == ".JPEG")
                                if (extension.ToUpper() == ".PDF")
                                {
                                    file2.SaveAs(filePath);
                                }
                            }
                        }
                        {
                            string pathmove = Server.MapPath("~/Images/Education/" + model.EntityID + "/");
                            string pathmove2 = Server.MapPath("~/Images/Education/Waiting/" + model.EntityID + "/");

                            string pathmove_1 = Server.MapPath("~/Images/Education/" + model.EntityID + "/" + model.EmployeeID + "_Education.PDF");
                            string pathmove2_1 = Server.MapPath("~/Images/Education/Waiting/" + model.EntityID + "/" + model.EmployeeID + "_Education.PDF");

                            if (!Directory.Exists(pathmove))
                            {
                                Directory.CreateDirectory(pathmove);
                            }

                            //if (System.IO.File.Exists(pathmove_1))
                            //{
                            //    System.IO.File.Delete(pathmove_1);
                            //}

                            if (System.IO.File.Exists(pathmove2_1))
                            {
                                System.IO.File.Delete(pathmove_1);
                                System.IO.File.Move(pathmove2_1, pathmove_1);
                                filename_EDU = Path.GetFileName(pathmove_1);
                                FilePathEdu = pathmove_1;
                            }
                            else
                            {
                                filename_EDU = Session["FileName_Educationx"].ToString();
                                FilePathEdu = Session["FilePath_Educationx"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorRequest"] = ex.ToString();
                        return View(model);
                    }

                    //FilePay
                    Helper helperPay = new Helper();
                    try
                    {
                        string filePath = string.Empty;

                        if (file3 != null)
                        {
                            string path = Server.MapPath("~/Images/Payroll/" + model.EntityID + "/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }


                            filePath = path + model.EmployeeID + "_Payroll.PDF";
                            FilePathPay = filePath;

                            int fileSize = file3.ContentLength;
                            if (fileSize > (1 * 1000000))
                            {
                                TempData["messageRequest"] = "<script>alert('Size Maximum File 1 Mb');</script>";
                                return View(model);
                            }
                            else
                            {
                                if (System.IO.File.Exists(filePath))
                                {
                                    System.IO.File.Delete(filePath);
                                }

                                string extension = Path.GetExtension(file3.FileName);

                                filename_Pay = Path.GetFileName(file3.FileName);
                                //if (extension.ToUpper() == ".JPG" || extension.ToUpper()  == ".PNG" || extension.ToUpper() == ".JPEG")
                                if (extension.ToUpper() == ".PDF")
                                {
                                    file3.SaveAs(filePath);
                                }
                            }
                        }
                        else
                        {
                            filename_Pay = Session["FileName_Payrollx"].ToString();
                            FilePathPay = Session["FilePath_Payrollx"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorRequest"] = ex.ToString();
                        return View(model);
                    }
                    //End FilePay

                    if (model.KTP != null)
                    {
                        if (model.KTP.Length < 16 && model.KTP.ToString() != "")
                        {
                            TempData["messageRequest"] = "<script>alert('KTP Must be 16 Digits.');</script>";
                            return View(model);
                        }
                    }


                    if (model.NIK_Spouse != null)
                    {
                        if (model.NIK_Spouse.Length < 16 && model.NIK_Spouse.ToString() != "")
                        {
                            TempData["messageRequest"] = "<script>alert('KTP Spouse Must be 16 Digits.');</script>";
                            return View(model);
                        }
                    }

                    if (model.NIK_child_1 != null)
                    {
                        if (model.NIK_child_1.Length < 16 && model.NIK_child_1.ToString() != "")
                        {
                            TempData["messageRequest"] = "<script>alert('KTP Child 1 Must be 16 Digits.');</script>";
                            return View(model);
                        }
                    }

                    if (model.NIK_child_2 != null)
                    {
                        if (model.NIK_child_2.Length < 16 && model.NIK_child_2.ToString() != "")
                        {
                            TempData["messageRequest"] = "<script>alert('KTP Child 2 Must be 16 Digits.');</script>";
                            return View(model);
                        }
                    }

                    if (model.NIK_child_3 != null)
                    {
                        if (model.NIK_child_3.Length < 16 && model.NIK_child_3.ToString() != "")
                        {
                            TempData["messageRequest"] = "<script>alert('KTP Child 3 Must be 16 Digits.');</script>";
                            return View(model);
                        }
                    }


                    //if (model.MaritalStatus == "Married" && model.Attach_File_Married != "" && model.Education != "" && model.Attach_File_Education != "")
                    //{
                    //if (model.State_ID != "- Please Select -" && model.Kotamadya_ID != "- Please Select -" && model.Kecamatan_ID != "- Please Select -" && model.Kelurahan_ID != "- Please Select -" && model.Handphone != "")
                        if (model.Education != "- Please Select -" || model.Education != "" && model.Education != null && (model.EmployeeID != "" && model.EmployeeName != ""))
                        {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            DataTable dtcheck = Common.ExecuteQuery("exec sp_entity_StoL '" + model.EntityID + "'");
                            if(dtcheck.Rows.Count > 0)
                            {
                                model.EntityID = dtcheck.Rows[0]["LongEntity"].ToString();
                            }
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_EMPLOYEE_DATA", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@EmployeeID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                                cmd.Parameters.AddWithValue("@EmployeeName", (string.IsNullOrEmpty(model.EmployeeName)) ? "" : model.EmployeeName.ToString());
                                cmd.Parameters.AddWithValue("@Entity", (string.IsNullOrEmpty(model.EntityID)) ? "" : model.EntityID.ToString());
                                cmd.Parameters.AddWithValue("@Department", (string.IsNullOrEmpty(model.DepartmentID1)) ? "" : model.DepartmentID1.ToString());
                                cmd.Parameters.AddWithValue("@Job_Title", (string.IsNullOrEmpty(model.JobTitleID)) ? "" : model.JobTitleID.ToString());
                                cmd.Parameters.AddWithValue("@Grade", Common.Encrypt(model.GradeID));
                                cmd.Parameters.AddWithValue("@Hiredate", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.HiredDate)) ? "1900-01-01" : model.HiredDate.ToString()));

                                cmd.Parameters.AddWithValue("@Reporting_To", (string.IsNullOrEmpty(model.ReportingTo)) ? "" : model.ReportingTo.ToString());
                                cmd.Parameters.AddWithValue("@Email", (string.IsNullOrEmpty(model.Email)) ? "" : model.Email.ToString());
                                cmd.Parameters.AddWithValue("@NIK", (string.IsNullOrEmpty(model.KTP)) ? "" : model.KTP.ToString());
                                cmd.Parameters.AddWithValue("@NPWP", (string.IsNullOrEmpty(model.NPWP)) ? "" : model.NPWP.ToString());
                                cmd.Parameters.AddWithValue("@No_KK", (string.IsNullOrEmpty(model.KK)) ? "" : model.KK.ToString());
                                cmd.Parameters.AddWithValue("@Married_Date", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.MarriedDate)) ? "1900-01-01" : model.MarriedDate.ToString()));
                                cmd.Parameters.AddWithValue("@Marital_Status", (string.IsNullOrEmpty(model.MaritalStatus)) ? "" : model.MaritalStatus.ToString());
                                cmd.Parameters.AddWithValue("@Gender", (string.IsNullOrEmpty(model.Gender_ID)) ? "" : model.Gender_ID.ToString());
                                cmd.Parameters.AddWithValue("@Place_Birth", (string.IsNullOrEmpty(model.PlaceBirth_ID)) ? "" : model.PlaceBirth_ID.ToString());
                                cmd.Parameters.AddWithValue("@Spouse", (string.IsNullOrEmpty(model.Full_Name_Spouse)) ? "" : model.Full_Name_Spouse.ToString());

                                cmd.Parameters.AddWithValue("@Birthdate", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.Birthdate)) ? "1900-01-01" : model.Birthdate.ToString()));
                                cmd.Parameters.AddWithValue("@Nationality", (string.IsNullOrEmpty(model.Nationality_ID)) ? "" : model.Nationality_ID.ToString());
                                cmd.Parameters.AddWithValue("@Religion", (string.IsNullOrEmpty(model.Religion_ID)) ? "" : model.Religion_ID.ToString());
                                cmd.Parameters.AddWithValue("@Home_Address", (string.IsNullOrEmpty(model.Home_Address)) ? "" : model.Home_Address.ToString());
                                cmd.Parameters.AddWithValue("@City", (string.IsNullOrEmpty(model.City_ID)) ? "" : model.City_ID.ToString());
                                cmd.Parameters.AddWithValue("@KodePos", (string.IsNullOrEmpty(model.Kodepos)) ? "" : model.Kodepos.ToString());
                                cmd.Parameters.AddWithValue("@RT", (string.IsNullOrEmpty(model.RT)) ? "" : model.RT.ToString());
                                cmd.Parameters.AddWithValue("@RW", (string.IsNullOrEmpty(model.RW)) ? "" : model.RW.ToString());

                                cmd.Parameters.AddWithValue("@State", (string.IsNullOrEmpty(model.State_ID)) ? "" : model.State_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya", (string.IsNullOrEmpty(model.Kotamadya_ID)) ? "" : model.Kotamadya_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan", (string.IsNullOrEmpty(model.Kecamatan_ID)) ? "" : model.Kecamatan_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kelurahan", (string.IsNullOrEmpty(model.Kelurahan_ID)) ? "" : model.Kelurahan_ID.ToString());
                                cmd.Parameters.AddWithValue("@Physical_Address", (string.IsNullOrEmpty(model.Physical_Address)) ? "" : model.Physical_Address.ToString());
                                cmd.Parameters.AddWithValue("@Personal_Email", (string.IsNullOrEmpty(model.Personal_Email)) ? "" : model.Personal_Email.ToString());
                                cmd.Parameters.AddWithValue("@Home_Phone", (string.IsNullOrEmpty(model.Home_Phone)) ? "" : model.Home_Phone.ToString());
                                cmd.Parameters.AddWithValue("@Handphone", (string.IsNullOrEmpty(model.Handphone)) ? "" : model.Handphone.ToString());

                                cmd.Parameters.AddWithValue("@Payroll_Bank", (string.IsNullOrEmpty(model.Payroll_Bank)) ? "" : model.Payroll_Bank.ToString());
                                cmd.Parameters.AddWithValue("@Payroll_Branch", (string.IsNullOrEmpty(model.Payroll_Branch)) ? "" : model.Payroll_Branch.ToString());
                                cmd.Parameters.AddWithValue("@Payroll_AccountNo", (string.IsNullOrEmpty(model.Payroll_Account_No)) ? "" : model.Payroll_Account_No.ToString());
                                cmd.Parameters.AddWithValue("@Payroll_Account_Name", (string.IsNullOrEmpty(model.Payroll_Accoun_Name)) ? "" : model.Payroll_Accoun_Name.ToString());
                                cmd.Parameters.AddWithValue("@DPLK_NoPeserta", (string.IsNullOrEmpty(model.DPLK_No_Peserta)) ? "" : model.DPLK_No_Peserta.ToString());
                                cmd.Parameters.AddWithValue("@DPLK_Join_Date", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.DPLK_Joint_Date)) ? "1900-01-01" : model.DPLK_Joint_Date.ToString())); 
                                cmd.Parameters.AddWithValue("@DPLK_saldo", Convert.ToDecimal(model.DPLK_Saldo));
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPolis", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Polis)) ? "" : model.Health_Plan_Membership_No_Polis.ToString());


                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPeserta", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Peserta)) ? "" : model.Health_Plan_Membership_No_Peserta.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoKartu", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Kartu)) ? "" : model.Health_Plan_Membership_No_Kartu.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Inap", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Inap)) ? "" : model.Health_Plan_Benefit_Rawat_Inap.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Jalan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Jalan)) ? "" : model.Health_Plan_Benefit_Rawat_Jalan.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Persalinan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Persalinan)) ? "" : model.Health_Plan_Benefit_Persalinan.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Gigi", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Gigi)) ? "" : model.Health_Plan_Benefit_Gigi.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Kacamata", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Kacamata)) ? "" : model.Health_Plan_Benefit_Kacamata.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Ketenagakerjaan_ID", (string.IsNullOrEmpty(model.BPJS_Ketenagakerjaan_ID)) ? "" : model.BPJS_Ketenagakerjaan_ID.ToString());

                                cmd.Parameters.AddWithValue("@BPJS_JoinDate", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.BPJS_JoinDate)) ? "1900-01-01" : model.BPJS_JoinDate.ToString()));
                                
                                cmd.Parameters.AddWithValue("@BPJS_Pensiun_ID", (string.IsNullOrEmpty(model.BPJS_Pensiun_ID)) ? "" : model.BPJS_Pensiun_ID.ToString());

                                cmd.Parameters.AddWithValue("@Class", (string.IsNullOrEmpty(model.Class)) ? "" : model.Class.ToString());

                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Active)) ? "" : model.BPJS_Kesehatan_Active.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active_Spouse", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Active_Spouse)) ? "" : model.BPJS_Kesehatan_Active_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active_Child_1", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Active_Child_1)) ? "" : model.BPJS_Kesehatan_Active_Child_1.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active_Child_2", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Active_Child_2)) ? "" : model.BPJS_Kesehatan_Active_Child_2.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Active_Child_3", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Active_Child_3)) ? "" : model.BPJS_Kesehatan_Active_Child_3.ToString());

                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_ID", (string.IsNullOrEmpty(model.BPJS_Kesehatan_ID)) ? "" : model.BPJS_Kesehatan_ID.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Tingkat_Pertama", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID)) ? "" : model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Dokter_Gigi", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID)) ? "" : model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID.ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_TK", (string.IsNullOrEmpty(model.Provinsi_FASKES_ID)) ? "" : model.Provinsi_FASKES_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_TK", (string.IsNullOrEmpty(model.Kotamadya_FASKES_ID)) ? "" : model.Kotamadya_FASKES_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_TK", (string.IsNullOrEmpty(model.Kecamatan_FASKES_ID)) ? "" : model.Kecamatan_FASKES_ID.ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_Gigi", (string.IsNullOrEmpty(model.Provinsi_GIGI_ID)) ? "" : model.Provinsi_GIGI_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_Gigi", (string.IsNullOrEmpty(model.Kotamadya_GIGI_ID)) ? "" : model.Kotamadya_GIGI_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_Gigi", (string.IsNullOrEmpty(model.Kecamatan_GIGI_ID)) ? "" : model.Kecamatan_GIGI_ID.ToString());

                                cmd.Parameters.AddWithValue("@Attach_File_Married", (string.IsNullOrEmpty(FilePathMaried)) ? "" : FilePathMaried.ToString());
                                cmd.Parameters.AddWithValue("@FileName_Married", (string.IsNullOrEmpty(filename_Married)) ? "" : filename_Married.ToString());
                                cmd.Parameters.AddWithValue("@Attach_File_Education", (string.IsNullOrEmpty(FilePathEdu)) ? "" : FilePathEdu.ToString());
                                cmd.Parameters.AddWithValue("@FileName_Education", (string.IsNullOrEmpty(filename_EDU)) ? "" : filename_EDU.ToString());
                                cmd.Parameters.AddWithValue("@Attach_File_Payroll", (string.IsNullOrEmpty(FilePathPay)) ? "" : FilePathPay.ToString());
                                cmd.Parameters.AddWithValue("@FileName_Payroll", (string.IsNullOrEmpty(filename_Pay)) ? "" : filename_Pay.ToString());
                                cmd.Parameters.AddWithValue("@Univ_Name", (string.IsNullOrEmpty(model.Univ_Name)) ? "" : model.Univ_Name.ToString());
                                cmd.Parameters.AddWithValue("@Education", (string.IsNullOrEmpty(model.Education)) ? "" : model.Education.ToString());



                                cmd.Parameters.AddWithValue("@UploadID", 0);

                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_Rev_EMPLOYEE_DATA", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Employee_ID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Mutasi", (string.IsNullOrEmpty(model.JenisMutasi)) ? "" : model.JenisMutasi.ToString());
                                cmd.Parameters.AddWithValue("@Tgl_AktifBerlakuMutasi", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TglAktifBerlakuMutasi)) ? "1900-01-01" : model.TglAktifBerlakuMutasi.ToString()));
                                cmd.Parameters.AddWithValue("@Kode_HubKel", (string.IsNullOrEmpty(model.KodeHubKel)) ? "" : model.KodeHubKel.ToString());
                                cmd.Parameters.AddWithValue("@Tempat_Lahir", (string.IsNullOrEmpty(model.TempatLahir)) ? "" : model.TempatLahir.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Kelamin", (string.IsNullOrEmpty(model.JenisKelamin)) ? "" : model.JenisKelamin.ToString());
                                cmd.Parameters.AddWithValue("@Status_Kawin", (string.IsNullOrEmpty(model.StatusKawin)) ? "" : model.StatusKawin.ToString()); //GetDateInYYYYMMDD((string.IsNullOrEmpty(model.HiredDate)) ? "" : model.HiredDate.ToString()));

                                cmd.Parameters.AddWithValue("@Alamat_TempatTinggal", (string.IsNullOrEmpty(model.AlamatTempatTinggal)) ? "" : model.AlamatTempatTinggal.ToString());
                                cmd.Parameters.AddWithValue("@RT", (string.IsNullOrEmpty(model.RT2)) ? "" : model.RT2.ToString());
                                cmd.Parameters.AddWithValue("@RW", (string.IsNullOrEmpty(model.RW2)) ? "" : model.RW2.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Pos", (string.IsNullOrEmpty(model.KodePosAddn)) ? "" : model.KodePosAddn.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Kecamatan", (string.IsNullOrEmpty(model.kodekecamatan)) ? "" : model.kodekecamatan.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Desa", (string.IsNullOrEmpty(model.kodedesa)) ? "" : model.kodedesa.ToString());

                                cmd.Parameters.AddWithValue("@Kode_Faskes_Tk1", (string.IsNullOrEmpty(model.FaskesTKICode)) ? "" : model.FaskesTKICode.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Faskes_DokterGigi", (string.IsNullOrEmpty(model.FASKESDRGCODE)) ? "" : model.FASKESDRGCODE.ToString());
                                cmd.Parameters.AddWithValue("@No_TelpPeserta", (string.IsNullOrEmpty(model.NOMORPESERTA)) ? "" : model.NOMORPESERTA.ToString());
                                cmd.Parameters.AddWithValue("@Email", (string.IsNullOrEmpty(model.EMAILS)) ? "" : model.EMAILS.ToString());
                                
                                cmd.Parameters.AddWithValue("@ID_Person", (string.IsNullOrEmpty(model.IDPERSON)) ? "" : model.IDPERSON.ToString());
                                cmd.Parameters.AddWithValue("@Status", (string.IsNullOrEmpty(model.STATUS)) ? "" : model.STATUS.ToString());
                                cmd.Parameters.AddWithValue("@TMT_Kerja", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TMTKERJA)) ? "1900-01-01" : model.TMTKERJA.ToString()));
                                cmd.Parameters.AddWithValue("@Kewarganegaraan", (string.IsNullOrEmpty(model.KEWARGANEGARAAN)) ? "" : model.KEWARGANEGARAAN.ToString());
                                cmd.Parameters.AddWithValue("@No_KartuAsuransi", (string.IsNullOrEmpty(model.NOMORKARTU)) ? "" : model.NOMORKARTU.ToString());
                                cmd.Parameters.AddWithValue("@Nama_Asuransi", (string.IsNullOrEmpty(model.NAMAASURANSI)) ? "" : model.NAMAASURANSI.ToString());
                                cmd.Parameters.AddWithValue("@No_Passport", (string.IsNullOrEmpty(model.NOMORPASSPORT)) ? "" : model.NOMORPASSPORT.ToString());
                                
                                cmd.Parameters.AddWithValue("@FLAG", (string.IsNullOrEmpty(model.FLAG)) ? "0" : model.FLAG.ToString());
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                                cmd.Parameters.AddWithValue("@Faskes_Tk_Code", (string.IsNullOrEmpty(model.FaskesTKICode)) ? "" : model.FaskesTKICode.ToString());
                                cmd.Parameters.AddWithValue("@Faskes_drg_Code", (string.IsNullOrEmpty(model.FASKESDRGCODE)) ? "" : model.FASKESDRGCODE.ToString());
                                cmd.Parameters.AddWithValue("@Job_Title", (string.IsNullOrEmpty(model.jobtittle)) ? "" : model.jobtittle.ToString());
                                cmd.Parameters.AddWithValue("@Fixed_Salary", (string.IsNullOrEmpty(model.fixedsalary)) ? "0" : model.fixedsalary.ToString());
                                cmd.Parameters.AddWithValue("@NPWP", (string.IsNullOrEmpty(model.NPWP2)) ? "" : model.NPWP2.ToString());

                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }

                    if (model.Birthdate_Spouse != "1900-01-01")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_SPOUSE_EMPLOYEE", conn))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
                                cmd.Parameters.AddWithValue("@Full_Name", (string.IsNullOrEmpty(model.Full_Name_Spouse)) ? "" : model.Full_Name_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Birthdate", GetDateInYYYYMMDD(model.Birthdate_Spouse.ToString()));
                                cmd.Parameters.AddWithValue("@Gender", (string.IsNullOrEmpty(model.Gender_Spouse_ID)) ? "" : model.Gender_Spouse_ID.ToString());
                                cmd.Parameters.AddWithValue("@KK_Spouse", (string.IsNullOrEmpty(model.KK_Spouse)) ? "" : model.KK_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@NIK", (string.IsNullOrEmpty(model.NIK_Spouse)) ? "" : model.NIK_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Handphone", (string.IsNullOrEmpty(model.Handphone_Spouse)) ? "" : model.Handphone_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPolis", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Polis_Spouse)) ? "" : model.Health_Plan_Membership_No_Polis_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPeserta", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Peserta_Spouse)) ? "" : model.Health_Plan_Membership_No_Peserta_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoKartu", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Kartu_Spouse)) ? "" : model.Health_Plan_Membership_No_Kartu_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Inap", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Inap_Spouse)) ? "" : model.Health_Plan_Benefit_Rawat_Inap_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Jalan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Jalan_Spouse)) ? "" : model.Health_Plan_Benefit_Rawat_Jalan_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Persalinan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Persalinan_Spouse)) ? "" : model.Health_Plan_Benefit_Persalinan_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Gigi", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Gigi_Spouse)) ? "" : model.Health_Plan_Benefit_Gigi_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Kacamata", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Kacamata_Spouse)) ? "" : model.Health_Plan_Benefit_Kacamata_Spouse.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_ID", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Spouse_ID)) ? "" : model.BPJS_Kesehatan_Spouse_ID.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Tingkat_Pertama", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse_ID)) ? "" : model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse_ID.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Dokter_Gigi", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse_ID)) ? "" : model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse_ID.ToString());
                                cmd.Parameters.AddWithValue("@UploadID", 0);
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                                cmd.Parameters.AddWithValue("@idx", (string.IsNullOrEmpty(model.idx_spouse)) ? "" : model.idx_spouse.ToString());

                                cmd.Parameters.AddWithValue("@Class_Spouse", (string.IsNullOrEmpty(model.Class_Spouse)) ? "" : model.Class_Spouse.ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_TK", (string.IsNullOrEmpty(model.Provinsi_FASKES_Spouse_ID)) ? "" : model.Provinsi_FASKES_Spouse_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_TK", (string.IsNullOrEmpty(model.Kotamadya_FASKES_Spouse_ID)) ? "" : model.Kotamadya_FASKES_Spouse_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_TK", (string.IsNullOrEmpty(model.Kecamatan_FASKES_Spouse_ID)) ? "" : model.Kecamatan_FASKES_Spouse_ID.ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_Gigi", (string.IsNullOrEmpty(model.Provinsi_GIGI_Spouse_ID)) ? "" : model.Provinsi_GIGI_Spouse_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_Gigi", (string.IsNullOrEmpty(model.Kotamadya_GIGI_Spouse_ID)) ? "" : model.Kotamadya_GIGI_Spouse_ID.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_Gigi", (string.IsNullOrEmpty(model.Kecamatan_GIGI_Spouse_ID)) ? "" : model.Kecamatan_GIGI_Spouse_ID.ToString());

                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }


                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_Rev_EMPLOYEE_DATA_SPOUSE", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Employee_ID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Mutasi", (string.IsNullOrEmpty(model.JENISMUTASI_SPOUSE)) ? "" : model.JENISMUTASI_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Tgl_AktifBerlakuMutasi", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TglAktifBerlakuMutasi_SPOUSE)) ? "1900-01-01" : model.TglAktifBerlakuMutasi_SPOUSE.ToString()));
                                cmd.Parameters.AddWithValue("@Kode_HubKel", (string.IsNullOrEmpty(model.FAMILYRELATIONSHIP_SPOUSE)) ? "" : model.FAMILYRELATIONSHIP_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Tempat_Lahir", (string.IsNullOrEmpty(model.TEMPATLAHIR_SPOUSE)) ? "" : model.TEMPATLAHIR_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Kelamin", (string.IsNullOrEmpty(model.Gender_Spouse_ID)) ? "" : model.Gender_Spouse_ID.ToString());
                                cmd.Parameters.AddWithValue("@Status_Kawin", (string.IsNullOrEmpty(model.MARITALSTATUS_SPOUSE)) ? "" : model.MARITALSTATUS_SPOUSE.ToString()); //GetDateInYYYYMMDD((string.IsNullOrEmpty(model.HiredDate)) ? "" : model.HiredDate.ToString()));

                                cmd.Parameters.AddWithValue("@Alamat_TempatTinggal", (string.IsNullOrEmpty(model.ADDRESS_SPOUSE)) ? "" : model.ADDRESS_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@RT", (string.IsNullOrEmpty(model.RT_SPOUSE)) ? "" : model.RT_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@RW", (string.IsNullOrEmpty(model.RW_SPOUSE)) ? "" : model.RW_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Pos", (string.IsNullOrEmpty(model.POSTALCODE_SPOUSE)) ? "" : model.POSTALCODE_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Kecamatan", (string.IsNullOrEmpty(model.KECAMATANCODE_SPOUSE)) ? "" : model.KECAMATANCODE_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Desa", (string.IsNullOrEmpty(model.KELURAHANCODE_SPOUSE)) ? "" : model.KELURAHANCODE_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Faskes_Tk1", (string.IsNullOrEmpty(model.FASKESCODETKI_SPOUSE)) ? "" : model.FASKESCODETKI_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Faskes_DokterGigi", (string.IsNullOrEmpty(model.FASKESDRGCODE_SPOUSE)) ? "" : model.FASKESDRGCODE_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@No_TelpPeserta", (string.IsNullOrEmpty(model.MOBILEPHONE_SPOUSE)) ? "" : model.MOBILEPHONE_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Email", (string.IsNullOrEmpty(model.EMAIL_SPOUSE)) ? "" : model.EMAIL_SPOUSE.ToString());

                                cmd.Parameters.AddWithValue("@ID_Person", (string.IsNullOrEmpty(model.EMPLID_SPOUSE)) ? "" : model.EMPLID_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Status", (string.IsNullOrEmpty(model.EMPLSTATUS_SPOUSE)) ? "" : model.EMPLSTATUS_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@TMT_Kerja", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TMTKERJA_SPOUSE)) ? "1900-01-01" : model.TMTKERJA_SPOUSE.ToString()));
                                cmd.Parameters.AddWithValue("@Kewarganegaraan", (string.IsNullOrEmpty(model.CITIZENSHIP_SPOUSE)) ? "" : model.CITIZENSHIP_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@No_KartuAsuransi", (string.IsNullOrEmpty(model.INSURANCECARD_SPOUSE)) ? "" : model.INSURANCECARD_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Nama_Asuransi", (string.IsNullOrEmpty(model.INSURANCENAME_SPOUSE)) ? "" : model.INSURANCENAME_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@No_Passport", (string.IsNullOrEmpty(model.PASSPORT_SPOUSE)) ? "" : model.PASSPORT_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@FLAG", (string.IsNullOrEmpty(model.FLAG)) ? "0" : model.FLAG.ToString());
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());

                                cmd.Parameters.AddWithValue("@Faskes_Tk_Code", (string.IsNullOrEmpty(model.FASKESCODETKI_SPOUSE)) ? "" : model.FASKESCODETKI_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Faskes_drg_Code", (string.IsNullOrEmpty(model.FASKESDRGCODE_SPOUSE)) ? "" : model.FASKESDRGCODE_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Job_Title", (string.IsNullOrEmpty(model.JOBTITTLE_SPOUSE)) ? "" : model.JOBTITTLE_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@Fixed_Salary", (string.IsNullOrEmpty(model.FIXEDSALARY_SPOUSE)) ? "0" : model.FIXEDSALARY_SPOUSE.ToString());
                                cmd.Parameters.AddWithValue("@NPWP", (string.IsNullOrEmpty(model.NPWP_SPOUSE)) ? "" : model.NPWP_SPOUSE.ToString());


                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }

                    if (model.Birthdate_Parent1 != "")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_PARENT", conn))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@idx", (string.IsNullOrEmpty(model.idx_parent1)) ? "" : model.idx_parent1.ToString());
                                cmd.Parameters.AddWithValue("@EMPLOYEEID", model.EmployeeID);
                                cmd.Parameters.AddWithValue("@FULL_NAME_PARENT1", (string.IsNullOrEmpty(model.Full_Name_Parent1)) ? "" : model.Full_Name_Parent1.ToString());
                                cmd.Parameters.AddWithValue("@BIRTHDATE_PARENT1", GetDateInYYYYMMDD(model.Birthdate_Parent1.ToString()));
                                cmd.Parameters.AddWithValue("@GENDER_PARENT1", (string.IsNullOrEmpty(model.Gender_ID_Parent1)) ? "" : model.Gender_ID_Parent1.ToString());
                                cmd.Parameters.AddWithValue("@HANDPHONE_PARENT1", (string.IsNullOrEmpty(model.Handphone_Parent1)) ? "" : model.Handphone_Parent1.ToString());

                                cmd.Parameters.AddWithValue("@FULL_NAME_PARENT2", (string.IsNullOrEmpty(model.Full_Name_Parent2)) ? "" : model.Full_Name_Parent2.ToString());
                                cmd.Parameters.AddWithValue("@BIRTHDATE_PARENT2", GetDateInYYYYMMDD(model.Birthdate_Parent2.ToString()));
                                cmd.Parameters.AddWithValue("@GENDER_PARENT2", (string.IsNullOrEmpty(model.Gender_ID_Parent2)) ? "" : model.Gender_ID_Parent2.ToString());
                                cmd.Parameters.AddWithValue("@HANDPHONE_PARENT2", (string.IsNullOrEmpty(model.Handphone_Parent2)) ? "" : model.Handphone_Parent2.ToString());
                                cmd.Parameters.AddWithValue("@CREATEDBY", (string.IsNullOrEmpty(Session["UserID"].ToString()) ? "" : Session["UserID"].ToString()));

                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }

                    if (model.Birthdate_child_1 != "1900-01-01")
                    //if (model.idx_child_1 == "0" && model.EmployeeName_child_1 != "null")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_CHILD_EMPLOYEE_CHILD_1", conn))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                //cmd.Parameters.AddWithValue("@Child_Sequence", model.Sequence_child_1);
                                cmd.Parameters.AddWithValue("@idx", (string.IsNullOrEmpty(model.idx_child_1)) ? "" : model.idx_child_1.ToString());
                                cmd.Parameters.AddWithValue("@EmployeeID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                                cmd.Parameters.AddWithValue("@Birthdate", GetDateInYYYYMMDD(model.Birthdate_child_1.ToString()));
                                cmd.Parameters.AddWithValue("@Full_Name", (string.IsNullOrEmpty(model.EmployeeName_child_1)) ? "" : model.EmployeeName_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Gender", (string.IsNullOrEmpty(model.Gender_ID_child_1)) ? "" : model.Gender_ID_child_1.ToString());
                                cmd.Parameters.AddWithValue("@KK", (string.IsNullOrEmpty(model.KK_Child_1)) ? "" : model.KK_Child_1.ToString());
                                cmd.Parameters.AddWithValue("@NIK", (string.IsNullOrEmpty(model.NIK_child_1)) ? "" : model.NIK_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Handphone", (string.IsNullOrEmpty(model.Handphone_child_1)) ? "" : model.Handphone_child_1.ToString());

                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPolis", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Polis_child_1)) ? "" : model.Health_Plan_Membership_No_Polis_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPeserta", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Peserta_child_1)) ? "" : model.Health_Plan_Membership_No_Peserta_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoKartu", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Kartu_child_1)) ? "" : model.Health_Plan_Membership_No_Kartu_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Inap", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Inap_child_1)) ? "" : model.Health_Plan_Benefit_Rawat_Inap_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Jalan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Jalan_child_1)) ? "" : model.Health_Plan_Benefit_Rawat_Jalan_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Persalinan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Persalinan_child_1)) ? "" : model.Health_Plan_Benefit_Persalinan_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Gigi", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Gigi_child_1)) ? "" : model.Health_Plan_Benefit_Gigi_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Kacamata", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Kacamata_child_1)) ? "" : model.Health_Plan_Benefit_Kacamata_child_1.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_ID", (string.IsNullOrEmpty(model.BPJS_Kesehatan_ID_child_1)) ? "" : model.BPJS_Kesehatan_ID_child_1.ToString());

                                cmd.Parameters.AddWithValue("@Class_Child", (string.IsNullOrEmpty(model.Class_Child_1)) ? "" : model.Class_Child_1.ToString());

                                cmd.Parameters.AddWithValue("@UploadID", 0);
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_TK", (string.IsNullOrEmpty(model.Provinsi_FASKES_ID_child_1)) ? "" : model.Provinsi_FASKES_ID_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_TK", (string.IsNullOrEmpty(model.Kotamadya_FASKES_ID_child_1)) ? "" : model.Kotamadya_FASKES_ID_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_TK", (string.IsNullOrEmpty(model.Kecamatan_FASKES_ID_child_1)) ? "" : model.Kecamatan_FASKES_ID_child_1.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Tingkat_Pertama", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1)) ? "" : model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1.ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_Gigi", (string.IsNullOrEmpty(model.Provinsi_GIGI_ID_child_1)) ? "" : model.Provinsi_GIGI_ID_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_Gigi", (string.IsNullOrEmpty(model.Kotamadya_GIGI_ID_child_1)) ? "" : model.Kotamadya_GIGI_ID_child_1.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_Gigi", (string.IsNullOrEmpty(model.Kecamatan_GIGI_ID_child_1)) ? "" : model.Kecamatan_GIGI_ID_child_1.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Dokter_Gigi", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1)) ? "" : model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1.ToString());

                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_Rev_EMPLOYEE_DATA_CHILD", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Employee_ID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Mutasi", (string.IsNullOrEmpty(model.JENISMUTASI_CHILD1)) ? "" : model.JENISMUTASI_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Tgl_AktifBerlakuMutasi", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TglAktifBerlakuMutasi)) ? "1900-01-01" : model.TglAktifBerlakuMutasi.ToString()));

                                cmd.Parameters.AddWithValue("@Kode_HubKel", (string.IsNullOrEmpty(model.FAMILYRELATIONSHIP_CHILD1)) ? "" : model.FAMILYRELATIONSHIP_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Tempat_Lahir", (string.IsNullOrEmpty(model.TEMPATLAHIR_CHILD1)) ? "" : model.TEMPATLAHIR_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Kelamin", (string.IsNullOrEmpty(model.GENDER_CHILD1)) ? "" : model.GENDER_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Status_Kawin", (string.IsNullOrEmpty(model.MARITALSTATUS_CHILD1)) ? "" : model.MARITALSTATUS_CHILD1.ToString()); //GetDateInYYYYMMDD((string.IsNullOrEmpty(model.HiredDate)) ? "" : model.HiredDate.ToString()));

                                cmd.Parameters.AddWithValue("@Alamat_TempatTinggal", (string.IsNullOrEmpty(model.ADDRESS_CHILD1)) ? "" : model.ADDRESS_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@RT", (string.IsNullOrEmpty(model.RT_CHILD1)) ? "" : model.RT_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@RW", (string.IsNullOrEmpty(model.RW_CHILD1)) ? "" : model.RW_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Pos", (string.IsNullOrEmpty(model.POSTALCODE_CHILD1)) ? "" : model.POSTALCODE_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Kecamatan", (string.IsNullOrEmpty(model.KECAMATANCODE_CHILD1)) ? "" : model.KECAMATANCODE_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Desa", (string.IsNullOrEmpty(model.KELURAHANCODE_CHILD1)) ? "" : model.KELURAHANCODE_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Faskes_Tk1", (string.IsNullOrEmpty(model.FASKESCODETKI_CHILD1)) ? "" : model.FASKESCODETKI_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Faskes_DokterGigi", (string.IsNullOrEmpty(model.FASKESDRGCODE_CHILD1)) ? "" : model.FASKESDRGCODE_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@No_TelpPeserta", (string.IsNullOrEmpty(model.MOBILEPHONE_CHILD1)) ? "" : model.MOBILEPHONE_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Email", (string.IsNullOrEmpty(model.EMAIL_CHILD1)) ? "" : model.EMAIL_CHILD1.ToString());

                                cmd.Parameters.AddWithValue("@ID_Person", (string.IsNullOrEmpty(model.EMPLID_CHILD1)) ? "" : model.EMPLID_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Status", (string.IsNullOrEmpty(model.EMPLSTATUS_CHILD1)) ? "" : model.EMPLSTATUS_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@TMT_Kerja", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TMTKERJA_CHILD1)) ? "1900-01-01" : model.TMTKERJA_CHILD1.ToString()));
                                cmd.Parameters.AddWithValue("@Kewarganegaraan", (string.IsNullOrEmpty(model.CITIZENSHIP_CHILD1)) ? "" : model.CITIZENSHIP_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@No_KartuAsuransi", (string.IsNullOrEmpty(model.INSURANCECARD_CHILD1)) ? "" : model.INSURANCECARD_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Nama_Asuransi", (string.IsNullOrEmpty(model.INSURANCENAME_CHILD1)) ? "" : model.INSURANCENAME_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@No_Passport", (string.IsNullOrEmpty(model.PASSPORT_CHILD1)) ? "" : model.PASSPORT_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@FLAG", (string.IsNullOrEmpty(model.FLAG)) ? "0" : model.FLAG.ToString());
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                                cmd.Parameters.AddWithValue("@FULL_NAME", (string.IsNullOrEmpty(model.EmployeeName_child_1)) ? "" : model.EmployeeName_child_1.ToString());


                                cmd.Parameters.AddWithValue("@Faskes_Tk_Code", (string.IsNullOrEmpty(model.FASKESCODETKI_CHILD1)) ? "" : model.FASKESCODETKI_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Faskes_drg_Code", (string.IsNullOrEmpty(model.FASKESDRGCODE_CHILD1)) ? "" : model.FASKESDRGCODE_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Job_Title", (string.IsNullOrEmpty(model.JOBTITTLE_CHILD1)) ? "" : model.JOBTITTLE_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@Fixed_Salary", (string.IsNullOrEmpty(model.FIXEDSALARY_CHILD1)) ? "0" : model.FIXEDSALARY_CHILD1.ToString());
                                cmd.Parameters.AddWithValue("@NPWP", (string.IsNullOrEmpty(model.NPWP_CHILD1)) ? "" : model.NPWP_CHILD1.ToString());


                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }

                    if (model.Birthdate_child_2 != "1900-01-01")
                    //if (model.idx_child_2 == "0" && model.EmployeeName_child_2 != "null")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_CHILD_EMPLOYEE_CHILD_2", conn))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                //cmd.Parameters.AddWithValue("@Child_Sequence", model.Sequence_child_2);
                                cmd.Parameters.AddWithValue("@idx", (string.IsNullOrEmpty(model.idx_child_2)) ? "" : model.idx_child_2.ToString());
                                cmd.Parameters.AddWithValue("@EmployeeID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                                cmd.Parameters.AddWithValue("@Birthdate", GetDateInYYYYMMDD(model.Birthdate_child_2.ToString()));
                                cmd.Parameters.AddWithValue("@Full_Name", (string.IsNullOrEmpty(model.EmployeeName_child_2)) ? "" : model.EmployeeName_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Gender", (string.IsNullOrEmpty(model.Gender_ID_child_2)) ? "" : model.Gender_ID_child_2.ToString());
                                cmd.Parameters.AddWithValue("@KK", (string.IsNullOrEmpty(model.KK_Child_2)) ? "" : model.KK_Child_2.ToString());
                                cmd.Parameters.AddWithValue("@NIK", (string.IsNullOrEmpty(model.NIK_child_2)) ? "" : model.NIK_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Handphone", (string.IsNullOrEmpty(model.Handphone_child_2)) ? "" : model.Handphone_child_2.ToString());

                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPolis", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Polis_child_2)) ? "" : model.Health_Plan_Membership_No_Polis_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPeserta", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Peserta_child_2)) ? "" : model.Health_Plan_Membership_No_Peserta_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoKartu", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Kartu_child_2)) ? "" : model.Health_Plan_Membership_No_Kartu_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Inap", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Inap_child_2)) ? "" : model.Health_Plan_Benefit_Rawat_Inap_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Jalan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Jalan_child_2)) ? "" : model.Health_Plan_Benefit_Rawat_Jalan_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Persalinan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Persalinan_child_2)) ? "" : model.Health_Plan_Benefit_Persalinan_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Gigi", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Gigi_child_2)) ? "" : model.Health_Plan_Benefit_Gigi_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Kacamata", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Kacamata_child_2)) ? "" : model.Health_Plan_Benefit_Kacamata_child_2.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_ID", (string.IsNullOrEmpty(model.BPJS_Kesehatan_ID_child_2)) ? "" : model.BPJS_Kesehatan_ID_child_2.ToString());

                                cmd.Parameters.AddWithValue("@Class_Child", (string.IsNullOrEmpty(model.Class_Child_2)) ? "" : model.Class_Child_2.ToString());

                                cmd.Parameters.AddWithValue("@UploadID", 0);
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_TK", (string.IsNullOrEmpty(model.Provinsi_FASKES_ID_child_2)) ? "" : model.Provinsi_FASKES_ID_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_TK", (string.IsNullOrEmpty(model.Kotamadya_FASKES_ID_child_2)) ? "" : model.Kotamadya_FASKES_ID_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_TK", (string.IsNullOrEmpty(model.Kecamatan_FASKES_ID_child_2)) ? "" : model.Kecamatan_FASKES_ID_child_2.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Tingkat_Pertama", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2)) ? "" : model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2.ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_Gigi", (string.IsNullOrEmpty(model.Provinsi_GIGI_ID_child_2)) ? "" : model.Provinsi_GIGI_ID_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_Gigi", (string.IsNullOrEmpty(model.Kotamadya_GIGI_ID_child_2)) ? "" : model.Kotamadya_GIGI_ID_child_2.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_Gigi", (string.IsNullOrEmpty(model.Kecamatan_GIGI_ID_child_2)) ? "" : model.Kecamatan_GIGI_ID_child_2.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Dokter_Gigi", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2)) ? "" : model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2.ToString());


                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_Rev_EMPLOYEE_DATA_CHILD", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Employee_ID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Mutasi", (string.IsNullOrEmpty(model.JENISMUTASI_CHILD2)) ? "" : model.JENISMUTASI_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Tgl_AktifBerlakuMutasi", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TglAktifBerlakuMutasi_CHILD2)) ? "1900-01-01" : model.TglAktifBerlakuMutasi_CHILD2.ToString()));
                                cmd.Parameters.AddWithValue("@Kode_HubKel", (string.IsNullOrEmpty(model.FAMILYRELATIONSHIP_CHILD2)) ? "" : model.FAMILYRELATIONSHIP_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Tempat_Lahir", (string.IsNullOrEmpty(model.TEMPATLAHIR_CHILD2)) ? "" : model.TEMPATLAHIR_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Kelamin", (string.IsNullOrEmpty(model.GENDER_CHILD2)) ? "" : model.GENDER_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Status_Kawin", (string.IsNullOrEmpty(model.MARITALSTATUS_CHILD2)) ? "" : model.MARITALSTATUS_CHILD2.ToString()); //GetDateInYYYYMMDD((string.IsNullOrEmpty(model.HiredDate)) ? "" : model.HiredDate.ToString()));

                                cmd.Parameters.AddWithValue("@Alamat_TempatTinggal", (string.IsNullOrEmpty(model.ADDRESS_CHILD2)) ? "" : model.ADDRESS_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@RT", (string.IsNullOrEmpty(model.RT_CHILD2)) ? "" : model.RT_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@RW", (string.IsNullOrEmpty(model.RW_CHILD2)) ? "" : model.RW_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Pos", (string.IsNullOrEmpty(model.POSTALCODE_CHILD2)) ? "" : model.POSTALCODE_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Kecamatan", (string.IsNullOrEmpty(model.KECAMATANCODE_CHILD2)) ? "" : model.KECAMATANCODE_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Desa", (string.IsNullOrEmpty(model.KELURAHANCODE_CHILD2)) ? "" : model.KELURAHANCODE_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Faskes_Tk1", (string.IsNullOrEmpty(model.FASKESCODETKI_CHILD2)) ? "" : model.FASKESCODETKI_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Faskes_DokterGigi", (string.IsNullOrEmpty(model.FASKESDRGCODE_CHILD2)) ? "" : model.FASKESDRGCODE_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@No_TelpPeserta", (string.IsNullOrEmpty(model.MOBILEPHONE_CHILD2)) ? "" : model.MOBILEPHONE_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Email", (string.IsNullOrEmpty(model.EMAIL_CHILD2)) ? "" : model.EMAIL_CHILD2.ToString());

                                cmd.Parameters.AddWithValue("@ID_Person", (string.IsNullOrEmpty(model.EMPLID_CHILD2)) ? "" : model.EMPLID_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Status", (string.IsNullOrEmpty(model.EMPLSTATUS_CHILD2)) ? "" : model.EMPLSTATUS_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@TMT_Kerja", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TMTKERJA_CHILD2)) ? "1900-01-01" : model.TMTKERJA_CHILD2.ToString()));
                                cmd.Parameters.AddWithValue("@Kewarganegaraan", (string.IsNullOrEmpty(model.CITIZENSHIP_CHILD2)) ? "" : model.CITIZENSHIP_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@No_KartuAsuransi", (string.IsNullOrEmpty(model.INSURANCECARD_CHILD2)) ? "" : model.INSURANCECARD_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Nama_Asuransi", (string.IsNullOrEmpty(model.INSURANCENAME_CHILD2)) ? "" : model.INSURANCENAME_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@No_Passport", (string.IsNullOrEmpty(model.PASSPORT_CHILD2)) ? "" : model.PASSPORT_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@FLAG", (string.IsNullOrEmpty(model.FLAG)) ? "0" : model.FLAG.ToString());
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                                cmd.Parameters.AddWithValue("@FULL_NAME", (string.IsNullOrEmpty(model.EmployeeName_child_2)) ? "" : model.EmployeeName_child_2.ToString());


                                cmd.Parameters.AddWithValue("@Faskes_Tk_Code", (string.IsNullOrEmpty(model.FASKESCODETKI_CHILD2)) ? "" : model.FASKESCODETKI_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Faskes_drg_Code", (string.IsNullOrEmpty(model.FASKESDRGCODE_CHILD2)) ? "" : model.FASKESDRGCODE_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Job_Title", (string.IsNullOrEmpty(model.JOBTITTLE_CHILD2)) ? "" : model.JOBTITTLE_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@Fixed_Salary", (string.IsNullOrEmpty(model.FIXEDSALARY_CHILD2)) ? "0" : model.FIXEDSALARY_CHILD2.ToString());
                                cmd.Parameters.AddWithValue("@NPWP", (string.IsNullOrEmpty(model.NPWP_CHILD2)) ? "" : model.NPWP_CHILD2.ToString());


                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }

                    if (model.Birthdate_child_3 != "1900-01-01")
                    //if (model.idx_child_3 == "0" && model.EmployeeName_child_3 != "null")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_CHILD_EMPLOYEE_CHILD_3", conn))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                //cmd.Parameters.AddWithValue("@Child_Sequence", model.Sequence_child_3);
                                cmd.Parameters.AddWithValue("@idx", (string.IsNullOrEmpty(model.idx_child_3)) ? "" : model.idx_child_3.ToString());
                                cmd.Parameters.AddWithValue("@EmployeeID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                                cmd.Parameters.AddWithValue("@Birthdate", GetDateInYYYYMMDD(model.Birthdate_child_3.ToString()));
                                cmd.Parameters.AddWithValue("@Full_Name", (string.IsNullOrEmpty(model.EmployeeName_child_3)) ? "" : model.EmployeeName_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Gender", (string.IsNullOrEmpty(model.Gender_ID_child_3)) ? "" : model.Gender_ID_child_3.ToString());
                                cmd.Parameters.AddWithValue("@KK", (string.IsNullOrEmpty(model.KK_Child_3)) ? "" : model.KK_Child_3.ToString());
                                cmd.Parameters.AddWithValue("@NIK", (string.IsNullOrEmpty(model.NIK_child_3)) ? "" : model.NIK_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Handphone", (string.IsNullOrEmpty(model.Handphone_child_3)) ? "" : model.Handphone_child_3.ToString());

                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPolis", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Polis_child_3)) ? "" : model.Health_Plan_Membership_No_Polis_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoPeserta", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Peserta_child_3)) ? "" : model.Health_Plan_Membership_No_Peserta_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Membership_NoKartu", (string.IsNullOrEmpty(model.Health_Plan_Membership_No_Kartu_child_3)) ? "" : model.Health_Plan_Membership_No_Kartu_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Inap", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Inap_child_3)) ? "" : model.Health_Plan_Benefit_Rawat_Inap_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Rawat_Jalan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Rawat_Jalan_child_3)) ? "" : model.Health_Plan_Benefit_Rawat_Jalan_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Persalinan", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Persalinan_child_3)) ? "" : model.Health_Plan_Benefit_Persalinan_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Gigi", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Gigi_child_3)) ? "" : model.Health_Plan_Benefit_Gigi_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Health_Plan_Benefit_Kacamata", (string.IsNullOrEmpty(model.Health_Plan_Benefit_Kacamata_child_3)) ? "" : model.Health_Plan_Benefit_Kacamata_child_3.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_ID", (string.IsNullOrEmpty(model.BPJS_Kesehatan_ID_child_3)) ? "" : model.BPJS_Kesehatan_ID_child_3.ToString());

                                cmd.Parameters.AddWithValue("@Class_Child", (string.IsNullOrEmpty(model.Class_Child_3)) ? "" : model.Class_Child_3.ToString());

                                cmd.Parameters.AddWithValue("@UploadID", 0);
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_TK", (string.IsNullOrEmpty(model.Provinsi_FASKES_ID_child_3)) ? "" : model.Provinsi_FASKES_ID_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_TK", (string.IsNullOrEmpty(model.Kotamadya_FASKES_ID_child_3)) ? "" : model.Kotamadya_FASKES_ID_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_TK", (string.IsNullOrEmpty(model.Kecamatan_FASKES_ID_child_3)) ? "" : model.Kecamatan_FASKES_ID_child_3.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Tingkat_Pertama", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3)) ? "" : model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3.ToString());

                                cmd.Parameters.AddWithValue("@Provinsi_Faskes_Gigi", (string.IsNullOrEmpty(model.Provinsi_GIGI_ID_child_3)) ? "" : model.Provinsi_GIGI_ID_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Kotamadya_Faskes_Gigi", (string.IsNullOrEmpty(model.Kotamadya_GIGI_ID_child_3)) ? "" : model.Kotamadya_GIGI_ID_child_3.ToString());
                                cmd.Parameters.AddWithValue("@Kecamatan_Faskes_Gigi", (string.IsNullOrEmpty(model.Kecamatan_GIGI_ID_child_3)) ? "" : model.Kecamatan_GIGI_ID_child_3.ToString());
                                cmd.Parameters.AddWithValue("@BPJS_Kesehatan_Faskes_Dokter_Gigi", (string.IsNullOrEmpty(model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3)) ? "" : model.BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3.ToString());

                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_Rev_EMPLOYEE_DATA_CHILD", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Employee_ID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Mutasi", (string.IsNullOrEmpty(model.JENISMUTASI_CHILD3)) ? "" : model.JENISMUTASI_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Tgl_AktifBerlakuMutasi", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TglAktifBerlakuMutasi)) ? "1900-01-01" : model.TglAktifBerlakuMutasi.ToString()));

                                cmd.Parameters.AddWithValue("@Kode_HubKel", (string.IsNullOrEmpty(model.FAMILYRELATIONSHIP_CHILD3)) ? "" : model.FAMILYRELATIONSHIP_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Tempat_Lahir", (string.IsNullOrEmpty(model.TEMPATLAHIR_CHILD3)) ? "" : model.TEMPATLAHIR_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Jenis_Kelamin", (string.IsNullOrEmpty(model.GENDER_CHILD3)) ? "" : model.GENDER_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Status_Kawin", (string.IsNullOrEmpty(model.MARITALSTATUS_CHILD3)) ? "" : model.MARITALSTATUS_CHILD3.ToString()); //GetDateInYYYYMMDD((string.IsNullOrEmpty(model.HiredDate)) ? "" : model.HiredDate.ToString()));

                                cmd.Parameters.AddWithValue("@Alamat_TempatTinggal", (string.IsNullOrEmpty(model.ADDRESS_CHILD3)) ? "" : model.ADDRESS_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@RT", (string.IsNullOrEmpty(model.RT_CHILD3)) ? "" : model.RT_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@RW", (string.IsNullOrEmpty(model.RW_CHILD3)) ? "" : model.RW_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Pos", (string.IsNullOrEmpty(model.POSTALCODE_CHILD3)) ? "" : model.POSTALCODE_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Kecamatan", (string.IsNullOrEmpty(model.KECAMATANCODE_CHILD3)) ? "" : model.KECAMATANCODE_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Desa", (string.IsNullOrEmpty(model.KELURAHANCODE_CHILD3)) ? "" : model.KELURAHANCODE_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Faskes_Tk1", (string.IsNullOrEmpty(model.FASKESCODETKI_CHILD3)) ? "" : model.FASKESCODETKI_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Kode_Faskes_DokterGigi", (string.IsNullOrEmpty(model.FASKESDRGCODE_CHILD3)) ? "" : model.FASKESDRGCODE_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@No_TelpPeserta", (string.IsNullOrEmpty(model.MOBILEPHONE_CHILD3)) ? "" : model.MOBILEPHONE_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Email", (string.IsNullOrEmpty(model.EMAIL_CHILD3)) ? "" : model.EMAIL_CHILD3.ToString());

                                cmd.Parameters.AddWithValue("@ID_Person", (string.IsNullOrEmpty(model.EMPLID_CHILD3)) ? "" : model.EMPLID_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Status", (string.IsNullOrEmpty(model.EMPLSTATUS_CHILD3)) ? "" : model.EMPLSTATUS_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@TMT_Kerja", GetDateInYYYYMMDD((string.IsNullOrEmpty(model.TMTKERJA_CHILD3)) ? "1900-01-01" : model.TMTKERJA_CHILD3.ToString()));
                                cmd.Parameters.AddWithValue("@Kewarganegaraan", (string.IsNullOrEmpty(model.CITIZENSHIP_CHILD3)) ? "" : model.CITIZENSHIP_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@No_KartuAsuransi", (string.IsNullOrEmpty(model.INSURANCECARD_CHILD3)) ? "" : model.INSURANCECARD_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Nama_Asuransi", (string.IsNullOrEmpty(model.INSURANCENAME_CHILD3)) ? "" : model.INSURANCENAME_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@No_Passport", (string.IsNullOrEmpty(model.PASSPORT_CHILD3)) ? "" : model.PASSPORT_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@FLAG", (string.IsNullOrEmpty(model.FLAG)) ? "0" : model.FLAG.ToString());
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                                cmd.Parameters.AddWithValue("@FULL_NAME", (string.IsNullOrEmpty(model.EmployeeName_child_3)) ? "" : model.EmployeeName_child_3.ToString());


                                cmd.Parameters.AddWithValue("@Faskes_Tk_Code", (string.IsNullOrEmpty(model.FASKESCODETKI_CHILD3)) ? "" : model.FASKESCODETKI_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Faskes_drg_Code", (string.IsNullOrEmpty(model.FASKESDRGCODE_CHILD3)) ? "" : model.FASKESDRGCODE_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Job_Title", (string.IsNullOrEmpty(model.JOBTITTLE_CHILD3)) ? "" : model.JOBTITTLE_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@Fixed_Salary", (string.IsNullOrEmpty(model.FIXEDSALARY_CHILD3)) ? "0" : model.FIXEDSALARY_CHILD3.ToString());
                                cmd.Parameters.AddWithValue("@NPWP", (string.IsNullOrEmpty(model.NPWP_CHILD3)) ? "" : model.NPWP_CHILD3.ToString());


                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_INSERT_REMARK", conn))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idx_trn", (string.IsNullOrEmpty(model.idx)) ? "" : model.idx.ToString());
                            cmd.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
                            cmd.Parameters.AddWithValue("@Remark", (string.IsNullOrEmpty(model.Remark)) ? "" : model.Remark.ToString());
                            cmd.Parameters.AddWithValue("@CreatedBy", (string.IsNullOrEmpty(Session["UserID"].ToString()) ? "" : Session["UserID"].ToString()));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_INSERT_TRN_REMARKS", conn))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idx_trn", (string.IsNullOrEmpty(model.idx)) ? "" : model.idx.ToString());
                            cmd.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
                            cmd.Parameters.AddWithValue("@REMARKS", (string.IsNullOrEmpty(model.RemarkApproval)) ? "" : model.RemarkApproval.ToString());
                            cmd.Parameters.AddWithValue("@CreatedBy", (string.IsNullOrEmpty(Session["UserID"].ToString()) ? "" : Session["UserID"].ToString()));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_UPDATE_APPROVAL_REJECT_BY", conn))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idx_trn", (string.IsNullOrEmpty(model.idx)) ? "" : model.idx.ToString());
                            cmd.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
                            cmd.Parameters.AddWithValue("@CreatedBy", (string.IsNullOrEmpty(Session["UserID"].ToString()) ? "" : Session["UserID"].ToString()));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_APPROVE", conn))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
                            cmd.Parameters.AddWithValue("@Remarks", (string.IsNullOrEmpty(model.RemarkApproval)) ? "" : model.RemarkApproval.ToString());
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    

                    TempData["Success"] = "Approved Data Success";
                    //TempData["messageRequest"] = "<script>alert('Save Data Success.');</script>";
                    //return Redirect(model.LINK + "?NIK=" + model.EmployeeID + "&Name=" + model.EmployeeName + "&Department=" + model.DepartmentID + "");
                    return RedirectToAction("ListApprove", "ListApprove");
                }
                else if (Submit == "Reject")
                {

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_UPDATE_APPROVAL_REJECT_BY", conn))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@idx_trn", (string.IsNullOrEmpty(model.idx)) ? "" : model.idx.ToString());
                            cmd.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
                            cmd.Parameters.AddWithValue("@CreatedBy", (string.IsNullOrEmpty(Session["UserID"].ToString()) ? "" : Session["UserID"].ToString()));

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_REJECT", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
                            cmd.Parameters.AddWithValue("@Remarks", (string.IsNullOrEmpty(model.RemarkApproval)) ? "" : model.RemarkApproval.ToString());
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }


                    //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                    //{
                    //    using (SqlCommand cmd = new SqlCommand("sp_INSERT_REMARK", conn))
                    //    {

                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        cmd.Parameters.AddWithValue("@idx_trn", (string.IsNullOrEmpty(model.idx)) ? "" : model.idx.ToString());
                    //        cmd.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
                    //        cmd.Parameters.AddWithValue("@Remark", (string.IsNullOrEmpty(model.Remark)) ? "" : model.Remark.ToString());
                    //        cmd.Parameters.AddWithValue("@CreatedBy", (string.IsNullOrEmpty(Session["UserID"].ToString()) ? "" : Session["UserID"].ToString()));

                    //        conn.Open();
                    //        cmd.ExecuteNonQuery();
                    //        conn.Close();
                    //    }
                    //}

                    //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                    //{
                    //    using (SqlCommand cmd = new SqlCommand("sp_SEND_NOTIF_TO_EMPLOYEE_AFTER_REJECT", conn))
                    //    {

                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        cmd.Parameters.AddWithValue("@EMPLOYEEID", (string.IsNullOrEmpty(model.EmployeeID)) ? "" : model.EmployeeID.ToString());
                    //        cmd.Parameters.AddWithValue("@EMPLOYEENAME", (string.IsNullOrEmpty(model.EmployeeName)) ? "" : model.EmployeeName.ToString());

                    //        conn.Open();
                    //        cmd.ExecuteNonQuery();
                    //        conn.Close();
                    //    }
                    //}
                    TempData["Success"] = "Rejected Data Success";
                    //TempData["messageRequest"] = "<script>alert('Save Data Success.');</script>";
                    //return Redirect(model.LINK + "?NIK=" + model.EmployeeID + "&Name=" + model.EmployeeName + "&Department=" + model.DepartmentID + "");
                    return RedirectToAction("ListApprove", "ListApprove");
                }
                else if (Submit == "Back")
                {
                    return RedirectToAction("AddEmployeeData", "EmployeeData");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.ToString();
                return View(model);
            }
            return View(model);
        }


        #region Menu
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
        #endregion


        public static List<ListApproveModels> ListEmployee(string ID, string Name, string Entity, string role)
        {
            SqlConnection conn = Common.GetConnection();
            List<EmployeeData.Models.ListApproveModels> model = new List<ListApproveModels>();
            SqlCommand cmd = new SqlCommand("sp_TMP_Get_List_Data_Employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@Entity", SqlDbType.VarChar).Value = Entity;
            cmd.Parameters.Add("@Role", SqlDbType.VarChar).Value = role;

            //cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = Dept;
            //cmd.Parameters.Add("@Entity", SqlDbType.VarChar).Value = Entity;


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            conn.Open();
            da.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new ListApproveModels
                {
                    No = dr["No"].ToString(),
                    NIK = dr["NIK"].ToString(),
                    Name = dr["Name"].ToString(),
                    Department = dr["Department"].ToString(),
                    Entity1 = dr["Entity"].ToString(),
                    Email = dr["Email"].ToString()
                });
            }

            return model;
        }


        public ActionResult DeleteUser(string id)
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;


            try
            {
                Common.ExecuteNonQuery("Delete From TRN_EMPLOYEE_DATA where [EmployeeID]='" + id + "'");
                TempData["message"] = "<script>alert('Delete succesfully');</script>";
                return RedirectToAction("ListApprove");
            }
            catch (Exception)
            {
                TempData["message"] = "<script>alert('Delete unsuccesfully');</script>";
                return RedirectToAction("ListApprove");
            }
        }
        // [HttpGet]

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

        //public ActionResult DDL()
        //{
        //    ListApproveModels model = new ListApproveModels();
        //    DataTable dt;
        //    dt = Common.ExecuteQuery("dbo.[sp_SEL_DepartmentID]");
        //    if (dt.Rows.Count > 0)
        //    {
        //        model.DepartmentID1 = DDLDept();

        //    }
        //    return Json(new SelectList(model.DepartmentID1, "Value", "Text", JsonRequestBehavior.AllowGet));
        //}


        #region DDL
        private static List<SelectListItem> DDLDept1()
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

        //private static List<SelectListItem> DDLDept1()
        //{
        //    SqlConnection con = Common.GetConnection();
        //    List<SelectListItem> item = new List<SelectListItem>();
        //    string query = "exec sp_SEL_DepartmentID";
        //    using (SqlCommand cmd = new SqlCommand(query))
        //    {
        //        cmd.Connection = con;
        //        con.Open();
        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                item.Add(new SelectListItem
        //                {
        //                    Text = dr["Department"].ToString(),
        //                    Value = dr["Department"].ToString()
        //                });
        //            }
        //        }
        //        con.Close();
        //    }
        //    return item;
        //}

        private static List<SelectListItem> DDLEntity()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_MST_ENTITY";
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
                            Text = dr["ShortEntity"].ToString(),
                            Value = dr["ShortEntity"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDLJobTitle()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_MST_JOB_TITLE";
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
                            Text = dr["Job_Title"].ToString(),
                            Value = dr["Job_Title"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLGrade()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_MST_GRADE";
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
                            //Text = Common.Decrypt(dr["GRADE"].ToString()),
                            //Value = Common.Decrypt(dr["GRADE"].ToString())
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        //private static List<SelectListItem> DDLUNIVERSITY()
        //{
        //    SqlConnection con = Common.GetConnection();
        //    List<SelectListItem> item = new List<SelectListItem>();
        //    string query = "exec sp_UNIVERSITY";
        //    using (SqlCommand cmd = new SqlCommand(query))
        //    {
        //        cmd.Connection = con;
        //        con.Open();
        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                item.Add(new SelectListItem
        //                {
        //                    Text = dr["University"].ToString(),
        //                    Value = dr["University"].ToString()
        //                });
        //            }
        //        }
        //        con.Close();
        //    }
        //    return item;
        //}

        public static List<SelectListItem> DDLMarital()
        {
            var list = new List<SelectListItem>
                {
                    new SelectListItem {Text="Single", Value="Single"},
                    new SelectListItem {Text="Married",Value="Married" },
                };
            return list;
        }

        public static List<SelectListItem> DDLEdu()
        {
            var list = new List<SelectListItem>
                {
                    new SelectListItem {Text="- Please Select -", Value=""},
                    new SelectListItem {Text="SMP", Value="SMP"},
                    new SelectListItem {Text="SMA",Value="SMA" },
                    new SelectListItem {Text="SMK", Value="SMK"},
                    new SelectListItem {Text="D1",Value="D1" },
                    new SelectListItem {Text="D2", Value="D2"},
                    new SelectListItem {Text="D3",Value="D3" },
                    new SelectListItem {Text="S1", Value="S1"},
                    new SelectListItem {Text="S2",Value="S2" },
                    new SelectListItem {Text="S3", Value="S3"},
                };
            return list;
        }

        public static List<SelectListItem> DDLSex()
        {
            var list = new List<SelectListItem>
                {
                    new SelectListItem {Text=" - Please select - ", Value=""},
                    new SelectListItem {Text="Male", Value="Male"},
                    new SelectListItem {Text="Female",Value="Female" },
                };
            return list;
        }

        private static List<SelectListItem> DDLPB()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_PLACEBIRTH";
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
                            Text = dr["Place_Birth"].ToString(),
                            Value = dr["Place_Birth"].ToString().ToUpper()
                        });
                    }
                }
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["Place_Birth"].ToString(),
                //            Value = dr["Place_Birth"].ToString()
                //        });
                //    }
                //}
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLNATIONALITY()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_NATIONALITY";
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
                            Text = dr["Nationality"].ToString(),
                            Value = dr["Nationality"].ToString().ToUpper()
                        });
                    }
                }
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["Nationality"].ToString(),
                //            Value = dr["Nationality"].ToString()
                //        });
                //    }
                //}
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLRELIGION()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_RELIGION";
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
                            Text = dr["RELIGION"].ToString(),
                            Value = dr["RELIGION"].ToString().ToUpper()
                        });
                    }
                }
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["RELIGION"].ToString(),
                //            Value = dr["RELIGION"].ToString()
                //        });
                //    }
                //}
                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDLUNIVERSITY()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_UNIVERSITY";
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
                            Text = dr["University"].ToString(),
                            Value = dr["University"].ToString().ToUpper()
                        });
                    }
                }
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["University"].ToString(),
                //            Value = dr["University"].ToString()
                //        });
                //    }
                //}
                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDLSTATE()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_PROVINSI";
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
                            Text = dr["PROVINSI"].ToString(),
                            Value = dr["PROVINSI"].ToString().ToUpper()
                        });
                    }
                }
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["PROVINSI"].ToString(),
                //            Value = dr["PROVINSI"].ToString()
                //        });
                //    }
                //}
                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDLSTATE_FASKES()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec sp_GET_PROVINSI_FASKES";
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
                            Text = dr["PROVINSI"].ToString(),
                            Value = dr["PROVINSI"].ToString().ToUpper()
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
            string query = "exec sp_GET_PROVINSI_GIGI";
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
                            Text = dr["PROVINSI"].ToString(),
                            Value = dr["PROVINSI"].ToString().ToUpper()
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
        private static List<SelectListItem> DDL_FASKES_TK1_BYKEC(string PROVINSI, string KOTAMADYA, string KECAMATAN)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec [sp_GET_FASKES_APPROVAL] '"+ PROVINSI + "', '"+ KOTAMADYA +"', '"+ KECAMATAN +"'";
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

        private static List<SelectListItem> DDL_FASKES_GIGI_APPROVAL(string PROV, string KOTA, string KEC)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec [sp_GET_FASKES_GIGI_APPROVAL] '"+ PROV +"', '"+ KOTA +"', '"+ KEC +"'";
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


        public JsonResult getKotaMadyaList(string id)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kotamadya = DDLKotamadyaByProv(id);
            return Json(new SelectList(model.Kotamadya, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public JsonResult getKecamatanKode(string id)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            
                DataTable dt = Common.ExecuteQuery("EXEC SP_KECAMATAN_LIST '', '"+ id +"', 'GENERATE'");
                if (dt.Rows.Count > 0)
                {
                    var record = dt;
                    var result = new
                    {
                        txtkodekecamatan = dt.Rows[0]["Kode_Kec"].ToString(),
                    };


                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                //}
                return Json("", JsonRequestBehavior.AllowGet);
            
        }


        public JsonResult getDesaKode(string id, string id2)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            
                DataTable dt = Common.ExecuteQuery("EXEC SP_DESA_LIST '"+ id +"', '" + id2 + "', 'GENERATE'");
                if (dt.Rows.Count > 0)
                {
                    var record = dt;
                    var result = new
                    {
                        txtkodedesa = dt.Rows[0]["Kode_Desa"].ToString(),
                    };


                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                //}
                return Json("", JsonRequestBehavior.AllowGet);
            
        }
        public static List<SelectListItem> DDLKotamadyaByProv(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KOTAMADYA FROM [Mst_Geografis] WHERE PROVINSI = '" + id + "' ORDER BY KOTAMADYA";
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

        public static List<SelectListItem> DDLKecamatanList(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC SP_KECAMATAN_LIST '', '', 'LIST'";
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
                            Text = dr["Nama_Kec"].ToString(),
                            Value = dr["Nama_Kec"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        public static List<SelectListItem> DDLKecamatanApproval(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC SP_KECAMATAN_LIST '"+ id +"', '', 'APPROVAL'";
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
                            Text = dr["Nama_Kec"].ToString(),
                            Value = dr["Nama_Kec"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        public JsonResult getDesaListbyKec(string id)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.Desaddl = DDLDesaList(id);
            return Json(new SelectList(model.Desaddl, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public static List<SelectListItem> DDLDesaList(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SP_DESA_LIST '"+ id +"', '', 'LIST'";
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
                            Text = dr["Nama_Desa"].ToString(),
                            Value = dr["Nama_Desa"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        public static List<SelectListItem> DDLDesaApproval(string id, string id2)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SP_DESA_LIST '" + id + "', '"+ id2 + "', 'APPROVAL'";
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
                            Text = dr["Nama_Desa"].ToString(),
                            Value = dr["Nama_Desa"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        public JsonResult getKotaMadyaList_FASKES(string id)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kotamadya_FASKES = DDLKotamadyaByProv_FASKES(id);
            return Json(new SelectList(model.Kotamadya_FASKES, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKotamadyaByProv_FASKES(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KOTAMADYA FROM [MST_Faskes_TK1] WHERE PROVINSI = '" + id + "' ORDER BY KOTAMADYA";
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

        public JsonResult getKotaMadyaList_FASKES_Spouse(string id)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kotamadya_FASKES_Spouse = DDLKotamadyaByProv_FASKES_Spouse(id);
            return Json(new SelectList(model.Kotamadya_FASKES_Spouse, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKotamadyaByProv_FASKES_Spouse(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KOTAMADYA FROM [MST_Faskes_TK1] WHERE PROVINSI = '" + id + "' ORDER BY KOTAMADYA";
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
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kotamadya_GIGI = DDLKotamadyaByProv_GIGI(id);
            return Json(new SelectList(model.Kotamadya_GIGI, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKotamadyaByProv_GIGI(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KOTAMADYA FROM [MST_Faskes_Gigi] WHERE PROVINSI = '" + id + "' ORDER BY KOTAMADYA";
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
        public JsonResult getKotaMadyaList_GIGI_Spouse(string id)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kotamadya_GIGI_Spouse = DDLKotamadyaByProv_GIGI_Spouse(id);
            return Json(new SelectList(model.Kotamadya_GIGI_Spouse, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKotamadyaByProv_GIGI_Spouse(string id)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KOTAMADYA FROM [MST_Faskes_Gigi] WHERE PROVINSI = '" + id + "' ORDER BY KOTAMADYA";
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


        public JsonResult getKecamatanList(string id, string idx)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kecamatan = DDLKecamatanByKotamadya(id, idx);
            return Json(new SelectList(model.Kecamatan, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKecamatanByKotamadya(string id, string idx)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KECAMATAN FROM [Mst_Geografis] WHERE KOTAMADYA = '" + id + "' and PROVINSI = '" + idx + "' ORDER BY KECAMATAN";
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
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kecamatan_FASKES = DDLKecamatanByKotamadya_FASKES(id, idx);
            return Json(new SelectList(model.Kecamatan_FASKES, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKecamatanByKotamadya_FASKES(string id, string idx)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KECAMATAN FROM [MST_Faskes_TK1] WHERE KOTAMADYA = '" + id + "' and PROVINSI = '" + idx + "' ORDER BY KECAMATAN";
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

        public JsonResult getKecamatanList_FASKES_Spouse(string id, string idx)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kecamatan_FASKES_Spouse = DDLKecamatanByKotamadya_FASKES_Spouse(id, idx);
            return Json(new SelectList(model.Kecamatan_FASKES_Spouse, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKecamatanByKotamadya_FASKES_Spouse(string id, string idx)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KECAMATAN FROM [MST_Faskes_TK1] WHERE KOTAMADYA = '" + id + "' and PROVINSI = '" + idx + "' ORDER BY KECAMATAN";
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
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kecamatan_GIGI = DDLKecamatanByKotamadya_GIGI(id, idx);
            return Json(new SelectList(model.Kecamatan_GIGI, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKecamatanByKotamadya_GIGI(string id, string idx)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KECAMATAN FROM [MST_Faskes_Gigi] WHERE KOTAMADYA = '" + id + "' and PROVINSI = '" + idx + "' ORDER BY KECAMATAN";
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

        public JsonResult getKecamatanList_GIGI_Spouse(string id, string idx)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kecamatan_GIGI_Spouse = DDLKecamatanByKotamadya_GIGI_Spouse(id, idx);
            return Json(new SelectList(model.Kecamatan_GIGI_Spouse, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKecamatanByKotamadya_GIGI_Spouse(string id, string idx)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KECAMATAN FROM [MST_Faskes_Gigi] WHERE KOTAMADYA = '" + id + "' and PROVINSI = '" + idx + "' ORDER BY KECAMATAN";
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

        public JsonResult getKelurahanList(string id, string idx, string idx2)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.Kelurahan = DDLKelurahanByKecamatan(id, idx, idx2);
            return Json(new SelectList(model.Kelurahan, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLKelurahanByKecamatan(string id, string idx, string idx2)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT KELURAHAN  FROM [Mst_Geografis] WHERE PROVINSI = '" + idx + "' AND KOTAMADYA = '" + idx2 + "' AND KECAMATAN = '" + id + "' ORDER BY KELURAHAN";
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

        public JsonResult getFASKES_LIST(string id, string idx, string idx2)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama = DDLFASKESBYKECAMATAN(id, idx, idx2);
            return Json(new SelectList(model.BPJS_Kesehatan_Faskes_Tingkat_Pertama, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLFASKESBYKECAMATAN(string id, string idx, string idx2)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT NAMA_FASKES_TINGKAT_PERTAMA  FROM [MST_Faskes_TK1] WHERE PROVINSI = '" + idx + "' AND KOTAMADYA = '" + idx2 + "' AND KECAMATAN = '" + id + "' ORDER BY NAMA_FASKES_TINGKAT_PERTAMA";
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

        public JsonResult getFASKES_LIST_Spouse(string id, string idx, string idx2)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse = DDLFASKESBYKECAMATAN_Spouse(id, idx, idx2);
            return Json(new SelectList(model.BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLFASKESBYKECAMATAN_Spouse(string id, string idx, string idx2)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT NAMA_FASKES_TINGKAT_PERTAMA  FROM [MST_Faskes_TK1] WHERE PROVINSI = '" + idx + "' AND KOTAMADYA = '" + idx2 + "' AND KECAMATAN = '" + id + "' ORDER BY NAMA_FASKES_TINGKAT_PERTAMA";
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
        public JsonResult getFASKES_GIGI_LIST(string id, string idx, string idx2)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi = DDLGIGIBYKECAMATAN(id, idx, idx2);
            return Json(new SelectList(model.BPJS_Kesehatan_Faskes_Dokter_Gigi, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLGIGIBYKECAMATAN(string id, string idx, string idx2)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT NAMA_FASKES_TINGKAT_PERTAMA  FROM [MST_Faskes_Gigi] WHERE PROVINSI = '" + idx + "' AND KOTAMADYA = '" + idx2 + "' AND KECAMATAN = '" + id + "' ORDER BY NAMA_FASKES_TINGKAT_PERTAMA";
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

        public JsonResult getFASKES_GIGI_LIST_Spouse(string id, string idx, string idx2)
        {
            EmployeeDataModels model = new EmployeeDataModels();
            model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse = DDLGIGIBYKECAMATAN_Spouse(id, idx, idx2);
            return Json(new SelectList(model.BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public static List<SelectListItem> DDLGIGIBYKECAMATAN_Spouse(string id, string idx, string idx2)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "SELECT DISTINCT NAMA_FASKES_TINGKAT_PERTAMA  FROM [MST_Faskes_Gigi] WHERE PROVINSI = '" + idx + "' AND KOTAMADYA = '" + idx2 + "' AND KECAMATAN = '" + id + "' ORDER BY NAMA_FASKES_TINGKAT_PERTAMA";
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
                            Value = dr["KOTAMADYA"].ToString().ToUpper()
                        });
                    }
                }
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KOTAMADYA"].ToString(),
                //            Value = dr["KOTAMADYA"].ToString()
                //        });
                //    }
                //}
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
                            Value = dr["KOTAMADYA"].ToString().ToUpper()
                        });
                    }
                }
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KOTAMADYA"].ToString(),
                //            Value = dr["KOTAMADYA"].ToString()
                //        });
                //    }
                //}
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDLKOTAMADYA_FASKES_BYPROV(string ID        )
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec [sp_KOTAMADYA_FASKES_APPROVAL] '"+ ID +"'";
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KOTAMADYA"].ToString(),
                //            Value = dr["KOTAMADYA"].ToString()
                //        });
                //    }
                //}
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

        private static List<SelectListItem> DDLKOTAMADYA_GIGI_BYPROV(string PROV)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec [sp_KOTAMADYA_GIGI_APPROVAL] '"+ PROV +"'";
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KOTAMADYA"].ToString(),
                //            Value = dr["KOTAMADYA"].ToString()
                //        });
                //    }
                //}
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KOTAMADYA"].ToString(),
                //            Value = dr["KOTAMADYA"].ToString()
                //        });
                //    }
                //}
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KOTAMADYA"].ToString(),
                //            Value = dr["KOTAMADYA"].ToString()
                //        });
                //    }
                //}
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KECAMATAN"].ToString(),
                //            Value = dr["KECAMATAN"].ToString()
                //        });
                //    }
                //}
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KECAMATAN"].ToString(),
                //            Value = dr["KECAMATAN"].ToString()
                //        });
                //    }
                //}
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KECAMATAN"].ToString(),
                //            Value = dr["KECAMATAN"].ToString()
                //        });
                //    }
                //}
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KECAMATAN"].ToString(),
                //            Value = dr["KECAMATAN"].ToString()
                //        });
                //    }
                //}
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
        private static List<SelectListItem> DDLKECAMATAN_FASKES_BYKOTA(string ID, string ID2)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec [sp_KECAMATAN_FASKES_APPROVAL] '"+ ID +"', '"+ ID2 +"'";
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

        private static List<SelectListItem> DDLKECAMATAN_GIGI_BYKOT(string PROV, string KOTA)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec [sp_KECAMATAN_GIGI_APPROVAL] '"+ PROV +"', '"+ KOTA +"'";
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KELURAHAN"].ToString(),
                //            Value = dr["KELURAHAN"].ToString()
                //        });
                //    }
                //}
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString(),
                //            Value = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString()
                //        });
                //    }
                //}
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString(),
                //            Value = dr["NAMA_FASKES_TINGKAT_PERTAMA"].ToString()
                //        });
                //    }
                //}
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
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        item.Add(new SelectListItem
                //        {
                //            Text = dr["KELURAHAN"].ToString(),
                //            Value = dr["KELURAHAN"].ToString()
                //        });
                //    }
                //}
                con.Close();
            }
            return item;
        }
        public ActionResult DeleteChild(string idx, string url)
        {
            string urlx = Request.Url.OriginalString;
            Session["url"] = urlx;


            try
            {
                Common.ExecuteNonQuery("Delete From TRN_Child_Employee where [IDX]='" + idx + "'");
                //TempData["message"] = "<script>alert('Delete succesfully');</script>";
                return Redirect(url);
            }
            catch (Exception)
            {
                TempData["message"] = "<script>alert('Delete unsuccesfully');</script>";
                return Redirect(url);
            }
        }

        //private static List<ListApproveModels> GetChildDatas(string EmployeeID)
        //{
        //    List<ListApproveModels> model = new List<ListApproveModels>();
        //    SqlConnection connX = Common.GetConnection();
        //    SqlCommand cmdX = new SqlCommand("sp_GET_CHILD_DATA", connX);
        //    cmdX.CommandType = CommandType.StoredProcedure;
        //    cmdX.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeID;

        //    SqlDataAdapter da = new SqlDataAdapter(cmdX);
        //    DataTable dtX = new DataTable();
        //    connX.Open();
        //    da.Fill(dtX);
        //    connX.Close();

        //    foreach (DataRow dr in dtX.Rows)
        //    {
        //        model.Add(
        //            new ListApproveModels
        //            {
        //                EmployeeName = dr["Full_Name"].ToString(),
        //                Birthdate = dr["BirthDate"].ToString(),
        //                NIK = dr["NIK"].ToString(),
        //                Gender_ID = dr["Gender"].ToString(),
        //                idx = dr["idx"].ToString(),
        //                //No = dr["No"].ToString()
        //            }
        //            );
        //    }
        //    return (model);
        //}

        private static List<SelectListItem> DDL_JENISMUTASI()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISMUTASI] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Mutasi"].ToString(),
                            Value = dr["Jenis_Mutasi"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_JENISMUTASI_SPOUSE()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISMUTASI] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Mutasi"].ToString(),
                            Value = dr["Jenis_Mutasi"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_JENISMUTASICHILD1()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISMUTASI] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Mutasi"].ToString(),
                            Value = dr["Jenis_Mutasi"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDL_JENISMUTASICHILD2()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISMUTASI] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Mutasi"].ToString(),
                            Value = dr["Jenis_Mutasi"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_JENISMUTASICHILD3()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISMUTASI] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Mutasi"].ToString(),
                            Value = dr["Jenis_Mutasi"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        //private static List<SelectListItem> DDL_KODEHUBKEL()
        //{
        //    SqlConnection con = Common.GetConnection();
        //    List<SelectListItem> item = new List<SelectListItem>();
        //    string query = "EXEC [dbo].[SP_KODEHUBKEL] ";

        //    using (SqlCommand cmd = new SqlCommand(query))
        //    {
        //        cmd.Connection = con;
        //        con.Open();

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            if (dr.HasRows)
        //            {
        //                item.Add(new SelectListItem
        //                {
        //                    Text = " --Choose-- ",
        //                    Value = ""
        //                });
        //            }
        //            while (dr.Read())
        //            {
        //                item.Add(new SelectListItem
        //                {
        //                    Text = dr["Formula"].ToString(),
        //                    Value = dr["Formula"].ToString()
        //                });
        //            }
        //        }


        //        con.Close();
        //    }
        //    return item;
        //}

        private static List<SelectListItem> DDL_KODEHUBKELSPOUSE()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_KODEHUBKEL_ALL] '2' ";

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
                            Text = dr["Formula"].ToString(),
                            Value = dr["Formula"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDL_KODEHUBKELCHILD1()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_KODEHUBKEL_ALL] '3' ";

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
                            Text = dr["Formula"].ToString(),
                            Value = dr["Formula"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDL_KODEHUBKELCHILD2()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_KODEHUBKEL_ALL] '4' ";

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
                            Text = dr["Formula"].ToString(),
                            Value = dr["Formula"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_KODEHUBKELCHILD3()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_KODEHUBKEL_ALL] '5' ";

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
                            Text = dr["Formula"].ToString(),
                            Value = dr["Formula"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDL_KODEHUBKEL()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_KODEHUBKEL_ALL] '1'";

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
                            Text = dr["Formula"].ToString(),
                            Value = dr["Formula"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }



        private static List<SelectListItem> DDL_FAMILYRELATIONSHIP_SPOUSE()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_KODEHUBKEL_ALL] '2'";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Formula"].ToString(),
                            Value = dr["Formula"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_FAMILYRELATIONSHIP_CHILD1()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_KODEHUBKEL_ALL] '3'";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Formula"].ToString(),
                            Value = dr["Formula"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDL_FAMILYRELATIONSHIP_CHILD2()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_KODEHUBKEL_ALL] '4'";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Formula"].ToString(),
                            Value = dr["Formula"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_FAMILYRELATIONSHIP_CHILD3()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_KODEHUBKEL_ALL] '5'";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Formula"].ToString(),
                            Value = dr["Formula"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_JENISKELAMIN()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISKELAMIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Kelamin"].ToString(),
                            Value = dr["Jenis_Kelamin"].ToString()
                        });
                    }
                }

                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_GENDER_SPOUSE()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISKELAMIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Kelamin"].ToString(),
                            Value = dr["Jenis_Kelamin"].ToString()
                        });
                    }
                }

                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_GENDER_CHILD1()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISKELAMIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Kelamin"].ToString(),
                            Value = dr["Jenis_Kelamin"].ToString()
                        });
                    }
                }

                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_GENDER_CHILD2()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISKELAMIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Kelamin"].ToString(),
                            Value = dr["Jenis_Kelamin"].ToString()
                        });
                    }
                }

                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDL_GENDER_CHILD3()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_JENISKELAMIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Jenis_Kelamin"].ToString(),
                            Value = dr["Jenis_Kelamin"].ToString()
                        });
                    }
                }

                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_STATUSKAWIN()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_STATUSKAWIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Status_Kawin"].ToString(),
                            Value = dr["Status_Kawin"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_MARITALSTATUS_SPOUSE()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_STATUSKAWIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Status_Kawin"].ToString(),
                            Value = dr["Status_Kawin"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_MARITALSTATUS_CHILD1()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_STATUSKAWIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Status_Kawin"].ToString(),
                            Value = dr["Status_Kawin"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_MARITALSTATUS_CHILD2()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_STATUSKAWIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Status_Kawin"].ToString(),
                            Value = dr["Status_Kawin"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_MARITALSTATUS_CHILD3()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_STATUSKAWIN] ";

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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Status_Kawin"].ToString(),
                            Value = dr["Status_Kawin"].ToString()
                        });
                    }
                }


                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_STATUS()
        {

            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_STATUS";
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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Status"].ToString(),
                            Value = dr["Status"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_KEWARGANEGARAAN()
        {

            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_KEWARGANEGARAAN";
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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Kewarganegaraan"].ToString(),
                            Value = dr["Kewarganegaraan"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;

        }
        private static List<SelectListItem> DDL_CITIZENSHIP_SPOUSE()
        {

            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_KEWARGANEGARAAN";
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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Kewarganegaraan"].ToString(),
                            Value = dr["Kewarganegaraan"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;

        }
        private static List<SelectListItem> DDL_CITIZENSHIP_CHILD1()
        {

            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_KEWARGANEGARAAN";
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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Kewarganegaraan"].ToString(),
                            Value = dr["Kewarganegaraan"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;

        }
        private static List<SelectListItem> DDL_CITIZENSHIP_CHILD2()
        {

            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_KEWARGANEGARAAN";
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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Kewarganegaraan"].ToString(),
                            Value = dr["Kewarganegaraan"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;

        }
        private static List<SelectListItem> DDL_CITIZENSHIP_CHILD3()
        {

            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_KEWARGANEGARAAN";
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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Kewarganegaraan"].ToString(),
                            Value = dr["Kewarganegaraan"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;

        }
        #endregion
    }

}