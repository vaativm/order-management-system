using Microsoft.Extensions.Logging;
using Moq;
using OrderManagement.Api.Application.Abstractions;
using OrderManagement.Api.Application.Services;
using OrderManagement.Api.Domain.Entities;

namespace OrderManagement.Api.Test.UnitTests.DiscountServiceTests;
public class DetermineCustomerSegmentTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IPromotionRepository> _promotionRepositoryMock;
    private readonly Mock<IDiscountStrategyFactory> _discountStrategyFactoryMock;
    private readonly Mock<ILogger<DiscountService>> _loggerMock;
    private readonly IDiscountService _discountService;

    public DetermineCustomerSegmentTests()
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
    public async Task When_The_Total_Amount_Spend_Is_LessThan_500_And_Order_Count_Is_LessThan_2_Then_DetermineCustomerSegmentAsync_Should_Return_NewSegment()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        _orderRepositoryMock.Setup(x => x.GetCustomerOrderHistoryAsync(customerId)).ReturnsAsync(new List<Order>());

        // Act
        CustomerSegment segment = await _discountService.DetermineCustomerSegmentAsync(customerId);

        // Assert
        Assert.Equal(CustomerSegment.New, segment);
    }

    [Fact]
    public async Task When_The_Total_Amount_Spend_Is_GreaterThan_500_And_Order_Count_Is_GreaterThan_2_Then_DetermineCustomerSegmentAsync_Should_Return_RegularSegment()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var orderHistory = new List<Order>
        {
            new Order { TotalAmount = 101 },
            new Order { TotalAmount = 101 },
            new Order { TotalAmount = 101 },
            new Order { TotalAmount = 101 },
            new Order { TotalAmount = 101 },
            new Order { TotalAmount = 1 }
        };
        _orderRepositoryMock.Setup(x => x.GetCustomerOrderHistoryAsync(customerId)).ReturnsAsync(orderHistory);

        // Act
        CustomerSegment segment = await _discountService.DetermineCustomerSegmentAsync(customerId);

        // Assert
        Assert.Equal(CustomerSegment.Regular, segment);
    }

    [Fact]
    public async Task When_The_Total_Amount_Spend_Is_GreaterThan_1000_And_Order_Count_Is_GreaterThan_5_Then_DetermineCustomerSegmentAsync_Should_Return_VIPSegment()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var orderHistory = new List<Order>
        {
            new Order { TotalAmount = 1000 },
            new Order { TotalAmount = 1500 },
            new Order { TotalAmount = 1000 },
            new Order { TotalAmount = 2000 },
            new Order { TotalAmount = 3000 },
        };
        _orderRepositoryMock.Setup(x => x.GetCustomerOrderHistoryAsync(customerId)).ReturnsAsync(orderHistory);

        // Act
        CustomerSegment segment = await _discountService.DetermineCustomerSegmentAsync(customerId);

        // Assert
        Assert.Equal(CustomerSegment.Regular, segment);
    }
}
