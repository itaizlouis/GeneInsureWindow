﻿using Insurance.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Gene
{
    public class SmsService
    {



        public async void SendSMS(string numberTO, string body)
        {

            // return;
            string resendSms = WebConfigurationManager.AppSettings["resendSms"];
            if (resendSms == "false")
            {
                return ;
            }

            using (var client = new HttpClient())
            {
                string username = System.Configuration.ConfigurationManager.AppSettings["smsGatewayUsername"].ToString();

                // Webservices token for above Webservice username
                string token = System.Configuration.ConfigurationManager.AppSettings["smsGatewayToken"].ToString();

                // BulkSMS Webservices URL
                string bulksms_ws = "http://portal.bulksmsweb.com/index.php?app=ws";

                // destination numbers, comma seperated or use #groupcode for sending to group
                // $destinations = '#devteam,263071077072,26370229338';
                // $destinations = '26300123123123,26300456456456';  for multiple recipients

                string destinations = numberTO;

                // SMS Message to send
                string message = body;

                // send via BulkSMS HTTP API

                string ws_str = bulksms_ws + "&u=" + username + "&h=" + token + "&op=pv";
                ws_str += "&to=" + Uri.EscapeDataString(destinations) + "&msg=" + Uri.EscapeDataString(message);

                HttpResponseMessage response = await client.GetAsync(ws_str);

                response.EnsureSuccessStatusCode();
                string responseBody = "";
                using (HttpContent content = response.Content)
                {
                    responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody + "........");
                }

           

            }
        }



    



    }
}
