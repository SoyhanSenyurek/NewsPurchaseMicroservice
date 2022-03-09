using MediatR;
using NewsPurchaseMicroservices.Order.Application.Dtos;
using NewsPurchaseMicroservices.Order.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPurchaseMicroservices.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
