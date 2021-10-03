using MongoDB.Bson.Serialization.Attributes;

namespace Entities_Devices
{
    public class Device
    {
        //Properties

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Type { get; set; }
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public bool State { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }

        //Constructors
        public Device() { }

        public Device(string type, string serialNumber, string firmwareVersion, bool state, string ip, string port)
		{
            Type = type;
            SerialNumber = serialNumber;
            FirmwareVersion = firmwareVersion;
            State = state;
            IP = ip;
            Port = port;
		}
    }
}
