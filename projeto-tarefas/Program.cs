using API.model;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ADICIONE ESTAS CONFIGURAÇÕES:
builder.Services.AddDbContext<APIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

var app = builder.Build();

// ################################################# - POST
app.MapPost("/monitor", async (APIDbContext db, MonitorDTO novoMoni) =>
{
    var monitor = new Moni
    {
        Nome = novoMoni.Nome,
        Apelido = novoMoni.Apelido,
        RA = novoMoni.RA
    };
    db.Moni.Add(monitor);
    await db.SaveChangesAsync();
    return Results.Created($"/Monitor/{monitor.IdMonitor}", monitor);
});

app.MapPost("/horarios", async (APIDbContext db, HorarioDTO novoHorario) =>
{
    var horario = new Horario
    {
        DiaSemana = novoHorario.DiaSemana,
        horario = novoHorario.Horario,
        IdMonitor = novoHorario.IdMonitor
    };
    db.Horario.Add(horario);
    await db.SaveChangesAsync();
    
    var response = new
    {
        IdHorario = horario.IdHorario,
        DiaSemana = horario.DiaSemana,
        Horario = horario.horario,
        IdMonitor = horario.IdMonitor
    };
    
    return Results.Created($"/horarios/{horario.IdHorario}", response);
});

// ################################################# - GET
app.MapGet("/monitores/listar", async (APIDbContext db) =>
{
    var monitores = await db.Moni.ToListAsync();
    return Results.Ok(monitores);
});

app.MapGet("/monitores/{apelido}", async (APIDbContext db, string apelido) =>
{
    var monitor = await db.Moni.FirstOrDefaultAsync(m => m.Apelido == apelido);
    if (monitor == null)
        return Results.NotFound();
    
    var horarios = await db.Horario
        .Where(h => h.IdMonitor == monitor.IdMonitor)
        .ToListAsync();
    
    var response = new
    {
        Monitor = monitor,
        Horarios = horarios.Select(h => new
        {
            DiaSemana = Enum.GetName(typeof(DiaDaSemana), h.DiaSemana),
            Horario = h.horario
        })
    };
    return Results.Ok(response);
});

// ################################################# - PUT
app.MapPut("/monitores/{id}", async (int id, MonitorDTO novoMoni, APIDbContext db) =>
{
    var monitor = await db.Moni.FindAsync(id);
    if (monitor != null)
    {
        monitor.Nome = novoMoni.Nome;
        monitor.Apelido = novoMoni.Apelido;
        monitor.RA = novoMoni.RA;
        await db.SaveChangesAsync();
        return Results.Ok(monitor);
    }
    return Results.NotFound();
});

app.MapPut("/horario/{id}", async (int id, HorarioDTO novoHorario, APIDbContext db) =>
{
    var horarioExistente = await db.Horario.FindAsync(id);
    if (horarioExistente != null)
    {
        horarioExistente.DiaSemana = novoHorario.DiaSemana;
        horarioExistente.horario = novoHorario.Horario;
        await db.SaveChangesAsync();
        return Results.Ok(horarioExistente);
    }
    return Results.NotFound();
});

// ################################################# - DELETE
app.MapDelete("/monitor/{id}", async (int id, APIDbContext db) =>
{
    var monitor = await db.Moni.FindAsync(id);
    if (monitor == null)
        return Results.NotFound();
    
    var horarios = await db.Horario
        .Where(h => h.IdMonitor == monitor.IdMonitor)
        .ToListAsync();
    
    if (!horarios.Any())
    {
        db.Moni.Remove(monitor);
        await db.SaveChangesAsync();
        return Results.Ok($"Monitor com id {id} foi deletado.");
    }
    return Results.BadRequest("Não é possível deletar: o monitor possui horários associados.");
});

app.MapDelete("/horarios/{id}", async (int id, APIDbContext db) =>
{
    var horario = await db.Horario.FindAsync(id);
    if (horario is null)
        return Results.NotFound($"Horário com id {id} não encontrado.");
    
    db.Horario.Remove(horario);
    await db.SaveChangesAsync();
    return Results.Ok($"Horário com id {id} foi deletado.");
});

// ################################################# - RUN
app.Run();