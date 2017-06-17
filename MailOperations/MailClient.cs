using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace SpaChallApi.MailOperations
{
public class MailClient{
        
    private IEmail email;
    private readonly IOptions<EmailConfig>  _emailConfig;

    public MailClient(IEmail email, IOptions<EmailConfig>  emailConfig)
    {    
        this.email = email;
        this._emailConfig = emailConfig;
    }

    //Attempt to send email using one host and attempt another host if the first failed
    public void SendEmail(){

       var response = SendSingleEmail(this._emailConfig.Value.MailGunUrl, this._emailConfig.Value.MailGunApiKey, 
            this._emailConfig.Value.MailGunDomain);

       if (!response.IsSuccessStatusCode)
        { 
            response = SendSingleEmail(this._emailConfig.Value.MailjetURL,
                string.Format("{0}:{1}", this._emailConfig.Value.MailJetApiKey, this._emailConfig.Value.MailJetSecretKey));
        }  
    }

    //Send an email 
    public HttpResponseMessage SendSingleEmail(string url, string authInfo, string domain = null)   {

        HttpResponseMessage response;
 
        using (var client = new HttpClient()) {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Basic", 
                Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo)));
            response = client.PostAsync(domain ?? url ,EmailContentCreator(email)).Result;
         }

         return response;
    }

    //Created FormUrlEncodedContent from incoming email object
    public FormUrlEncodedContent EmailContentCreator(IEmail email){
 
        return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", email.Sender),
                new KeyValuePair<string, string>("to", email.Recipients ),
                new KeyValuePair<string, string>("bcc", email.BccList),
                new KeyValuePair<string, string>("cc", email.CCList ),
                new KeyValuePair<string, string>("subject", email.Subject),
                new KeyValuePair<string, string>("text", email.Content)
            });
    }
}
}