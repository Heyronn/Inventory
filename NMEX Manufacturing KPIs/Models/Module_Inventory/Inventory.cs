using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Inventory
{
    public class Inventory
    {
        public int Inventory_Id { get; set; }
        public int D_type_id { get; set; }
        [Display(Name = "SERIAL NO")]
        [Required(ErrorMessage = "FIELD {0} IS REQUIRED")]
        [StringLength(maximumLength: 50, ErrorMessage = "VALUE MUST NOT EXCEED 50 CHARACTERS")]
        public string SerialNo { get; set; }
        [Required(ErrorMessage = "FIELD {0} IS REQUIRED")]
        public string PurchaseDate { get; set; }
        public int Location_id { get; set; }
        public int Version_id { get; set; }
        public int Model_id { get; set; }
        public bool Active { get; set; }
        
       

        //Views properties
        public string DeviceType { get; set; }
        public string Version { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
        [Display(Name = "PLANT")]
        public string PlantRecord { get; set; }

        //Filter by plant
        public string Plant { get; set; }
        public int Plant_id { get; set; }
    }
}
