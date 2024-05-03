using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LinqBuild.Migrations
{
    /// <inheritdoc />
    public partial class InitiliazDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_Classes_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCourses",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCourses", x => new { x.TeacherId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherCourses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseName" },
                values: new object[,]
                {
                    { 1, "Backend" },
                    { 2, "Frontend" },
                    { 3, "OOP" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherId", "Name" },
                values: new object[,]
                {
                    { 1, "Mr. Smith" },
                    { 2, "Ms. Johnson" },
                    { 3, "Mr. Clark" },
                    { 4, "Ms. Davis" },
                    { 5, "Mr. Brown" },
                    { 6, "Ms. Miller" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassName", "TeacherId" },
                values: new object[,]
                {
                    { 1, "Class 1", 1 },
                    { 2, "Class 2", 2 },
                    { 3, "Class 3", 3 },
                    { 4, "Class 4", 4 },
                    { 5, "Class 5", 5 },
                    { 6, "Class 6", 6 }
                });

            migrationBuilder.InsertData(
                table: "TeacherCourses",
                columns: new[] { "CourseId", "TeacherId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 3, 4 },
                    { 2, 5 },
                    { 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "ClassId", "Name" },
                values: new object[,]
                {
                    { 1, 2, "James Smith" },
                    { 2, 3, "Maria Garcia" },
                    { 3, 4, "Robert Johnson" },
                    { 4, 5, "Sophia Martinez" },
                    { 5, 6, "Michael Brown" },
                    { 6, 1, "Linda Taylor" },
                    { 7, 2, "William Jones" },
                    { 8, 3, "Barbara Wilson" },
                    { 9, 4, "Elizabeth Moore" },
                    { 10, 5, "David Lee" },
                    { 11, 6, "Oscar Howard" },
                    { 12, 1, "Edith Singleton" },
                    { 13, 2, "Jame Spears" },
                    { 14, 3, "Victor Horne" },
                    { 15, 4, "Dale Osborn" },
                    { 16, 5, "Dominique Hendricks" },
                    { 17, 6, "Rodrigo Chen" },
                    { 18, 1, "Latoya Barry" },
                    { 19, 2, "Joaquin Soto" },
                    { 20, 3, "Raymond Donaldson" },
                    { 21, 4, "Shari Valenzuela" },
                    { 22, 5, "Maude Le" },
                    { 23, 6, "Claudine Pruitt" },
                    { 24, 1, "Fannie Ferrell" },
                    { 25, 2, "Imogene Gilmore" },
                    { 26, 3, "Roberto Park" },
                    { 27, 4, "Prince Gallegos" },
                    { 28, 5, "Colby Bowen" },
                    { 29, 6, "Jody Wilkinson" },
                    { 30, 1, "Graciela Mendez" }
                });

            migrationBuilder.InsertData(
                table: "StudentCourses",
                columns: new[] { "CourseId", "StudentId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 3, 6 },
                    { 1, 7 },
                    { 2, 7 },
                    { 3, 7 },
                    { 1, 8 },
                    { 2, 8 },
                    { 3, 8 },
                    { 2, 9 },
                    { 3, 9 },
                    { 1, 10 },
                    { 1, 11 },
                    { 2, 11 },
                    { 3, 11 },
                    { 1, 12 },
                    { 2, 12 },
                    { 3, 12 },
                    { 1, 13 },
                    { 2, 13 },
                    { 3, 13 },
                    { 1, 14 },
                    { 2, 14 },
                    { 1, 15 },
                    { 2, 15 },
                    { 3, 15 },
                    { 1, 16 },
                    { 2, 16 },
                    { 3, 16 },
                    { 1, 17 },
                    { 2, 17 },
                    { 3, 17 },
                    { 1, 18 },
                    { 2, 19 },
                    { 3, 19 },
                    { 1, 20 },
                    { 2, 20 },
                    { 3, 20 },
                    { 1, 21 },
                    { 2, 21 },
                    { 3, 21 },
                    { 2, 22 },
                    { 1, 23 },
                    { 2, 23 },
                    { 3, 23 },
                    { 1, 24 },
                    { 3, 24 },
                    { 2, 25 },
                    { 3, 25 },
                    { 1, 26 },
                    { 2, 27 },
                    { 2, 28 },
                    { 3, 28 },
                    { 2, 29 },
                    { 1, 30 },
                    { 2, 30 },
                    { 3, 30 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourses_CourseId",
                table: "TeacherCourses",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropTable(
                name: "TeacherCourses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
