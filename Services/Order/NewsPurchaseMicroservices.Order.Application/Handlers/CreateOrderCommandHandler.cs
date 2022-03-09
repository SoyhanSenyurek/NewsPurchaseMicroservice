using MediatR;
using NewsPurchaseMicroservices.Order.Application.Commands;
using NewsPurchaseMicroservices.Order.Application.Dtos;
using NewsPurchaseMicroservices.Order.Application.Response;
using NewsPurchaseMicroservices.Order.Insfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsPurchaseMicroservices.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Domain.OrderAggregate.Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line);

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(newAddress,request.BuyerId);

            request.OrderItems.ForEach(x => {
                newOrder.AddOrderItem(x.ProductId,x.ProductName,x.PictureUrl,x.Price);
            });

            await _orderDbContext.Orders.AddAsync(newOrder);
            await _orderDbContext.SaveChangesAsync();

            return new Response<CreatedOrderDto>() { Data = new CreatedOrderDto() { OrderId = newOrder.Id }, IsSuccessful = true,StatusCode = 200 };
        }
    }
}
