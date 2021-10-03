using Connector_Devices;
using Entities_Devices;
using NUnit.Framework;
using System.Threading.Tasks;

namespace UnitTests_Devices
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public async Task AddDeleteDevice_AnyType()
		{
			bool result = true;

			try {
				Device electricDevice = CreateElectricDevice();
				await ConnectorDevices.AddDevice(electricDevice, new System.Threading.CancellationToken());

				Device waterDevice = CreateWaterDevice();
				await ConnectorDevices.AddDevice(waterDevice, new System.Threading.CancellationToken());

				Device gatewaysDevice = CreateGatewaysDevice();
				await ConnectorDevices.AddDevice(gatewaysDevice, new System.Threading.CancellationToken());

				await ConnectorDevices.DeleteDevice("SerialNumber", electricDevice.SerialNumber, new System.Threading.CancellationToken());
				await ConnectorDevices.DeleteDevice("SerialNumber", waterDevice.SerialNumber, new System.Threading.CancellationToken());
				await ConnectorDevices.DeleteDevice("SerialNumber", gatewaysDevice.SerialNumber, new System.Threading.CancellationToken());
			}
			catch (System.Exception) {
				result = false;
			}

			Assert.That(result == true);
		}

		[Test]
		public async Task AddRepeatedDevice_ReturnFalse()
		{
			Device electricDevice = CreateElectricDevice();
			bool result = true;

			try {
				await ConnectorDevices.AddDevice(electricDevice, new System.Threading.CancellationToken());

				await ConnectorDevices.AddDevice(electricDevice, new System.Threading.CancellationToken());

				await ConnectorDevices.DeleteDevice("SerialNumber", electricDevice.SerialNumber, new System.Threading.CancellationToken());
			}
			catch (System.Exception) {
				result = false;
				await ConnectorDevices.DeleteDevice("SerialNumber", electricDevice.SerialNumber, new System.Threading.CancellationToken());
			}

			Assert.That(result == false);
		}

		[Test]
		public async Task AddGetDevice()
		{
			bool result = true;

			try {
				Device electricDevice = CreateElectricDevice();
				await ConnectorDevices.AddDevice(electricDevice, new System.Threading.CancellationToken());

				await ConnectorDevices.GetDevice("SerialNumber", electricDevice.SerialNumber, new System.Threading.CancellationToken());

				await ConnectorDevices.DeleteDevice("SerialNumber", electricDevice.SerialNumber, new System.Threading.CancellationToken());
			}
			catch (System.Exception) {
				result = false;
			}

			Assert.That(result == true);
		}

		[Test]
		public async Task GetUnexistingDevice()
		{
			bool result = true;

			try {
				await ConnectorDevices.GetDevice("SerialNumber", "UnexistingDevice", new System.Threading.CancellationToken());
			}
			catch (System.Exception) {
				result = false;
			}

			Assert.That(result == true);
		}

		private Device CreateElectricDevice()
		{
			string type = "Electric";
			string serialNumber = "ElectricMeterSerialNumber";
			string firmwareVersion = "10.25.16.142";
			bool state = true;

			return new Device(type, serialNumber, firmwareVersion, state, null, null);
		}

		private Device CreateWaterDevice()
		{
			string type = "Water";
			string serialNumber = "WaterMeterSerialNumber";
			string firmwareVersion = "10.25.16.142";
			bool state = true;

			return new Device(type, serialNumber, firmwareVersion, state, null, null);
		}

		private Device CreateGatewaysDevice()
		{
			string type = "Gateways";
			string serialNumber = "GatewaysSerialNumber";
			string firmwareVersion = "10.25.16.142";
			bool state = true;
			string ip = "32.64.92.148";
			string port = "45102";

			return new Device(type, serialNumber, firmwareVersion, state, ip, port);
		}
	}
}