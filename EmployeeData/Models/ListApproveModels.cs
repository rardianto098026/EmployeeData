using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeData.Models
{
    public class ListApproveModels
    {
        public string LinkAttachment { get; set; }
        public string KK_Spouse { get; set; }
        public string KK_Child_1 { get; set; }
        public string KK_Child_2 { get; set; }
        public string KK_Child_3 { get; set; }
        public string Class { get; set; }
        public string Class_Spouse { get; set; }
        public string Class_Child_1 { get; set; }
        public string Class_Child_2 { get; set; }
        public string Class_Child_3 { get; set; }

        public string BPJS_JoinDate { get; set; }

        public string Remark { get; set; }
        public string Username1 { get; set; }
        public string No1 { get; set; }
        public string NIK1 { get; set; }
        public string EmployeeID1 { get; set; }
        public string Name { get; set; }
        public string EmployeeName1 { get; set; }
        public string Entity1 { get; set; }
        public string EntityID1 { get; set; }
        public IEnumerable<SelectListItem> EntitySearch { get; set; }
        public string EntityIDSearch { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public IEnumerable<SelectListItem> DepartmentID { get; set; }

        public string idx { get; set; }
        public string idx_rev { get; set; }
        public string idx_rev_spouse { get; set; }
        public string idx_rev_child_1 { get; set; }
        public string idx_rev_child_2 { get; set; }
        public string idx_rev_child_3 { get; set; }
        public string idx_child_1 { get; set; }
        public string idx_child_2 { get; set; }
        public string idx_child_3 { get; set; }

        public string FLAG { get; set; }
        public string FLAG_Spouse { get; set; }
        public string FLAG_Parent { get; set; }
        public string FLAG_Child_1 { get; set; }
        public string FLAG_Child_2 { get; set; }
        public string FLAG_Child_3 { get; set; }


        public string Username { get; set; }
        public string No { get; set; }
        public string NIK { get; set; }
        public string NIK_child_1 { get; set; }
        public string NIK_child_2 { get; set; }
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
        public IEnumerable<SelectListItem> Univ_Name_ID { get; set; }
        public IEnumerable<SelectListItem> MaritalStatus_ID { get; set; }
        public IEnumerable<SelectListItem> Education_ID { get; set; }
        public IEnumerable<SelectListItem> Gender { get; set; }
        public string Gender_ID { get; set; }
        public string Gender_ID_child_1 { get; set; }
        public string Gender_ID_child_2 { get; set; }
        public string Gender_ID_child_3 { get; set; }

        public IEnumerable<SelectListItem> PlaceBirth { get; set; }
        public string PlaceBirth_ID { get; set; }
        public string Birthdate { get; set; }
        public string Birthdate_child_1 { get; set; }
        public string Birthdate_child_2 { get; set; }
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

        public IEnumerable<SelectListItem> Department1 { get; set; }
        public string DepartmentID1 { get; set; }
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

        //ADD ON CHILD 1 
        public string JENISMUTASI_CHILD1 { get; set; }
        public IEnumerable<SelectListItem> JENISMUTASI_CHILD1_ID { get; set; }
        public string TglAktifBerlakuMutasi_CHILD1 { get; set; }
        public string FAMILYRELATIONSHIP_CHILD1 { get; set; }
        public IEnumerable<SelectListItem> FAMILYRELATIONSHIP_CHILD1_ID { get; set; }
        public string TEMPATLAHIR_CHILD1 { get; set; }
        public string GENDER_CHILD1 { get; set; }
        public IEnumerable<SelectListItem> GENDER_CHILD1_ID { get; set; }
        public string MARITALSTATUS_CHILD1 { get; set; }
        public IEnumerable<SelectListItem> MARITALSTATUS_CHILD1_ID { get; set; }
        public string ADDRESS_CHILD1 { get; set; }
        public string RT_CHILD1 { get; set; }
        public string RW_CHILD1 { get; set; }
        public string POSTALCODE_CHILD1 { get; set; }
        public string KECAMATANCODE_CHILD1 { get; set; }
        public string KELURAHANCODE_CHILD1 { get; set; }
        public string FASKESCODETKI_CHILD1 { get; set; }

        public string FASKESDRGCODE_CHILD1 { get; set; }
        public string MOBILEPHONE_CHILD1 { get; set; }

        public string EMAIL_CHILD1 { get; set; }
        public string EMPLID_CHILD1 { get; set; }
        public string JOBTITTLE_CHILD1 { get; set; }

        public string EMPLSTATUS_CHILD1 { get; set; }
        public string TMTKERJA_CHILD1 { get; set; }

        public string FIXEDSALARY_CHILD1 { get; set; }
        public string CITIZENSHIP_CHILD1 { get; set; }
        public IEnumerable<SelectListItem> CITIZENSHIP_CHILD1_ID { get; set; }
        public string INSURANCECARD_CHILD1 { get; set; }
        public string INSURANCENAME_CHILD1 { get; set; }

        public string NPWP_CHILD1 { get; set; }
        public string PASSPORT_CHILD1 { get; set; }

        //END ADD ON CHILD 1

        //ADD ON CHILD 2
        public string JENISMUTASI_CHILD2 { get; set; }
        public IEnumerable<SelectListItem> JENISMUTASI_CHILD2_ID { get; set; }

        public string TglAktifBerlakuMutasi_CHILD2 { get; set; }
        public string FAMILYRELATIONSHIP_CHILD2 { get; set; }
        public IEnumerable<SelectListItem> FAMILYRELATIONSHIP_CHILD2_ID { get; set; }
        public string TEMPATLAHIR_CHILD2 { get; set; }
        public string GENDER_CHILD2 { get; set; }
        public IEnumerable<SelectListItem> GENDER_CHILD2_ID { get; set; }
        public string MARITALSTATUS_CHILD2 { get; set; }
        public IEnumerable<SelectListItem> MARITALSTATUS_CHILD2_ID { get; set; }
        public string ADDRESS_CHILD2 { get; set; }
        public string RT_CHILD2 { get; set; }
        public string RW_CHILD2 { get; set; }
        public string POSTALCODE_CHILD2 { get; set; }
        public string KECAMATANCODE_CHILD2 { get; set; }
        public string KELURAHANCODE_CHILD2 { get; set; }
        public string FASKESCODETKI_CHILD2 { get; set; }

        public string FASKESDRGCODE_CHILD2 { get; set; }
        public string MOBILEPHONE_CHILD2 { get; set; }

        public string EMAIL_CHILD2 { get; set; }
        public string EMPLID_CHILD2 { get; set; }
        public string JOBTITTLE_CHILD2 { get; set; }

        public string EMPLSTATUS_CHILD2 { get; set; }
        public string TMTKERJA_CHILD2 { get; set; }

        public string FIXEDSALARY_CHILD2 { get; set; }
        public string CITIZENSHIP_CHILD2 { get; set; }
        public IEnumerable<SelectListItem> CITIZENSHIP_CHILD2_ID { get; set; }
        public string INSURANCECARD_CHILD2 { get; set; }
        public string INSURANCENAME_CHILD2 { get; set; }

        public string NPWP_CHILD2 { get; set; }
        public string PASSPORT_CHILD2 { get; set; }

        //END ADD ON CHILD 2

        // ADD ON CHILD 3
        public string JENISMUTASI_CHILD3 { get; set; }
        public IEnumerable<SelectListItem> JENISMUTASI_CHILD3_ID { get; set; }
        public string TglAktifBerlakuMutasi_CHILD3 { get; set; }
        public string FAMILYRELATIONSHIP_CHILD3 { get; set; }
        public IEnumerable<SelectListItem> FAMILYRELATIONSHIP_CHILD3_ID { get; set; }
        public string TEMPATLAHIR_CHILD3 { get; set; }
        public string GENDER_CHILD3 { get; set; }
        public IEnumerable<SelectListItem> GENDER_CHILD3_ID { get; set; }
        public string MARITALSTATUS_CHILD3 { get; set; }
        public IEnumerable<SelectListItem> MARITALSTATUS_CHILD3_ID { get; set; }
        public string ADDRESS_CHILD3 { get; set; }
        public string RT_CHILD3 { get; set; }
        public string RW_CHILD3 { get; set; }
        public string POSTALCODE_CHILD3 { get; set; }
        public string KECAMATANCODE_CHILD3 { get; set; }
        public string KELURAHANCODE_CHILD3 { get; set; }
        public string FASKESCODETKI_CHILD3 { get; set; }

        public string FASKESDRGCODE_CHILD3 { get; set; }
        public string MOBILEPHONE_CHILD3 { get; set; }
        public string EMAIL_CHILD3 { get; set; }
        public string EMPLID_CHILD3 { get; set; }
        public string JOBTITTLE_CHILD3 { get; set; }

        public string EMPLSTATUS_CHILD3 { get; set; }
        public string TMTKERJA_CHILD3 { get; set; }

        public string FIXEDSALARY_CHILD3 { get; set; }

        public string CITIZENSHIP_CHILD3 { get; set; }
        public IEnumerable<SelectListItem> CITIZENSHIP_CHILD3_ID { get; set; }
        public string INSURANCECARD_CHILD3 { get; set; }
        public string INSURANCENAME_CHILD3 { get; set; }

        public string NPWP_CHILD3 { get; set; }
        public string PASSPORT_CHILD3 { get; set; }

        // END ADD ON CHILD 3


        // ADD ON SPOUSE
        public string JENISMUTASI_SPOUSE { get; set; }
        public IEnumerable<SelectListItem> JENISMUTASI_SPOUSE_ID { get; set; }
        public string TglAktifBerlakuMutasi_SPOUSE { get; set; }
        public string NIK_Spouse { get; set; }
        public string FAMILYRELATIONSHIP_SPOUSE { get; set; }
        public IEnumerable<SelectListItem> FAMILYRELATIONSHIP_SPOUSE_ID { get; set; }
        public string Full_Name_Spouse { get; set; }
        public string TEMPATLAHIR_SPOUSE { get; set; }
        public string Birthdate_Spouse { get; set; }
        public string Gender_Spouse_ID { get; set; }
        public IEnumerable<SelectListItem> Gender_Spouse { get; set; }
        public string MARITALSTATUS_SPOUSE { get; set; }
        public IEnumerable<SelectListItem> MARITALSTATUS_SPOUSE_ID { get; set; }

        public string ADDRESS_SPOUSE { get; set; }
        public string RT_SPOUSE { get; set; }
        public string RW_SPOUSE { get; set; }
        public string POSTALCODE_SPOUSE { get; set; }
        public string KECAMATANCODE_SPOUSE { get; set; }
        public string KELURAHANCODE_SPOUSE { get; set; }
        public string FASKESCODETKI_SPOUSE { get; set; }
        public string FASKESDRGCODE_SPOUSE { get; set; }

        public string MOBILEPHONE_SPOUSE { get; set; }
        public string EMAIL_SPOUSE { get; set; }
        public string EMPLID_SPOUSE { get; set; }
        public string JOBTITTLE_SPOUSE { get; set; }

        public string EMPLSTATUS_SPOUSE { get; set; }
        public string TMTKERJA_SPOUSE { get; set; }

        public string FIXEDSALARY_SPOUSE { get; set; }

        public string CITIZENSHIP_SPOUSE { get; set; }
        public IEnumerable<SelectListItem> CITIZENSHIP_SPOUSE_ID { get; set; }

        public string INSURANCECARD_SPOUSE { get; set; }
        public string INSURANCENAME_SPOUSE { get; set; }
        public string NPWP_SPOUSE { get; set; }
        public string PASSPORT_SPOUSE { get; set; }
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
        // END ADD ON SPOUSE

        public string LinkEditChild { get; set; }
        public string Full_Name_Child { get; set; }
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

        public string RemarkApproval { get; set; }

        // ADD ON EMPLOYEE TAMBAHAN
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

        public string FaskesTKICode { get; set; }
        public string FASKESDRGCODE { get; set; }

        public string jobtittle { get; set; }

        public string fixedsalary { get; set; }

        public string NPWP2 { get; set; }
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
        // END ADD ON EMPLOYEE TAMBAHAN

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