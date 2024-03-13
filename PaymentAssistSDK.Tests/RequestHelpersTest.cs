using Xunit;
using System;
using System.Collections.Generic;

namespace PaymentAssistSDK.Tests;

public class RequestHelpersTest
{
    public RequestHelpersTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = false;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public async void TestCheckCredentials()
    {
        try
        {
            DataStore.APIKey = "";
            DataStore.APISecret = "";

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Account());
            Assert.Equal("APIKey cannot be empty - call PASDK.Initialise to pass in your credentials", exception.Message);
            
            PASDK.Initialise("test", "", "");
            exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Account());
            Assert.Equal("APISecret cannot be empty - call PASDK.Initialise to pass in your credentials", exception.Message);

            PASDK.Initialise("test", "test", "test");
            await PASDK.Account();
        }
        finally
        {
            PASDK.Initialise("test", "test", "");
        }
    }

    [Fact]
    public void TestGenerateSignature() 
    {
        var requestParams = new List<string>{
            "test=test",
            "test2=test2",
        };

        var hash = "7eba7f616af343d16ff09e242362345e6cfb09d24b78a73c81d267f049fc47c2";
        var output = RequestHelpers.GenerateSignature(requestParams, "secret");

        Assert.Equal(hash, output);
    }

    [Fact]
    public void TestGenerateSignature_2() 
    {
        var requestParams = new List<string>{
            "test1=test",
            "test2=test2",
        };

        var hash = "8226de39365226038be9598213e480d22f4dfe7147f50d977087a8d4eb124f52";
        var output = RequestHelpers.GenerateSignature(requestParams, "demo_2ec4449ac4a7a86e2f79c4794e8");

        Assert.Equal(hash, output);
    }

    [Fact]
    public void TestGetRequestURL()
    {
        try
        {
            TestHelpers.TestsAreRunning = false;

            PASDK.Initialise("test", "test", "https://test.com");
            Assert.Equal("https://test.com/", RequestHelpers.GetRequestURL());

            PASDK.Initialise("test", "test", "https://test.com/");
            Assert.Equal("https://test.com/", RequestHelpers.GetRequestURL());
        }
        finally
        {
            TestHelpers.TestsAreRunning = true;
        }
    }
}