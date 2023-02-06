using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseMonitoringClient
{
    class CovertToJSON
    {
        public ValueMember GetMember(string JSON)
        {
            var temp = JSON.Split('{');
            string res = "{" + temp[2].Remove(temp[2].Length - 1);
            ValueMember valueMember = new ValueMember();
            Value v = JsonConvert.DeserializeObject<Value>(res);
            valueMember.二氧化碳浓度 = int.Parse(v.二氧化碳浓度);
            valueMember.温度 = Convert.ToDouble(v.温度);
            valueMember.湿度 = Convert.ToDouble(v.湿度);
            valueMember.光线 = Convert.ToDouble(v.光线);
            return valueMember;
        }
        public WarnMsgMember GetMsgMember(string JSON)
        {
            WarnMsgMember valueMember = new WarnMsgMember();
            WarnMsgMember v = JsonConvert.DeserializeObject<WarnMsgMember>(JSON);
            valueMember.id = v.id;
            valueMember.name =v.name;
            valueMember.msg = v.msg;
            return valueMember;
        }
    }
    public class Value
    {
        public string 光线 { get; set; }
        public string 二氧化碳浓度 { get; set; }
        public string 温度 { get; set; }
        public string 湿度 { get; set; }
    }
}
