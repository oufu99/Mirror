using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SendLib
{
    public class TestSend
    {
        public static async void MyTest(ResvAlipayTradeNo alipayTradeNo)
        {
            try
            {
                var url = "http://10.0.31.31:18888//acct/updateCreditPayAcctToRefNo";
                var paramJson = JsonConvert.SerializeObject(alipayTradeNo);

                AlipayTransNumPMSRequest pmsRequest = new AlipayTransNumPMSRequest() { ResvId = alipayTradeNo.ResvId, RefNo = alipayTradeNo.AlipayTradeNo };
                ECService client = new ECService();
                await System.Threading.Tasks.Task.Run(() =>
                {
                    var res = client.HttpJsonPost<AlipayTransNumPMSResponse>(url, pmsRequest);

                    if (res.Code != 0)
                    {

                    }
                });
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public static T HttpJsonPost<T>(string url, object data = null)
        {
            var client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            try
            {
                var resText = client.UploadString(url, JsonConvert.SerializeObject(data));
                return JsonConvert.DeserializeObject<T>(resText);
            }
            catch (Exception ex)
            {

                return default(T);
            }



        }
    }

    public class ResvAlipayTradeNo
    {
        public string ResvId { get; set; }

        public string AlipayTradeNo { get; set; }

        public string OutOrderId { get; set; }

    }
    public class AlipayTransNumPMSResponse
    {
        public string RequestId { get; set; }
        public int Code { get; set; }
        public bool Item { get; set; }
        public string Message { get; set; }
    }

    public class AlipayTransNumPMSRequest
    {
        public string ResvId { get; set; }
        public string RefNo { get; set; }
    }

    public class ECService
    {
        public T HttpJsonPost<T>(string url, object data = null)
        {
            var client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            try
            {
                var resText = client.UploadString(url, JsonConvert.SerializeObject(data));
                return JsonConvert.DeserializeObject<T>(resText);
            }
            catch (Exception ex)
            {

                return default(T);
            }
        }
    }


}
