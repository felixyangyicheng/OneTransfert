
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using OnTransfert.srv.Hubs;

namespace OneTransfert.srv
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel((context, options) =>
            {
                options.ListenAnyIP(80, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    //listenOptions.UseHttps();
                });
            });
            builder.Services.AddCors(options => {

    
                options.AddPolicy("AllowAll",
                    b => {

                        b.WithOrigins("https://pwdman.duckdns.org", "https://pwdman.duckdns.org/");
                        b.WithMethods("GET", "POST");
                        b.AllowAnyHeader();
                        b.AllowCredentials();
                    }
            
                       );
                    //.AllowAnyMethod()
                    //.AllowAnyHeader()
                    //.SetIsOriginAllowed(origin => true) // allow any origin
                    //.AllowCredentials()
                    //.WithExposedHeaders("X-Pagination")
                    //   );
            });
            // Add services to the container.
            //builder.Services.AddAuthorization();
            builder.Services.AddSignalR();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            //app.UseAuthorization();
            app.MapHub<FileTransferHub>("/file-transfer-hub").RequireCors("AllowAll");
           // app.UseCors("AllowAll");

            app.Run();
        }
    }
}
