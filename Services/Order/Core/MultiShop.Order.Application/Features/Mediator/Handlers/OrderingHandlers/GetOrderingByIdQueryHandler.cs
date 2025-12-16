using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderDetailResults;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class GetOrderingByIdQueryHandler : IRequestHandler<GetOrderingByIdQuery, GetOrderingByIdQueryResult>
    {
        private readonly IOrderingRepository _repository;

        public GetOrderingByIdQueryHandler(IOrderingRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetOrderingByIdQueryResult> Handle(GetOrderingByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetOrderingDetailsById(request.OrderingId);

            return new GetOrderingByIdQueryResult
            {
                OrderingId = value.OrderingId,
                OrderDate = value.OrderDate,
                TotalPrice = value.TotalPrice,
                UserId = value.UserId,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Email = value.Email,
                PhoneNumber = value.PhoneNumber,
                Country = value.Country,
                City = value.City,
                District = value.District,
                ZipCode = value.ZipCode,
                AddressLine1 = value.AddressLine1,
                AddressLine2 = value.AddressLine2,

                OrderDetails = value.OrderDetails.Select(x => new ResultOrderDetailDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductPrice = x.ProductPrice,
                    ProductAmount = x.ProductAmount,
                    ProductTotalPrice = x.ProductTotalPrice,
                    ProductImageUrl = x.ProductImageUrl,
                    OrderDetailId = x.OrderDetailId,
                    OrderingId = x.OrderingId
                }).ToList()
            };
        }
    }
}
