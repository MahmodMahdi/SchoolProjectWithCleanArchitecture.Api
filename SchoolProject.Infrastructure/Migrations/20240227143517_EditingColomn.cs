using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditingColomn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_instructors_InstructorManager",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_departments_InstructorManager",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "instructors");

            migrationBuilder.RenameColumn(
                name: "InstructorManager",
                table: "departments",
                newName: "DepartmentManager");

            migrationBuilder.CreateIndex(
                name: "IX_departments_DepartmentManager",
                table: "departments",
                column: "DepartmentManager",
                unique: true,
                filter: "[DepartmentManager] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_instructors_DepartmentManager",
                table: "departments",
                column: "DepartmentManager",
                principalTable: "instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_instructors_DepartmentManager",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_departments_DepartmentManager",
                table: "departments");

            migrationBuilder.RenameColumn(
                name: "DepartmentManager",
                table: "departments",
                newName: "InstructorManager");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_departments_InstructorManager",
                table: "departments",
                column: "InstructorManager",
                unique: true,
                filter: "[InstructorManager] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_instructors_InstructorManager",
                table: "departments",
                column: "InstructorManager",
                principalTable: "instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
