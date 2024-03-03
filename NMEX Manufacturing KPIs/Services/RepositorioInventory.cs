using Dapper;
using Microsoft.Data.SqlClient;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;

namespace NMEX_Manufacturing_KPIs.Services
{

    public interface IRepositorioInventory
    {
        //Task<IEnumerable<Inventory>> ByLocationGetInventoryRecords(int Location_id);
        Task CreateDeviceType(DeviceType deviceType);
        Task CreateInventoryRecord(InventoryCreationViewModel inventoryRecord);
        Task CreateLocation(Location location);
        Task CreateModel(Model model);
        Task CreateVersion(Models.Module_Inventory.Version version);
        Task DeleteDeviceType(int D_type_id);
        Task DeleteInventoryRecord(int Inventory_id);
        Task DeleteLocation(int Location_Id);
        Task DeleteModel(int Model_id);
        Task DeleteVersion(int Version_id);
        Task EditDeviceType(DeviceType deviceType);
        Task EditInventoryRecord(InventoryCreationViewModel inventoryRecord);
        //Task EditInventoryRecordLocation(int Inventory_id, string Plant);
        Task EditLocation(Location location);
        Task EditModel(Model model);
        Task EditVersion(Models.Module_Inventory.Version version);
        Task<int> GetByDescriptionPlant(string Plant_description);
        Task<DeviceType> GetByIdDeviceType(int D_type_id);
        Task<Inventory> GetByIdInventoryRecord(int Inventory_id);
        Task<Location> GetByIdLocation(int Location_id);
        Task<Model> GetByIdModel(int Model_id);
        Task<Plant> GetByIdPlant(int Plant_id);
        Task<Models.Module_Inventory.Version> GetByIdVersion(int Version_id);
        Task<IEnumerable<DeviceType>> GetDevicesTypes();
        Task<IEnumerable<Inventory>> GetInventoryRecords();
        Task<IEnumerable<Inventory>> GetInventoryRecordsFilter(int Plant_id);
        Task<IEnumerable<Location>> GetLocations();
        Task<IEnumerable<Model>> GetModels();
        Task<IEnumerable<Plant>> GetPlants();
        Task<IEnumerable<Models.Module_Inventory.Version>> GetVersions();
    }

    public class RepositorioInventory : IRepositorioInventory
    {
        private readonly string connectionString;

        public RepositorioInventory(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Métodos queries del modulo de Inventario ------------------------------------------------------------------------------------------------------------------------------




        //Método para crear un nuevo registro
        public async Task CreateInventoryRecord(InventoryCreationViewModel inventoryRecord)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Inventory (D_type_id, SerialNo, PurchaseDate, Location_id, Version_id, Model_id, Active)
                                                             VALUES (@D_type_id,@SerialNo, @PurchaseDate, @Location_id, @Version_id, @Model_id, '1')
                                                              SELECT SCOPE_IDENTITY();", inventoryRecord);

            inventoryRecord.Inventory_Id = id;
        }

        //Método para obtener el listado de inventory
        public async Task<IEnumerable<Inventory>> GetInventoryRecords()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Inventory>(@"SELECT I.Inventory_id,I.D_type_id, I.Location_id,P.Plant_description AS PlantRecord, I.Version_id, I.Model_id, DT.D_type_description AS DeviceType, I.SerialNo, I.PurchaseDate, L.Location_description AS Location, V.Version_description AS Version, M.Model_description AS Model, I.Active
                                                            FROM Inventory AS I inner join DeviceType AS DT 
                                                            ON I.D_type_id = DT.D_type_id inner join Version AS V
                                                            ON V.Version_id = I.Version_id inner join Model AS M
                                                            ON M.Model_id = I.Model_id inner join Location AS L
                                                            ON L.Location_id = I.Location_id inner join Plant AS P
                                                            ON L.Plant_id = P.Plant_id
                                                            WHERE I.Active=1");
        }

