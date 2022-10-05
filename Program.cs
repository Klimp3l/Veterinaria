//RODAR ESSES COMANDOS NA PRIMEIRA VEZ
//dotnet new webapi -minimal -o NomeDoProjeto
//cd NomeDoProjeto
//dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0
//dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0
//dotnet tool install --global dotnet-ef
//dotnet ef migrations add InitialCreate

//RODAR ESSE COMANDO SEMPRE QUE MEXER NAS CLASSES RELATIVAS A BASE DE DADOS
//dotnet ef database update

//INCLUIR ESSE NAMESPACE
using Microsoft.EntityFrameworkCore;

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Trabalho
{
	class Pessoa
    {
    	public int id { get; set; }
		public string? nome { get; set; }
    	public string? telefone { get; set; }
    	public string? email { get; set; }
        public bool isVeterinario { get; set; }
    }
    class Paciente
    {
    	public int id { get; set; }
		public string? nome { get; set; }
    	public string? especie { get; set; }
    	public string? raca { get; set; }
    	public char? sexo { get; set; }
    	public int idDono { get; set; }
    }
	class Agenda
    {
    	public int id { get; set; }
		public int idPaciente { get; set; }
        public int idVeterinario { get; set; }
		public DateTime dataHora { get; set; }
    }
	class Base : DbContext
	{
		public Base(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Pessoa> Pessoas { get; set; } = null!;
        public DbSet<Paciente> Pacientes { get; set; } = null!;
        public DbSet<Agenda> Agendamentos { get; set; } = null!;
	}

	class Program
	{
		static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			
			var connectionString = builder.Configuration.GetConnectionString("Base") ?? "Data Source=base.db";
			builder.Services.AddSqlite<Base>(connectionString);
			
			var app = builder.Build();
			
            // Cadastrar Pessoa
			app.MapPost("/pessoa", (Base db, Pessoa pessoa) =>
			{
				db.Pessoas.Add(pessoa);
				db.SaveChanges();
				return "Pessoa adicionado";
			});
			// Listar todos as Pessoa
			app.MapGet("/pessoas", (Base db) => {
				return db.Pessoas.ToList();
			});
            // Listar Pessoas específico (por tipo)
			app.MapGet("/pessoas/{type}", (Base db, string type) => {
                if (type == "Veterinario") {
				    return db.Pessoas.Where(p => p.isVeterinario == true);
                } else {
				    return db.Pessoas.Where(p => p.isVeterinario == false);
                }
			});
			// Listar Pessoa específico (por id)
			app.MapGet("/pessoa/{id}", (Base db, int id) => {
				return db.Pessoas.Find(id);
			});
			// Deletar Pessoa específico (por id)
			app.MapDelete("/pessoa/{id}", (Base db, int id) => {
				var vet = db.Pessoas.Find(id);
				if (vet != null){					
					db.SaveChanges();
					db.Pessoas.Remove(vet);
                    return "Pessoa Removido";
				} else {
                    return "Pessoa não existe"; 
                }
			});
            // Atualizar Pessoa
			app.MapPut("/pessoa/{id}", (Base db, Pessoa pessoa, int id) =>
			{
				var vet = db.Pessoas.Find(id);
                if (vet != null)
                {
                    vet.nome = pessoa.nome;
                    vet.telefone = pessoa.telefone;
                    db.SaveChanges();
                    return "Pessoa atualizado";
                } else {
                    return "Pessoa não encontrado";
                }
			});

			// Cadastrar Paciente
			app.MapPost("/cadastrarPaciente", (Base db, Paciente paciente) =>
			{
				db.Pacientes.Add(paciente);
				db.SaveChanges();
				return "Paciente adicionado";
			});
            // Listar todos as Pacientes
			app.MapGet("/pacientes", (Base db) => {
				return db.Pacientes.ToList();
			});
            // Listar Paciente específico (por id)
			app.MapGet("/paciente/{id}", (Base db, int id) => {
				return db.Pacientes.Find(id);
			});
            // Deletar Paciente específico (por id)
			app.MapDelete("/paciente/{id}", (Base db, int id) => {
				var pac = db.Pacientes.Find(id);
				if (pac != null){					
					db.SaveChanges();
					db.Pacientes.Remove(pac);
                    return "Paciente Removido";
				} else {
                    return "Paciente não existe";
                }
			});
            // Atualizar Paciente
			app.MapPut("/paciente/{id}", (Base db, Paciente paciente, int id) =>
			{
				var pac = db.Pacientes.Find(id);
                if (pac != null)
                {
                    pac.nome = paciente.nome;
                    pac.especie = paciente.especie;
                    pac.raca = paciente.raca;
                    pac.sexo = paciente.sexo;
                    pac.idDono = paciente.idDono;

                    db.SaveChanges();
                    return "Paciente atualizado";
                } else {
					return "Paciente Indisponível";
                }	
			});

			// Cadastrar agendamento
			app.MapPost("/agendamento", (Base db, Agenda agenda) =>
			{
				// ConvertToDateTime(agenda);
                bool veterinarioIndisponivel = db.Agendamentos.Where(a => a.dataHora == Convert.ToDateTime(agenda.dataHora) && a.idVeterinario == agenda.idVeterinario).ToList().Count() > 0;

                if (!veterinarioIndisponivel) {
					db.Agendamentos.Add(agenda);
                    db.SaveChanges();
                    return "Agenda adicionada";
                } else {
                    return "Veterinário Indisponível";
                }
			});
            // Listar todos as Agendamentos
			app.MapGet("/agendamentos", (Base db) => {
				return db.Agendamentos.ToList();
			});
            // Listar Agendamento específico (por id)
			app.MapGet("/agendamento/{id}", (Base db, int id) => {
				return db.Agendamentos.Find(id);
			});
			// Deletar Adentamento
			app.MapDelete("/agendamento/{id}", (Base db, int id) =>
			{
                var agenda = db.Agendamentos.Find(id);

                if (agenda != null) {
					db.Agendamentos.Add(agenda);
                    db.SaveChanges();
                    return "Agenda removida";
                } else {
                    return "Agenda Indisponível";
                }
			});

			app.Run();
		}
	}
}