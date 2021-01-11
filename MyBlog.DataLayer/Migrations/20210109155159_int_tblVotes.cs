using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.DataLayer.Migrations
{
    public partial class int_tblVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PageVotes",
                columns: table => new
                {
                    VoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<bool>(type: "bit", nullable: false),
                    VoteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageVoteVoteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageVotes", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_PageVotes_Pages_PageID",
                        column: x => x.PageID,
                        principalTable: "Pages",
                        principalColumn: "PageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageVotes_PageVotes_PageVoteVoteId",
                        column: x => x.PageVoteVoteId,
                        principalTable: "PageVotes",
                        principalColumn: "VoteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PageVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageVotes_PageID",
                table: "PageVotes",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_PageVotes_PageVoteVoteId",
                table: "PageVotes",
                column: "PageVoteVoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PageVotes_UserId",
                table: "PageVotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageVotes");
        }
    }
}
