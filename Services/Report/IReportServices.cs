using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using SmsUpdateCustomer_Api.DTOs.Report;
using SmsUpdateCustomer_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsUpdateCustomer_Api.Services.Report
{
    public interface IReportServices
    {
        //UI10
        Task<byte[]> GetSMSSended(GetSMSDto datedata);
        //UI11
        Task<byte[]> GetFollowupCustomerReply(GetFollowupDto datedata);
        //UI12
        Task<byte[]> GetFollowupCustomerEdit(int branchId);
    }
}
