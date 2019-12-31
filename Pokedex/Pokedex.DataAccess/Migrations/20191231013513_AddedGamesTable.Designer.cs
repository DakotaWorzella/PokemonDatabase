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
    [Migration("20191231013513_AddedGamesTable")]
    partial class AddedGamesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pokedex.DataAccess.Models.Ability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300);

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

                    b.Property<int>("PokemonId");

                    b.Property<short>("SpecialAttack");

                    b.Property<short>("SpecialDefense");

                    b.Property<short>("Speed");

                    b.HasKey("Id");

                    b.ToTable("BaseStats");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.BattleItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GenerationId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("OnlyInThisGeneration");

                    b.Property<int?>("PokemonId");

                    b.HasKey("Id");

                    b.ToTable("BattleItems");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.CaptureRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("CatchRate");

                    b.HasKey("Id");

                    b.ToTable("CaptureRates");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Classification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Classifications");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentType");

                    b.Property<string>("CommentedPage");

                    b.Property<int?>("CommentorId");

                    b.Property<bool>("IsCompleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("PokemonName");

                    b.HasKey("Id");

                    b.HasIndex("CommentorId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.EVYield", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("Attack");

                    b.Property<short>("Defense");

                    b.Property<short>("Health");

                    b.Property<int>("PokemonId");

                    b.Property<short>("SpecialAttack");

                    b.Property<short>("SpecialDefense");

                    b.Property<short>("Speed");

                    b.HasKey("Id");

                    b.ToTable("EVYields");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.EggCycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("CycleCount");

                    b.HasKey("Id");

                    b.ToTable("EggCycles");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.EggGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<int>("EvolutionPokemonId");

                    b.Property<int>("PreevolutionPokemonId");

                    b.HasKey("Id");

                    b.HasIndex("EvolutionMethodId");

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

            modelBuilder.Entity("Pokedex.DataAccess.Models.ExperienceGrowth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExpPointTotal");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.FormItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("PokemonId");

                    b.HasKey("Id");

                    b.ToTable("FormItems");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<int>("GenerationId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("ReleaseDate");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.GenderRatio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("FemaleRatio");

                    b.Property<double>("MaleRatio");

                    b.HasKey("Id");

                    b.ToTable("GenderRatios");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.LegendaryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("LegendaryTypes");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRead");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("MessageTitle")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("ReceiverId")
                        .IsRequired();

                    b.Property<int?>("SenderId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte?>("Accuracy");

                    b.Property<byte?>("BasePower");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("MoveCategoryId");

                    b.Property<int>("MoveTypeId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte>("PP");

                    b.HasKey("Id");

                    b.HasIndex("MoveCategoryId");

                    b.HasIndex("MoveTypeId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.MoveCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("MoveCategories");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Nature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Natures");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonAbilityDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HiddenAbilityId");

                    b.Property<int>("PokemonId");

                    b.Property<int?>("PrimaryAbilityId")
                        .IsRequired();

                    b.Property<int?>("SecondaryAbilityId");

                    b.Property<int?>("SpecialEventAbilityId");

                    b.HasKey("Id");

                    b.HasIndex("HiddenAbilityId");

                    b.HasIndex("PrimaryAbilityId");

                    b.HasIndex("SecondaryAbilityId");

                    b.HasIndex("SpecialEventAbilityId");

                    b.ToTable("PokemonAbilityDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonEggGroupDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PokemonId");

                    b.Property<int?>("PrimaryEggGroupId")
                        .IsRequired();

                    b.Property<int?>("SecondaryEggGroupId");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryEggGroupId");

                    b.HasIndex("SecondaryEggGroupId");

                    b.ToTable("PokemonEggGroupDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonFormDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AltFormPokemonId");

                    b.Property<int>("FormId");

                    b.Property<int>("OriginalPokemonId");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("PokemonFormDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonGameDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId");

                    b.Property<int>("PokemonId");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("PokemonGameDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonLegendaryDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LegendaryTypeId");

                    b.Property<int>("PokemonId");

                    b.HasKey("Id");

                    b.HasIndex("LegendaryTypeId");

                    b.ToTable("PokemonLegendaryDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FifthPokemonId");

                    b.Property<int?>("FirstPokemonId");

                    b.Property<int?>("FourthPokemonId");

                    b.Property<int?>("GameId");

                    b.Property<string>("PokemonTeamName")
                        .IsRequired();

                    b.Property<int?>("SecondPokemonId");

                    b.Property<int?>("SixthPokemonId");

                    b.Property<int?>("ThirdPokemonId");

                    b.Property<int?>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FifthPokemonId");

                    b.HasIndex("FirstPokemonId");

                    b.HasIndex("FourthPokemonId");

                    b.HasIndex("GameId");

                    b.HasIndex("SecondPokemonId");

                    b.HasIndex("SixthPokemonId");

                    b.HasIndex("ThirdPokemonId");

                    b.HasIndex("UserId");

                    b.ToTable("PokemonTeams");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTeamDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AbilityId")
                        .IsRequired();

                    b.Property<int?>("BattleItemId");

                    b.Property<string>("Gender")
                        .HasMaxLength(6);

                    b.Property<byte>("Happiness");

                    b.Property<bool>("IsShiny");

                    b.Property<byte>("Level");

                    b.Property<int?>("NatureId")
                        .IsRequired();

                    b.Property<string>("Nickname");

                    b.Property<int>("PokemonId");

                    b.Property<int?>("PokemonTeamEVId");

                    b.Property<int?>("PokemonTeamIVId");

                    b.Property<int?>("PokemonTeamMovesetId");

                    b.HasKey("Id");

                    b.HasIndex("AbilityId");

                    b.HasIndex("BattleItemId");

                    b.HasIndex("NatureId");

                    b.HasIndex("PokemonTeamEVId");

                    b.HasIndex("PokemonTeamIVId");

                    b.HasIndex("PokemonTeamMovesetId");

                    b.ToTable("PokemonTeamDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTeamEV", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Attack");

                    b.Property<byte>("Defense");

                    b.Property<byte>("Health");

                    b.Property<byte>("SpecialAttack");

                    b.Property<byte>("SpecialDefense");

                    b.Property<byte>("Speed");

                    b.HasKey("Id");

                    b.ToTable("PokemonTeamEVs");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTeamIV", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Attack");

                    b.Property<byte>("Defense");

                    b.Property<byte>("Health");

                    b.Property<byte>("SpecialAttack");

                    b.Property<byte>("SpecialDefense");

                    b.Property<byte>("Speed");

                    b.HasKey("Id");

                    b.ToTable("PokemonTeamIVs");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTeamMoveset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstMove");

                    b.Property<string>("FourthMove");

                    b.Property<string>("SecondMove");

                    b.Property<string>("ThirdMove");

                    b.HasKey("Id");

                    b.ToTable("PokemonTeamMovesets");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTypeDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PokemonId");

                    b.Property<int?>("PrimaryTypeId")
                        .IsRequired();

                    b.Property<int?>("SecondaryTypeId");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryTypeId");

                    b.HasIndex("SecondaryTypeId");

                    b.ToTable("PokemonTypeDetails");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.ReviewedPokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PokemonId")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("ReviewedPokemons");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.ShinyHunt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId");

                    b.Property<bool>("HasShinyCharm");

                    b.Property<bool>("HuntComplete");

                    b.Property<bool>("IsPokemonCaught");

                    b.Property<int>("PokemonId");

                    b.Property<int>("ShinyAttemptCount");

                    b.Property<int?>("ShinyHuntingTechniqueId")
                        .IsRequired();

                    b.Property<int?>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("ShinyHuntingTechniqueId");

                    b.HasIndex("UserId");

                    b.ToTable("ShinyHunts");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.ShinyHuntingTechnique", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<decimal>("Effective")
                        .HasColumnType("decimal(2,1)");

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

                    b.Property<bool>("IsOwner");

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Comment", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.User", "Commentor")
                        .WithMany()
                        .HasForeignKey("CommentorId");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Evolution", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.EvolutionMethod", "EvolutionMethod")
                        .WithMany()
                        .HasForeignKey("EvolutionMethodId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Message", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.Move", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.MoveCategory", "MoveCategory")
                        .WithMany()
                        .HasForeignKey("MoveCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Type", "MoveType")
                        .WithMany()
                        .HasForeignKey("MoveTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonAbilityDetail", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Ability", "HiddenAbility")
                        .WithMany()
                        .HasForeignKey("HiddenAbilityId");

                    b.HasOne("Pokedex.DataAccess.Models.Ability", "PrimaryAbility")
                        .WithMany()
                        .HasForeignKey("PrimaryAbilityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.Ability", "SecondaryAbility")
                        .WithMany()
                        .HasForeignKey("SecondaryAbilityId");

                    b.HasOne("Pokedex.DataAccess.Models.Ability", "SpecialEventAbility")
                        .WithMany()
                        .HasForeignKey("SpecialEventAbilityId");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonEggGroupDetail", b =>
                {
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
                    b.HasOne("Pokedex.DataAccess.Models.Form", "Form")
                        .WithMany()
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonGameDetail", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonLegendaryDetail", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.LegendaryType", "LegendaryType")
                        .WithMany()
                        .HasForeignKey("LegendaryTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTeam", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.PokemonTeamDetail", "FifthPokemon")
                        .WithMany()
                        .HasForeignKey("FifthPokemonId");

                    b.HasOne("Pokedex.DataAccess.Models.PokemonTeamDetail", "FirstPokemon")
                        .WithMany()
                        .HasForeignKey("FirstPokemonId");

                    b.HasOne("Pokedex.DataAccess.Models.PokemonTeamDetail", "FourthPokemon")
                        .WithMany()
                        .HasForeignKey("FourthPokemonId");

                    b.HasOne("Pokedex.DataAccess.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("Pokedex.DataAccess.Models.PokemonTeamDetail", "SecondPokemon")
                        .WithMany()
                        .HasForeignKey("SecondPokemonId");

                    b.HasOne("Pokedex.DataAccess.Models.PokemonTeamDetail", "SixthPokemon")
                        .WithMany()
                        .HasForeignKey("SixthPokemonId");

                    b.HasOne("Pokedex.DataAccess.Models.PokemonTeamDetail", "ThirdPokemon")
                        .WithMany()
                        .HasForeignKey("ThirdPokemonId");

                    b.HasOne("Pokedex.DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTeamDetail", b =>
                {
                    b.HasOne("Pokedex.DataAccess.Models.Ability", "Ability")
                        .WithMany()
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.BattleItem", "BattleItem")
                        .WithMany()
                        .HasForeignKey("BattleItemId");

                    b.HasOne("Pokedex.DataAccess.Models.Nature", "Nature")
                        .WithMany()
                        .HasForeignKey("NatureId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokedex.DataAccess.Models.PokemonTeamEV", "PokemonTeamEV")
                        .WithMany()
                        .HasForeignKey("PokemonTeamEVId");

                    b.HasOne("Pokedex.DataAccess.Models.PokemonTeamIV", "PokemonTeamIV")
                        .WithMany()
                        .HasForeignKey("PokemonTeamIVId");

                    b.HasOne("Pokedex.DataAccess.Models.PokemonTeamMoveset", "PokemonTeamMoveset")
                        .WithMany()
                        .HasForeignKey("PokemonTeamMovesetId");
                });

            modelBuilder.Entity("Pokedex.DataAccess.Models.PokemonTypeDetail", b =>
                {
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
                    b.HasOne("Pokedex.DataAccess.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
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
