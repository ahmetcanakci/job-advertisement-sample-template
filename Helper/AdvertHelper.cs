using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Job.Advertisement.Service.Entities;
using Job.Advertisement.Service.Repositories;

namespace Job.Advertisement.Service.Helper
{
    public static class AdvertHelper
    {

        public static bool HasAdvertDescriptionBannedWords(string advertDescription)
        {
            var swearWords = File.ReadAllLines("swears.txt").ToList();
            return swearWords.Contains(advertDescription.ToLower(CultureInfo.InvariantCulture));

        }


        public static int CalculateAdvertQuality(Advert item)
        {
            var advertQuality = 0;

            if (item.WorkType > 0)
                advertQuality += 1;

            if (item.Salary > 0)
                advertQuality += 1;

            if (!string.IsNullOrEmpty(item.SideBenefits))
                advertQuality += 1;

            if (!HasAdvertDescriptionBannedWords(item.AdvertDescription))
                advertQuality += 2;

            return advertQuality;
        }


    }

}