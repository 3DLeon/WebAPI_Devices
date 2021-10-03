using Entities_Devices;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Connector_Devices
{
	public class ConnectorDevices
	{
		//=============================================================================================================================================================
		//METHODS
		//=============================================================================================================================================================

		private static HttpRequestMessage CreateRequest(HttpMethod metodoHtpp, string url, HttpContent contenido = null)
		{
			HttpRequestMessage solicitud = new HttpRequestMessage(metodoHtpp, url);
			if (contenido != null) solicitud.Content = contenido;
			return solicitud;
		}

		private static string GetBaseURL()
		{
			return "http://localhost:44510/";
		}

		private static JsonSerializerOptions GetOptions() => new() {
			PropertyNameCaseInsensitive = true,
			WriteIndented = true,
			ReferenceHandler = ReferenceHandler.Preserve,
			Encoder = JavaScriptEncoder.Default
		};

		#region "CRUD"

		public async static Task<List<Device>> GetDevices(CancellationToken tokenCancelacion)
		{
			using (var cliente = new HttpClient())
			using (var solicitud = CreateRequest(HttpMethod.Get, $"{GetBaseURL()}Devices/GetDevices"))
			using (var resultado = await cliente.SendAsync(solicitud, HttpCompletionOption.ResponseHeadersRead, tokenCancelacion).ConfigureAwait(false)) {

				resultado.EnsureSuccessStatusCode(); // Return Exception if the result is negative.

				using (Stream contenido = await resultado.Content.ReadAsStreamAsync()) {
					List<Device> res = null;
					if (resultado.ReasonPhrase != "No Content") {
						res = await JsonSerializer.DeserializeAsync<List<Device>>(contenido, GetOptions());
					}

					return res;
				}
			}
		}

		public async static Task<Device> GetDevice(string propertyName, string propertyValue, CancellationToken tokenCancelacion)
		{
			using (var cliente = new HttpClient())
			using (var solicitud = CreateRequest(HttpMethod.Get, $"{GetBaseURL()}Devices/GetDevice?propertyName={propertyName}&propertyValue={propertyValue}"))
			using (var resultado = await cliente.SendAsync(solicitud, HttpCompletionOption.ResponseHeadersRead, tokenCancelacion).ConfigureAwait(false)) {

				resultado.EnsureSuccessStatusCode(); // Return Exception if the result is negative.

				using (Stream contenido = await resultado.Content.ReadAsStreamAsync()) {
					Device res = null;
					if (resultado.ReasonPhrase != "No Content") {
						res = await JsonSerializer.DeserializeAsync<Device>(contenido, GetOptions());
					}

					return res;
				}
			}
		}

		public async static Task<Device> AddDevice(Device device, CancellationToken tokenCancelacion)
		{
			using (var cliente = new HttpClient())
			using (var solicitud = CreateRequest(HttpMethod.Post, $"{GetBaseURL()}Devices/AddDevice", new StringContent(JsonSerializer.Serialize(device), Encoding.UTF8, "application/json")))
			using (var resultado = await cliente.SendAsync(solicitud, HttpCompletionOption.ResponseHeadersRead, tokenCancelacion).ConfigureAwait(false)) {

				resultado.EnsureSuccessStatusCode(); // Return Exception if the result is negative.

				using (Stream contenido = await resultado.Content.ReadAsStreamAsync()) {
					Device res = null;
					if (resultado.ReasonPhrase != "No Content") {
						res = await JsonSerializer.DeserializeAsync<Device>(contenido, GetOptions());
					}

					return res;
				}
			}
		}

		public async static Task<Device> DeleteDevice(string propertyName, string propertyValue, CancellationToken tokenCancelacion)
		{
			using (var cliente = new HttpClient())
			using (var solicitud = CreateRequest(HttpMethod.Delete, $"{GetBaseURL()}Devices/DeleteDevice?propertyName={propertyName}&propertyValue={propertyValue}"))
			using (var resultado = await cliente.SendAsync(solicitud, HttpCompletionOption.ResponseHeadersRead, tokenCancelacion).ConfigureAwait(false)) {

				resultado.EnsureSuccessStatusCode(); // Return Exception if the result is negative.

				using (Stream contenido = await resultado.Content.ReadAsStreamAsync()) {
					Device res = null;
					if (resultado.ReasonPhrase != "No Content") {
						res = await JsonSerializer.DeserializeAsync<Device>(contenido, GetOptions());
					}

					return res;
				}
			}
		}

		#endregion
	}
}
