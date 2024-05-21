using Coravel;
using Coravel.Invocable;
using GraduationProjectAPI.BL.DTO;
using GraduationProjectAPI.BL.Services;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace GraduationProjectAPI.BL.BackgroindJob
{
    public class SendNotificationJob:BackgroundService
    {
        private readonly IServiceProvider provider;

      
       
        public SendNotificationJob(IServiceProvider provider)
        {

            this.provider = provider;
          
        }

      
      
        public async Task SendNotification()
        {
            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            int differenceInDays = 0;

            var data = db.Medicines.ToList();
            var count = 0; ;
            var outofstock = 0; ;
            var empty = 0;
            foreach (var item in data)
            {
                var date = DateTime.Now;
                var medicineExpDate = item.ExpirationDate;
                TimeSpan difference = medicineExpDate - date;
                differenceInDays = (int)difference.TotalDays +1;
                if ((differenceInDays <= 7 && differenceInDays >= 0))
                {

                    count++;
                }
                if (item.NumberInStock < 10 && item.NumberInStock != 0)
                {
                    outofstock++;
                }
                if (item.NumberInStock == 0)
                {
                    empty++;
                }


            }
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var pharmasits = db.Pharmacists.ToList();
            foreach (var item1 in pharmasits)
            {
                var user = await userManager.FindByEmailAsync(item1.Email);
                if(user!=null)
                {

              
                if (await userManager.IsInRoleAsync(user, "ADMIN"))
                {
                    var Message = "";
                    if (count > 0)
                    {
                        Message += $"there is {count} Medicines will be Expired After {differenceInDays} day ";
                    }
                    if (outofstock > 0)
                    {
                        Message += $"  {outofstock} Medicines will Be Out Of Stock Soon ";
                    }
                    if (empty > 0)
                    {
                        Message += $" Now Exist {empty} Medicines are out of Stock.";
                    }
                       if(Message != "")
                        {
                            MailSender.sendmail(item1.Email, Message);
                        }
                            
                        

                }
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                SendNotification();
                await Task.Delay(1440000, stoppingToken);
             

            }
            Console.WriteLine("finish");
        }
    }
}
