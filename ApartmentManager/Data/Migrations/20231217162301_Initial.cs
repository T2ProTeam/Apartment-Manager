using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Acc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Birthday = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IDCard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Staked = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectricMoneyOfGovernment = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    WaterMoneyOfGovernment = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    ServiceOfGovernment = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Month = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DepositsContract",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDRoom = table.Column<int>(type: "int", nullable: false),
                    IDLeader = table.Column<int>(type: "int", nullable: false),
                    DepositsDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Money = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositsContract", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DepositsContract_People_IDLeader",
                        column: x => x.IDLeader,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepositsContract_Room_IDRoom",
                        column: x => x.IDRoom,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentalContract",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDroom = table.Column<int>(type: "int", nullable: false),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    RoomMoney = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    ElectricMoney = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    WaterMoney = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    ServiceMoney = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    PeopleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalContract", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RentalContract_People_PeopleID",
                        column: x => x.PeopleID,
                        principalTable: "People",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RentalContract_Room_IDroom",
                        column: x => x.IDroom,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomDetails",
                columns: table => new
                {
                    IDFur = table.Column<int>(type: "int", nullable: false),
                    IDRoom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDetails", x => new { x.IDFur, x.IDRoom });
                    table.ForeignKey(
                        name: "FK_RoomDetails_Furniture_IDFur",
                        column: x => x.IDFur,
                        principalTable: "Furniture",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomDetails_Room_IDRoom",
                        column: x => x.IDRoom,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomImage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDroom = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoomImage_Room_IDroom",
                        column: x => x.IDroom,
                        principalTable: "Room",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDRTC = table.Column<int>(type: "int", nullable: false),
                    ElectricQuantity = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Active = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    PayDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    TotalMoney = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bill_RentalContract_IDRTC",
                        column: x => x.IDRTC,
                        principalTable: "RentalContract",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeopleRental",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPeople = table.Column<int>(type: "int", nullable: false),
                    IDRental = table.Column<int>(type: "int", nullable: false),
                    Membership = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleRental", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PeopleRental_People_IDPeople",
                        column: x => x.IDPeople,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeopleRental_RentalContract_IDRental",
                        column: x => x.IDRental,
                        principalTable: "RentalContract",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentExtension",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDBill = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 12, 17, 23, 23, 1, 355, DateTimeKind.Local).AddTicks(8510))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentExtension", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentExtension_Bill_IDBill",
                        column: x => x.IDBill,
                        principalTable: "Bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "ID", "Acc", "Pass" },
                values: new object[] { 1, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Furniture",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Chair" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "ID", "Address", "Birthday", "Email", "IDCard", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Vietnam", new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8251), "thanh@gmail.com", "1234123", "Jonhny Deep", "1234" },
                    { 2, "USA", new DateTime(1993, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily@gmail.com", "56781234", "Emily Stone", "5678" },
                    { 3, "Canada", new DateTime(1991, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "robert@gmail.com", "910111213", "Robert Smith", "91011" },
                    { 4, "Australia", new DateTime(1993, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna@gmail.com", "14151617", "Anna Johnson", "1415" },
                    { 5, "UK", new DateTime(1997, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael@gmail.com", "18192021", "Michael Brown", "1819" }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "ID", "Name", "Quantity", "Staked" },
                values: new object[,]
                {
                    { 1, "A201", 4, 0 },
                    { 2, "A202", 5, 0 },
                    { 3, "A203", 3, 0 },
                    { 4, "A240", 3, 0 }
                });

            migrationBuilder.InsertData(
                table: "Statistics",
                columns: new[] { "ID", "ElectricMoneyOfGovernment", "Month", "ServiceOfGovernment", "WaterMoneyOfGovernment", "Year" },
                values: new object[] { 1, 90, 10, 90, 90, 2023 });

            migrationBuilder.InsertData(
                table: "DepositsContract",
                columns: new[] { "ID", "CheckOutDate", "DepositsDate", "IDLeader", "IDRoom", "Money", "ReceiveDate" },
                values: new object[] { 1, new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8193), new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8190), 1, 1, 10000, new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8191) });

            migrationBuilder.InsertData(
                table: "RentalContract",
                columns: new[] { "ID", "Active", "CheckOutDate", "ElectricMoney", "IDroom", "PeopleID", "ReceiveDate", "RoomMoney", "ServiceMoney", "WaterMoney" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8384), 100, 1, null, new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8383), 100, 100, 100 },
                    { 2, 1, new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8389), 100, 2, null, new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8388), 100, 100, 100 }
                });

            migrationBuilder.InsertData(
                table: "RoomImage",
                columns: new[] { "ID", "IDroom", "Name", "Url" },
                values: new object[,]
                {
                    { 1, 1, "A1 Image", "D:jett.png" },
                    { 2, 1, "A2 Image", "D:home.png" }
                });

            migrationBuilder.InsertData(
                table: "Bill",
                columns: new[] { "ID", "Active", "ElectricQuantity", "IDRTC", "PayDate", "TotalMoney" },
                values: new object[] { 1, 1, 150, 1, new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8165), 1000000.0 });

            migrationBuilder.InsertData(
                table: "PeopleRental",
                columns: new[] { "ID", "IDPeople", "IDRental", "Membership" },
                values: new object[,]
                {
                    { 1, 2, 1, 0 },
                    { 2, 3, 1, 1 },
                    { 3, 4, 1, 1 },
                    { 4, 5, 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "PaymentExtension",
                columns: new[] { "ID", "Days", "IDBill" },
                values: new object[] { 1, new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8228), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_IDRTC",
                table: "Bill",
                column: "IDRTC");

            migrationBuilder.CreateIndex(
                name: "IX_DepositsContract_IDLeader",
                table: "DepositsContract",
                column: "IDLeader");

            migrationBuilder.CreateIndex(
                name: "IX_DepositsContract_IDRoom",
                table: "DepositsContract",
                column: "IDRoom");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentExtension_IDBill",
                table: "PaymentExtension",
                column: "IDBill");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleRental_IDPeople",
                table: "PeopleRental",
                column: "IDPeople");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleRental_IDRental",
                table: "PeopleRental",
                column: "IDRental");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContract_IDroom",
                table: "RentalContract",
                column: "IDroom");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContract_PeopleID",
                table: "RentalContract",
                column: "PeopleID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomDetails_IDRoom",
                table: "RoomDetails",
                column: "IDRoom");

            migrationBuilder.CreateIndex(
                name: "IX_RoomImage_IDroom",
                table: "RoomImage",
                column: "IDroom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "DepositsContract");

            migrationBuilder.DropTable(
                name: "PaymentExtension");

            migrationBuilder.DropTable(
                name: "PeopleRental");

            migrationBuilder.DropTable(
                name: "RoomDetails");

            migrationBuilder.DropTable(
                name: "RoomImage");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "RentalContract");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
