﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trabalho;

#nullable disable

namespace Veterinaria.Migrations
{
    [DbContext(typeof(Base))]
    partial class BaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Trabalho.Agenda", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("dataHora")
                        .HasColumnType("TEXT");

                    b.Property<int>("idPaciente")
                        .HasColumnType("INTEGER");

                    b.Property<int>("idVeterinario")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Agendamentos");
                });

            modelBuilder.Entity("Trabalho.Paciente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("especie")
                        .HasColumnType("TEXT");

                    b.Property<int>("idDono")
                        .HasColumnType("INTEGER");

                    b.Property<string>("nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("raca")
                        .HasColumnType("TEXT");

                    b.Property<char?>("sexo")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Trabalho.Pessoa", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isVeterinario")
                        .HasColumnType("INTEGER");

                    b.Property<string>("nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("telefone")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Pessoas");
                });
#pragma warning restore 612, 618
        }
    }
}
