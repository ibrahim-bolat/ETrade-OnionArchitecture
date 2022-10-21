using ETrade.Application.Model;

namespace ETrade.Application.Services;

public interface IEmailService
{
    bool SendEmail(MailRequest mailRequest);
}