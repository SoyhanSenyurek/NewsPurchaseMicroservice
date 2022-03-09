using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsPurchaseMicroservices.Order.Application.Dtos;
using NewsPurchaseMicroservices.Order.Application.Mapping;
using NewsPurchaseMicroservices.Order.Application.Queries;
using NewsPurchaseMicroservices.Order.Application.Response;
using NewsPurchaseMicroservices.Order.Insfrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsPurchaseMicroservices.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();

            if (!orders.Any())
                return new Response<List<OrderDto>>() { IsSuccessful = true, StatusCode = 200, Data = new List<OrderDto>() };

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            return new Response<List<OrderDto>>() { IsSuccessful = true, StatusCode = 200, Data = ordersDto };
        }
    }
}
