using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSenderAPIS.CommonLayer.Model
{
    public class SenderEmailRequest
    {

        public string EmailId { get; set; }

        public string Subject { get; set; }

        public string body { get; set; }
    }

    public class SenderEmailResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
