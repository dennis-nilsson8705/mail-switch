using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace SpaChallApi.MailOperations
{
public class MailClient{
private Email email;
private const string MailGunUrl = "https://api.mailgun.net/v3/";
private const string MailGunDomain = "sandbox8133ee969b5642bf9198bd09c4e2e5d4.mailgun.org/messages";
private const string MailGunApiKey = "api:key-2192450d9fb23c99b48be0e6cf5dec94";
private const string SendGridUrl = "https://api.sendgrid.com";
private const string SendGridDomain= "/api/mail.send.json";

 private const string SendGriApiKey = "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";

    public MailClient(Email email)
    {
        this.email = email;
    }

    //Attempt to send email using one host and attempt a second one if the first fails
    public string SendEmail(){
       //var response = SendSingleEmail(MailGunUrl, MailGunApiKey, MailGunDomain);
    //    if (response.IsSuccessStatusCode)
    //    { 
    //        return response.StatusCode.ToString();
    //    }
    //    else{
            var response = SendSingleEmail(SendGridUrl, SendGriApiKey, SendGridDomain);
      // }  
        if (response.IsSuccessStatusCode)
       { 
           return response.StatusCode.ToString();
       }  
       else {
           return "Error: " + response?.StatusCode;
       }
    }


    //Send an email
    public HttpResponseMessage SendSingleEmail(string url, string apiKey, string domain){
        
        HttpResponseMessage response;

        using (var client = new HttpClient()) {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey)));

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("from", email.Sender),
            new KeyValuePair<string, string>("to", email.Recipients.Aggregate( (x,y) => x + "," + y) ),
            new KeyValuePair<string, string>("subject", email.Subject),
            new KeyValuePair<string, string>("text", email.Content)
        });
            response = client.PostAsync(domain,content).Result;
         }
        
         return response;
    }
}
}