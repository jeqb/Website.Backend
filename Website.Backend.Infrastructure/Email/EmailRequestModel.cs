﻿namespace Website.Backend.Infrastructure.Email
{
    public class EmailRequestModel
    {
        public string ToEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string HtmlBody { get; set; } = string.Empty;
    }
}
