using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.Email
{
    public class SendEmailResponse
    {
        public bool Successful => !(Errors?.Count > 0) ;
        public List<string> Errors { get; set; }
    }
}
