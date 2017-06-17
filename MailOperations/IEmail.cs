using System.Collections.Generic;

public interface IEmail {
     string Recipients { get; set;}
     string CCList { get; set;}
     string BccList { get; set;}
     string Subject { get; set;}
     string Sender { get; set;}
     string Content { get; set;}
 }