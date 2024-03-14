namespace PaymentAssistSDK;

internal static class MockResponses
{
    public static APIResponse<T> GetMockAPIResponse<T>(string endpoint) where T : new()
    {
        // If this is a GET request then the endpoint will have parameters on it. Take them
        // off so we can match on the actual endpoint.
        if (endpoint.Contains("?"))
            endpoint = endpoint.Split('?')[0];

        switch (endpoint)
        {
            case "begin":
                return JSONHelpers.Deserialize<APIResponse<T>>(@"
                {
                    ""status"": ""ok"",
                    ""msg"": null,
                    ""data"": {
                        ""token"": ""0138ef43-f703-41cb-8f08-f36f41b47560"",
                        ""url"": ""https://example.com""
                    }
                }");
            case "preapproval":
                return JSONHelpers.Deserialize<APIResponse<T>>(@"
                {
                    ""status"": ""ok"",
                    ""msg"": null,
                    ""data"": {
                        ""approved"": true
                    }
                }");
            case "update":
                return JSONHelpers.Deserialize<APIResponse<T>>(@"
                {
                    ""status"": ""ok"",
                    ""msg"": null,
                    ""data"": {
                        ""token"": ""aed3bd4e-c478-4d73-a6fa-3640a7155e4f"",
                        ""order_id"": ""neworderid"",
                        ""expiry"": ""600"",
                        ""amount"": ""100000""
                    }
                }");
            case "plan":
                return JSONHelpers.Deserialize<APIResponse<T>>(@"
                {  
                    ""status"": ""ok"",
                    ""msg"": null,
                    ""data"": {  
                        ""plan"": ""4-Payment"",
                        ""amount"": 50000,
                        ""interest"": 0,
                        ""repayable"": 50000,
                        ""schedule"": [  
                            {  
                                ""date"": ""2019-03-12"",
                                ""amount"": 12500
                            },
                            {  
                                ""date"": ""2019-04-12"",
                                ""amount"": 12500
                            },
                            {  
                                ""date"": ""2019-05-12"",
                                ""amount"": 12500
                            },
                            {  
                                ""date"": ""2019-06-12"",
                                ""amount"": 12500
                            }
                        ]
                    }
                }");
            case "capture":
                return JSONHelpers.Deserialize<APIResponse<T>>(@"
                {
                    ""status"": ""ok"",
                    ""msg"": null,
                    ""data"": {
                        ""token"": ""aed3bd4e-c478-4d73-a6fa-3640a7155e4f"",
                        ""status"": ""completed"",
                        ""deposit_captured"": true
                    }
                }");
            case "invoice":
                return JSONHelpers.Deserialize<APIResponse<T>>(@"
                {
                    ""status"": ""ok"",
                    ""msg"": null,
                    ""data"": {
                        ""token"": ""aed3bd4e-c478-4d73-a6fa-3640a7155e4f"",
                        ""upload_status"": ""success""
                    }
                }");
            case "status":
                return JSONHelpers.Deserialize<APIResponse<T>>(@"
                {
                    ""status"": ""ok"",
                    ""msg"": null,
                    ""data"": {
                        ""token"": ""aed3bd4e-c478-4d73-a6fa-3640a7155e4f"",
                        ""status"": ""pending"",
                        ""amount"": 50000,
                        ""expires_at"": ""2022-05-24T19:28:06+01:00"",
                        ""pa_ref"": ""testreference"",
                        ""requires_invoice"": true,
                        ""has_invoice"": true
                    }
                }");
            case "account":
                return JSONHelpers.Deserialize<APIResponse<T>>(@"
                {
                    ""status"": ""ok"",
                    ""msg"": null,
                    ""data"": {
                        ""legal_name"": ""Test Dealer"",
                        ""display_name"": ""Test Dealer"",
                        ""plans"": [
                            {
                                ""plan_id"": 6,
                                ""name"": ""3-Payment"",
                                ""instalments"": 3,
                                ""deposit"": true,
                                ""apr"": 0,
                                ""frequency"": ""monthly"",
                                ""min_amount"": null,
                                ""max_amount"": 500000,
                                ""commission_rate"": ""8.50"",
                                ""commission_fixed_fee"": null
                            },
                            {
                                ""plan_id"": 1,
                                ""name"": ""4-Payment"",
                                ""instalments"": 4,
                                ""deposit"": false,
                                ""apr"": 5.5,
                                ""frequency"": ""monthly"",
                                ""min_amount"": 10000,
                                ""max_amount"": 300000,
                                ""commission_rate"": ""0"",
                                ""commission_fixed_fee"": 5000
                            }
                        ]
                    }
                }");
            default:
                throw new ArgumentException("unrecognised endpoint " + endpoint);
        }
    }
}