using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SpaChallApi.MailOperations;

namespace SpaChallApi.Controllers
{
    [Route("api/[controller]")]
    public class MailController : Controller
    {
        private readonly IOptions<EmailConfig>  _emailConfig;
        
        public MailController(IOptions<EmailConfig> emailConfig){
            this._emailConfig = emailConfig;
        }

        // POST api/mail
        [HttpPost]
        public void Post([FromBody]Email value)
        {            
            MailClient client = new MailClient(value, this._emailConfig);
             client.SendEmail();      
        }
    }
}