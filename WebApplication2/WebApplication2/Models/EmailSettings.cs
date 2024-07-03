namespace WebApplication2.Models
{
    public class EmailSettings
    {
        public string FromEmailAddress { get; set; }
        public string FromEmailDisplayName { get; set; }
        public string FromEmailPassword { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public bool EnabledSSL { get; set; }
        public string ToEmailAddress { get; set; }
    }
}
