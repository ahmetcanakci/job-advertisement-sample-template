using System;
using System.Collections;

namespace Job.Advertisement.Service.Entities
{
    public class Employer
    {

        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public int AdvertCount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}

