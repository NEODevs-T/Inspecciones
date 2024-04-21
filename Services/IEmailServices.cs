using System;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using Inspecciones.Model;

namespace Inspecciones.Services
{
    public interface IEmailServices
    {
        Task SendEmailAsync(Model.Email request);

        Task<bool> ContruccionEmail(string personas,string cuerpo,string asunto);
    }
    public class EmailServices : IEmailServices
    {
        private readonly SmtpSettings _smtpSettings;

        //public EmailServices(SmtpSettings smtpSettings)
        //{
        //    _smtpSettings = smtpSettings;
        //}
        public async Task SendEmailAsync(Email request)
        {
            try
            {
                var messge = new MimeMessage();
                messge.From.Add(new MailboxAddress("MejoraContinua@paveca.com.ve", "MejoraContinua@paveca.com.ve"));
                foreach (var item in request.email)
                {
                    messge.To.Add(new MailboxAddress("", item));
                }

                foreach (var item in request.cc)
                {
                    messge.Cc.Add(new MailboxAddress("", item));
                }

                

                messge.Subject = request.subject;
                messge.Body = new TextPart(TextFormat.Html) { Text = request.body };
                using(var client = new SmtpClient()){
                    await client.ConnectAsync("AZTDSIGO1.papeleslatinos.com", 25, SecureSocketOptions.Auto);
                    // AZTDSIGO1.papeleslatinos.com 
                    // AZTDSIGO1.papeleslatinos.com 
                    // await client.AuthenticateAsync("user@paveca.com.ve", "password");
                    await client.SendAsync(messge);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> ContruccionEmail(string personas,string cuerpo,string asunto)
        {
            Email mail = new Email();
            mail.email = new List<string>();
            mail.cc = new List<string>();
            mail.subject = asunto;
            mail.body = cuerpo;

            try
            {
                String[] usuarios = personas.Split(";");
                for (var i = 0; i < usuarios.Count(); i++)
                {
                    mail.email.Add(usuarios[i] + "@paveca.com.ve");
                }
                
                mail.cc.Add("gabriel.arcila@paveca.com.ve");

                if (mail.email[0] != "@paveca.com.ve")
                {
                    await SendEmailAsync(mail);
                    return true;
                }
                else
                {   
                    return false;
                }
            }catch{
                return false;
            }
        }
    }
}
