﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Music4Every1.Server.Data;

#nullable disable

namespace Music4Every1.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Music4Every1.Shared.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeilaoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeilaoId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Music4Every1.Shared.Leilao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompradorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duracao")
                        .HasColumnType("time");

                    b.Property<double?>("PrecoCompraImediata")
                        .HasColumnType("float");

                    b.Property<double>("PrecoInicial")
                        .HasColumnType("float");

                    b.Property<int>("VendedorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompradorId");

                    b.HasIndex("VendedorId");

                    b.ToTable("Leiloes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataInicio = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Guitarra",
                            Duracao = new TimeSpan(1, 0, 0, 0, 0),
                            PrecoCompraImediata = 200.0,
                            PrecoInicial = 100.0,
                            VendedorId = 1
                        },
                        new
                        {
                            Id = 2,
                            DataInicio = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Bateria",
                            Duracao = new TimeSpan(1, 0, 0, 0, 0),
                            PrecoCompraImediata = 200.0,
                            PrecoInicial = 100.0,
                            VendedorId = 2
                        },
                        new
                        {
                            Id = 3,
                            DataInicio = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Piano",
                            Duracao = new TimeSpan(1, 0, 0, 0, 0),
                            PrecoCompraImediata = 200.0,
                            PrecoInicial = 100.0,
                            VendedorId = 3
                        },
                        new
                        {
                            Id = 4,
                            DataInicio = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Violino",
                            Duracao = new TimeSpan(1, 0, 0, 0, 0),
                            PrecoCompraImediata = 200.0,
                            PrecoInicial = 100.0,
                            VendedorId = 4
                        },
                        new
                        {
                            Id = 5,
                            DataInicio = new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Descricao = "Saxofone",
                            Duracao = new TimeSpan(1, 0, 0, 0, 0),
                            PrecoCompraImediata = 200.0,
                            PrecoInicial = 100.0,
                            VendedorId = 5
                        });
                });

            modelBuilder.Entity("Music4Every1.Shared.Utilizador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Saldo")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Utilizadores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "João",
                            Password = "123",
                            Saldo = 1000.0
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Maria",
                            Password = "123",
                            Saldo = 1000.0
                        },
                        new
                        {
                            Id = 3,
                            Nome = "José",
                            Password = "123",
                            Saldo = 1000.0
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Ana",
                            Password = "123",
                            Saldo = 1000.0
                        },
                        new
                        {
                            Id = 5,
                            Nome = "Carlos",
                            Password = "123",
                            Saldo = 1000.0
                        });
                });

            modelBuilder.Entity("Music4Every1.Shared.Item", b =>
                {
                    b.HasOne("Music4Every1.Shared.Leilao", "Leilao")
                        .WithMany("Itens")
                        .HasForeignKey("LeilaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Leilao");
                });

            modelBuilder.Entity("Music4Every1.Shared.Leilao", b =>
                {
                    b.HasOne("Music4Every1.Shared.Utilizador", "Comprador")
                        .WithMany()
                        .HasForeignKey("CompradorId");

                    b.HasOne("Music4Every1.Shared.Utilizador", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comprador");

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("Music4Every1.Shared.Leilao", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
