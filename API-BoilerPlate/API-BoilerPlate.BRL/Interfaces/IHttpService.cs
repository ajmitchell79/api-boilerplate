using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_BoilerPlate.BRL.Interfaces
{
    public interface IHttpService
    {
        Task<T> PostData<T>(string accessToken, string url, HttpContent httpContent);

        Task<List<T>> GetData<T>(string accessToken, string url) where T : class;

        Task<T> GetDataSingle<T>(string accessToken, string url) where T : class;
    }
}