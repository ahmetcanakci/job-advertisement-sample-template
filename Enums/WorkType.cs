using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Job.Advertisement.Service.Enums
{

    [Flags]
    public enum WorkType : int
    {

        [Display(Description = "Full time")]
        Fulltime = 1,

        [Display(Description = "Part time")]
        PartType = 2
    }

}