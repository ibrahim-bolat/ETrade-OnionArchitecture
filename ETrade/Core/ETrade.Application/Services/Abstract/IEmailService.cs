using ETrade.Application.Model;

namespace ETrade.Application.Services.Abstract;

public interface IEmailService
{
    bool SendEmail(MailRequest mailRequest);
}