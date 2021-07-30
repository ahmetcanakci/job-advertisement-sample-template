using System;
using System.ComponentModel.DataAnnotations;
using Job.Advertisement.Service.Enums;

namespace Job.Advertisement.Service.Dtos
{
    public record AdvertDto(Guid Id, Guid FirmId, string Position, string AdvertDescription, DateTime AirTime, int AdvertQuality, string SideBenefits, WorkType WorkType, decimal Salary);

    public record CreateAdvertDto([Required] Guid FirmId, [Required] string Position, [Required] string AdvertDescription, string SideBenefits, WorkType WorkType, [Range(0, Int32.MaxValue)] decimal Salary);

    public record UpdateAdvertDto([Required] string Position, [Required] string AdvertDescription, string SideBenefits, WorkType WorkType, [Range(0, Int32.MaxValue)] decimal Salary);


    public record EmployerDto(Guid Id, string PhoneNumber, string Address, int AdvertCount, DateTime CreateDate, DateTime ModifiedDate);

    public record CreateEmployerDto([Required] string PhoneNumber, [Required] string Address, [Required] int AdvertCount = 2);

    public record UpdateEmployerDto([Required] string PhoneNumber, [Required] string Address, [Required] int AdvertCount = 2);

}