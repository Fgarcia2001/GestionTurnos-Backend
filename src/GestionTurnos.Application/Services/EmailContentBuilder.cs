using GestionTurnos.Application.Abstraction;
using GestionTurnos.Application.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTurnos.Application.Services
{
    public class EmailContentBuilder : IEmailContentBuilder
    {
        public EmailMessage BuildExpiredEmail(string email, string businessName)
        {
            var EmailExpiredPlan = new EmailMessage
            {
                To = email,
                Subject = $"{businessName} - Subscription Expired",
                Body = "Your free plan has expired. Please renew your subscription to continue using our services."
            };

            return EmailExpiredPlan;
        }

        public EmailMessage BuildVencimientoEmail(string email, string businessName, int daysLeft)
        {
            var EmailExpiredPlan = new EmailMessage
            {
                To = email,
                Subject = $"{businessName} - Subscription Renewal Notice",
                Body = $"Your free plan will expire soon. Please renew your subscription. It expires in {daysLeft} days."
            };

            return EmailExpiredPlan;
        }
    }
}