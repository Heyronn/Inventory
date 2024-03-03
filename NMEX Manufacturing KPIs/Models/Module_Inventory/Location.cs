using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Inventory
{
    public class Location
    {
        public int Location_id { get; set; }
        [Display(Name = "PLANT")]
        [Required(ErrorMessage = "FIELD {0} IS REQUIRED")]
        public int Plant_id { get; set; }
        [Display(Name = "LOCATION DESCRIPTION")]
        [Required(ErrorMessage = "FIELD {0} IS REQUIRED")]
        [StringLength(maximumLength: 50, ErrorMessage = "VALUE MUST NOT EXCEED 50 CHARACTERS")]
        public string Location_description { get; set; }
        public bool Active { get; set; }

        //Views properties
        public string Plant { get; set; }
       
    }
}
