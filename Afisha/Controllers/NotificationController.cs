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
        static Timer timer;
        long interval = 30000; //30 секунд
        static object synclock = new object();
        static bool sent = false;
        public IActionResult SendNotificationEmail(int idEvent, string date, string username, string titleEvent)
        {
            //timer = new Timer(new TimerCallback(ss), null, 0, interval);
            //Console.WriteLine("sss");
            //void ss(Object obj)
            {
                lock (synclock)
                {
                    var datess = "2021-04-05 17:38:00.000";
                    var dates = DateTime.Parse(datess);
                    DateTime dd = DateTime.Today;
                    if (dd == dates.Date && sent == false)
                        try
                        {
                            MimeMessage message = new MimeMessage();
                            message.From.Add(new MailboxAddress("Моя компания", "admin@mycompany.com"));
                            message.To.Add(new MailboxAddress($"{username}"));
                            message.Subject = "Уведомление от Afisha!";
                            message.Body = new BodyBuilder() { HtmlBody = $"<h1 style=\"color: green;\">Доброго времени суток {username}! B {date} состоится концерт {titleEvent}, не забудьте." }.ToMessageBody(); //тело сообщения (так же в формате HTML)

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
                    else if (dd.Hour != 1 && dd.Minute != 30)
                    {
                        sent = false;
                    }
                }
            }
            return RedirectToAction("ReservationsTicket", "Reservations", new { date = date, idEvent = idEvent });

        }
        public void Ss(Object obj)
        {

        }
    }
}
