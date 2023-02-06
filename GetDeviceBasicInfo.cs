using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseMonitoringClient
{
    class GetDeviceBasicInfo
    {
        public async Task<string> GetMember(string id)
        {
            HttpClient client = new HttpClient();
            string sURL = @"http://localhost:5043/api/baseinfo?id=21345";
            var response = await client.GetStringAsync(sURL).ConfigureAwait(false);
            string res = response;
            return response;
        }
    }
}
