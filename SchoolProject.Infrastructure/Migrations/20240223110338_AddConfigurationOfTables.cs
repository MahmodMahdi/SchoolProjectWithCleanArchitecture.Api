using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AddConfigurationOfTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "subjects",
				columns: table => new
				{
					SubjectID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					SubjectNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					SubjectNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Period = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_subjects", x => x.SubjectID);
				});

			migrationBuilder.CreateTable(
				name: "departments",
				columns: table => new
				{
					DepartmentID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					DepartmentNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DepartmentNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
					InstructorManager = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_departments", x => x.DepartmentID);
				});

			migrationBuilder.CreateTable(
				name: "departmentSubjects",
				columns: table => new
				{
					DepartmentID = table.Column<int>(type: "int", nullable: false),
					SubjectID = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_departmentSubjects", x => new { x.SubjectID, x.DepartmentID });
					table.ForeignKey(
						name: "FK_departmentSubjects_departments_DepartmentID",
						column: x => x.DepartmentID,
						principalTable: "departments",
						principalColumn: "DepartmentID",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_departmentSubjects_subjects_SubjectID",
						column: x => x.SubjectID,
						principalTable: "subjects",
						principalColumn: "SubjectID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "instructors",
				columns: table => new
				{
					InstructorId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
					NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SuperVisorId = table.Column<int>(type: "int", nullable: true),
					Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
					DepartmentId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_instructors", x => x.InstructorId);
					table.ForeignKey(
						name: "FK_instructors_departments_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "departments",
						principalColumn: "DepartmentID");
					table.ForeignKey(
						name: "FK_instructors_instructors_SuperVisorId",
						column: x => x.SuperVisorId,
						principalTable: "instructors",
						principalColumn: "InstructorId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "students",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
					NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Age = table.Column<int>(type: "int", nullable: false),
					Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
					DepartmentID = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_students", x => x.Id);
					table.ForeignKey(
						name: "FK_students_departments_DepartmentID",
						column: x => x.DepartmentID,
						principalTable: "departments",
						principalColumn: "DepartmentID");
				});

			migrationBuilder.CreateTable(
				name: "instructorSubjects",
				columns: table => new
				{
					InstructorId = table.Column<int>(type: "int", nullable: false),
					SubjectId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_instructorSubjects", x => new { x.SubjectId, x.InstructorId });
					table.ForeignKey(
						name: "FK_instructorSubjects_instructors_InstructorId",
						column: x => x.InstructorId,
						principalTable: "instructors",
						principalColumn: "InstructorId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_instructorSubjects_subjects_SubjectId",
						column: x => x.SubjectId,
						principalTable: "subjects",
						principalColumn: "SubjectID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "studentSubjects",
				columns: table => new
				{
					StudentID = table.Column<int>(type: "int", nullable: false),
					SubjectID = table.Column<int>(type: "int", nullable: false),
					Grade = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_studentSubjects", x => new { x.SubjectID, x.StudentID });
					table.ForeignKey(
						name: "FK_studentSubjects_students_StudentID",
						column: x => x.StudentID,
						principalTable: "students",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_studentSubjects_subjects_SubjectID",
						column: x => x.SubjectID,
						principalTable: "subjects",
						principalColumn: "SubjectID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_departments_InstructorManager",
				table: "departments",
				column: "InstructorManager",
				unique: true,
				filter: "[InstructorManager] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_departmentSubjects_DepartmentID",
				table: "departmentSubjects",
				column: "DepartmentID");

			migrationBuilder.CreateIndex(
				name: "IX_instructors_DepartmentId",
				table: "instructors",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_instructors_SuperVisorId",
				table: "instructors",
				column: "SuperVisorId");

			migrationBuilder.CreateIndex(
				name: "IX_instructorSubjects_InstructorId",
				table: "instructorSubjects",
				column: "InstructorId");

			migrationBuilder.CreateIndex(
				name: "IX_students_DepartmentID",
				table: "students",
				column: "DepartmentID");

			migrationBuilder.CreateIndex(
				name: "IX_studentSubjects_StudentID",
				table: "studentSubjects",
				column: "StudentID");

			migrationBuilder.AddForeignKey(
				name: "FK_departments_instructors_InstructorManager",
				table: "departments",
				column: "InstructorManager",
				principalTable: "instructors",
				principalColumn: "InstructorId",
				onDelete: ReferentialAction.Restrict);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_departments_instructors_InstructorManager",
				table: "departments");

			migrationBuilder.DropTable(
				name: "departmentSubjects");

			migrationBuilder.DropTable(
				name: "instructorSubjects");

			migrationBuilder.DropTable(
				name: "studentSubjects");

			migrationBuilder.DropTable(
				name: "students");

			migrationBuilder.DropTable(
				name: "subjects");

			migrationBuilder.DropTable(
				name: "instructors");

			migrationBuilder.DropTable(
				name: "departments");
		}
	}
}
