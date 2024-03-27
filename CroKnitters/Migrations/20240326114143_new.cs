using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CroKnitters.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 2,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 4,
                column: "UserId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 5,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 7,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 8,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumn: "UproId",
                keyValue: 2,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumn: "UproId",
                keyValue: 3,
                column: "UserId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumn: "UproId",
                keyValue: 4,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumn: "UproId",
                keyValue: 5,
                column: "UserId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2632));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2669));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2674));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2701));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 5, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2796));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 5, 16, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2837));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 5, 6, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2843));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 5,
                column: "Date",
                value: new DateTime(2024, 4, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2854));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 6,
                column: "Date",
                value: new DateTime(2024, 5, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2859));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3003));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "GroupId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3008));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3136));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3160));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3163));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3169));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3173));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3176));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3180));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3183));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3186));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 10,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3190));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 11,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3193));

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 12,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3196));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3623));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3720));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3726));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3730));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3734));

            migrationBuilder.UpdateData(
                table: "PrivateChat",
                keyColumn: "PChatId",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(3738));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 2, 26, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2012));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 2, 16, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 3, 2, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2405));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 2, 6, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2411));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 2, 21, 13, 14, 19, 912, DateTimeKind.Local).AddTicks(2417));

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 2,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 4,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 5,
                column: "UserId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 7,
                column: "UserId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "UserPatterns",
                keyColumn: "UpatId",
                keyValue: 8,
                column: "UserId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumn: "UproId",
                keyValue: 2,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumn: "UproId",
                keyValue: 3,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumn: "UproId",
                keyValue: 4,
                column: "UserId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "UserProjects",
                keyColumn: "UproId",
                keyValue: 5,
                column: "UserId",
                value: 3);
        }
    }
}
