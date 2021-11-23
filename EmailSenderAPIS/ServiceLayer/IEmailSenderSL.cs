using EmailSenderAPIS.CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSenderAPIS.ServiceLayer
{
    public interface IEmailSenderSL
    {
        public SenderEmailResponse CheckEmailValidation(SenderEmailRequest request);

    }
}
