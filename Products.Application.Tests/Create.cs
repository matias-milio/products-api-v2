using MediatR;
using Moq;
using System;
using Xunit;

namespace Products.Application.Tests
{
    public class Create
    {
        [Fact]
        public async void CreateProductCommand_ProductCreated()
        {
            //Arange
            var mediator = new Mock<IMediator>();

            Products.Handlers.Create command = new Products.RequestModels.Create(new Infrastructure.AppDbContext.MyStoreDbContext());
            UpdateCustomerCommandHandler handler = new UpdateCustomerCommandHandler(mediator.Object);

            //Act
            Unit x = await handler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            //Do the assertion

            //something like:
            mediator.Verify(x => x.Publish(It.IsAny<CustomersChanged>()));
        }
    }
}
