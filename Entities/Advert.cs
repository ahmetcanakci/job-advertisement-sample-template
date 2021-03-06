using System;
using System.Collections;
using Job.Advertisement.Service.Enums;

namespace Job.Advertisement.Service.Entities
{
    public class Advert
    {

        public Guid Id { get; set; }

        public Guid FirmId { get; set; }
        public string Position { get; set; }

        public string AdvertDescription { get; set; }

        public DateTime Airtime { get; set; }

        public int AdvertQuality { get; set; }

        public string SideBenefits { get; set; }

        public WorkType WorkType { get; set; }

        public decimal Salary { get; set; }


    }
}

