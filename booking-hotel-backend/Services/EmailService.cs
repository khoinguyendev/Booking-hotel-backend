
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using booking_hotel_backend.Services.Interfaces;   
namespace booking_hotel_backend.Services;

using booking_hotel_backend.Configurations;
public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendOtpAsync(string email, string otp)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(
            _settings.DisplayName,
            _settings.Email));

        message.To.Add(MailboxAddress.Parse(email));

        message.Subject = "Xác thực Email";

        message.Body = new TextPart("html")
        {
            Text = $@"
                <h2>Booking Hotel</h2>

                <p>Mã OTP của bạn là:</p>

                <h1>{otp}</h1>

                <p>OTP có hiệu lực trong 5 phút.</p>
            "
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            _settings.Host,
            _settings.Port,
            SecureSocketOptions.SslOnConnect);

        await smtp.AuthenticateAsync(
            _settings.Email,
            _settings.Password);

        await smtp.SendAsync(message);

        await smtp.DisconnectAsync(true);
    }
}