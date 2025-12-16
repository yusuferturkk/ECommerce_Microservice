using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class CreateOrderingCommandHandler : IRequestHandler<CreateOrderingCommand>
    {
        private readonly IRepository<Ordering> _repository;

        public CreateOrderingCommandHandler(IRepository<Ordering> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {
            var ordering = new Ordering
            {
                OrderDate = DateTime.Now,
                UserId = request.UserId,
                TotalPrice = request.TotalPrice,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Country = request.Country,
                City = request.City,
                District = request.District,
                ZipCode = request.ZipCode,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,

                OrderDetails = request.OrderItems.Select(x => new OrderDetail
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPrice = x.ProductPrice,
                    ProductAmount = x.ProductAmount,
                    ProductTotalPrice = x.ProductTotalPrice
                }).ToList()
            };

            await _repository.CreateAsync(ordering);
        }
    }
}
