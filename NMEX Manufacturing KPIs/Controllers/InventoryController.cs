using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;
using NMEX_Manufacturing_KPIs.Services;
using System.ComponentModel;

namespace NMEX_Manufacturing_KPIs.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IRepositorioInventory repositorioInventory;
        private readonly IMapper mapper;

        public InventoryController(IRepositorioInventory repositorioInventory, IMapper mapper) {
            this.repositorioInventory = repositorioInventory;
            this.mapper = mapper;
        }

        //Inventory Accions -----------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Index(int plantFilter = 0)
        {
            try
            {
                //Logica del filtro de inventory records
                if(plantFilter== 0)
                {
                    var inventoryRecords = await repositorioInventory.GetInventoryRecords();
                    return View(inventoryRecords);
                }

                var inventoryRecordsFilter = await repositorioInventory.GetInventoryRecordsFilter(plantFilter);
                return View(inventoryRecordsFilter);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateInventoryRecord()
        {
            try
            {
                var model = new InventoryCreationViewModel();
                model.DeviceTypes = await GetDevicesTypes();
                model.Versions = await GetVersions();
                model.Models = await GetModels();
                model.Locations = await GetLocations();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateInventoryRecord(InventoryCreationViewModel inventoryRecord)
        {
            try
            {
                var location = await repositorioInventory.GetByIdLocation(inventoryRecord.Location_id);
                inventoryRecord.PlantRecord=location.Plant;

                await repositorioInventory.CreateInventoryRecord(inventoryRecord);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditInventoryRecord(int id)
        {
            try
            {
                var inventoryRecord = await repositorioInventory.GetByIdInventoryRecord(id);
                var model = mapper.Map<InventoryCreationViewModel>(inventoryRecord);
                
                model.DeviceTypes = await GetDevicesTypes();
                model.Versions = await GetVersions();
                model.Models = await GetModels();
                model.Locations = await GetLocations();

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditInventoryRecord(InventoryCreationViewModel inventoryRecord)
        {
            try
            {
                var location = await repositorioInventory.GetByIdLocation(inventoryRecord.Location_id);
                inventoryRecord.PlantRecord = location.Plant;

                await repositorioInventory.EditInventoryRecord(inventoryRecord);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                var inventoryRecord = await repositorioInventory.GetByIdInventoryRecord(id);

                return View(inventoryRecord);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInventoryRecord(int Inventory_id)
        {
            try
            {
                await repositorioInventory.DeleteInventoryRecord(Inventory_id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> FilterByPlant()
        {
            try
            {
                var model = new InventoryCreationViewModel();
                model.Plants = await GetPlants();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> FilterByPlant(InventoryCreationViewModel plantFilter)
        {
            try
            {

                var plantRecord = await repositorioInventory.GetByIdPlant(plantFilter.Plant_id);
                return RedirectToAction(nameof(Index), new { plantFilter = plantRecord.Plant_id });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }



        [HttpGet]
        public IActionResult InventoryRecordExcel()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public async Task<IActionResult> InventoryRecordExcel(IFormFile excelFile)
        {
            try
            {
                if (excelFile == null || excelFile.Length == 0)
                {
                    // Manejar el caso en que no se ha seleccionado ningún archivo
                    return RedirectToAction(nameof(Index));
                }

                using (var stream = new MemoryStream())
                {
                    await excelFile.CopyToAsync(stream);
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);

                        var firstRowUsed = worksheet.FirstRowUsed();
                        var lastRowUsed = worksheet.LastRowUsed();

                        for (int rowNum = 2; rowNum <= lastRowUsed.RowNumber(); rowNum++)
                        {
                            var inventoryRecord = new Dictionary<string, object>();


                            for (int colNum = 2; colNum <= 8; colNum++) // Columnas de la B a la H
                            {
                                //Console.WriteLine(worksheet.Cell(rowNum, colNum).Value);

                                if(colNum == 2)
                                {
                                    inventoryRecord.Add("Serial No.", worksheet.Cell(rowNum, colNum).Value);
                                }

                                if(colNum == 3)
                                {
                                    var deviceType = new DeviceType();
                                    deviceType.D_type_description = (string)worksheet.Cell(rowNum, colNum).Value;

                                    await repositorioInventory.CreateDeviceType(deviceType);
                                    inventoryRecord.Add("Device Type", deviceType.D_type_id);
                                }

                                if (colNum == 4)
                                {
                                    var model = new Model();
                                    model.Model_description = (string)worksheet.Cell(rowNum, colNum).Value;

                                    await repositorioInventory.CreateModel(model);
                                    inventoryRecord.Add("Model", model.Model_id);
                                }

                                if (colNum == 5)
                                {
                                    var version = new Models.Module_Inventory.Version();
                                    version.Version_description = (string)worksheet.Cell(rowNum, colNum).Value;
                                    version.EndOfSupport = null;

                                    await repositorioInventory.CreateVersion(version);
                                    inventoryRecord.Add("Version", version.Version_id);
                                }

                                if (colNum == 6)
                                {
                                    var location = new Location();
                                    location.Location_description = (string)worksheet.Cell(rowNum, colNum).Value;

                                    var plant_id = await repositorioInventory.GetByDescriptionPlant((string)worksheet.Cell(rowNum, colNum+1).Value);
                                    location.Plant_id = plant_id;
                                    await repositorioInventory.CreateLocation(location);
                                    inventoryRecord.Add("Location", location.Location_id);
                                }
                                if (colNum == 8)
                                {
                                    inventoryRecord.Add("Purchase Date", worksheet.Cell(rowNum, colNum).Value);
                                }
                            }

                            //Console.WriteLine(inventoryRecord);
                            var inventoryRecords = new InventoryCreationViewModel();
                            inventoryRecords.SerialNo = inventoryRecord["Serial No."].ToString();
                            inventoryRecords.D_type_id = (int)inventoryRecord["Device Type"];
                            inventoryRecords.Model_id = (int)inventoryRecord["Model"];
                            inventoryRecords.Version_id = (int)inventoryRecord["Version"];
                            inventoryRecords.Location_id = (int)inventoryRecord["Location"];
                            inventoryRecords.PurchaseDate = inventoryRecord["Purchase Date"].ToString();
                            await repositorioInventory.CreateInventoryRecord(inventoryRecords);
                            
                        }
                        
                    }
                }
                // Redirigir a una acción de éxito o a donde sea necesario
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetDevicesTypes()
        {
            var devicesTypes = await repositorioInventory.GetDevicesTypes();
            return devicesTypes.Select(x => new SelectListItem(x.D_type_description, x.D_type_id.ToString()));
        }

        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetVersions()
        {
            var versions = await repositorioInventory.GetVersions();
            return versions.Select(x => new SelectListItem(x.Version_description, x.Version_id.ToString()));
        }

        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetModels()
        {
            var models = await repositorioInventory.GetModels();
            return models.Select(x => new SelectListItem(x.Model_description, x.Model_id.ToString()));
        }

        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetLocations()
        {
            var locations = await repositorioInventory.GetLocations();
            return locations.Select(x => new SelectListItem(x.Location_description, x.Location_id.ToString()));
        }

        //Location Accions -----------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Location()
        {
            try
            {
                var locations = await repositorioInventory.GetLocations();
                return View(locations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreateLocation()
        {
            try
            {
                var model = new LocationCreationViewModel();
                model.Plants = await GetPlants();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationCreationViewModel location)
        {
            try
            {
                await repositorioInventory.CreateLocation(location);
                return RedirectToAction(nameof(Location));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditLocation(int id)
        {
            try
            {
                var location = await repositorioInventory.GetByIdLocation(id);
                var model = mapper.Map<LocationCreationViewModel>(location);

                model.Plants = await GetPlants();

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditLocation(Location location)
        {
            try
            {
                ////Modificacion de planta para registros de inventario
                //var oldLocationRecord = await repositorioInventory.GetByIdLocation(location.Location_id);

                //if(location.Plant_id != oldLocationRecord.Plant_id)
                //{
                //    var inventoryRecords = await repositorioInventory.ByLocationGetInventoryRecords(location.Location_id);
                //    var plant = await repositorioInventory.GetByIdPlant(location.Plant_id);

                //    if (inventoryRecords != null)//No se puede asignar count por que es un conjunto de metodos
                //    {
                //        foreach (var record in inventoryRecords)
                //        {
                //            await repositorioInventory.EditInventoryRecordLocation(record.Inventory_Id, plant.Plant_description);
                //        }
                //    }
                //}
             
                await repositorioInventory.EditLocation(location);
                return RedirectToAction(nameof(Location));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            try
            {
                var location = await repositorioInventory.GetByIdLocation(id);

                return View(location);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecordLocation(int Location_id)
        {
            try
            {
                await repositorioInventory.DeleteLocation(Location_id);

                return RedirectToAction(nameof(Location));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }


        // Model Accions --------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Model()
        {
            try
            {
                var models = await repositorioInventory.GetModels();
                return View(models);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public IActionResult CreateModel()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(Model model)
        {
            try
            {
                await repositorioInventory.CreateModel(model);
                return RedirectToAction(nameof(Model));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditModel(int id)
        {
            try
            {
                var model = await repositorioInventory.GetByIdModel(id);

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditModel(Model model)
        {
            try
            {
                await repositorioInventory.EditModel(model);
                return RedirectToAction(nameof(Model));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModel(int id)
        {
            try
            {
                var model = await repositorioInventory.GetByIdModel(id);

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecordModel(int Model_id)
        {
            try
            {
                await repositorioInventory.DeleteModel(Model_id);

                return RedirectToAction(nameof(Model));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        //Version Accions --------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Version()
        {
            try
            {
                var versions = await repositorioInventory.GetVersions();
                return View(versions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult CreateVersion()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVersion(Models.Module_Inventory.Version version)
        {
            try
            {
                await repositorioInventory.CreateVersion(version);
                return RedirectToAction(nameof(Version));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditVersion(int id)
        {
            try
            {
                var version = await repositorioInventory.GetByIdVersion(id);

                return View(version);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditVersion(Models.Module_Inventory.Version version)
        {
            try
            {
                await repositorioInventory.EditVersion(version);
                return RedirectToAction(nameof(Version));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteVersion(int id)
        {
            try
            {
                var version = await repositorioInventory.GetByIdVersion(id);

                return View(version);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecordVersion(int Version_id)
        {
            try
            {
                await repositorioInventory.DeleteVersion(Version_id);

                return RedirectToAction(nameof(Version));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        //DeviceType Accions --------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> DeviceType()
        {
            try
            {
                var devicesTypes = await repositorioInventory.GetDevicesTypes();
                return View(devicesTypes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult CreateDeviceType()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeviceType(DeviceType deviceType)
        {
            try
            {
                await repositorioInventory.CreateDeviceType(deviceType);
                return RedirectToAction(nameof(DeviceType));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditDeviceType(int id)
        {
            try
            {
                var deviceType = await repositorioInventory.GetByIdDeviceType(id);

                return View(deviceType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditDeviceType(DeviceType deviceType)
        {
            try
            {
                await repositorioInventory.EditDeviceType(deviceType);
                return RedirectToAction(nameof(DeviceType));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDeviceType(int id)
        {
            try
            {
                var deviceType = await repositorioInventory.GetByIdDeviceType(id);

                return View(deviceType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteRecordDeviceType(int D_type_id)
        {
            try
            {
                await repositorioInventory.DeleteDeviceType(D_type_id);

                return RedirectToAction(nameof(DeviceType));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }


        //Plant Accions --------------------------------------------------------------
        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetPlants()
        {
            var tiposAreas = await repositorioInventory.GetPlants();
            return tiposAreas.Select(x => new SelectListItem(x.Plant_description, x.Plant_id.ToString()));
        }
    }
}
