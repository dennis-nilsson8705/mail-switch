using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaChallApi.MailOperations;

namespace SpaChallApi.Controllers
{
    [Route("api/[controller]")]
    public class MailController : Controller
    {
        // POST api/mail
        [HttpPost]
        public void Post([FromBody]Email value)
        {
            MailClient client = new MailClient(MockEmail());
            client.SendEmail();      
        }

        public Email MockEmail(){

            return new Email {
                Sender = "tty1dev@gmail.com",
                Recipients = new List<string>{ "dennis87532@gmail.com" },
                CCList = new List<string>{ "dennis87532@gmail.com" },
                BccList = new List<string>{ "dennis87532@gmail.com" },
                Subject = "testsubject",
                Content = "testcontent"
            };
        }
    }
}