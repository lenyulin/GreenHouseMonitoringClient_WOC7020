using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace GreenHouseMonitoringClient
{
    public sealed partial class msgListControl : UserControl
    {
        public string id_num { get; set; }
        public string msg_num { get; set; }
        public msgListControl(string id, string msg)
        {
            this.InitializeComponent();
            this.id_num = id;
            this.msg_num = msg;
            this.msg.Text = msg;
            this.id.Text = id;
        }
    }
}
