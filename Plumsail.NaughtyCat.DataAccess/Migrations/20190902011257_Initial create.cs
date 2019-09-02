﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Plumsail.NaughtyCat.DataAccess.Migrations
{
    public partial class Initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					CreateDate = table.Column<DateTime>(nullable: false),
					UpdateDate = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					UserName = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
					Email = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(nullable: false),
					PasswordHash = table.Column<string>(nullable: true),
					SecurityStamp = table.Column<string>(nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					PhoneNumber = table.Column<string>(nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(nullable: false),
					TwoFactorEnabled = table.Column<bool>(nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
					LockoutEnabled = table.Column<bool>(nullable: false),
					AccessFailedCount = table.Column<int>(nullable: false),
					CreateDate = table.Column<DateTime>(nullable: false),
					UpdateDate = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "RabbitDelicacy",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RabbitDelicacy", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "RabbitPriority",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RabbitPriority", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RoleId = table.Column<int>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					UserId = table.Column<int>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(nullable: false),
					ProviderKey = table.Column<string>(nullable: false),
					ProviderDisplayName = table.Column<string>(nullable: true),
					UserId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					RoleId = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<int>(nullable: false),
					LoginProvider = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Value = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AspNetUserTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Rabbits",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					CreateDate = table.Column<DateTime>(nullable: false),
					UpdateDate = table.Column<DateTime>(nullable: false),
					Name = table.Column<string>(nullable: true),
					Age = table.Column<int>(nullable: false),
					Color = table.Column<string>(nullable: true),
					IdRabbitPriority = table.Column<int>(nullable: true),
					IdRabbitDelicacy = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Rabbits", x => x.Id);
					table.ForeignKey(
						name: "FK_Rabbits_RabbitDelicacy_IdRabbitDelicacy",
						column: x => x.IdRabbitDelicacy,
						principalTable: "RabbitDelicacy",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Rabbits_RabbitPriority_IdRabbitPriority",
						column: x => x.IdRabbitPriority,
						principalTable: "RabbitPriority",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.InsertData(
				table: "RabbitDelicacy",
				columns: new[] { "Id", "Description", "Name" },
				values: new object[,]
				{
					{ 1, "LotOfBones", "LotOfBones" },
					{ 2, "GonnaStruggle", "GonnaStruggle" },
					{ 3, "PrettyGood", "PrettyGood" },
					{ 4, "NiceToHave", "NiceToHave" },
					{ 5, "SuperTasty", "SuperTasty" }
				});

			migrationBuilder.InsertData(
				table: "RabbitPriority",
				columns: new[] { "Id", "Description", "Name" },
				values: new object[,]
				{
					{ 1, "Worst", "Worst" },
					{ 2, "RatherNot", "RatherNot" },
					{ 3, "Average", "Average" },
					{ 4, "NotBad", "NotBad" },
					{ 5, "Highest", "Highest" }
				});

			migrationBuilder.CreateIndex(
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_RabbitDelicacy_Name",
				table: "RabbitDelicacy",
				column: "Name",
				unique: true,
				filter: "[Name] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_RabbitPriority_Name",
				table: "RabbitPriority",
				column: "Name",
				unique: true,
				filter: "[Name] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Rabbits_IdRabbitDelicacy",
				table: "Rabbits",
				column: "IdRabbitDelicacy");

			migrationBuilder.CreateIndex(
				name: "IX_Rabbits_IdRabbitPriority",
				table: "Rabbits",
				column: "IdRabbitPriority");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Rabbits");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RabbitDelicacy");

            migrationBuilder.DropTable(
                name: "RabbitPriority");
        }
    }
}
