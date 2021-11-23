using EmailSenderAPIS.CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSenderAPIS.RepositoryLayer
{
    public interface IEmailSenderRL
    {
        public SenderEmailResponse CheckEmailValidation(SenderEmailRequest request);
    }
}
