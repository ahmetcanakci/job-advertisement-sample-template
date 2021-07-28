using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Job.Advertisement.Service.Dtos;
using Job.Advertisement.Service.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Job.Advertisement.Service
{
    public static class Extensions
    {
        public static AdvertDto AsDto(this Advert item)
        {
            return new AdvertDto(item.Id, item.FirmId, item.Position, item.AdvertDescription, item.Airtime, item.AdvertQuality, item.SideBenefits, item.WorkType, item.Salary);
        }


        public static EmployerDto AsEmployerDto(this Employer item)
        {
            return new EmployerDto(item.Id, item.PhoneNumber, item.Address, item.AdvertCount, item.CreateDate, item.ModifiedDate);
        }
    }
}