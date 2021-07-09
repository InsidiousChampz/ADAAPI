using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using INFOEDITORAPI.Data;
using INFOEDITORAPI.DTOs.Order;
using INFOEDITORAPI.Models;
using INFOEDITORAPI.Models.Order;
using mOrder = INFOEDITORAPI.Models.Order.Order;
using System.Linq.Dynamic.Core;
using System;
using Microsoft.AspNetCore.Http;
using INFOEDITORAPI.Helpers;

namespace INFOEDITORAPI.Services.Order
{
    public class OrdersService : ServiceBase, IOrdersService
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersService> _log;
        private readonly IHttpContextAccessor _httpcontext;

        public OrdersService(AppDBContext dBContext, IMapper mapper, ILogger<OrdersService> log, IHttpContextAccessor httpcontext) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
        }

        //Order and OrderList
        public async Task<ServiceResponse<GetOrderDto>> AddBothOrder(AddBothOrderDto newOrder)
        {

            var private_ordernumber = GetOrderNumber();

            var _order = new mOrder
            {
                DateOrder = Now(),
                ItemCount = newOrder.Header.ItemCount,
                Total = newOrder.Header.Total,
                Discount = newOrder.Header.Discount,
                Net = newOrder.Header.Net,
                CreateBy = GetUsername(),
                CreateDate = Now(),
                Status = true,
                OrderNumber = private_ordernumber,
            };

            _dbContext.Orders.Add(_order);
            await _dbContext.SaveChangesAsync();

            List<OrderList> _orderlist = new List<OrderList>();

            for (int i = 0; i < newOrder.Detail.Count; i++)
            {
                _orderlist.Add(new OrderList()
                {
                    OrderNumber = private_ordernumber,
                    ProductId = newOrder.Detail[i].ProductId,
                    Name = newOrder.Detail[i].Name,
                    Price = newOrder.Detail[i].Price,
                    Quantity = newOrder.Detail[i].Quantity,
                    TotalPrice = newOrder.Detail[i].TotalPrice,
                    CreateBy = GetUsername(),
                    CreateDate = Now(),
                    Status = true,
                    OrderId = _order.Id,
                });
            }

            _dbContext.OrderLists.AddRange(_orderlist);
            await _dbContext.SaveChangesAsync();
            var dto = _mapper.Map<GetOrderDto>(_order);
            return ResponseResult.Success(dto);
        }
        public async Task<ServiceResponse<GetOrderDto>> UpdateOrderStatusById(int OrderId, UpdateOrderStatusDto updateOrder)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == OrderId);
            if (order == null)
            {
                return ResponseResult.Failure<GetOrderDto>("Order  Not Found.");
            }
            else
            {
                order.Status = updateOrder.OrderStatus;
                order.CreateBy = GetUsername();
                order.CreateDate = Now();
                await _dbContext.SaveChangesAsync();

                // List<OrderList> orderlist = await _dbContext.OrderLists.Where(x => x.OrderId == OrderId).ToListAsync();
                // foreach (var item in orderlist)
                // {
                //     item.Status = updateOrder.OrderListStatus;
                // }
                // await _dbContext.SaveChangesAsync();

                var dto = _mapper.Map<GetOrderDto>(order);
                return ResponseResult.Success(dto);
            }
        }
        //Order
        public async Task<ServiceResponse<List<GetOrderDto>>> GetOrder()
        {
            var orders = await _dbContext.Orders
            .Include(x => x.OrderLists)
            .AsNoTracking()
            .ToListAsync();
            var dto = _mapper.Map<List<GetOrderDto>>(orders);
            return ResponseResult.Success(dto);
        }
        public async Task<ServiceResponse<GetOrderDto>> GetOrderById(int orderId)
        {
            var order = await _dbContext.Orders
           .Include(x => x.OrderLists)
           .FirstOrDefaultAsync(x => x.Id == orderId);
            if (order == null)
            {
                return ResponseResult.Failure<GetOrderDto>("order Not Found.");
            }
            else
            {
                var dto = _mapper.Map<GetOrderDto>(order);
                return ResponseResult.Success(dto);
            }
        }
        public async Task<ServiceResponseWithPagination<List<mOrder>>> GetOrderWithFilter(GetOrderFilterDto orderFilter)
        {
            var Queryable = _dbContext.Orders
             .AsQueryable();

            if (orderFilter.DateOrder != null)
            {
                Queryable = Queryable.Where(x => x.DateOrder.Date == orderFilter.DateOrder.Value);
            }

            if (orderFilter.ItemCount > 0)
            {
                Queryable = Queryable.Where(x => x.ItemCount == orderFilter.ItemCount);
            }

            if (orderFilter.Total > 0)
            {
                Queryable = Queryable.Where(x => x.Total == orderFilter.Total);
            }

            if (orderFilter.Discount > 0)
            {
                Queryable = Queryable.Where(x => x.Discount == orderFilter.Discount);
            }

            if (orderFilter.Net > 0)
            {
                Queryable = Queryable.Where(x => x.Net == orderFilter.Net);
            }

            if (!string.IsNullOrWhiteSpace(orderFilter.OrderNumber))
            {
                Queryable = Queryable.Where(x => x.OrderNumber.Contains(orderFilter.OrderNumber));
            }


            if (orderFilter.Status != null)
            {
                Queryable = Queryable.Where(x => x.Status == orderFilter.Status);
            }


            if (!string.IsNullOrWhiteSpace(orderFilter.OrderingField))
            {
                try
                {
                    Queryable = Queryable.OrderBy($"{orderFilter.OrderingField} {(orderFilter.AscendingOrder ? "ascending" : "descending")}");
                }
                catch
                {
                    return ResponseResultWithPagination.Failure<List<Models.Order.Order>>($"Could not order by field: {orderFilter.OrderingField}");
                }

              ;
            }

            var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(Queryable, orderFilter.RecordsPerPage, orderFilter.Page);
            var dto = await Queryable.Paginate(orderFilter).ToListAsync();
            return ResponseResultWithPagination.Success(dto, paginationResult);
        }
        public async Task<ServiceResponseWithPagination<List<mOrder>>> GetOrderWithFilterByDate(GetOrderDateFilterDTO orderFilter)
        {
            var Queryable = _dbContext.Orders.AsQueryable();

            if (orderFilter.DateFrom != null && orderFilter.DateTo != null)
            {
                //Queryable = Queryable.Where(x => x.DateOrder >= orderFilter.DateFrom.Value && x.DateOrder <= orderFilter.DateTo.Value.AddDays(1));
                Queryable = Queryable.Where(x => x.DateOrder >= orderFilter.DateFrom.Value.Date && x.DateOrder <= orderFilter.DateTo.Value.AddDays(1).Date);
            }

            if (!string.IsNullOrWhiteSpace(orderFilter.OrderingField))
            {
                try
                {
                    Queryable = Queryable.OrderBy($"{orderFilter.OrderingField} {(orderFilter.AscendingOrder ? "ascending" : "descending")}");
                }
                catch
                {
                    return ResponseResultWithPagination.Failure<List<Models.Order.Order>>($"Could not order by field: {orderFilter.OrderingField}");
                }

              ;
            }

            var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(Queryable, orderFilter.RecordsPerPage, orderFilter.Page);
            var dto = await Queryable.Paginate(orderFilter).ToListAsync();
            return ResponseResultWithPagination.Success(dto, paginationResult);
        }
        public async Task<ServiceResponse<GetOrderDto>> AddOrder(AddOrderDto newOrder)
        {
            var private_ordernumber = GetOrderNumber();

            var _order = new mOrder
            {
                DateOrder = Now(),
                ItemCount = newOrder.ItemCount,
                Total = newOrder.Total,
                Discount = newOrder.Discount,
                Net = newOrder.Net,
                CreateBy = GetUsername(),
                CreateDate = Now(),
                Status = true,
                OrderNumber = private_ordernumber,
            };

            _dbContext.Orders.Add(_order);
            await _dbContext.SaveChangesAsync();
            var dto = _mapper.Map<GetOrderDto>(_order);
            return ResponseResult.Success(dto);
        }

        //Orderlist
        public async Task<ServiceResponse<List<GetOrderListDto>>> GetOrderListbyOrderNumber(string ordernumber)
        {
            var orderlst = await _dbContext.OrderLists
           .Where(x => x.OrderNumber == ordernumber)
           .ToListAsync();
            var dto = _mapper.Map<List<GetOrderListDto>>(orderlst);
            return ResponseResult.Success(dto);
        }

        //Options
        private string GetOrderNumber()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            var FormNumber = BitConverter.ToUInt32(buffer, 0) ^ BitConverter.ToUInt32(buffer, 4) ^ BitConverter.ToUInt32(buffer, 8) ^ BitConverter.ToUInt32(buffer, 12);
            return FormNumber.ToString("X");

        }


    }
}