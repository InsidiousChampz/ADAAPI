using CustomerProFileAPI.DTOs.Info;
using CustomerProFileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProFileAPI.Services.Info
{
    public interface IInfoService
    {
        Task<ServiceResponse<List<GetPersonalInfoDto>>> GetPersonalInfo();
        Task<ServiceResponse<GetPersonalInfoDto>> GetPersonalInfoById(int personId);
        Task<ServiceResponse<GetPersonalInfoDto>> UpdatePersonalInfoById(int personId, UpdatePersonalInfoDto updatePerson);
    }
}
