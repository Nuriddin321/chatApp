using chatApi.Hubs;
using chatApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    options.UseInMemoryDatabase("chatapp");
});

builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(corsPolicy =>
    {
        corsPolicy.AllowAnyMethod();
        corsPolicy.AllowAnyHeader();
        corsPolicy.AllowAnyOrigin();
    });
});

builder.Services.AddSignalR();
builder.Services.AddSingleton(typeof(MessageService));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/jeki");
app.Run();
