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

var _wizyty = new List<Wizyta>
{
    new Wizyta { Id = 1, Data = "2021-10-10", IdZwierzecia = 1, Opis = "Badanie ogólne", cena = 50 },
    new Wizyta { Id = 2, Data = "2021-10-11", IdZwierzecia = 2, Opis = "Szczepienie", cena = 30 },
    new Wizyta { Id = 3, Data = "2021-10-12", IdZwierzecia = 3, Opis = "Badanie ogólne", cena = 50 },
    new Wizyta { Id = 4, Data = "2021-10-13", IdZwierzecia = 4, Opis = "Szczepienie", cena = 30 },
    new Wizyta { Id = 5, Data = "2021-10-14", IdZwierzecia = 1, Opis = "Badanie ogólne", cena = 50 },
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
app.MapGet("api/Wizyty/{id:int}", (int id) =>
{
    var wizyty = new List<Wizyta>{};
    for(int i = 0; i < _wizyty.Count; i++)
    {
        if(_wizyty[i].IdZwierzecia == id)
        {
            wizyty.Add(_wizyty[i]);
        }
    }
    return Results.Ok(wizyty);
}).WithName("GetWizyta").WithOpenApi();
app.MapPost("api/Wizyty", (Wizyta wizyta) =>
{
    _wizyty.Add(wizyta);
    return Results.StatusCode(StatusCodes.Status201Created);
}).WithName("PostWizyta").WithOpenApi();

app.Run();
