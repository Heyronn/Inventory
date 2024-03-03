using AutoMapper;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;


namespace NMEX_Manufacturing_KPIs.Services
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() 
        {
            //Module Inventory
            CreateMap<Location, LocationCreationViewModel>();
            CreateMap<Inventory, InventoryCreationViewModel>();

            //Module Security

        }
    }
}
