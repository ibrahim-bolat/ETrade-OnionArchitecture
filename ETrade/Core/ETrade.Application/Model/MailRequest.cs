namespace  ETrade.Application.Model;

public class MailRequest
{
    public string ToMail { get; set; }
    public string DisplayName { get; set; }
    public string ConfirmationLink { get; set; }
    public string MailSubject { get; set; }
    public bool IsBodyHtml { get; set; }
    public string MailLinkTitle { get; set; }
    
}