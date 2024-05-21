using GraduationProjectAPI.BL.VM;
using Microsoft.Identity.Client;
using MimeKit;

namespace GraduationProjectAPI.BL.Services
{
    public class PrescrptionSender
    {
        public static async Task<bool> SendPrescription(int id,string emaildata,string DoctorName,string url)
        {
            
            try
            {
                var email = new MimeMessage()
                {
                    Sender = MailboxAddress.Parse("atiffahmykhamis@gmail.com"),
                    Subject = "Message from Pharma PRO "


                };
                email.To.Add(MailboxAddress.Parse(emaildata)); ;
                var builder = new BodyBuilder();


                builder.HtmlBody = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Digital Prescription</title>
    <link href=""https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&display=swap"" rel=""stylesheet"">
    <style>
        body {{
            font-family: 'Roboto', sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }}

        .email-content {{
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }}

        
    `   .logo {{
            max-width: 100px; /* Adjust the size of your logo as needed */
            display: block;
            margin: 0 auto 20px auto; /* Center the logo */
            color:#263b7e;
        }}
        .text-content {{
            text-align: left;
        }}
        .address {{
            text-align: center;
            margin-bottom: 20px;
        }}
        .text-content p {{
            color: #666;
            font-size: 16px;
            line-height: 1.6;
            margin: 0;
        }}

        .text-content a {{
            display: inline-block;
            margin-top: 20px;
            padding: 10px 20px;
            color: #fff;
            background-color: #4a90e2;
            text-decoration: none;
            border-radius: 5px;
            font-size: 16px;
            transition: background-color 0.3s ease;
        }}

        .text-content a:hover {{
            background-color: #357ae8;
        }}
    </style>
</head>
<body>
    <div class=""email-content"">
        <h1 class=""logo"">Pharma <span class=""classp"">P</span>ro</h1>
        <div class='text-content'>
            <p>Dear {DoctorName},</p>
            <p>Your digital prescription is ready. Please follow the instructions carefully and feel free to reach out if you have any questions.</p>
        <a href={url + "?id=" + id} target=""_blank"">View Prescription</a>
        </div>
    </div>
</body>
</html>
";
               email.Body = builder.ToMessageBody();
               

                email.From.Add(new MailboxAddress("Pharma PRO", "atiffahmykhamis@gmail.com"));



                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    smtp.Authenticate("atiffahmykhamis@gmail.com", "fmnjnhsdhmrugigq");
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
