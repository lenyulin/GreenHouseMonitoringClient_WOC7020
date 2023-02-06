using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using static Google.Protobuf.WellKnownTypes.Field;

namespace GreenHouseMonitoringClient
{
    public class GetImg
    {
        public async Task<BitmapImage> GetImage(string name, string id)
        {
            BitmapImage bitmapImage;
            using (var client = new HttpClient())
            {
                var content = await client.GetByteArrayAsync("");
                //img.FromStream(content);
                MemoryStream stream = new MemoryStream(content);
                bitmapImage = new BitmapImage();
            }
            return bitmapImage;
        }
    }

}
