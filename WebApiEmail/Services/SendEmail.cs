using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using System;
using WebApiEmail.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.IO;
using System.Linq;

namespace WebApiEmail.Services
{
    public class SendEmail : ISendEmail
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IWebHostEnvironment _env;

        public SendEmail(IOptions<SmtpSettings> smtpSettings, IWebHostEnvironment env)
        {
            _smtpSettings = smtpSettings.Value;
            _env = env;
        }

        //public async Task SendEmailAsync(string email, string subject, string body)
        //{
        //    try
        //    {
        //        var message = new MimeMessage();

        //        message.From.Add(new MailboxAddress(_smtpSettings.SenderName,
        //                                            _smtpSettings.SenderEmail));
        //        message.To.Add(new MailboxAddress("destino", email));
        //        message.Subject = subject;
        //        message.Body = new TextPart("html")
        //        {
        //            Text = body
        //        };

        //        //using (var client = new SmtpClient())
        //        //{
        //        //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

        //        //    if (_env.IsDevelopment())
        //        //    {
        //        //        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
        //        //    }
        //        //    else
        //        //    {
        //        //        await client.ConnectAsync(_smtpSettings.Server);
        //        //    }

        //        //    await client.AuthenticateAsync(_smtpSettings.Username,
        //        //                                   _smtpSettings.Password);
        //        //    await client.SendAsync(message);
        //        //    await client.DisconnectAsync(true);
        //        //}

        //        using (var client = new SmtpClient())
        //        {
        //            await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
        //            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        //            await client.SendAsync(message);
        //            await client.DisconnectAsync(true);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new InvalidOperationException(e.Message);
        //    }
        //}

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                //Obtem o arquivo pdf mais recente dentro da pasta
                string directoryPath = _smtpSettings.PdfFilePath;

                string pdfFile = new DirectoryInfo(directoryPath).GetFiles().OrderByDescending(x => x.LastWriteTime).FirstOrDefault()?.FullName;

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("destino", email));
                message.Subject = subject;

                // Crie a parte do corpo do e-mail
                var bodyPart = new TextPart("html")
                {
                    Text = body
                };

                // Crie a parte do anexo

                if (pdfFile != null)
                {
                    var attachment = new MimePart("application", "pdf")
                    {
                        Content = new MimeContent(File.OpenRead(pdfFile)),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(pdfFile)
                    };

                    // Combine o corpo e o anexo em uma única mensagem
                    var multipart = new Multipart("mixed");
                    multipart.Add(bodyPart);
                    multipart.Add(attachment);

                    message.Body = multipart;
                }
                else
                {
                    message.Body = bodyPart;
                }

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
