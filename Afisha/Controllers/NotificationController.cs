using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Afisha.Controllers
{
    public class NotificationController : BaseController
    {
        public NotificationController(ILogger<HomeController> logger, AfishaContext _db) : base(logger, _db)
        { }

        public IActionResult SendNotificationEmail(int Id, string date, string username, string titleEvent)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Моя компания", "admin@mycompany.com"));
                message.To.Add(new MailboxAddress($"{username}"));
                message.Subject = "Уведомление от Afisha!";
                message.Body = new BodyBuilder() { HtmlBody = $"<h1 style=\"color: green;\">Good day {username}! On {date} a concert will take place {titleEvent}, do not forget!" }.ToMessageBody(); //тело сообщения (так же в формате HTML)

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("AfishaBishkek@gmail.com", "Afisha01");
                    client.Send(message);

                    client.Disconnect(true);
                    TempData["SuccessNotification"] = $"Notification sent!";

                }
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("ReservationsTicket", "Reservations", new { date = date, Id = Id });
        }
    }
}

