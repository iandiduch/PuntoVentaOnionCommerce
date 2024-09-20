using Newtonsoft.Json;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Onion_Commerce.Utilitarios
{
    public class GetPrecioDolar
    {
        private string apiUrl = "https://dolarapi.com/v1/dolares/blue";

        public async Task<ApiResponseDolar> Get()
        {
            // Crear una instancia de HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realizar la solicitud GET a la API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Verificar si la solicitud fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ApiResponseDolar>(json);

                        return result;
                    }
                    else
                    {
                        MessageBox.Show($"Error en la solicitud: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return null;
                }
            }

        }
    }
}
