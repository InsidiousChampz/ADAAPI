using ADAAPI.DTOs.Admin;

using ADAAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADAAPI.Services.Admin
{
    public interface IAdminServices
    {
        Task<ServiceResponse<List<GetTableDTO>>> GetAllTableA();
        Task<ServiceResponse<List<GetTableDTO>>> GetAllTableB();
        Task<ServiceResponse<List<GetTableDTO>>> GetAllTwoTable();

        Task<ServiceResponse<GetTableDTO>> SaveTableA(AddTableDto payload);
        Task<ServiceResponse<GetTableDTO>> SaveTableB(AddTableDto payload);
        Task<ServiceResponse<GetTableDTO>> SaveAllTable(AddTableDto payload);

        Task<ServiceResponse<GetTableDTO>> UpdatetableA(string empId, UpdateTableDTO payload);
        Task<ServiceResponse<GetTableDTO>> UpdatetableB(string empId, UpdateTableDTO payload);
        Task<ServiceResponse<GetTableDTO>> UpdateAlltable(string empId, UpdateTableDTO payload);


        Task<ServiceResponse<GetTableDTO>> DeleteAlltable(string empId);


    }
}
