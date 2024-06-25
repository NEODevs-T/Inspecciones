using System;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using Inspecciones.Models;

namespace Inspecciones.Services
{
    public interface IEmailServices
    {
        Task SendEmailAsync(Email request);

        Task<bool> ContruccionEmail(string personas,string cuerpo,string asunto);

        string ConstruccionCuerpo(Inspeccion inspeccion,List<InspecDatum> listData);
    }
    public class EmailServices : IEmailServices
    {
        private readonly SmtpSettings _smtpSettings;
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
                
                //mail.cc.Add("gabriel.arcila@paveca.com.ve");

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

        public string ConstruccionCuerpo(Inspeccion inspeccion,List<InspecDatum> listData)
        {
            List<InspecDatum> listDataCopi = listData.Where( l => l.Iddata == 0).ToList();

            if(listDataCopi.Count == 0){
                return "Ningun defecto";
            }
            
            string cuerpo = @"
                    <div style='height: 99vh;'>div
                    <table style='width: 100%;' class='tableImpresion'>
                        <tr class='encabezadoTableImpresion'>
                            <th>#</th>
                            <th>Pregunta</th>
                            <th>Defectuoso</th>
                            <th>Observación</th>
                        </tr>       
            ";

            foreach (var data in listDataCopi){
                cuerpo += @$"
                    <tr>
                        <td>{listDataCopi.IndexOf(data)}</td>
                        <td>{data.IdMaqPreNavigation.IdPreguntaNavigation.Pdescri}</td>
                        <td>Defectuoso</td>
                        <td>{data.Idobserv}</td>
                    </tr>
            
                ";
            }

            cuerpo += @"
                    </table>
                    </div>

                    <style>
                        .tableImpresion tr{
                            text-align: center;
                        }

                        .encabezadoTableImpresion{
                            color: white;
                            background-color: rgb(0, 120, 83);
                            font-size: 1.1em;
                            font-weight: 800;
                            width: 14%;
                            text-align: center;
                        }

                        .encabezadoTableImpresion th{
                            padding: 6px;
                            border: solid 1px black;
                        }

                        .tableImpresion tr td{

                            border: solid 0.5px black;
                            height: 40px;
                        }

                        .separadorTablaImpresion{
                            border: #000 3px solid;
                            height:10%;
                            display: flex;
                            justify-content: end;
                            align-items: end;
                        }
                    </style>
            ";
            return cuerpo;
        }
    }
}
