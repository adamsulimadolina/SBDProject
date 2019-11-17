using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Miasto",
                columns: table => new
                {
                    CityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miasto", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Mieszkanie",
                columns: table => new
                {
                    FlatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomsCount = table.Column<int>(nullable: false),
                    BathroomCount = table.Column<int>(nullable: false),
                    Surface = table.Column<double>(nullable: false),
                    KitchenType = table.Column<string>(maxLength: 20, nullable: true),
                    CityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mieszkanie", x => x.FlatID);
                    table.ForeignKey(
                        name: "FK_Mieszkanie_Miasto_CityID",
                        column: x => x.CityID,
                        principalTable: "Miasto",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logi",
                columns: table => new
                {
                    LogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageDate = table.Column<DateTime>(nullable: true),
                    Log = table.Column<string>(maxLength: 50, nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logi", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_Logi_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Najemca",
                columns: table => new
                {
                    TenantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Surname = table.Column<string>(maxLength: 30, nullable: true),
                    Age = table.Column<int>(nullable: false),
                    IsSmoking = table.Column<bool>(nullable: false),
                    IsVege = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Najemca", x => x.TenantID);
                    table.ForeignKey(
                        name: "FK_Najemca_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wiadomości",
                columns: table => new
                {
                    MessageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(maxLength: 256, nullable: true),
                    MessageDate = table.Column<DateTime>(nullable: true),
                    UserSenderID = table.Column<int>(nullable: false),
                    UserReceiverID = table.Column<int>(nullable: false),
                    UserSUserID = table.Column<int>(nullable: true),
                    UserRUserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wiadomości", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Wiadomości_User_UserRUserID",
                        column: x => x.UserRUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wiadomości_User_UserSUserID",
                        column: x => x.UserSUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wlasciciel",
                columns: table => new
                {
                    OwnerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Surname = table.Column<string>(maxLength: 30, nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wlasciciel", x => x.OwnerID);
                    table.ForeignKey(
                        name: "FK_Wlasciciel_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pokoj",
                columns: table => new
                {
                    RoomID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Surface = table.Column<double>(nullable: false),
                    Balcony = table.Column<bool>(nullable: false),
                    Bed = table.Column<bool>(nullable: false),
                    Wardrobe = table.Column<bool>(nullable: false),
                    AdditionalInfo = table.Column<string>(maxLength: 40, nullable: true),
                    Rent = table.Column<double>(nullable: false),
                    FlatID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokoj", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Pokoj_Mieszkanie_FlatID",
                        column: x => x.FlatID,
                        principalTable: "Mieszkanie",
                        principalColumn: "FlatID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parowanie",
                columns: table => new
                {
                    PairID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PairCompatibility = table.Column<double>(nullable: false),
                    TenantID_1 = table.Column<int>(nullable: false),
                    TenantID_2 = table.Column<int>(nullable: false),
                    Tenant_1TenantID = table.Column<int>(nullable: true),
                    Tenant_2TenantID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parowanie", x => x.PairID);
                    table.ForeignKey(
                        name: "FK_Parowanie_Najemca_Tenant_1TenantID",
                        column: x => x.Tenant_1TenantID,
                        principalTable: "Najemca",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parowanie_Najemca_Tenant_2TenantID",
                        column: x => x.Tenant_2TenantID,
                        principalTable: "Najemca",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ogłoszenie",
                columns: table => new
                {
                    AdvertisementID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvertisementType = table.Column<string>(maxLength: 40, nullable: true),
                    OwnerID = table.Column<int>(nullable: false),
                    FlatID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogłoszenie", x => x.AdvertisementID);
                    table.ForeignKey(
                        name: "FK_Ogłoszenie_Mieszkanie_FlatID",
                        column: x => x.FlatID,
                        principalTable: "Mieszkanie",
                        principalColumn: "FlatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogłoszenie_Wlasciciel_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Wlasciciel",
                        principalColumn: "OwnerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opinie",
                columns: table => new
                {
                    OpinionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Opinion = table.Column<string>(maxLength: 250, nullable: true),
                    OwnerID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinie", x => x.OpinionID);
                    table.ForeignKey(
                        name: "FK_Opinie_Wlasciciel_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Wlasciciel",
                        principalColumn: "OwnerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opinie_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logi_UserID",
                table: "Logi",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Mieszkanie_CityID",
                table: "Mieszkanie",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Najemca_UserID",
                table: "Najemca",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Ogłoszenie_FlatID",
                table: "Ogłoszenie",
                column: "FlatID");

            migrationBuilder.CreateIndex(
                name: "IX_Ogłoszenie_OwnerID",
                table: "Ogłoszenie",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Opinie_OwnerID",
                table: "Opinie",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Opinie_UserID",
                table: "Opinie",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Parowanie_Tenant_1TenantID",
                table: "Parowanie",
                column: "Tenant_1TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Parowanie_Tenant_2TenantID",
                table: "Parowanie",
                column: "Tenant_2TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Pokoj_FlatID",
                table: "Pokoj",
                column: "FlatID");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomości_UserRUserID",
                table: "Wiadomości",
                column: "UserRUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomości_UserSUserID",
                table: "Wiadomości",
                column: "UserSUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Wlasciciel_UserID",
                table: "Wlasciciel",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logi");

            migrationBuilder.DropTable(
                name: "Ogłoszenie");

            migrationBuilder.DropTable(
                name: "Opinie");

            migrationBuilder.DropTable(
                name: "Parowanie");

            migrationBuilder.DropTable(
                name: "Pokoj");

            migrationBuilder.DropTable(
                name: "Wiadomości");

            migrationBuilder.DropTable(
                name: "Wlasciciel");

            migrationBuilder.DropTable(
                name: "Najemca");

            migrationBuilder.DropTable(
                name: "Mieszkanie");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Miasto");
        }
    }
}
