using Microsoft.AspNetCore.Mvc.Rendering;

namespace NMEX_Manufacturing_KPIs.Models.Module_Inventory
{
    public class LocationCreationViewModel:Location
    {

        public IEnumerable<SelectListItem> Plants { get; set; }
    }
}
