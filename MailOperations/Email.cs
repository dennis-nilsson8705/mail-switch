using System.Collections.Generic;

namespace SpaChallApi.MailOperations
{
public class Email 
{
    public List<string> Recipients;
    public List<string> CCList;
    public List<string> BccList;
    public string Subject { get; set;}
    public string Sender { get; set;}
    public string Content { get; set;}
}
}