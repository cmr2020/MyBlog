using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.DataLayer.Migrations
{
    public partial class Mig_Votes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageVotes_PageVotes_PageVoteVoteId",
                table: "PageVotes");

            migrationBuilder.DropIndex(
                name: "IX_PageVotes_PageVoteVoteId",
                table: "PageVotes");

            migrationBuilder.DropColumn(
                name: "PageVoteVoteId",
                table: "PageVotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PageVoteVoteId",
                table: "PageVotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageVotes_PageVoteVoteId",
                table: "PageVotes",
                column: "PageVoteVoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageVotes_PageVotes_PageVoteVoteId",
                table: "PageVotes",
                column: "PageVoteVoteId",
                principalTable: "PageVotes",
                principalColumn: "VoteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
