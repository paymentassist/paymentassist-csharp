using Xunit;
using System;

namespace PaymentAssistSDK.Tests;

public class PlanEndpointTest
{
    public PlanEndpointTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = false;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public void TestPlan()
    {
        var request = new PlanRequest{
            Amount = 100000,
            PlanID = 5,
        };

        var response = PASDK.Plan(request);

        Assert.Equal("4-Payment", response.PlanName);
        Assert.Equal(50000, response.Amount);
        Assert.Equal(0, response.Interest);
        Assert.Equal(50000, response.TotalRepayable);
        Assert.Equal(12500, response.PaymentSchedule[3].Amount);
        Assert.Equal(new DateTime(2019, 6, 12), response.PaymentSchedule[3].Date);
    }

    [Fact]
    public void TestValidate()
    {
        var request = new PlanRequest();

        var exception = Assert.Throws<ArgumentException>(() => PASDK.Plan(request));
        Assert.Equal("Amount must be greater than 0", exception.Message);

        request.Amount = 10000;
        PASDK.Plan(request);
    }
}