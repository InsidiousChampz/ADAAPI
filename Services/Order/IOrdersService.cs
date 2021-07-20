using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerProFileAPI.DTOs.Order;
using CustomerProFileAPI.Models;

namespace CustomerProFileAPI.Services.Order
{
    public interface IOrdersService
    {
        ////Orderlist
        //Task<ServiceResponse<List<GetOrderListDto>>> GetOrderListbyOrderNumber(string ordernumber);
        ////Order
        //Task<ServiceResponse<List<GetOrderDto>>> GetOrder();
        //Task<ServiceResponse<GetOrderDto>> GetOrderById(int orderId);

        //Task<ServiceResponseWithPagination<List<Models.Order.Order>>> GetOrderWithFilter(GetOrderFilterDto orderFilter);
        //Task<ServiceResponseWithPagination<List<Models.Order.Order>>> GetOrderWithFilterByDate(GetOrderDateFilterDTO orderFilter);
        //Task<ServiceResponse<GetOrderDto>> AddOrder(AddOrderDto newOrder);

        ////Order and OrderList
        //Task<ServiceResponse<GetOrderDto>> AddBothOrder(AddBothOrderDto newOrder);
        //Task<ServiceResponse<GetOrderDto>> UpdateOrderStatusById(int OrderId, UpdateOrderStatusDto updateOrder);
    }
}