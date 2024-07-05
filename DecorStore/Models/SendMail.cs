using System.Net.Mail;
using System.Net;

namespace DecorStore.Models
{
    public class SendMail
    {
        public void MailSending(string email,string username1, string password1)
        {
            string smtpServer = "smtp.gmail.com";
            int port = 587;
            bool enableSSL = true;
            string userName = "nguyenthanhtuoi1230@gmail.com";
            string password = "gjiq rwwb vmos bltj";

            // Tạo đối tượng SmtpClient
            using (var client = new SmtpClient(smtpServer, port))
            {
                client.EnableSsl = enableSSL;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, password);

                // Cấu hình thông điệp email
                var from = new MailAddress("nguyenthanhtuoi1230@gmail.com", "DecorStore");
                var to = new MailAddress(email, "DecorStore");
                var message = new MailMessage(from, to);
                message.Subject = "Cảm ơn bạn đã đăng ký tài khoản";
                message.Body = "Username:"+username1+"\nPassword:"+ password1;

                try
                {
                    // Gửi email
                    client.Send(message);
                    Console.WriteLine("Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
        }
    }
}
