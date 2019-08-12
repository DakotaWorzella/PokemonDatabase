﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pokedex.DataAccess.Models;

namespace Pokedex.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190812135726_AddLegendaryType")]
    partial class AddLegendaryType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pokedex.DataAccess.Models.Ability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<bool>("IsArchived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.BaseHappiness", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Happiness");

                    b.Property<bool>("IsArchived");

                    b.HasKey("Id");

                    b.ToTable("BaseHappiness");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.BaseStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("Attack");

                    b.Property<short>("Defense");

                    b.Property<short>("Health");

                    b.Property<string>("PokemonId");

                    b.Property<short>("SpecialAttack");

                    b.Property<short>("SpecialDefense");

                    b.Property<short>("Speed");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.ToTable("BaseStats");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.CaptureRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("CatchRate");

                    b.Property<bool>("IsArchived");

                    b.HasKey("Id");

                    b.ToTable("CaptureRates");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Classification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Classifications");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.EggCycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("CycleCount");

                    b.Property<bool>("IsArchived");

                    b.HasKey("Id");

                    b.ToTable("EggCycles");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.EggGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("EggGroups");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Evolution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EvolutionDetails")
                        .HasMaxLength(200);

                    b.Property<int?>("EvolutionMethodId")
                        .IsRequired();

                    b.Property<string>("EvolutionPokemonId")
                        .IsRequired();

                    b.Property<string>("PreevolutionPokemonId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EvolutionMethodId");

                    b.HasIndex("EvolutionPokemonId");

                    b.HasIndex("PreevolutionPokemonId");

                    b.ToTable("Evolutions");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.EvolutionMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("EvolutionMethods");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.EVYield", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("Attack");

                    b.Property<short>("Defense");

                    b.Property<short>("Health");

                    b.Property<string>("PokemonId");

                    b.Property<short>("SpecialAttack");

                    b.Property<short>("SpecialDefense");

                    b.Property<short>("Speed");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.ToTable("EVYields");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.ExperienceGrowth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExpPointTotal");

                    b.Property<bool>("IsArchived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("ExperienceGrowths");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.GenderRatio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("FemaleRatio");

                    b.Property<bool>("IsArchived");

                    b.Property<double>("MaleRatio");

                    b.HasKey("Id");

                    b.ToTable("GenderRatios");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Generation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(4);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Games")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsArchived");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<DateTime>("ReleaseDate");

                    b.HasKey("Id");

                    b.ToTable("Generations");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.LegendaryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchived");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("LegendaryTypes");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Pokemon", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(6);

                    b.Property<int?>("BaseHappinessId")
                        .IsRequired();

                    b.Property<int?>("CaptureRateId")
                        .IsRequired();

                    b.Property<int?>("ClassificationId")
                        .IsRequired();

                    b.Property<int?>("EggCycleId")
                        .IsRequired();

                    b.Property<int>("ExpYield");

                    b.Property<int?>("ExperienceGrowthId")
                        .IsRequired();

                    b.Property<int?>("GenderRatioId")
                        .IsRequired();

                    b.Property<string>("GenerationId")
                        .IsRequired();

                    b.Property<decimal>("Height");

                    b.Property<bool>("IsArchived");

                    b.Property<bool>("IsComplete");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<decimal>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("BaseHappinessId");

                    b.HasIndex("CaptureRateId");

                    b.HasIndex("ClassificationId");

                    b.HasIndex("EggCycleId");

                    b.HasIndex("ExperienceGrowthId");

                    b.HasIndex("GenderRatioId");

                    b.HasIndex("GenerationId");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonAbilityDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HiddenAbilityId");

                    b.Property<string>("PokemonId")
                        .IsRequired();

                    b.Property<int?>("PrimaryAbilityId")
                        .IsRequired();

                    b.Property<int?>("SecondaryAbilityId");

                    b.HasKey("Id");

                    b.HasIndex("HiddenAbilityId");

                    b.HasIndex("PokemonId");

                    b.HasIndex("PrimaryAbilityId");

                    b.HasIndex("SecondaryAbilityId");

                    b.ToTable("PokemonAbilityDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonEggGroupDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PokemonId")
                        .IsRequired();

                    b.Property<int?>("PrimaryEggGroupId")
                        .IsRequired();

                    b.Property<int?>("SecondaryEggGroupId");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.HasIndex("PrimaryEggGroupId");

                    b.HasIndex("SecondaryEggGroupId");

                    b.ToTable("PokemonEggGroupDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonFormDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AltFormPokemonId")
                        .IsRequired();

                    b.Property<int>("FormId");

                    b.Property<bool>("IsArchived");

                    b.Property<string>("OriginalPokemonId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AltFormPokemonId");

                    b.HasIndex("FormId");

                    b.HasIndex("OriginalPokemonId");

                    b.ToTable("PokemonFormDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonLegendaryDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchived");

                    b.Property<int>("LegendaryTypeId");

                    b.Property<string>("PokemonId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LegendaryTypeId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonLegendaryDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTypeDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PokemonId")
                        .IsRequired();

                    b.Property<int?>("PrimaryTypeId")
                        .IsRequired();

                    b.Property<int?>("SecondaryTypeId");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.HasIndex("PrimaryTypeId");

                    b.HasIndex("SecondaryTypeId");

                    b.ToTable("PokemonTypeDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.ShinyHunt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenerationId")
                        .IsRequired();

                    b.Property<bool>("HasShinyCharm");

                    b.Property<bool>("HuntComplete");

                    b.Property<bool>("HuntRetried");

                    b.Property<bool>("IsArchived");

                    b.Property<bool>("IsPokemonCaught");

                    b.Property<string>("PokemonId")
                        .IsRequired();

                    b.Property<int>("ShinyAttemptCount");

                    b.Property<int?>("ShinyHuntingTechniqueId")
                        .IsRequired();

                    b.Property<int?>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GenerationId");

                    b.HasIndex("PokemonId");

                    b.HasIndex("ShinyHuntingTechniqueId");

                    b.HasIndex("UserId");

                    b.ToTable("ShinyHunts");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.ShinyHuntingTechnique", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchived");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Technique")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ShinyHuntingTechniques");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchived");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.TypeChart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttackId");

                    b.Property<int>("DefendId");

                    b.Property<decimal>("Effective");

                    b.HasKey("Id");

                    b.HasIndex("AttackId");

                    b.HasIndex("DefendId");

                    b.ToTable("TypeCharts");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsArchived");

                    b.Property<bool>("IsOwner");

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.BaseStat", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Evolution", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.EvolutionMethod", "EvolutionMethod")
                        .WithMany()
                        .HasForeignKey("EvolutionMethodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "EvolutionPokemon")
                        .WithMany()
                        .HasForeignKey("EvolutionPokemonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "PreevolutionPokemon")
                        .WithMany()
                        .HasForeignKey("PreevolutionPokemonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.EVYield", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Pokemon", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.BaseHappiness", "BaseHappiness")
                        .WithMany()
                        .HasForeignKey("BaseHappinessId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.CaptureRate", "CaptureRate")
                        .WithMany()
                        .HasForeignKey("CaptureRateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Classification", "Classification")
                        .WithMany()
                        .HasForeignKey("ClassificationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.EggCycle", "EggCycle")
                        .WithMany()
                        .HasForeignKey("EggCycleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.ExperienceGrowth", "ExperienceGrowth")
                        .WithMany()
                        .HasForeignKey("ExperienceGrowthId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.GenderRatio", "GenderRatio")
                        .WithMany()
                        .HasForeignKey("GenderRatioId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Generation", "Generation")
                        .WithMany()
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonAbilityDetail", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Ability", "HiddenAbility")
                        .WithMany()
                        .HasForeignKey("HiddenAbilityId");

                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Ability", "PrimaryAbility")
                        .WithMany()
                        .HasForeignKey("PrimaryAbilityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Ability", "SecondaryAbility")
                        .WithMany()
                        .HasForeignKey("SecondaryAbilityId");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonEggGroupDetail", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.EggGroup", "PrimaryEggGroup")
                        .WithMany()
                        .HasForeignKey("PrimaryEggGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.EggGroup", "SecondaryEggGroup")
                        .WithMany()
                        .HasForeignKey("SecondaryEggGroupId");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonFormDetail", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "AltFormPokemon")
                        .WithMany()
                        .HasForeignKey("AltFormPokemonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Form", "Form")
                        .WithMany()
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "OriginalPokemon")
                        .WithMany()
                        .HasForeignKey("OriginalPokemonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonLegendaryDetail", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.LegendaryType", "LegendaryType")
                        .WithMany()
                        .HasForeignKey("LegendaryTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTypeDetail", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Type", "PrimaryType")
                        .WithMany()
                        .HasForeignKey("PrimaryTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Type", "SecondaryType")
                        .WithMany()
                        .HasForeignKey("SecondaryTypeId");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.ShinyHunt", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Generation", "Generation")
                        .WithMany()
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.ShinyHuntingTechnique", "ShinyHuntingTechnique")
                        .WithMany()
                        .HasForeignKey("ShinyHuntingTechniqueId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.TypeChart", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Type", "Attack")
                        .WithMany()
                        .HasForeignKey("AttackId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Type", "Defend")
                        .WithMany()
                        .HasForeignKey("DefendId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
