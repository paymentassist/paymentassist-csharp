using Xunit;
using System;

namespace PaymentAssistSDK.Tests;

public class AccountEndpointTest
{
    public AccountEndpointTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = false;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public async void TestAccount()
    {
        var response = await PASDK.Account();

        Assert.Equal("Test Dealer", response.DisplayName);
        Assert.Equal("Test Dealer", response.LegalName);
        Assert.Equal(6, response.Plans[0].ID);
        Assert.Equal("3-Payment", response.Plans[0].Name);
        Assert.Equal(3, response.Plans[0].Instalments);
        Assert.True(response.Plans[0].DepositRequired);
        Assert.Equal(0m, response.Plans[0].APR);
        Assert.Equal("monthly", response.Plans[0].Frequency);
        Assert.Null(response.Plans[0].MinAmount);
        Assert.Equal(500000, response.Plans[0].MaxAmount);
        Assert.Equal(8.50m, response.Plans[0].CommissionRate);
        Assert.Null(response.Plans[0].CommissionFixedFee);

        Assert.Equal(1, response.Plans[1].ID);
        Assert.Equal("4-Payment", response.Plans[1].Name);
        Assert.Equal(4, response.Plans[1].Instalments);
        Assert.False(response.Plans[1].DepositRequired);
        Assert.Equal(5.5m, response.Plans[1].APR);
        Assert.Equal("monthly", response.Plans[1].Frequency);
        Assert.Equal(10000, response.Plans[1].MinAmount);
        Assert.Equal(300000, response.Plans[1].MaxAmount);
        Assert.Equal(0m, response.Plans[1].CommissionRate);
        Assert.Equal(5000, response.Plans[1].CommissionFixedFee);
    }
}