using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddStudentExamIsSubmited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAnswered",
                table: "Studens_Exams",
                newName: "IsAttended");
            
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmited",
                table: "Studens_Exams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmited",
                table: "Studens_Exams");

            migrationBuilder.RenameColumn(
                name: "IsAttended",
                table: "Studens_Exams",
                newName: "IsAnswered");
        }
    }
}
