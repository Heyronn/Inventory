using Microsoft.AspNetCore.Mvc.Rendering;

namespace NMEX_Manufacturing_KPIs.Models.Module_Inventory
{
    public class InventoryCreationViewModel:Inventory
    {

        public IEnumerable<SelectListItem> DeviceTypes { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<SelectListItem> Versions { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }

        //Filter by plant
        public IEnumerable<SelectListItem> Plants { get; set; }
    }
}
