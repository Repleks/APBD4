using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();

var _zwierzeta = new List<Zwierze>
{
    new Zwierze { Id = 1, Imie = "Pankracy", Kategoria = "Kot", Masa = 5, KolorSiersci = "Czarny" },
    new Zwierze { Id = 2, Imie = "Bonifacy", Kategoria = "Pies", Masa = 10, KolorSiersci = "Biały" },
    new Zwierze { Id = 3, Imie = "Filemon", Kategoria = "Kot", Masa = 4, KolorSiersci = "Szary" },
    new Zwierze { Id = 4, Imie = "Kajtek", Kategoria = "Pies", Masa = 8, KolorSiersci = "Brązowy" }
};

app.MapGet("/api/Zwierzeta",() => Results.Ok(_zwierzeta)).WithName("GetZwierzeta").WithOpenApi();
app.MapGet("/api/Zwierzeta/{id:int}", (int id) =>
{
    var zwierze = _zwierzeta.FirstOrDefault(z => z.Id == id);
    if (zwierze is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(zwierze);
}).WithName("GetZwierze").WithOpenApi();
app.MapPost("api/Zwierzeta", (Zwierze zwierze) =>
{
    _zwierzeta.Add(zwierze);
    return Results.StatusCode(StatusCodes.Status201Created);
}).WithName("PostZwierze").WithOpenApi();
app.MapPut("api/Zwierzeta/{id:int}", (int id, Zwierze zwierze) =>
{
    var existingZwierze = _zwierzeta.FirstOrDefault(z => z.Id == id);
    if (existingZwierze is null)
    {
        return Results.NotFound();
    }
    existingZwierze.Imie = zwierze.Imie;
    existingZwierze.Kategoria = zwierze.Kategoria;
    existingZwierze.Masa = zwierze.Masa;
    existingZwierze.KolorSiersci = zwierze.KolorSiersci;
    return Results.Ok(existingZwierze);
}).WithName("PutZwierze").WithOpenApi();
app.MapDelete("api/Zwierzeta/{id:int}", (int id) =>
{
    var zwierze = _zwierzeta.FirstOrDefault(z => z.Id == id);
    if (zwierze is null)
    {
        return Results.NotFound();
    }
    _zwierzeta.Remove(zwierze);
    return Results.Ok();
}).WithName("DeleteZwierze").WithOpenApi();


app.Run();
