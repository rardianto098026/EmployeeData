using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeData.Models
{
    public class EmployeeDataModels
    {
        public string disablebutton { get; set; }
        public string Univ_Name2 { get; set; }
        //[Required(ErrorMessage = "Tes")]
        public string Remark2 { get; set; }
        public string KK_Spouse { get; set; }
        public string KK_Child_1 { get; set; }
        public string KK_Child_2 { get; set; }
        public string KK_Child_3 { get; set; }
        public string PPP { get; set; }
        public string StylEmployee { get; set; }
        public string StylSpouse { get; set; }
        public string StylChild1 { get; set; }
        public string StylChild2 { get; set; }
        public string StylChild3 { get; set; }

        public string DsblEducation { get; set; }
        public string DsblEmployee { get; set; }
        public string DsblSpouse { get; set; }
        public string DsblChild1 { get; set; }
        public string Style { get; set; }
        public string Style1 { get; set; }

        public bool? DsblChild11 { get; set; }
        public string DsblChild2 { get; set; }
        public string DsblChild3 { get; set; }


        public string Class { get; set; }
        public string Class_Spouse { get; set; }
        public string Class_Child_1 { get; set; }
        public string Class_Child_2 { get; set; }
        public string Class_Child_3 { get; set; }

        public string BPJS_JoinDate { get; set; }

        public string idx { get; set; }
        public string idx_child_1 { get; set; }
        public string idx_child_2 { get; set; }
        public string idx_child_3 { get; set; }

        public string FLAG { get; set; }
        public string FLAG_Spouse { get; set; }
        public string FLAG_Parent { get; set; }
        public string FLAG_Child_1 { get; set; }
        public string FLAG_Child_2 { get; set; }
        public string FLAG_Child_3 { get; set; }

        public string Remark { get; set; }

        public string Username { get; set; }
        public string No { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please check your NIK")]
        public string NIK { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please check your NIK")]
        public string NIK_child_1 { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please check your NIK")]
        public string NIK_child_2 { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please check your NIK")]
        public string NIK_child_3 { get; set; }

        public string KTP { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeID_child_1 { get; set; }
        public string EmployeeID_child_2 { get; set; }
        public string EmployeeID_child_3 { get; set; }

        public string EmployeeName { get; set; }
        public string EmployeeName_child_1 { get; set; }
        public string EmployeeName_child_2 { get; set; }
        public string EmployeeName_child_3 { get; set; }

        public string Spouse { get; set; }

        public IEnumerable<SelectListItem> Entity { get; set; }
        public string EntityID { get; set; }
        public IEnumerable<SelectListItem> EntitySearch { get; set; }
        public string EntityIDSearch { get; set; }
        public string Email { get; set; }
        public IEnumerable<SelectListItem> Department { get; set; }
        public string DepartmentID { get; set; }
        public IEnumerable<SelectListItem> JobTitle { get; set; }
        public string JobTitleID { get; set; }
        public IEnumerable<SelectListItem> Grade { get; set; }
        public string GradeID { get; set; }
        public string HiredDate { get; set; }
        public string ReportingTo { get; set; }
        public string OfficeE_Mail { get; set; }
        public string National_IDNo { get; set; }
        public string NPWP { get; set; }
        public string KK { get; set; }
        public string MarriedDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Education { get; set; }
        public string Univ_Name { get; set; }
        public IEnumerable<SelectListItem> MaritalStatus_ID { get; set; }
        public IEnumerable<SelectListItem> Education_ID { get; set; }
        public IEnumerable<SelectListItem> Univ_Name_ID { get; set; }
        public IEnumerable<SelectListItem> Gender { get; set; }
        public string Gender_ID { get; set; }
        public string Gender_ID_child_1 { get; set; }
        public string Gender_ID_child_2 { get; set; }
        public string Gender_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> PlaceBirth { get; set; }
        public string PlaceBirth_ID { get; set; }
        public string Birthdate { get; set; }
        //[Required(ErrorMessage = "Tes")]
        public string Birthdate_child_1 { get; set; }
        //[Required(ErrorMessage = "Tes")]
        public string Birthdate_child_2 { get; set; }
        //[Required(ErrorMessage = "Tes")]
        public string Birthdate_child_3 { get; set; }
        public IEnumerable<SelectListItem> Nationality { get; set; }
        public string Nationality_ID { get; set; }
        public IEnumerable<SelectListItem> Religion { get; set; }
        public string Religion_ID { get; set; }
        public string Home_Address { get; set; }
        public IEnumerable<SelectListItem> City { get; set; }
        public string City_ID { get; set; }
        public string Kodepos { get; set; }
        public string RT { get; set; }
        public string RW { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        public string State_ID { get; set; }

        [Required(ErrorMessage = "Document Code is required.")]
        public string DocCode { get; set; }

        public IEnumerable<SelectListItem> Kotamadya { get; set; }
        public string Kotamadya_ID { get; set; }
        public IEnumerable<SelectListItem> Kecamatan { get; set; }
        public string Kecamatan_ID { get; set; }
        public IEnumerable<SelectListItem> Kelurahan { get; set; }
        public string Kelurahan_ID { get; set; }
        public string Physical_Address { get; set; }
        public string Personal_Email { get; set; }
        public string Home_Phone { get; set; }
        public string Handphone { get; set; }
        public string Handphone_child_1 { get; set; }
        public string Handphone_child_2 { get; set; }
        public string Handphone_child_3 { get; set; }
        public string Payroll_Bank { get; set; }
        public string Payroll_Branch { get; set; }
        public string Payroll_Account_No { get; set; }
        public string Payroll_Accoun_Name { get; set; }
        public string DPLK_No_Peserta { get; set; }
        public string DPLK_Joint_Date { get; set; }
        public string DPLK_Saldo { get; set; }
        public string Health_Plan_Benefit_Rawat_Jalan { get; set; }
        public string Health_Plan_Membership_No_Polis { get; set; }
        public string Health_Plan_Membership_No_Peserta { get; set; }
        public string Health_Plan_Membership_No_Kartu { get; set; }
        public string Health_Plan_Benefit_Rawat_Inap { get; set; }
        public string Health_Plan_Benefit_Persalinan { get; set; }
        public string Health_Plan_Benefit_Gigi { get; set; }
        public string Health_Plan_Benefit_Kacamata { get; set; }
        public string Health_Plan_Membership_No_Polis_child_1 { get; set; }
        public string Health_Plan_Membership_No_Peserta_child_1 { get; set; }
        public string Health_Plan_Membership_No_Kartu_child_1 { get; set; }
        public string Health_Plan_Benefit_Rawat_Inap_child_1 { get; set; }
        public string Health_Plan_Benefit_Rawat_Jalan_child_1 { get; set; }
        public string Health_Plan_Benefit_Persalinan_child_1 { get; set; }
        public string Health_Plan_Benefit_Gigi_child_1 { get; set; }
        public string Health_Plan_Benefit_Kacamata_child_1 { get; set; }
        public string Health_Plan_Membership_No_Polis_child_2 { get; set; }
        public string Health_Plan_Membership_No_Peserta_child_2 { get; set; }
        public string Health_Plan_Membership_No_Kartu_child_2 { get; set; }
        public string Health_Plan_Benefit_Rawat_Inap_child_2 { get; set; }
        public string Health_Plan_Benefit_Rawat_Jalan_child_2 { get; set; }
        public string Health_Plan_Benefit_Persalinan_child_2 { get; set; }
        public string Health_Plan_Benefit_Gigi_child_2 { get; set; }
        public string Health_Plan_Benefit_Kacamata_child_2 { get; set; }
        public string Health_Plan_Membership_No_Polis_child_3 { get; set; }
        public string Health_Plan_Membership_No_Peserta_child_3 { get; set; }
        public string Health_Plan_Membership_No_Kartu_child_3 { get; set; }
        public string Health_Plan_Benefit_Rawat_Inap_child_3 { get; set; }
        public string Health_Plan_Benefit_Rawat_Jalan_child_3 { get; set; }
        public string Health_Plan_Benefit_Persalinan_child_3 { get; set; }
        public string Health_Plan_Benefit_Gigi_child_3 { get; set; }
        public string Health_Plan_Benefit_Kacamata_child_3 { get; set; }
        public string BPJS_Ketenagakerjaan_ID { get; set; }
        public string BPJS_Pensiun_ID { get; set; }
        public string BPJS_Kesehatan_Active { get; set; }
        public string BPJS_Kesehatan_Active_Spouse { get; set; }
        public string BPJS_Kesehatan_Active_Child_1 { get; set; }
        public string BPJS_Kesehatan_Active_Child_2 { get; set; }
        public string BPJS_Kesehatan_Active_Child_3 { get; set; }

        public string BPJS_Kesehatan_ID { get; set; }
        public string BPJS_Kesehatan_ID_child_1 { get; set; }
        public string BPJS_Kesehatan_ID_child_2 { get; set; }
        public string BPJS_Kesehatan_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Tingkat_Pertama { get; set; }
        public string BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID { get; set; }

        //public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child { get; set; }
        ////public string BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID { get; set; }
        //public string BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 { get; set; }
        //public string BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 { get; set; }
        //public string BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Dokter_Gigi { get; set; }
        public string BPJS_Kesehatan_Faskes_Dokter_Gigi_ID { get; set; }

        //public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_Child { get; set; }
        ////public string BPJS_Kesehatan_Faskes_Dokter_Gigi_ID { get; set; }
        //public string BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 { get; set; }
        //public string BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 { get; set; }
        //public string BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 { get; set; }

        public string Sequence { get; set; }
        public string Sequence_child_1 { get; set; }
        public string Sequence_child_2 { get; set; }
        public string Sequence_child_3 { get; set; }

        public string LINK { get; set; }
        public string messageRequest { get; set; }
        public string ErrorRequest { get; set; }
        public string CountData { get; set; }
        public bool? Disabled { get; set; }
        public bool? Disabledbpjs { get; set; }
        public bool? Disabledbpjskerja { get; set; }
        public bool? Disabledpayroll { get; set; }
        public bool? Disablededu { get; set; }

        public string ImageURL { get; set; }

        public string Attach_File_Married { get; set; }
        public string Attach_File_Education { get; set; }
        public string Attach_File_Payroll { get; set; }
        public string FileName_Married { get; set; }
        public string FileName_Education { get; set; }
        public string FileName_Payroll { get; set; }


        public IEnumerable<SelectListItem> Provinsi_FASKES { get; set; }
        public string Provinsi_FASKES_ID { get; set; }
        public IEnumerable<SelectListItem> Kotamadya_FASKES { get; set; }
        public string Kotamadya_FASKES_ID { get; set; }
        public IEnumerable<SelectListItem> Kecamatan_FASKES { get; set; }
        public string Kecamatan_FASKES_ID { get; set; }

        public IEnumerable<SelectListItem> Kelurahan_FASKES { get; set; }
        public string Kelurahan_FASKES_ID { get; set; }

        public IEnumerable<SelectListItem> Provinsi_GIGI { get; set; }
        public string Provinsi_GIGI_ID { get; set; }
        public IEnumerable<SelectListItem> Kotamadya_GIGI { get; set; }
        public string Kotamadya_GIGI_ID { get; set; }
        public IEnumerable<SelectListItem> Kecamatan_GIGI { get; set; }
        public string Kecamatan_GIGI_ID { get; set; }
        public IEnumerable<SelectListItem> Kelurahan_GIGI { get; set; }
        public string Kelurahan_GIGI_ID { get; set; }

        //public List<EmployeeDataModels> GetChildData { get; set; }
        //public bool? CheckSpouse { get; set; }
        //public bool? ChkChild1 { get; set; }
        //public bool? ChkChild2 { get; set; }
        //public bool? ChkChild3 { get; set; }


        public IEnumerable<SelectListItem> Provinsi_FASKES_Child_1 { get; set; }
        public string Provinsi_FASKES_ID_child_1 { get; set; }

        public IEnumerable<SelectListItem> Provinsi_FASKES_Child_2 { get; set; }
        public string Provinsi_FASKES_ID_child_2 { get; set; }

        public IEnumerable<SelectListItem> Provinsi_FASKES_Child_3 { get; set; }
        public string Provinsi_FASKES_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> Kotamadya_FASKES_Child_1 { get; set; }
        public string Kotamadya_FASKES_ID_child_1 { get; set; }
        public IEnumerable<SelectListItem> Kotamadya_FASKES_Child_2 { get; set; }
        public string Kotamadya_FASKES_ID_child_2 { get; set; }
        public IEnumerable<SelectListItem> Kotamadya_FASKES_Child_3 { get; set; }
        public string Kotamadya_FASKES_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> Kecamatan_FASKES_Child_1 { get; set; }
        public string Kecamatan_FASKES_ID_child_1 { get; set; }
        public IEnumerable<SelectListItem> Kecamatan_FASKES_Child_2 { get; set; }
        public string Kecamatan_FASKES_ID_child_2 { get; set; }
        public IEnumerable<SelectListItem> Kecamatan_FASKES_Child_3 { get; set; }
        public string Kecamatan_FASKES_ID_child_3 { get; set; }


        public IEnumerable<SelectListItem> Provinsi_GIGI_Child_1 { get; set; }
        public string Provinsi_GIGI_ID_child_1 { get; set; }

        public IEnumerable<SelectListItem> Provinsi_GIGI_Child_2 { get; set; }
        public string Provinsi_GIGI_ID_child_2 { get; set; }

        public IEnumerable<SelectListItem> Provinsi_GIGI_Child_3 { get; set; }
        public string Provinsi_GIGI_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> Kotamadya_GIGI_Child_1 { get; set; }
        public string Kotamadya_GIGI_ID_child_1 { get; set; }
        public IEnumerable<SelectListItem> Kotamadya_GIGI_Child_2 { get; set; }
        public string Kotamadya_GIGI_ID_child_2 { get; set; }
        public IEnumerable<SelectListItem> Kotamadya_GIGI_Child_3 { get; set; }
        public string Kotamadya_GIGI_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> Kecamatan_GIGI_Child_1 { get; set; }
        public string Kecamatan_GIGI_ID_child_1 { get; set; }
        public IEnumerable<SelectListItem> Kecamatan_GIGI_Child_2 { get; set; }
        public string Kecamatan_GIGI_ID_child_2 { get; set; }
        public IEnumerable<SelectListItem> Kecamatan_GIGI_Child_3 { get; set; }
        public string Kecamatan_GIGI_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_1 { get; set; }
        public string BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_1 { get; set; }

        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_2 { get; set; }
        public string BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_2 { get; set; }
        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Tingkat_Pertama_Child_3 { get; set; }
        public string BPJS_Kesehatan_Faskes_Tingkat_Pertama_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_1 { get; set; }
        public string BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_1 { get; set; }

        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_2 { get; set; }
        public string BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_2 { get; set; }

        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Dokter_Gigi_Child_3 { get; set; }
        public string BPJS_Kesehatan_Faskes_Dokter_Gigi_ID_child_3 { get; set; }

        public string Full_Name_Spouse { get; set; }

        //[Required(ErrorMessage = "Tes")]
        public string Birthdate_Spouse { get; set; }
        public IEnumerable<SelectListItem> Gender_Spouse { get; set; }
        public string Gender_Spouse_ID { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please check your NIK")]
        public string NIK_Spouse { get; set; }
        public string Handphone_Spouse { get; set; }
        public string Health_Plan_Membership_No_Polis_Spouse { get; set; }
        public string Health_Plan_Membership_No_Peserta_Spouse { get; set; }
        public string Health_Plan_Membership_No_Kartu_Spouse { get; set; }
        public string Health_Plan_Benefit_Rawat_Inap_Spouse { get; set; }
        public string Health_Plan_Benefit_Rawat_Jalan_Spouse { get; set; }
        public string Health_Plan_Benefit_Persalinan_Spouse { get; set; }
        public string Health_Plan_Benefit_Gigi_Spouse { get; set; }
        public string Health_Plan_Benefit_Kacamata_Spouse { get; set; }
        public string BPJS_Kesehatan_Spouse_ID { get; set; }


        public IEnumerable<SelectListItem> Provinsi_FASKES_Spouse { get; set; }
        public string Provinsi_FASKES_Spouse_ID { get; set; }
        public IEnumerable<SelectListItem> Kotamadya_FASKES_Spouse { get; set; }
        public string Kotamadya_FASKES_Spouse_ID { get; set; }
        public IEnumerable<SelectListItem> Kecamatan_FASKES_Spouse { get; set; }
        public string Kecamatan_FASKES_Spouse_ID { get; set; }
        public IEnumerable<SelectListItem> Kelurahan_FASKES_Spouse { get; set; }
        public string Kelurahan_FASKES_Spouse_ID { get; set; }

        public IEnumerable<SelectListItem> Provinsi_GIGI_Spouse { get; set; }
        public string Provinsi_GIGI_Spouse_ID { get; set; }
        public IEnumerable<SelectListItem> Kotamadya_GIGI_Spouse { get; set; }
        public string Kotamadya_GIGI_Spouse_ID { get; set; }
        public IEnumerable<SelectListItem> Kecamatan_GIGI_Spouse { get; set; }
        public string Kecamatan_GIGI_Spouse_ID { get; set; }
        public IEnumerable<SelectListItem> Kelurahan_GIGI_Spouse { get; set; }
        public string Kelurahan_GIGI_Spouse_ID { get; set; }
        public string idx_spouse { get; set; }

        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse { get; set; }
        public string BPJS_Kesehatan_Faskes_Tingkat_Pertama_Spouse_ID { get; set; }
        public IEnumerable<SelectListItem> BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse { get; set; }
        public string BPJS_Kesehatan_Faskes_Dokter_Gigi_Spouse_ID { get; set; }
        public string LinkEditChild { get; set; }
        public string Full_Name_Child { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(16)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please check your NIK")]
        public string NIK_CHILD { get; set; }


        public string idx_parent1 { get; set; }
        public string Full_Name_Parent1 { get; set; }
        public string Birthdate_Parent1 { get; set; }
        public IEnumerable<SelectListItem> Gender_Parent1 { get; set; }
        public string Gender_ID_Parent1 { get; set; }
        public string Handphone_Parent1 { get; set; }

        public string idx_parent2 { get; set; }
        public string Full_Name_Parent2 { get; set; }
        public string Birthdate_Parent2 { get; set; }
        public IEnumerable<SelectListItem> Gender_Parent2 { get; set; }
        public string Gender_ID_Parent2 { get; set; }
        public string Handphone_Parent2 { get; set; }
        public string FlagEducation_Completed { get; set; }
        public string CheckUploadEdu { get; set; }
        public string ClassLIEdu { get; set; }
        public string ClassLIBPJS { get; set; }
        public string AriaExpanded { get; set; }
        public string tabPaneEdu { get; set; }
        public string AriaExpandedBPJS { get; set; }
        public string AriaExpandedBPJS_Empl { get; set; }
        public string AriaExpandedBPJS_Sps { get; set; }
        public string AriaExpandedBPJS_Chld1 { get; set; }
        public string AriaExpandedBPJS_Chld2 { get; set; }
        public string AriaExpandedBPJS_Chld3 { get; set; }
        public string ClassbpjsEmpl { get; set; }
        public string ClassbpjsSps { get; set; }
        public string ClassbpjsChld1 { get; set; }
        public string ClassbpjsChld2 { get; set; }
        public string ClassbpjsChld3 { get; set; }
        public string ClasschldbpjsEmpl { get; set; }
        public string ClasschldbpjsSps { get; set; }
        public string ClasschldbpjsChld1 { get; set; }
        public string ClasschldbpjsChld2 { get; set; }
        public string ClasschldbpjsChld3 { get; set; }
        public string tabPaneBpjschild { get; set; }
        public string JavascriptToRunEmpl { get; set; }
        public string JavascriptToRunSps { get; set; }
        public string JavascriptToRunChld1 { get; set; }
        public string JavascriptToRunChld2 { get; set; }
        public string JavascriptToRunChld3 { get; set; }

        public string RemarkApproval { get; set; }

        // Add On
        public string JenisMutasi { get; set; }
        public IEnumerable<SelectListItem> JenisMutasiDDL { get; set; }

        public string TglAktifBerlakuMutasi { get; set; }

        public string KodeHubKel { get; set; }
        public IEnumerable<SelectListItem> KodeHubKelDDL { get; set; }

        public string TempatLahir { get; set; }

        public string JenisKelamin { get; set; }
        public IEnumerable<SelectListItem> JenisKelaminDDL { get; set; }

        public string StatusKawin { get; set; }
        public IEnumerable<SelectListItem> StatusKawinDDL { get; set; }

        public string AlamatTempatTinggal { get; set; }

        public string RT2 { get; set; }

        public string RW2 { get; set; }

        public string KodePosAddn { get; set; }

        public string kodekecamatan { get; set; }

        public string kodedesa { get; set; }

        public string KodeFaskesTkI { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesTkIDDL { get; set; }

        public string KodeFaskesDrg { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesDrgDDL { get; set; }

        public string NOMORPESERTA { get; set; }

        [Required(ErrorMessage = "Please Fill The Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string EMAILS { get; set; }

        public string IDPERSON { get; set; }

        public string STATUS { get; set; }
        public IEnumerable<SelectListItem> STATUSDDL { get; set; }

        //[Display(Name = "TMT KERJA")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime TMTKERJA { get; set; }
        //public DateTime? TMTKERJA { get; set; }

        public string TMTKERJA { get; set; }

        public string KEWARGANEGARAAN { get; set; }
        public IEnumerable<SelectListItem> KEWARGANEGARAANDDL { get; set; }

        public string NOMORKARTU { get; set; }

        public string NAMAASURANSI { get; set; }

        public string NOMORPASSPORT { get; set; }
        public string FaskesTK1Code { get; set; }
        public string FaskesdrgCode { get; set; }
        public string JobTittle { get; set; }

        public string FixedSalary { get; set; }
        
        public string NPWP2 { get; set; }


        // End Add On

        // Add On spouse
        public string JenisMutasi_Spouse { get; set; }
        public IEnumerable<SelectListItem> JenisMutasiDDL_Spouse { get; set; }

        public string TglAktifBerlakuMutasi_Spouse { get; set; }

        public string KodeHubKel_Spouse { get; set; }
        public IEnumerable<SelectListItem> KodeHubKelDDL_Spouse { get; set; }

        public string TempatLahir_Spouse { get; set; }

        public string JenisKelamin_Spouse { get; set; }
        public IEnumerable<SelectListItem> JenisKelaminDDL_Spouse { get; set; }

        public string StatusKawin_Spouse { get; set; }
        public IEnumerable<SelectListItem> StatusKawinDDL_Spouse { get; set; }

        public string AlamatTempatTinggal_Spouse { get; set; }

        public string RT2_Spouse { get; set; }

        public string RW2_Spouse { get; set; }

        public string KodePosAddn_Spouse { get; set; }

        public string kodekecamatan_Spouse { get; set; }

        public string kodedesa_Spouse { get; set; }

        public string KodeFaskesTkI_Spouse { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesTkIDDL_Spouse { get; set; }

        public string KodeFaskesDrg_Spouse { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesDrgDDL_Spouse { get; set; }

        public string NOMORPESERTA_Spouse { get; set; }

        [Required(ErrorMessage = "Please Fill The Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string EMAILS_Spouse { get; set; }

        public string IDPERSON_Spouse { get; set; }

        public string STATUS_Spouse { get; set; }
        public IEnumerable<SelectListItem> STATUSDDL_Spouse { get; set; }

        //[Display(Name = "TMT KERJA")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime TMTKERJA { get; set; }
        //public DateTime? TMTKERJA { get; set; }

        public string TMTKERJA_Spouse { get; set; }

        public string KEWARGANEGARAAN_Spouse { get; set; }
        public IEnumerable<SelectListItem> KEWARGANEGARAANDDL_Spouse { get; set; }

        public string NOMORKARTU_Spouse { get; set; }

        public string NAMAASURANSI_Spouse { get; set; }

        public string NOMORPASSPORT_Spouse { get; set; }
        public string FaskesTK1Code_Spouse { get; set; }
        public string FaskesdrgCode_Spouse { get; set; }
        public string JobTittle_Spouse { get; set; }

        public string FixedSalary_Spouse { get; set; }
        public string NPWP2_Spouse { get; set; }


        // End Add On spouse



        // add on child 1
        public string JenisMutasi_child1 { get; set; }
        public IEnumerable<SelectListItem> JenisMutasiDDL_child1 { get; set; }

        public string TglAktifBerlakuMutasi_child1 { get; set; }

        public string KodeHubKel_child1 { get; set; }
        public IEnumerable<SelectListItem> KodeHubKelDDL_child1 { get; set; }

        public string TempatLahir_child1 { get; set; }

        public string JenisKelamin_child1 { get; set; }
        public IEnumerable<SelectListItem> JenisKelaminDDL_child1 { get; set; }

        public string StatusKawin_child1 { get; set; }
        public IEnumerable<SelectListItem> StatusKawinDDL_child1 { get; set; }

        public string AlamatTempatTinggal_child1 { get; set; }

        public string RT2_child1 { get; set; }

        public string RW2_child1 { get; set; }

        public string KodePosAddn_child1 { get; set; }

        public string kodekecamatan_child1 { get; set; }

        public string kodedesa_child1 { get; set; }

        public string KodeFaskesTkI_child1 { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesTkIDDL_child1 { get; set; }

        public string KodeFaskesDrg_child1 { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesDrgDDL_child1 { get; set; }

        public string NOMORPESERTA_child1 { get; set; }

        [Required(ErrorMessage = "Please Fill The Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string EMAILS_child1 { get; set; }

        public string IDPERSON_child1 { get; set; }

        public string STATUS_child1 { get; set; }
        public IEnumerable<SelectListItem> STATUSDDL_child1 { get; set; }

        //[Display(Name = "TMT KERJA")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime TMTKERJA { get; set; }
        //public DateTime? TMTKERJA { get; set; }

        public string TMTKERJA_child1 { get; set; }

        public string KEWARGANEGARAAN_child1 { get; set; }
        public IEnumerable<SelectListItem> KEWARGANEGARAANDDL_child1 { get; set; }

        public string NOMORKARTU_child1 { get; set; }

        public string NAMAASURANSI_child1 { get; set; }

        public string NOMORPASSPORT_child1 { get; set; }
        public string FaskesTK1Code_child1 { get; set; }
        public string FaskesdrgCode_child1 { get; set; }
        public string JobTittle_child1 { get; set; }

        public string FixedSalary_child1 { get; set; }
        public string NPWP2_child1 { get; set; }


        // End Add child 1




        // add on child 2
        public string JenisMutasi_child2 { get; set; }
        public IEnumerable<SelectListItem> JenisMutasiDDL_child2 { get; set; }

        public string TglAktifBerlakuMutasi_child2 { get; set; }

        public string KodeHubKel_child2 { get; set; }
        public IEnumerable<SelectListItem> KodeHubKelDDL_child2 { get; set; }

        public string TempatLahir_child2 { get; set; }

        public string JenisKelamin_child2 { get; set; }
        public IEnumerable<SelectListItem> JenisKelaminDDL_child2 { get; set; }

        public string StatusKawin_child2 { get; set; }
        public IEnumerable<SelectListItem> StatusKawinDDL_child2 { get; set; }

        public string AlamatTempatTinggal_child2 { get; set; }

        public string RT2_child2 { get; set; }

        public string RW2_child2 { get; set; }

        public string KodePosAddn_child2 { get; set; }

        public string kodekecamatan_child2 { get; set; }

        public string kodedesa_child2 { get; set; }

        public string KodeFaskesTkI_child2 { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesTkIDDL_child2 { get; set; }

        public string KodeFaskesDrg_child2 { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesDrgDDL_child2 { get; set; }

        public string NOMORPESERTA_child2 { get; set; }

        [Required(ErrorMessage = "Please Fill The Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string EMAILS_child2 { get; set; }

        public string IDPERSON_child2 { get; set; }

        public string STATUS_child2 { get; set; }
        public IEnumerable<SelectListItem> STATUSDDL_child2 { get; set; }

        //[Display(Name = "TMT KERJA")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime TMTKERJA { get; set; }
        //public DateTime? TMTKERJA { get; set; }

        public string TMTKERJA_child2 { get; set; }

        public string KEWARGANEGARAAN_child2 { get; set; }
        public IEnumerable<SelectListItem> KEWARGANEGARAANDDL_child2 { get; set; }

        public string NOMORKARTU_child2 { get; set; }

        public string NAMAASURANSI_child2 { get; set; }

        public string NOMORPASSPORT_child2 { get; set; }
        public string FaskesTK1Code_child2 { get; set; }
        public string FaskesdrgCode_child2 { get; set; }
        public string JobTittle_child2 { get; set; }

        public string FixedSalary_child2 { get; set; }
        public string NPWP2_child2 { get; set; }


        // End Add child 2

        // add on child 3
        public string JenisMutasi_child3 { get; set; }
        public IEnumerable<SelectListItem> JenisMutasiDDL_child3 { get; set; }

        public string TglAktifBerlakuMutasi_child3 { get; set; }

        public string KodeHubKel_child3 { get; set; }
        public IEnumerable<SelectListItem> KodeHubKelDDL_child3 { get; set; }

        public string TempatLahir_child3 { get; set; }

        public string JenisKelamin_child3 { get; set; }
        public IEnumerable<SelectListItem> JenisKelaminDDL_child3 { get; set; }

        public string StatusKawin_child3 { get; set; }
        public IEnumerable<SelectListItem> StatusKawinDDL_child3 { get; set; }

        public string AlamatTempatTinggal_child3 { get; set; }

        public string RT2_child3 { get; set; }

        public string RW2_child3 { get; set; }

        public string KodePosAddn_child3 { get; set; }

        public string kodekecamatan_child3 { get; set; }

        public string kodedesa_child3 { get; set; }

        public string KodeFaskesTkI_child3 { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesTkIDDL_child3 { get; set; }

        public string KodeFaskesDrg_child3 { get; set; }
        public IEnumerable<SelectListItem> KodeFaskesDrgDDL_child3 { get; set; }

        public string NOMORPESERTA_child3 { get; set; }

        [Required(ErrorMessage = "Please Fill The Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string EMAILS_child3 { get; set; }

        public string IDPERSON_child3 { get; set; }

        public string STATUS_child3 { get; set; }
        public IEnumerable<SelectListItem> STATUSDDL_child3 { get; set; }

        //[Display(Name = "TMT KERJA")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime TMTKERJA { get; set; }
        //public DateTime? TMTKERJA { get; set; }

        public string TMTKERJA_child3 { get; set; }

        public string KEWARGANEGARAAN_child3 { get; set; }
        public IEnumerable<SelectListItem> KEWARGANEGARAANDDL_child3 { get; set; }

        public string NOMORKARTU_child3 { get; set; }

        public string NAMAASURANSI_child3 { get; set; }

        public string NOMORPASSPORT_child3 { get; set; }
        public string FaskesTK1Code_child3 { get; set; }
        public string FaskesdrgCode_child3 { get; set; }
        public string JobTittle_child3 { get; set; }

        public string FixedSalary_child3 { get; set; }
        public string NPWP2_child3 { get; set; }


        // End Add child 2

        //TAMBAHAN LAGI
        public string KecamatanID { get; set; }
        public IEnumerable<SelectListItem> Kecamatanddl { get; set; }
        public string DesaID { get; set; }
        public IEnumerable<SelectListItem> Desaddl { get; set; }

        public string KecamatanID_Spouse { get; set; }
        public IEnumerable<SelectListItem> Kecamatanddl_Spouse { get; set; }
        public string DesaID_Spouse { get; set; }
        public IEnumerable<SelectListItem> Desaddl_Spouse { get; set; }

        public string KecamatanID_Ch1 { get; set; }
        public IEnumerable<SelectListItem> Kecamatanddl_Ch1 { get; set; }
        public string DesaID_Ch1 { get; set; }
        public IEnumerable<SelectListItem> Desaddl_Ch1 { get; set; }

        public string KecamatanID_Ch2 { get; set; }
        public IEnumerable<SelectListItem> Kecamatanddl_Ch2 { get; set; }
        public string DesaID_Ch2 { get; set; }
        public IEnumerable<SelectListItem> Desaddl_Ch2 { get; set; }

        public string KecamatanID_Ch3 { get; set; }
        public IEnumerable<SelectListItem> Kecamatanddl_Ch3 { get; set; }
        public string DesaID_Ch3 { get; set; }
        public IEnumerable<SelectListItem> Desaddl_Ch3 { get; set; }

        
    }
}