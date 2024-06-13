namespace Inspecciones.Models
{
    public class Email
    {
        public List<string> email { get; set; }
        public List<string> cc { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

    }

    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
