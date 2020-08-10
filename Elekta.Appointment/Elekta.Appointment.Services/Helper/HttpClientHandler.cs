using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Elekta.Appointment.Services.Helper
{
    public class HttpClientHandler : IHttpHandler
    {
        private HttpClient _client = new HttpClient();
        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await _client.PostAsync(url, content);
        }
    }
}
