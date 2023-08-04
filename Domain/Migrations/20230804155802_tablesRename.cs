using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class tablesRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestTranslation_Interest_InterestId",
                table: "InterestTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestUser_Interest_InterestsId",
                table: "InterestUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestTranslation",
                table: "InterestTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interest",
                table: "Interest");

            migrationBuilder.RenameTable(
                name: "InterestTranslation",
                newName: "InterestTranslations");

            migrationBuilder.RenameTable(
                name: "Interest",
                newName: "Interests");

            migrationBuilder.RenameIndex(
                name: "IX_InterestTranslation_InterestId",
                table: "InterestTranslations",
                newName: "IX_InterestTranslations_InterestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestTranslations",
                table: "InterestTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interests",
                table: "Interests",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "48bd8963-2df2-4fbf-b4bc-27a8f9aea2ed");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e8f1cfb3-8e33-4695-ab10-e77bf252a461", "AQAAAAEAACcQAAAAEPH7IGziCnsGeCMiYGgJqAvZttWTfiWV8OrMLYC//sglzkqYThRMBk3Kt18DGslS9g==" });

            migrationBuilder.AddForeignKey(
                name: "FK_InterestTranslations_Interests_InterestId",
                table: "InterestTranslations",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestUser_Interests_InterestsId",
                table: "InterestUser",
                column: "InterestsId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestTranslations_Interests_InterestId",
                table: "InterestTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestUser_Interests_InterestsId",
                table: "InterestUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterestTranslations",
                table: "InterestTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interests",
                table: "Interests");

            migrationBuilder.RenameTable(
                name: "InterestTranslations",
                newName: "InterestTranslation");

            migrationBuilder.RenameTable(
                name: "Interests",
                newName: "Interest");

            migrationBuilder.RenameIndex(
                name: "IX_InterestTranslations_InterestId",
                table: "InterestTranslation",
                newName: "IX_InterestTranslation_InterestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterestTranslation",
                table: "InterestTranslation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interest",
                table: "Interest",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "67a35b6e-6528-4806-94eb-640e1e65354a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd64c4e9-17b9-4046-97b3-45ee42a1b5f5", "AQAAAAEAACcQAAAAEG7kwZVxsTaBerdPxUr7JfrT3+o49i3m5jUfxzJiSyjz0Zq/g5tdEUDC5GS8075XnQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_InterestTranslation_Interest_InterestId",
                table: "InterestTranslation",
                column: "InterestId",
                principalTable: "Interest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestUser_Interest_InterestsId",
                table: "InterestUser",
                column: "InterestsId",
                principalTable: "Interest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