        //Método para obtener el listado de inventory filtrado
        public async Task<IEnumerable<Inventory>> GetInventoryRecordsFilter(int Plant_id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Inventory>(@"SELECT I.Inventory_id,I.D_type_id, I.Location_id,P.Plant_description AS PlantRecord,I.Version_id, I.Model_id, DT.D_type_description AS DeviceType, I.SerialNo, I.PurchaseDate, L.Location_description AS Location, V.Version_description AS Version, M.Model_description AS Model, I.Active
                                                            FROM Inventory AS I inner join DeviceType AS DT 
                                                            ON I.D_type_id = DT.D_type_id inner join Version AS V
                                                            ON V.Version_id = I.Version_id inner join Model AS M
                                                            ON M.Model_id = I.Model_id inner join Location AS L
                                                            ON L.Location_id = I.Location_id inner join Plant AS P
                                                            ON L.Plant_id = P.Plant_id
                                                            WHERE I.Active=1 and P.Plant_id = @Plant_id", new { Plant_id });
        }


        //Método para seleccionar ID
        public async Task<Inventory> GetByIdInventoryRecord(int Inventory_id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Inventory>(@"SELECT I.Inventory_id,I.D_type_id, I.Location_id,P.Plant_description AS PlantRecord, I.Version_id, I.Model_id, DT.D_type_description AS DeviceType, I.SerialNo, I.PurchaseDate, L.Location_description AS Location, V.Version_description AS Version, M.Model_description AS Model, I.Active
                                                                        FROM Inventory AS I inner join DeviceType AS DT 
                                                                        ON I.D_type_id = DT.D_type_id inner join Version AS V
                                                                        ON V.Version_id = I.Version_id inner join Model AS M
                                                                        ON M.Model_id = I.Model_id inner join Location AS L
                                                                        ON L.Location_id = I.Location_id inner join Plant AS P
                                                                        ON L.Plant_id = P.Plant_id
                                                                        WHERE I.Active=1 and I.Inventory_id = @Inventory_id", new { Inventory_id });


        }

        ////Obtener inventrario por locacion
        //public async Task<IEnumerable<Inventory>> ByLocationGetInventoryRecords(int Location_id)
        //{
        //    using var connection = new SqlConnection(connectionString);
        //    return await connection.QueryAsync<Inventory>(@"SELECT I.Inventory_id, I.Plant, L.Location_id
        //                                                    FROM Inventory AS I inner join Location AS L 
        //                                                    ON I.Location_id = L.Location_id
        //                                                    WHERE L.Location_id = @Location_id", new {Location_id});
        //}

        ////Método para editar la planta del registro 
        //public async Task EditInventoryRecordLocation(int Inventory_id, string Plant)
        //{
        //    using var connection = new SqlConnection(connectionString);
        //    await connection.ExecuteAsync(@"UPDATE Inventory
        //                                    SET Plant = @Plant
        //                                    WHERE Inventory_id = @Inventory_id;", new {Inventory_id, Plant});
        //}

        //Método para editar registro
        public async Task EditInventoryRecord(InventoryCreationViewModel inventoryRecord)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Inventory
                                            SET D_type_id = @D_type_id, SerialNo = @SerialNo, PurchaseDate = @PurchaseDate, Location_id = @Location_id, Version_id = @Version_id, Model_id = @Model_id, Active = '1'
                                            WHERE Inventory_id = @Inventory_id;", inventoryRecord);
        }


        //Método para actualizar el active a 0 (Borrado Logico)
        public async Task DeleteInventoryRecord(int Inventory_id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Inventory
                                            SET Active='0'
                                            WHERE Inventory_id = @Inventory_id", new { Inventory_id });


        }

        //Métodos queries del modulo de Locations ------------------------------------------------------------------------------------------------------------------------------

        //Método para obtener el listado locations
        public async Task<IEnumerable<Location>> GetLocations()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Location>(@"SELECT Location_id, Location_description, L.Plant_id, L.Active, P.Plant_description AS Plant
                                                            FROM Location AS L inner join Plant AS P 
                                                            ON L.Plant_id = P.Plant_id 
                                                            WHERE L.Active=1");
        }

        //Método para crear un nuevo registro
        public async Task CreateLocation(Location location)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Location(Location_description, Plant_id, Active)
                                                              VALUES (@Location_description, @Plant_id, 1)
                                                              SELECT SCOPE_IDENTITY();", location);

            location.Location_id = id;
        }


        //Método para seleccionar ID
        public async Task<Location> GetByIdLocation(int Location_id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Location>(@"SELECT Location_id, Location_description, L.Plant_id, L.Active, P.Plant_description AS Plant
                                                                        FROM Location AS L inner join Plant AS P 
                                                                        ON L.Plant_id = P.Plant_id 
                                                                        WHERE L.Active=1 and Location_id = @Location_id", new {Location_id});

         
        }


        //Método para editar registro
        public async Task EditLocation(Location location)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Location
                                                SET Location_description = @Location_description, Plant_id=@Plant_id,Active='1'
                                                WHERE Location_id = @Location_id", location);

            
        }


        //Método para actualizar el active a 0 (Borrado Logico)
        public async Task DeleteLocation(int Location_Id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Location
                                                SET Active='0'
                                                WHERE Location_id = @Location_id", new {Location_Id});


        }


        //Métodos queries del modulo de Models ------------------------------------------------------------------------------------------------------------------------------

        //Método para obtener el listado models
        public async Task<IEnumerable<Model>> GetModels()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Model>(@"SELECT Model_id, Model_description, Active
                                                        FROM Model
                                                        Where Active = '1'");
        }

