using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Days",
                table: "PaymentExtension",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 17, 23, 23, 34, 208, DateTimeKind.Local).AddTicks(6887),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 17, 23, 23, 1, 355, DateTimeKind.Local).AddTicks(8510));

            migrationBuilder.UpdateData(
                table: "Bill",
                keyColumn: "ID",
                keyValue: 1,
                column: "PayDate",
                value: new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6342));

            migrationBuilder.UpdateData(
                table: "DepositsContract",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckOutDate", "DepositsDate", "ReceiveDate" },
                values: new object[] { new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6370), new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6368), new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6369) });

            migrationBuilder.UpdateData(
                table: "PaymentExtension",
                keyColumn: "ID",
                keyValue: 1,
                column: "Days",
                value: new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6393));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 1,
                column: "Birthday",
                value: new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6408));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 2,
                column: "Birthday",
                value: new DateTime(1995, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 3,
                column: "Birthday",
                value: new DateTime(1996, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 4,
                column: "Birthday",
                value: new DateTime(2000, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 5,
                column: "Birthday",
                value: new DateTime(1998, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RentalContract",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckOutDate", "ReceiveDate" },
                values: new object[] { new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6549), new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6548) });

            migrationBuilder.UpdateData(
                table: "RentalContract",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CheckOutDate", "ReceiveDate" },
                values: new object[] { new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6555), new DateTime(2023, 12, 17, 23, 23, 34, 211, DateTimeKind.Local).AddTicks(6554) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Days",
                table: "PaymentExtension",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 17, 23, 23, 1, 355, DateTimeKind.Local).AddTicks(8510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 17, 23, 23, 34, 208, DateTimeKind.Local).AddTicks(6887));

            migrationBuilder.UpdateData(
                table: "Bill",
                keyColumn: "ID",
                keyValue: 1,
                column: "PayDate",
                value: new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8165));

            migrationBuilder.UpdateData(
                table: "DepositsContract",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckOutDate", "DepositsDate", "ReceiveDate" },
                values: new object[] { new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8193), new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8190), new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8191) });

            migrationBuilder.UpdateData(
                table: "PaymentExtension",
                keyColumn: "ID",
                keyValue: 1,
                column: "Days",
                value: new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8228));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 1,
                column: "Birthday",
                value: new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8251));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 2,
                column: "Birthday",
                value: new DateTime(1993, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 3,
                column: "Birthday",
                value: new DateTime(1991, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 4,
                column: "Birthday",
                value: new DateTime(1993, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "ID",
                keyValue: 5,
                column: "Birthday",
                value: new DateTime(1997, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RentalContract",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckOutDate", "ReceiveDate" },
                values: new object[] { new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8384), new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8383) });

            migrationBuilder.UpdateData(
                table: "RentalContract",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CheckOutDate", "ReceiveDate" },
                values: new object[] { new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8389), new DateTime(2023, 12, 17, 23, 23, 1, 359, DateTimeKind.Local).AddTicks(8388) });
        }
    }
}
