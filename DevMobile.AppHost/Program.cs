
var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureAppServiceEnvironment("app-service-env");

var apiService = builder.AddProject<Projects.DevMobile_ApiService>("apiservice")
                        .WithExternalHttpEndpoints()
                        .WithHttpHealthCheck("/health");



builder.Build().Run();