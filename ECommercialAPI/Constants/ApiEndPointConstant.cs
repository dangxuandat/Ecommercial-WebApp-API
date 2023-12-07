namespace ECommercialAPI.Constants;

public static class ApiEndPointConstant
{
    public const string RootEndPoint = "/api";
    public const string ApiVersion = "/v1";
    public const string ApiEndpoint = RootEndPoint + ApiVersion;

    public static class Authentiction
    {
        public const string AuthenticationEndpoint = ApiEndpoint + "/auth";
    }
    
    
}