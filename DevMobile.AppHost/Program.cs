var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.DevMobile_ApiService>("apiservice");

builder.Build().Run();