        //Método para crear un nuevo registro
        public async Task CreateModel(Model model)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Model (Model_description, Active)
                                                              VALUES (@Model_description, '1')
                                                              SELECT SCOPE_IDENTITY();", model);

            model.Model_id = id;
        }

        //Método para seleccionar ID
        public async Task<Model> GetByIdModel(int Model_id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Model>(@"SELECT Model_id, Model_description, Active
                                                                    FROM Model
                                                                    Where Active = '1' and Model_id=@Model_id", new { Model_id });
        }



        //Método para editar registro
        public async Task EditModel(Model model)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Model
                                                SET Model_description = @Model_description, Active = '1'
                                                WHERE Model_id = @Model_id;", model);


        }



        //Método para actualizar el active a 0 (Borrado Logico)
        public async Task DeleteModel(int Model_id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Model
                                            SET Active='0'
                                            WHERE Model_id = @Model_id", new { Model_id });
        }


        //Métodos queries del modulo de Versions ------------------------------------------------------------------------------------------------------------------------------

        //Método para obtener el listado versions
        public async Task<IEnumerable<Models.Module_Inventory.Version>> GetVersions()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Models.Module_Inventory.Version>(@"SELECT Version_id, Version_description,Active, EndOfSupport
                                                                                FROM Version
                                                                                WHERE Active = 1;");
        }

        //Método para crear un nuevo registro
        public async Task CreateVersion(Models.Module_Inventory.Version version)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Version (Version_description, Active, EndOfSupport)
                                                              VALUES (@Version_description, '1', @EndOfSupport)
                                                              SELECT SCOPE_IDENTITY();", version);

            version.Version_id = id;
        }


        //Método para seleccionar ID
        public async Task<Models.Module_Inventory.Version> GetByIdVersion(int Version_id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Models.Module_Inventory.Version>(@"SELECT Version_id, Version_description,Active, EndOfSupport
                                                                                                FROM Version
                                                                                                WHERE Active = '1' and Version_id = @Version_id;", new { Version_id });
        }

        //Método para editar registro
        public async Task EditVersion(Models.Module_Inventory.Version version)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Version
                                            SET Version_description = @Version_description, EndOfSupport = @EndOfSupport, Active = '1'
                                            WHERE Version_id = @Version_id;", version);


        }

        //Método para actualizar el active a 0 (Borrado Logico)
        public async Task DeleteVersion(int Version_id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Version
                                            SET Active='0'
                                            WHERE Version_id = @Version_id", new { Version_id });
        }

        //Métodos queries del modulo de devicesTypes ------------------------------------------------------------------------------------------------------------------------------
        //Método para obtener el listado models
        public async Task<IEnumerable<DeviceType>> GetDevicesTypes()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<DeviceType>(@"SELECT D_type_id, D_type_description, Active
                                                            FROM DeviceType
                                                            WHERE Active = 1;");
        }


        //Método para crear un nuevo registro
        public async Task CreateDeviceType(DeviceType deviceType)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO DeviceType (D_type_description, Active)
                                                              VALUES (@D_type_description, '1')
                                                              SELECT SCOPE_IDENTITY();", deviceType);

            deviceType.D_type_id = id;
        }


        //Método para seleccionar ID
        public async Task<DeviceType> GetByIdDeviceType(int D_type_id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<DeviceType>(@"SELECT D_type_id, D_type_description, Active
                                                                            FROM DeviceType
                                                                            WHERE Active = 1 and D_type_id = @D_type_id;", new { D_type_id });
        }

        //Método para editar registro
        public async Task EditDeviceType(DeviceType deviceType)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE DeviceType
                                            SET D_type_description = @D_type_description, Active = '1'
                                            WHERE D_type_id = @D_type_id;", deviceType);


        }

        //Método para actualizar el active a 0 (Borrado Logico)
        public async Task DeleteDeviceType(int D_type_id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE DeviceType
                                            SET Active='0'
                                            WHERE D_type_id = @D_type_id", new { D_type_id });
        }

        //Métodos queries del modulo de Plants ------------------------------------------------------------------------------------------------------------------------------
        //Método para obtener el listado de plantas
        public async Task<IEnumerable<Plant>> GetPlants()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Plant>(@"SELECT Plant_id, Plant_description, Active
                                                        FROM Plant
                                                        WHERE Active='1'");
        }

        //Método para seleccionar ID
        public async Task<Plant> GetByIdPlant(int Plant_id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Plant>(@"SELECT Plant_id, Plant_description, Active
                                                                        FROM Plant
                                                                        WHERE Plant_id = @Plant_id", new { Plant_id });
        }

        //Método para seleccionar ID
        public async Task<int> GetByDescriptionPlant(string Plant_description)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<int>(@"SELECT Plant_id
                                                                    FROM Plant
                                                                    WHERE Plant_description = @Plant_description", new { Plant_description });
        }


    }
}
