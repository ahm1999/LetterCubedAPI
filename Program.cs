using LettercubedApi.Data;
using LettercubedApi.Repositories;
using LettercubedApi.utils;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);


    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {

        Title = "Authdemo",
        Version = "v1"

    });
    option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {

        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "please enter a token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {

            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"

            }

        }, []

    }
});
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<IMovieMapper, MovieMapper>();
builder.Services.AddTransient<IReviewMapper, ReviewMapper>();



var app = builder.Build();
app.MapGroup("/Api/")
   .MapIdentityApi<AppUser>();

app.MapPost("/Api/SetRoleUser", async Task<Results<Ok, NotFound, ProblemHttpResult>> (HttpContext context, [FromServices] SignInManager<AppUser> _signInManager) =>
{
    var userId = _signInManager.UserManager.GetUserId(context.User);


    var user = await _signInManager.UserManager.FindByIdAsync(userId);

    if (await _signInManager.UserManager.IsInRoleAsync(user, RolesConsts.USER)) {

        return TypedResults.Problem("already added to that role", statusCode: StatusCodes.Status400BadRequest);
        };
    var result= await _signInManager.UserManager.AddToRoleAsync(user, RolesConsts.USER);

    if (! result.Succeeded)
    {
        return TypedResults.Problem("error in adding", statusCode: StatusCodes.Status400BadRequest);
    }

    return TypedResults.Ok();
    ;
}) 
 .RequireAuthorization()
 .WithOpenApi();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger().RequireAuthorization();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{

    var roleManeger =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { RolesConsts.USER, RolesConsts.ADMIN,RolesConsts.EDITOR };
    foreach (var role in roles)
    {
        if (!await roleManeger.RoleExistsAsync(role))
        {
            await roleManeger.CreateAsync(new IdentityRole(role));
        }


    }

}
using (var scope = app.Services.CreateScope())
{

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    string email = "admin@email.com";
    string password = "132465_Ahmed";

    if (await userManager.FindByEmailAsync(email) == null)
    {


        var user = new AppUser();

        user.Email = email;
        user.UserName = email;

        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, RolesConsts.ADMIN);

    }

}

app.Run();
