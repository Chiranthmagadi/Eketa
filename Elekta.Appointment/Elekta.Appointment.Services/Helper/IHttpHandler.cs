using System.Net.Http;
using System.Threading.Tasks;

namespace Elekta.Appointment.Services.Helper
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    }
}