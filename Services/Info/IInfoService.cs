using INFOEDITORAPI.DTOs.Info;
using INFOEDITORAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFOEDITORAPI.Services.Info
{
    public interface IInfoService
    {
        Task<ServiceResponse<List<GetPersonalInfoDto>>> GetPersonalInfo();
        Task<ServiceResponse<GetPersonalInfoDto>> GetPersonalInfoById(int personId);
        Task<ServiceResponse<GetPersonalInfoDto>> UpdatePersonalInfoById(int personId, UpdatePersonalInfoDto updatePerson);
    }
}
