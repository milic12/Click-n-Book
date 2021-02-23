using System.ComponentModel.DataAnnotations;

namespace Click_and_Book.Email
{
    public class SendEmailDetails
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(255)]
        public string ReplayToName { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [StringLength(255)]
        public string ReplayToEmail { get; set; }
        public string TemplateId { get; set; }
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        [Required(ErrorMessage = "Please enter Subject")]
        [StringLength(255)]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter Content")]
        public string Content { get; set; }
        public bool IsHTML { get; set; }
        public EmailTemplateData TemplateData { get; set; }
    }
}