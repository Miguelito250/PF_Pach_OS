namespace PF_Pach_OS.Services
{
    public class AuthMessageSenderOptions
    {
        public string? SendGridKey { get; set; }

        public string ObtenerApiKey()
        {
            var sendGridApiKey = Environment.GetEnvironmentVariable("API_SEND_GRID");
            this.SendGridKey = sendGridApiKey;
            return sendGridApiKey;
        }
    }
}