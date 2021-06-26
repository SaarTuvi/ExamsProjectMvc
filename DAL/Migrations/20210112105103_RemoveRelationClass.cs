using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RemoveRelationClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classrooms_Students");

            migrationBuilder.DropTable(
                name: "Classrooms_Teachers");

            migrationBuilder.DropTable(
                name: "Teacher_Student");

            migrationBuilder.CreateTable(
                name: "ClassroomStudent",
                columns: table => new
                {
                    ClassroomsClassroomID = table.Column<int>(type: "int", nullable: false),
                    StudentsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassroomStudent", x => new { x.ClassroomsClassroomID, x.StudentsID });
                    table.ForeignKey(
                        name: "FK_ClassroomStudent_Classrooms_ClassroomsClassroomID",
                        column: x => x.ClassroomsClassroomID,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassroomStudent_Users_StudentsID",
                        column: x => x.StudentsID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassroomTeacher",
                columns: table => new
                {
                    ClassroomsClassroomID = table.Column<int>(type: "int", nullable: false),
                    TeachersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassroomTeacher", x => new { x.ClassroomsClassroomID, x.TeachersID });
                    table.ForeignKey(
                        name: "FK_ClassroomTeacher_Classrooms_ClassroomsClassroomID",
                        column: x => x.ClassroomsClassroomID,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassroomTeacher_Users_TeachersID",
                        column: x => x.TeachersID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTeacher",
                columns: table => new
                {
                    StudentsID = table.Column<int>(type: "int", nullable: false),
                    TeachersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTeacher", x => new { x.StudentsID, x.TeachersID });
                    table.ForeignKey(
                        name: "FK_StudentTeacher_Users_StudentsID",
                        column: x => x.StudentsID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StudentTeacher_Users_TeachersID",
                        column: x => x.TeachersID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomStudent_StudentsID",
                table: "ClassroomStudent",
                column: "StudentsID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomTeacher_TeachersID",
                table: "ClassroomTeacher",
                column: "TeachersID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeacher_TeachersID",
                table: "StudentTeacher",
                column: "TeachersID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassroomStudent");

            migrationBuilder.DropTable(
                name: "ClassroomTeacher");

            migrationBuilder.DropTable(
                name: "StudentTeacher");

            migrationBuilder.CreateTable(
                name: "Classrooms_Students",
                columns: table => new
                {
                    Classroom_StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassroomID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms_Students", x => x.Classroom_StudentID);
                    table.ForeignKey(
                        name: "FK_Classrooms_Students_Classrooms_ClassroomID",
                        column: x => x.ClassroomID,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classrooms_Students_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms_Teachers",
                columns: table => new
                {
                    Classroom_TeacherIDs = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassroomID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms_Teachers", x => x.Classroom_TeacherIDs);
                    table.ForeignKey(
                        name: "FK_Classrooms_Teachers_Classrooms_ClassroomID",
                        column: x => x.ClassroomID,
                        principalTable: "Classrooms",
                        principalColumn: "ClassroomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classrooms_Teachers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher_Student",
                columns: table => new
                {
                    Teacher_StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher_Student", x => x.Teacher_StudentID);
                    table.ForeignKey(
                        name: "FK_Teacher_Student_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teacher_Student_Users_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_Students_ClassroomID",
                table: "Classrooms_Students",
                column: "ClassroomID");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_Students_UserID",
                table: "Classrooms_Students",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_Teachers_ClassroomID",
                table: "Classrooms_Teachers",
                column: "ClassroomID");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_Teachers_UserID",
                table: "Classrooms_Teachers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_Student_StudentId",
                table: "Teacher_Student",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_Student_TeacherID",
                table: "Teacher_Student",
                column: "TeacherID");
        }
    }
}
