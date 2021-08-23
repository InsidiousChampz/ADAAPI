using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmsUpdateCustomer_Api.DTOs.Report;
using SmsUpdateCustomer_Api.Models;
using System;
using System.Collections.Generic;
using SmsUpdateCustomer_Api.Data;
using SmsUpdateCustomer_Api.DTOs.Admin;
using SmsUpdateCustomer_Api.DTOs.Customer;
using SmsUpdateCustomer_Api.DTOs.Customer_Profiles;
using SmsUpdateCustomer_Api.Helpers;
using SmsUpdateCustomer_Api.Models.Admin;
using SmsUpdateCustomer_Api.Models.Customer_Profiles;
using SmsUpdateCustomer_Api.Validations;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.ComponentModel;

namespace SmsUpdateCustomer_Api.Services.Report
{
    public class ReportServices : ServiceBase, IReportServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<IReportServices> _log;
        private readonly IHttpContextAccessor _httpcontext;
        public ReportServices(AppDBContext dBContext, IMapper mapper, ILogger<IReportServices> log, IHttpContextAccessor httpcontext) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
        }

        public async Task<byte[]> GetFollowupCustomerEdit(int branchId)
        {
            try
            {
                // wait join tmp.application for organize data
                var payer = (from o in _dbContext.Customer_FollowUps.AsNoTracking()
                             join x in _dbContext.Policy_Snapshots.AsNoTracking()
                             on o.PersonId equals x.PayerPersonId
                             select new GetFollowupEditDataDto
                             {
                                 PersonId = o.PersonId,
                                 PayerName = o.PayerFirstName + ' ' + o.PayerLastName,
                                 OrganizeName = o.OrganizeName,
                                 District = o.District,
                                 Province = o.Province,
                                 Area = o.Area,
                                 BranchId = o.BranchId,
                                 Branch = o.Branch,
                                 AgentId = o.AgentId,
                                 AgentName = o.AgentName,
                                 AppId = o.AppID,
                                 PrimaryPhone = o.PrimaryPhone,
                                 CustomerConfirm = o.CustomerConfirm,
                             }).AsQueryable();


                if (payer == null)
                {
                    return null;
                }

                if (branchId != 0 )
                {
                    payer = payer.Where(x => x.BranchId == branchId);
                }


                List<GetFollowupEditDataDto> dataDto = new List<GetFollowupEditDataDto>();
                dataDto.AddRange(payer);

                await Task.Delay(0);

                if (dataDto.Count == 0)
                {
                    return null;
                }

                using (var workbook = new XLWorkbook())
                {

                    DataTable table = new DataTable();
                    table = ToDataTable(dataDto);

                    bool retChange = ChangeColumnToThai(table);

                    if (retChange == false)
                    {
                        return null;
                    }

                    var worksheet = workbook.Worksheets.Add(table, "Followup");

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return content;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<byte[]> GetFollowupCustomerReply(GetFollowupDto datedata)
        {
            try
            {
                // wait join tmp.application for organize data
                var payer = (from o in _dbContext.Customer_Profile_Hotlines.AsNoTracking()
                             join x in _dbContext.Customer_FollowUps.AsNoTracking()
                             on o.PersonId equals x.PersonId
                             select new GetFollowupDataDto
                             {
                                 InformDate = o.InformDate,
                                 FirstName = o.FirstName,
                                 LastName = o.LastName,
                                 PhoneNumber = o.PrimaryPhone,
                                 Email = o.Email,
                                 Remark = o.Remark,
                                 PersonId = o.PersonId,
                                 PayerName = o.FirstName + ' ' + o.LastName,
                                 OrganizeName = x.OrganizeName,
                                 District = x.District,
                                 Province = x.Province,
                                 Branch = x.Branch,
                             }).AsQueryable();


                if (payer == null)
                {
                    return null;
                }

                if (datedata.FollowupDateStart != null)
                {
                    payer = payer.Where(x => x.InformDate.Date >= datedata.FollowupDateStart.Value.Date);
                }

                if (datedata.FollowupDateEnd != null)
                {
                    payer = payer.Where(x => x.InformDate.Date <= datedata.FollowupDateEnd.Value.Date);
                }


                List<GetFollowupDataDto> dataDto = new List<GetFollowupDataDto>();
                dataDto.AddRange(payer);

                await Task.Delay(0);

                if (dataDto.Count == 0)
                {
                    return null;
                }

                using (var workbook = new XLWorkbook())
                {

                    DataTable table = new DataTable();
                    table = ToDataTable(dataDto);

                    bool retChange = ChangeColumnToThai(table);

                    if (retChange == false)
                    {
                        return null;
                    }

                    var worksheet = workbook.Worksheets.Add(table, "Followup");

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return content;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<byte[]> GetSMSSended(GetSMSDto datedata)
        {
            try
            {
                var payer = (from o in _dbContext.Customer_Headers.Where(x => x.SentDate.Value != null)
                             select new GetSMSDataDto
                             {
                                 PersonId = o.PayerPersonId,
                                 //FullName = o.FirstName + ' ' + o.LastName,
                                 PrimaryPhone = o.PrimaryPhone,
                                 //Birthdate = o.Birthdate,
                                 DateSMSSended = o.SentDate.Value.Date,
                                 SMSResult = o.SMSResult,
                                 SMSCause = o.SMSCause,
                                 IsCustomerReply = o.IsCustomerReply,
                                 ReplyDate = o.ReplyDate.Value.Date,
                                 NumberofSended = o.NumberofSended,
                             }).AsQueryable();


                if (payer == null)
                {
                    return null;
                }

                if (datedata.SendedDateStart != null)
                {
                    payer = payer.Where(x => x.DateSMSSended.Value.Date >= datedata.SendedDateStart.Value.Date);
                }

                if ( datedata.SendedDateEnd != null)
                {
                    payer = payer.Where(x => x.DateSMSSended.Value.Date <= datedata.SendedDateEnd.Value.Date);
                }

                List<GetSMSDataDto> dataDto = new List<GetSMSDataDto>();
                dataDto.AddRange(payer);

                await Task.Delay(0);

                if (dataDto.Count == 0)
                {
                    byte[] b = new byte[0];
                    return b;
                }

                using (var workbook = new XLWorkbook())
                {

                    DataTable table = new DataTable();
                    table = ToDataTable(dataDto);

                    bool retChange = ChangeColumnToThai(table);

                    if (retChange == false)
                    {
                        return null;
                    }
                    
                    var worksheet = workbook.Worksheets.Add(table,"SMS");
                    
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return content;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private bool ChangeColumnToThai(DataTable table)
        {
            try
            {

                foreach (var itemColumn in table.Columns)
                {
                    switch (itemColumn.ToString())
                    {
                        case "PersonId":
                            table.Columns["PersonId"].ColumnName = "รหัสลูกค้า";
                            break;

                        case "FullName":
                            table.Columns["FullName"].ColumnName = "ชื่อ - สกุลลูกค้า";
                            break;

                        case "FirstName":
                            table.Columns["FirstName"].ColumnName = "ชื่อในการติดต่อกลับ";
                            break;
                            
                        case "LastName":
                            table.Columns["LastName"].ColumnName = "นามสกุลในการติดต่อกลับ";
                            break;

                        case "PayerName":
                            table.Columns["PayerName"].ColumnName = "ชื่อสกุลผู้ชำระเบี้ย";
                            break;

                        case "PrimaryPhone":
                            table.Columns["PrimaryPhone"].ColumnName = "เบอร์โทรศัพท์หลัก";
                            break;

                        case "PhoneNumber":
                            table.Columns["PhoneNumber"].ColumnName = "เบอรืโทรติดต่อกลับ";
                            break;

                        case "Birthdate":
                            table.Columns["Birthdate"].ColumnName = "วันเดือนปีเกิด";
                            break;

                        case "Email":
                            table.Columns["Email"].ColumnName = "อีเมล์";
                            break;

                        case "DateSMSSended":
                            table.Columns["DateSMSSended"].ColumnName = "วันที่ส่ง SMS";
                            break;

                        case "SMSResult":
                            table.Columns["SMSResult"].ColumnName = "ผลการส่ง SMS";
                            break;

                        case "SMSCause":
                            table.Columns["SMSCause"].ColumnName = "หมายเหตุ";
                            break;

                        case "IsCustomerReply":
                            table.Columns["IsCustomerReply"].ColumnName = "ผลการตอบกลับ";
                            break;

                        case "ReplyDate":
                            table.Columns["ReplyDate"].ColumnName = "วันที่ตอบกลับ";
                            break;

                        case "InformDate":
                            table.Columns["InformDate"].ColumnName = "วันที่ตอบกลับ";
                            break;

                        case "NumberofSended":
                            table.Columns["NumberofSended"].ColumnName = "จำนวนครั้งที่ส่ง";
                            break;

                        case "OrganizeName":
                            table.Columns["OrganizeName"].ColumnName = "ชื่อหน่วยงาน";
                            break;

                        case "District":
                            table.Columns["District"].ColumnName = "อำเภอ";
                            break;

                        case "Province":
                            table.Columns["Province"].ColumnName = "จังหวัด";
                            break;

                        case "Area":
                            table.Columns["Area"].ColumnName = "เขตพื้นที่";
                            break;

                        case "Branch":
                            table.Columns["Branch"].ColumnName = "สาขา";
                            break;
                        case "AppId":
                            table.Columns["AppId"].ColumnName = " APP ที่เกี่ยวข้อง";
                            break;

                        case "AgenId":
                            table.Columns["AgenId"].ColumnName = "รหัสตัวแทน";
                            break;

                        case "AgentName":
                            table.Columns["AgentName"].ColumnName = "ชื่อ - สกุล ตัวแทน";
                            break;

                        case "Result":
                            table.Columns["Result"].ColumnName = "ผลการตอบรับ";
                            break;

                        default:
                            break;
                    }   
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.Name != "Id" || prop.Name != "BranchId")
                {
                    switch (prop.Name)
                    {
                        case "IsCustomerReply":
                            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? typeof(String));
                            break;
                        case "CustomerConfirm":
                            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? typeof(String));
                            break;
                        default:
                            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                            break;
                    }                   
                }

                
            }
            
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.Name != "Id")
                    {
                        switch (prop.Name)
                        {
                            case "IsCustomerReply":
                                row[prop.Name] = prop.GetValue(item).ToString() == "True" ? "ตอบกลับแล้ว" : "ยังไม่ตอบกลับ";
                                break;
                            case "CustomerConfirm":
                                row[prop.Name] = prop.GetValue(item).ToString() == "True" ? "ตอบกลับแล้ว" : "ยังไม่ตอบกลับ";
                                break;
                            default:
                                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                                break;
                        }                        
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
