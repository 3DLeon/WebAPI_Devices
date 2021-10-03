using Entities_Devices;
using MongoDB.Driver;

namespace Service_Devices
{
    public interface DBClientInterface
    {
        IMongoCollection<Device> GetDevicesCollection();
    }
}
