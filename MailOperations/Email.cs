using System.Collections.Generic;

namespace SpaChallApi.MailOperations
{
public class Email : IEmail
{
    public string Recipients { get; set;}
    public string CCList { get; set;}
    public string BccList { get; set;}
    public string Subject { get; set;}
    public string Sender { get; set;}
    public string Content { get; set;}
}
}