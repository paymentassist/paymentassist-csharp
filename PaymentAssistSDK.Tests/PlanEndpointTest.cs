using Xunit;
using System;
using System.Threading.Tasks;

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
    public async Task TestPlan()
    {
        var request = new PlanRequest{
            Amount = 100000,
            PlanID = 5,
        };

        var response = await PASDK.Plan(request);

        Assert.Equal("4-Payment", response.PlanName);
        Assert.Equal(50000, response.Amount);
        Assert.Equal(0, response.Interest);
        Assert.Equal(50000, response.TotalRepayable);
        Assert.Equal(12500, response.PaymentSchedule[3].Amount);
        Assert.Equal(new DateTime(2019, 6, 12), response.PaymentSchedule[3].Date);
    }

    [Fact]
    public async Task TestValidate()
    {
        var request = new PlanRequest();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Plan(request));
        Assert.Equal("Amount must be greater than 0", exception.Message);

        request.Amount = 10000;
        await PASDK.Plan(request);
    }
}