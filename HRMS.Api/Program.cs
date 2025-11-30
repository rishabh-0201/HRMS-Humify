using HRMS.Api.Data;
using HRMS.Api.Models;
using HRMS.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HRMSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped <TokenService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin")); // Only users in Admin role can access
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
    {
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Reference = new Microsoft.OpenApi.Models.OpenApiReference
            {
                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
           },
        new string[] { }
    }});
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// GET all roles
app.MapGet("/roles", async (HRMSDbContext db) =>
{
    var roles = await db.Roles.Where(r => r.IsActive).ToListAsync();
    return Results.Ok(roles);
})
.RequireAuthorization("AdminOnly");

// GET role by ID
app.MapGet("/roles/{id:int}", async (int id, HRMSDbContext db) =>
{
    var role = await db.Roles.FindAsync(id);
    return role != null ? Results.Ok(role) : Results.NotFound();
})
.RequireAuthorization("AdminOnly");

// POST add role
app.MapPost("/roles", async (Role role, HRMSDbContext db) =>
{
    role.IsActive = true;
    db.Roles.Add(role);
    await db.SaveChangesAsync();
    return Results.Created($"/roles/{role.RoleId}", role);
})
.RequireAuthorization("AdminOnly");

// PUT update role
app.MapPut("/roles/{id:int}", async (int id, Role updatedRole, HRMSDbContext db) =>
{
    var role = await db.Roles.FindAsync(id);
    if (role == null) return Results.NotFound();

    role.RoleName = updatedRole.RoleName;
    role.Description = updatedRole.Description;
    await db.SaveChangesAsync();
    return Results.Ok(role);
})
.RequireAuthorization("AdminOnly");

// DELETE soft delete role
app.MapDelete("/roles/{id:int}", async (int id, HRMSDbContext db) =>
{
    var role = await db.Roles.FindAsync(id);
    if (role == null) return Results.NotFound();

    role.IsActive = false;
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.RequireAuthorization("AdminOnly");

// Assign role to a user
app.MapPost("/users/{userId:int}/assign-role/{roleId:int}", async (int userId, int roleId, HRMSDbContext db) =>
{
    var user = await db.Users.FindAsync(userId);
    var role = await db.Roles.FindAsync(roleId);
    if (user == null || role == null) return Results.NotFound();

    user.RoleId = roleId;
    await db.SaveChangesAsync();
    return Results.Ok(user);
})
.RequireAuthorization("AdminOnly");


app.Run();
