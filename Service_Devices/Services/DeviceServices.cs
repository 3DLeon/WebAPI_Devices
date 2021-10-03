using MongoDB.Driver;
using System.Collections.Generic;
using Entities_Devices;

namespace Service_Devices
{
    public class DeviceServices : DeviceInterface
    {
        private readonly IMongoCollection<Device> entity_Devices;

        public DeviceServices(DBClientInterface dBClient)
        {
            entity_Devices = dBClient.GetDevicesCollection();
        }

        public List<Device> GetDevices()
        {
            List<Device> result = entity_Devices.Find(d => true).ToList();
            if (result.Count == 0) { result = null; }

            return result;
        }

        public Device GetDevice(string propertyName, string propertyValue)
        {
            Device result;

            switch (propertyName)
            {
                case "Id":
                    result = entity_Devices.Find(d => d.Id == propertyValue).FirstOrDefault();
                    break;
                case "SerialNumber":
                    result = entity_Devices.Find(d => d.SerialNumber == propertyValue).FirstOrDefault();
                    break;
                default:
                    result = entity_Devices.Find(d => d.Id == propertyValue).FirstOrDefault();
                    break; 
            }

            return result;
        }

        public Device AddDevice(Device device)
        {
            if(GetDevice("SerialNumber", device.SerialNumber) != null)
            {
                throw new System.Exception("The introduced Serial Number already exists");
            }

            entity_Devices.InsertOne(device);
            return device;
        }

        public Device UpdateDevice(Device device)
        {
            if(GetDevice("Id", device.Id) == null)
            {
                throw new System.Exception("The device's ID could not be found");
            }

            entity_Devices.ReplaceOne(d => d.Id == device.Id, device);
            return device;
        }

        public void DeleteDevice(string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case "Id":
                    entity_Devices.DeleteOne(d => d.Id == propertyValue);
                    break;
                case "Type":
                    entity_Devices.DeleteMany(d => d.Type == propertyValue);
                    break;
                case "SerialNumber":
                    entity_Devices.DeleteOne(d => d.SerialNumber == propertyValue);
                    break;
                case "FirmwareVersion":
                    entity_Devices.DeleteMany(d => d.FirmwareVersion == propertyValue);
                    break;
                case "State":
                    entity_Devices.DeleteMany(d => d.State == bool.Parse(propertyValue));
                    break;
                case "IP":
                    entity_Devices.DeleteMany(d => d.IP == propertyValue);
                    break;
                case "Port":
                    entity_Devices.DeleteMany(d => d.Port == propertyValue);
                    break;
            }
        }
    }
}
