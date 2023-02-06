using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Composition;
using Windows.UI.Xaml.Hosting;
using Microsoft.Toolkit.Uwp;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using Windows.UI.Xaml.Shapes;
using System.Net.Http;
using Windows.UI.Xaml.Media.Imaging;
using Newtonsoft.Json;


// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace GreenHouseMonitoringClient
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static string pageDevUID { get; set; }
        private static string process;
        private static RandomViewModel model;
        public string x { get; set; }
        public MainPage()
        {
            this.InitializeComponent(); LoadingPage();
            pageDevUID = "21345";
        }
        private async void LoadingPage()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                LoadingControl.IsLoading = true;
                InitPage();
                InitDevList();
            });
        }
        private async void InitDevList()
        {
            updateProcessText("Get device list");
            HttpClient client = new HttpClient();
            string sURL = "http://localhost:5043/api/devices?name=leiyulin";
            string res = "";
            HttpResponseMessage response = await client.GetAsync(sURL);
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsStringAsync();
            }
            getDeviceFindo(res);
            updateProcessText(res);
        }
        private async void InitPage()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                DeviceCounterMember deviceCounterMember = new DeviceCounterMember();
                /*DeviceCounter deviceCounter = new DeviceCounter();
                deviceCounterMember = deviceCounter.GetOnlineDevice();
                DeviceCounterGrid.DataContext = deviceCounterMember;*/
                deviceCounterMember.onlineDevice = 3;
                deviceCounterMember.TotalDevice = 5;
                deviceCounterMember.offineDevice = 2;
                deviceCounterMember.pb = "60%";
                DeviceCounterGrid.DataContext = deviceCounterMember;
                LoadingControl.IsLoading = false;
            });
        }
        private async void UpdateServerCon()
        {
            FlushServerConn flushServerConn = new FlushServerConn();
            bool huabei = await flushServerConn.FlushAsync("114.114.114.114");
            bool huanan = await flushServerConn.FlushAsync("8.8.8.8");
            bool huadong = await flushServerConn.FlushAsync("8.8.4.4");
            if (huabei == true)
            {
                huabeiico.Glyph = "\uE774"; huabeiico.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Green);
            }//Glyph="&#xf384;" Foreground="Red"
            else
            {
                huabeiico.Glyph = "\uF384"; huabeiico.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }
            if (huanan == true)
            {
                huananico.Glyph = "\uE774"; huananico.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Green);
            }
            else
            {
                huananico.Glyph = "\uF384"; huananico.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }
            if (huadong == true)
            {
                huadongico.Glyph = "\uE774"; huadongico.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Green);
            }
            else
            {
                huadongico.Glyph = "\uF384"; huadongico.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }
        }
        private void UpdateConn(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UpdateServerCon();
        }
        public async void updateProcessText(string msg)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                process = DateTime.Now.ToString() + msg + "\r\n" + process;
                processdata.DataContext = process;
            });
        }//更新操作提示框
        public async void getDeviceFindo(string list) //加载在册设备列表
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var c=list.Split('+');
                foreach (var item in c)
                {
                    var temp=item.Split(',');
                    userControl userControl = new userControl(Uid: temp[1], x: temp[0]);
                    devicelist.Items.Add(userControl);
                }
            });
                /* var result = JObject.Parse(x);
                 await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
              {
                  foreach (JToken child in result.Children())
                  {
                      foreach (JToken grandChild in child)
                      {
                          foreach (JToken grandGrandChild in grandChild)
                          {
                              var property = grandGrandChild as JProperty;
                              if (property != null)
                              {
                                  if (property.Name != "null")
                                  {
                                      userControl userControl = new userControl(property.Value.ToString(), property.Name);
                                      //userControl.getUID = GetUID;
                                      devicelist.Items.Add(userControl);
                                  }
                              }
                          }
                      }
                  }
              });*/
            }
        private async void UpdateBaseInfo(object sender, ItemClickEventArgs e)
        {
            updateProcessText("Update device information");
            LoadingControl.IsLoading = true;
            //userControl user = devicelist.SelectedItem as userControl;
            //pageDevUID = user.Uid;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                GetDeviceBasicInfo getDevice = new GetDeviceBasicInfo();
                var c = getDevice.GetMember(pageDevUID).Result;
                DeviceMember v = JsonConvert.DeserializeObject<DeviceMember>(c);
                ContentArea.DataContext = v;
            });
            LoadingControl.IsLoading = false;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string a = e.AddedItems[0].ToString();
            GetPlantsValue getPlants = new GetPlantsValue();
            ValueMember valueMembers = getPlants.GetValue(a);
            ConfigGrid.DataContext = valueMembers;
        }
        private void OnLoaded()
        {
            if (pageDevUID != "")
            {
                model = this.Resources["ViewModel"] as RandomViewModel;
                model.setuid(pageDevUID);
                updateProcessText(" 开始监控");
                model.StartTimer();
            }
            else
            {
                
            }
        }

        private void OnUnLoaded()
        {
            RandomViewModel model = this.Resources["ViewModel"] as RandomViewModel; model.StopTimer();
            updateProcessText(" 停止监控");
        }

        private void toggleSwitch1_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    OnLoaded();
                    MQTTService.SetDevice(pageDevUID);
                    MQTTService.Start();
                    MQTTService.getNewData = GetNewData;
                    MQTTService.getMsg = GetMsg;
                }
                else
                {
                    OnUnLoaded();
                    MQTTService.Stop();
                }
            }
        }
        private async void GetMsg(WarnMsgMember msgMember)
        {
            try
            {
                /* model.UpdateValueMember(valueMember);
                 await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                 {
                     DeviceInfoView.DataContext = valueMember;
                 });*/
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    msgListControl msg = new msgListControl(id: msgMember.id, msg: msgMember.msg);
                    msgList.Items.Add(msg);
                });
            }
            catch
            {

            }
        }
        private async void GetNewData(ValueMember valueMember)
        {
            try
            {
                model.UpdateValueMember(valueMember);
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    DeviceInfoView.DataContext = valueMember;
                });
            }
            catch
            {

            }
        }

        private async void CheckWarningMsg(object sender, ItemClickEventArgs e)
        {
            msgListControl msgListControl = msgList.SelectedItem as msgListControl;
            BitmapImage bmp = new BitmapImage();
            WarningDetail warningDetail = new WarningDetail(id: msgListControl.id_num,msg: msgListControl.msg_num,img:bmp);
            var dialog = new ContentDialog()
            {
                Title = "MessageDialog",
                Content =warningDetail,
                PrimaryButtonText = "Confirmed",
                SecondaryButtonText = "Cancel",
                FullSizeDesired = false,
            };
            dialog.PrimaryButtonClick += (_s, _e) => {
                msgList.Items.Remove(msgListControl);
            };
            await dialog.ShowAsync();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //ADD NEW DEVICE
            string dev = dev_id.Text;
            string name = dev_naem.Text;
            string key = dev_key.Text;
            if (dev != "" && name != "" && key != "")
            {
                string content = "{" + dev + "+" + name + "+" + key + "}";
                PostDevInfo postDevInfo = new PostDevInfo();
                var res = postDevInfo.post(content);
                var dialog = new ContentDialog()
                {
                    Title = "MessageDialog",
                    Content = res,
                    PrimaryButtonText = "Confirmed",
                    SecondaryButtonText = "Cancel",
                    FullSizeDesired = false,
                };
                await dialog.ShowAsync();
            }
            else
            {
                var dialog = new ContentDialog()
                {
                    Title = "ErrorDialog",
                    Content = "Device info cannot be empty",
                    PrimaryButtonText = "Confirmed",
                    SecondaryButtonText = "Cancel",
                    FullSizeDesired = false,
                };
                await dialog.ShowAsync();
            }
        }
    }
}
