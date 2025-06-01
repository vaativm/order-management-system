using Microsoft.Extensions.Logging;
using Moq;
using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Application.Exceptions;
using OrderManagement.Api.Application.Services;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Test.UnitTests.DiscountServiceTests;

public class CalculateTotalDiscountTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IPromotionRepository> _promotionRepositoryMock;
    private readonly Mock<IDiscountStrategyFactory> _discountStrategyFactoryMock;
    private readonly Mock<ILogger<DiscountService>> _loggerMock;
    private readonly IDiscountService _discountService;

    public CalculateTotalDiscountTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _promotionRepositoryMock = new Mock<IPromotionRepository>();
        _discountStrategyFactoryMock = new Mock<IDiscountStrategyFactory>();
        _loggerMock = new Mock<ILogger<DiscountService>>();

        _discountService = new DiscountService(
            _orderRepositoryMock.Object,
            _promotionRepositoryMock.Object,
            _discountStrategyFactoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task When_CalculateTotalDiscountAsync_Receives_Non_existing_OrderId_Then_It_Should_Throw_NotFoundException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        _orderRepositoryMock.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync((Order)null!);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _discountService.CalculateTotalDiscountAsync(orderId));
    }

    [Fact]
    public async Task When_CalculateTotalDiscountAsync_Receives_Correct_Order_And_Promotion_Then_It_Should_Return_TotalDiscount()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var order = new Order { Id = orderId, CustomerId = customerId, TotalAmount = 100 };
        var promotion = new Promotion { Id = Guid.NewGuid(), Type = DiscountType.Percentage, Value = 10 };

        _orderRepositoryMock.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync(order);
        _orderRepositoryMock.Setup(x => x.GetCustomerOrderHistoryAsync(customerId)).ReturnsAsync(new List<Order>());
        _promotionRepositoryMock.Setup(x => x.GetApplicablePromotionsAsync(CustomerSegment.New)).ReturnsAsync(new List<Promotion> { promotion });
        
        var discountStrategyMock = new Mock<IDiscountStrategy>();
        discountStrategyMock.Setup(x => x.ApplyDiscount(order.TotalAmount, promotion.Value)).Returns(10);
        _discountStrategyFactoryMock.Setup(x => x.GetDiscountStrategy(promotion.Type)).Returns(discountStrategyMock.Object);

        // Act
        decimal discount = await _discountService.CalculateTotalDiscountAsync(orderId);

        // Assert
        Assert.Equal(10, discount);
    }
}
