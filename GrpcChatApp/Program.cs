using GrpcChatApp.Entities;
using GrpcChatApp.GrpcServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddSingleton<ChatManagerService>();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
if (app.Environment.IsDevelopment()) app.MapGrpcReflectionService();
app.UseCors();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GrpcChatService>().EnableGrpcWeb().RequireCors("AllowAll");
});
app.Run();