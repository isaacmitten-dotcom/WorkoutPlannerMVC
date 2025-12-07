using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutPlannerMVC.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE GetTopExercise
            AS
            BEGIN
                SELECT TOP 1 
                    ew.ExerciseId, 
                    e.Name AS ExerciseName, 
                    COUNT(*) AS Frequency
                FROM ExerciseWorkout ew
                INNER JOIN Exercise e ON ew.ExerciseId = e.Id
                GROUP BY ew.ExerciseId, e.Name
                ORDER BY Frequency DESC;
            END
            ");



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetTopExercise");

        }
    }
}
