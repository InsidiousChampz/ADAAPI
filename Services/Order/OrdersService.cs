using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using STANDARDAPI.Data;
using STANDARDAPI.DTOs.Order;
using STANDARDAPI.Models;

namespace STANDARDAPI.Services.Order
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersService> _log;

        public OrdersService(AppDBContext dBContext, IMapper mapper, ILogger<OrdersService> log)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
        }

        //Order
        public async Task<ServiceResponse<List<GetOrderDto>>> GetOrder()
        {
            var orders = await _dbContext.Orders.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<GetOrderDto>>(orders);
            return ResponseResult.Success(dto);
        }
    }
}