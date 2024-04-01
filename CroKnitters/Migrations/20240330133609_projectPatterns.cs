using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CroKnitters.Migrations
{
    /// <inheritdoc />
    public partial class projectPatterns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9406));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9416));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9423));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9429));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 18, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9585));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 6, 8, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9593));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 5, 29, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9599));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2024, 5, 19, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9608));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2024, 6, 18, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9614));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9713));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9722));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9728));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9852));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9860));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9866));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9872));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9878));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9883));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9894));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9900));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9907));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9914));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 744, DateTimeKind.Local).AddTicks(471));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 744, DateTimeKind.Local).AddTicks(490));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 744, DateTimeKind.Local).AddTicks(496));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 744, DateTimeKind.Local).AddTicks(502));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 744, DateTimeKind.Local).AddTicks(507));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 744, DateTimeKind.Local).AddTicks(513));

            migrationBuilder.InsertData(
                table: "ProjectPatterns",
                columns: new[] { "ProPatId", "PatternId", "ProjectId" },
                values: new object[,]
                {
                    { 1, 8, 1 },
                    { 2, 7, 2 },
                    { 3, 4, 3 },
                    { 4, 6, 4 },
                    { 5, 3, 5 },
                    { 6, 1, 6 }
                });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 20, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9111));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 10, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9174));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 3, 25, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9181));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 2, 29, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9189));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 3, 15, 9, 36, 8, 743, DateTimeKind.Local).AddTicks(9197));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectPatterns",
                keyColumn: "ProPatId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectPatterns",
                keyColumn: "ProPatId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectPatterns",
                keyColumn: "ProPatId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectPatterns",
                keyColumn: "ProPatId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectPatterns",
                keyColumn: "ProPatId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProjectPatterns",
                keyColumn: "ProPatId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(6975));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(6998));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7005));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7011));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 14, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7169));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 6, 4, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7191));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 5, 25, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7198));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2024, 5, 15, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7209));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2024, 6, 14, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7215));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7290));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7325));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7333));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7452));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7463));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7470));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7476));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7482));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7489));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7495));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7501));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7519));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7531));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(7998));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(8072));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(8079));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(8098));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 3, 16, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(6672));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 3, 6, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(6747));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 3, 21, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(6756));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 2, 25, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(6766));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 3, 11, 7, 41, 42, 493, DateTimeKind.Local).AddTicks(6776));
        }
    }
}
