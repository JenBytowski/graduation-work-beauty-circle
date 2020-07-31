namespace BC.API.Services.SMSService
{
    public class SMS
    {
        public string Phone { get; set; }

        public string Text { get; set; }

        public string Sender { get; set; }

        public int TimeStamp { get; set; }

        public bool Priority { get; set; }
    }
}