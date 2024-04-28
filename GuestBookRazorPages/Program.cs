using GuestBookRazorPages.Models;
using GuestBookRazorPages.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<GuestBookContext>(options => options.UseSqlServer(connection)); 
builder.Services.AddScoped<IRepository, GuestBookRepository>();
var app = builder.Build();




app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();   

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
