using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.Email
{
    public class SendGridResponse
    {
        public List<SendGridResponseError> Errors { get; set; }

    }
}
