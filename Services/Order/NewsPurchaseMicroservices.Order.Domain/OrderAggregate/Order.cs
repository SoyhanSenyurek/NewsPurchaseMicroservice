﻿using NewsPurchaseMicroservices.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPurchaseMicroservices.Order.Domain.OrderAggregate
{
    public class Order : Entity,IAggregateRoot
    {
        private Order()
        {

        }
        public Order(Address address, string buyerId)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
        }


        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;


        public void AddOrderItem(string productId,string productName,string pictureUrl,decimal price)
        {
            var exitsProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!exitsProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);  
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
