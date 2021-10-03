using Entities_Devices;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Service_Devices
{
    public class DBClient : DBClientInterface
    {
        private const string mongoDir = "mongodb+srv://DevicesAdminUser:DevicesAdminUser456a123@devicesdb.vm6ih.mongodb.net/DevicesDB?retryWrites=true&w=majority";
        private readonly IMongoCollection<Device> _devices;

        public DBClient(IOptions<DevicesDBConfig> devicesDBConfig)
        {           
            var client = new MongoClient(mongoDir);
            var dataBase = client.GetDatabase(devicesDBConfig.Value.DB_Name);
            _devices = dataBase.GetCollection<Device>(devicesDBConfig.Value.Devices_Collection_Name);
        }
        
        public IMongoCollection<Device> GetDevicesCollection()
        {
            return _devices;
        }
    }
}
