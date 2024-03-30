using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlockyHeroesBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    GachaType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GachaBanners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GachaBanners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(128)", maxLength: 128, nullable: false),
                    Coins = table.Column<long>(type: "bigint", nullable: false),
                    Stamina = table.Column<int>(type: "int", nullable: false),
                    MaxStamina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Lives = table.Column<int>(type: "int", nullable: false),
                    JumpForce = table.Column<float>(type: "real", nullable: false),
                    HorizontalSpeed = table.Column<float>(type: "real", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterLevels_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CoinsToPromote = table.Column<long>(type: "bigint", nullable: false),
                    Lives = table.Column<int>(type: "int", nullable: false),
                    JumpForce = table.Column<float>(type: "real", nullable: false),
                    HorizontalSpeed = table.Column<float>(type: "real", nullable: false),
                    EquipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipLevels_Equips_EquipId",
                        column: x => x.EquipId,
                        principalTable: "Equips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BannerDropRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    DropRate = table.Column<float>(type: "real", nullable: false),
                    GachaBannerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerDropRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerDropRates_GachaBanners_GachaBannerId",
                        column: x => x.GachaBannerId,
                        principalTable: "GachaBanners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GachaBannerCurrency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityPerPull = table.Column<int>(type: "int", nullable: false),
                    GachaBannerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GachaBannerCurrency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GachaBannerCurrency_GachaBanners_GachaBannerId",
                        column: x => x.GachaBannerId,
                        principalTable: "GachaBanners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GachaBannerCurrency_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterLevelRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CharacterLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterLevelRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterLevelRequirements_CharacterLevels_CharacterLevelId",
                        column: x => x.CharacterLevelId,
                        principalTable: "CharacterLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterLevelRequirements_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GachaBannerCharacter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RateUp = table.Column<float>(type: "real", nullable: false),
                    GachaBannerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GachaBannerCharacter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GachaBannerCharacter_CharacterLevels_CharacterLevelId",
                        column: x => x.CharacterLevelId,
                        principalTable: "CharacterLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GachaBannerCharacter_GachaBanners_GachaBannerId",
                        column: x => x.GachaBannerId,
                        principalTable: "GachaBanners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEquipments_EquipLevels_EquipLevelId",
                        column: x => x.EquipLevelId,
                        principalTable: "EquipLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEquipments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCharacters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEquipmentIdSlot1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserEquipmentIdSlot2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCharacters_CharacterLevels_CharacterLevelId",
                        column: x => x.CharacterLevelId,
                        principalTable: "CharacterLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCharacters_UserEquipments_UserEquipmentIdSlot1",
                        column: x => x.UserEquipmentIdSlot1,
                        principalTable: "UserEquipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCharacters_UserEquipments_UserEquipmentIdSlot2",
                        column: x => x.UserEquipmentIdSlot2,
                        principalTable: "UserEquipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCharacters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BannerDropRates_GachaBannerId",
                table: "BannerDropRates",
                column: "GachaBannerId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterLevelRequirements_CharacterLevelId",
                table: "CharacterLevelRequirements",
                column: "CharacterLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterLevelRequirements_ItemId",
                table: "CharacterLevelRequirements",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterLevels_CharacterId",
                table: "CharacterLevels",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipLevels_EquipId",
                table: "EquipLevels",
                column: "EquipId");

            migrationBuilder.CreateIndex(
                name: "IX_GachaBannerCharacter_CharacterLevelId",
                table: "GachaBannerCharacter",
                column: "CharacterLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_GachaBannerCharacter_GachaBannerId",
                table: "GachaBannerCharacter",
                column: "GachaBannerId");

            migrationBuilder.CreateIndex(
                name: "IX_GachaBannerCurrency_GachaBannerId",
                table: "GachaBannerCurrency",
                column: "GachaBannerId");

            migrationBuilder.CreateIndex(
                name: "IX_GachaBannerCurrency_ItemId",
                table: "GachaBannerCurrency",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCharacters_CharacterLevelId",
                table: "UserCharacters",
                column: "CharacterLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCharacters_UserEquipmentIdSlot1",
                table: "UserCharacters",
                column: "UserEquipmentIdSlot1",
                unique: true,
                filter: "[UserEquipmentIdSlot1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserCharacters_UserEquipmentIdSlot2",
                table: "UserCharacters",
                column: "UserEquipmentIdSlot2",
                unique: true,
                filter: "[UserEquipmentIdSlot2] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserCharacters_UserId",
                table: "UserCharacters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEquipments_EquipLevelId",
                table: "UserEquipments",
                column: "EquipLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEquipments_UserId",
                table: "UserEquipments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_ItemId",
                table: "UserItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_UserId",
                table: "UserItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerDropRates");

            migrationBuilder.DropTable(
                name: "CharacterLevelRequirements");

            migrationBuilder.DropTable(
                name: "GachaBannerCharacter");

            migrationBuilder.DropTable(
                name: "GachaBannerCurrency");

            migrationBuilder.DropTable(
                name: "UserCharacters");

            migrationBuilder.DropTable(
                name: "UserItems");

            migrationBuilder.DropTable(
                name: "GachaBanners");

            migrationBuilder.DropTable(
                name: "CharacterLevels");

            migrationBuilder.DropTable(
                name: "UserEquipments");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "EquipLevels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Equips");
        }
    }
}
