﻿using System;
using System.Collections.Generic;
using System.Linq;
using Pcf.GivingToCustomer.Core.Domain;

namespace Pcf.GivingToCustomer.WebHost.Models
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Preferences { get; set; }
        //public List<PreferenceResponse> Preferences { get; set; }
        public List<PromoCodeShortResponse> PromoCodes { get; set; }

        public CustomerResponse()
        {
            
        }

        public CustomerResponse(Customer customer, IEnumerable<PromoCode> promoCodes)
        {

            Id = customer.Id;
            Email = customer.Email;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Preferences = customer.Preferences;
            PromoCodes = promoCodes.Select(x => new PromoCodeShortResponse()
            {
                Id = x.Id,
                Code = x.Code,
                BeginDate = x.BeginDate.ToString("yyyy-MM-dd"),
                EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                PartnerId = x.PartnerId,
                ServiceInfo = x.ServiceInfo
            }).ToList();

            /*
            Id = customer.Id;
            Email = customer.Email;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Preferences = customer.Preferences.Select(x => new PreferenceResponse()
            {
                Id = x.PreferenceId,
                Name = x.Preference.Name
            }).ToList();
            PromoCodes = customer.PromoCodes.Select(x => new PromoCodeShortResponse()
            {
                Id = x.PromoCode.Id,
                Code = x.PromoCode.Code,
                BeginDate = x.PromoCode.BeginDate.ToString("yyyy-MM-dd"),
                EndDate = x.PromoCode.EndDate.ToString("yyyy-MM-dd"),
                PartnerId = x.PromoCode.PartnerId,
                ServiceInfo = x.PromoCode.ServiceInfo
            }).ToList();    */
        }
    }
}