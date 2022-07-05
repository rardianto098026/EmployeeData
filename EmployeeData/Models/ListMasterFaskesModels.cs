using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeData.Models
{
    public class ListMasterFaskesModels
    {
        public string Provinsi_ID { get; set; }
        public string No { get; set; }
        public string KotaMadya_ID { get; set; }
        public string Kecamatan_ID { get; set; }
        public string NamaFaskes_ID { get; set; }
        public string Faskes_GIGI_ID { get; set; }
        public string idx { get; set; }
        public IEnumerable<SelectListItem> Provinsi { get; set; }
        public IEnumerable<SelectListItem> KotaMadya { get; set; }
        public IEnumerable<SelectListItem> Kecamatan { get; set; }
        public IEnumerable<SelectListItem> NamaFaskes { get; set; }
        public IEnumerable<SelectListItem> Faskes_GIGI { get; set; }
        public List<ListMasterFaskesModels> GetFaskes { get; set; }

        public string LINK { get; set; }
    }
}