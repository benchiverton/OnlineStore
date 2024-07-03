﻿// <auto-generated />
using System;
using Company.Api.PetRocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Company.Api.Migrations
{
    [DbContext(typeof(PetRockContext))]
    partial class PetRockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Company.Api.PetRocks.Dtos.PetRockDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Catchphrase")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("VariantTypes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PetRocks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"),
                            Catchphrase = "Rolling through life one pebble at a time.",
                            Description = "Meet Pebble Dash, your adventurous little companion! Small but full of energy, Pebble Dash is always ready to roll into new adventures. With a smooth surface and a perfectly rounded shape, this pebble loves to explore the world and bring joy wherever it goes. Whether it's a playful dash across the desk or a calming presence on your nightstand, Pebble Dash is here to remind you that life's journey is best enjoyed one pebble at a time. Compact and full of charm, Pebble Dash is the perfect pocket-sized buddy for all your daily adventures.",
                            Images = "[\"images\\\\petrocks\\\\pebble_dash.png\"]",
                            Name = "Pebble Dash",
                            VariantTypes = "[\"Size\",\"Texture\"]"
                        },
                        new
                        {
                            Id = new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"),
                            Catchphrase = "Ready to rock and slate the day.",
                            Description = "Introducing Slate Mate, your steadfast and stylish rock friend! Crafted from sleek, smooth slate, Slate Mate is the epitome of cool and collected. With its flat, elegant surface and modern look, this rock is the perfect desk companion or decorative piece. Slate Mate is always there to offer unwavering support, whether you're tackling a tough task or relaxing after a long day. Its timeless appearance and dependable nature make Slate Mate a reliable friend for every moment. Embrace the stability and charm of Slate Mate, and let this solid companion help you slate the day!",
                            Images = "[\"images\\\\petrocks\\\\slate_mate.png\"]",
                            Name = "Slate Mate",
                            VariantTypes = "[\"Hardness\",\"Slateyness\"]"
                        });
                });

            modelBuilder.Entity("Company.Api.PetRocks.Dtos.VariantDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PetRockId")
                        .HasColumnType("TEXT");

                    b.Property<string>("VariantTypeValues")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PetRockId");

                    b.ToTable("Variants");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4e6a2f59-be6c-45fc-a815-4fddaa124088"),
                            PetRockId = new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"),
                            VariantTypeValues = "{\"Size\":\"Small\",\"Texture\":\"Smooth\"}"
                        },
                        new
                        {
                            Id = new Guid("e5e8504b-ad37-4c74-b5e7-6a1a1ee8074e"),
                            PetRockId = new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"),
                            VariantTypeValues = "{\"Size\":\"Small\",\"Texture\":\"Grainy\"}"
                        },
                        new
                        {
                            Id = new Guid("652ff3bf-4cdd-4fb1-80bb-3ec7ec6fc0dc"),
                            PetRockId = new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"),
                            VariantTypeValues = "{\"Size\":\"Small\",\"Texture\":\"Jagged\"}"
                        },
                        new
                        {
                            Id = new Guid("4f065f9a-8dbd-49ad-9098-5d95ca6e6b8c"),
                            PetRockId = new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"),
                            VariantTypeValues = "{\"Size\":\"Big\",\"Texture\":\"Smooth\"}"
                        },
                        new
                        {
                            Id = new Guid("5f9d8ca2-25d3-4d87-a509-13cfaa21eb21"),
                            PetRockId = new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"),
                            VariantTypeValues = "{\"Size\":\"Big\",\"Texture\":\"Grainy\"}"
                        },
                        new
                        {
                            Id = new Guid("50425cbd-7732-4b61-a820-4240a39bdc78"),
                            PetRockId = new Guid("9dd853c8-4776-4ab1-ad37-e90e9e523bcc"),
                            VariantTypeValues = "{\"Size\":\"Big\",\"Texture\":\"Jagged\"}"
                        },
                        new
                        {
                            Id = new Guid("6e055e20-9a49-4323-a1e6-e9c73386546c"),
                            PetRockId = new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"),
                            VariantTypeValues = "{\"Hardness\":\"Soft\",\"Slateyness\":\"Not very\"}"
                        },
                        new
                        {
                            Id = new Guid("a9f01390-0d4c-4f8c-bf0e-a2d8d0236735"),
                            PetRockId = new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"),
                            VariantTypeValues = "{\"Hardness\":\"Soft\",\"Slateyness\":\"Moderately slatey\"}"
                        },
                        new
                        {
                            Id = new Guid("9f214908-8b4e-4312-8768-3f942b96dfd7"),
                            PetRockId = new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"),
                            VariantTypeValues = "{\"Hardness\":\"Soft\",\"Slateyness\":\"Extra slatey\"}"
                        },
                        new
                        {
                            Id = new Guid("ca2f1e58-85f0-4a4a-bb88-e620a5835afe"),
                            PetRockId = new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"),
                            VariantTypeValues = "{\"Hardness\":\"Hard\",\"Slateyness\":\"Not very\"}"
                        },
                        new
                        {
                            Id = new Guid("f6d26e13-6519-42c1-b8d5-405a58598c84"),
                            PetRockId = new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"),
                            VariantTypeValues = "{\"Hardness\":\"Hard\",\"Slateyness\":\"Moderately slatey\"}"
                        },
                        new
                        {
                            Id = new Guid("5298d8cb-bd43-4b1d-be97-b2ad1197653e"),
                            PetRockId = new Guid("651ce7e2-0d9d-4ed9-81af-db7dc483e49a"),
                            VariantTypeValues = "{\"Hardness\":\"Hard\",\"Slateyness\":\"Extra slatey\"}"
                        });
                });

            modelBuilder.Entity("Company.Api.PetRocks.Dtos.VariantDto", b =>
                {
                    b.HasOne("Company.Api.PetRocks.Dtos.PetRockDto", "PetRock")
                        .WithMany("Variants")
                        .HasForeignKey("PetRockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PetRock");
                });

            modelBuilder.Entity("Company.Api.PetRocks.Dtos.PetRockDto", b =>
                {
                    b.Navigation("Variants");
                });
#pragma warning restore 612, 618
        }
    }
}
