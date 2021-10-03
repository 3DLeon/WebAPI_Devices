using Entities_Devices;
using System.Collections.Generic;

namespace Service_Devices
{
    public interface DeviceInterface
    {

        List<Device> GetDevices();
        Device GetDevice(string propertyName, string propertyValue);
        Device AddDevice(Device device);
        Device UpdateDevice(Device device);
        void DeleteDevice(string propertyName, string propertyValue);
    }
}
