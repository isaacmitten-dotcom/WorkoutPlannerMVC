# Week15: Stored Procedure

This week's assignment was to implement the stored procedures feature. Call one procedure from your app and render the result. The requirements were to execute a stored procedure via EF Core (raw SQL or mapped), handle parameters safely and render the result in a view or API response, and commit the SQL script used to create the procedure. For my project I created a stored procedure called GetTopExercise. This is the code for it:

```
CREATE PROCEDURE GetTopExercise
AS
BEGIN
    SELECT TOP 1 
        ew.ExercisesId, 
        e.Name AS ExerciseName, 
        COUNT(*) AS Frequency
    FROM ExerciseWorkout ew
    INNER JOIN Exercises e ON ew.ExercisesId = e.Id
    GROUP BY ew.ExercisesId, e.Name
    ORDER BY Frequency DESC;
END
```

This SQL statement looks at the join table called ExerciseWorkout. It joins with the Exercise table to retrieve the name of the exercise. It looks through the ExerciseId column from ExerciseWorkout and sorts them in descending order with the most frequent ExerciseId on top. It then only returns the top item from the query, which is the most frequent ExerciseId. I created a model to hold the result of this procedure called TopExercise. It has the ExercisesId, ExerciseName, and frequency properties. I then created a function in ExerciseService to execute the procedure, and I call that in the controller. I pass the data into the TopExercise view, which displays it. 

Here is a screenshot of that:

<img width="1532" height="816" alt="image" src="https://github.com/user-attachments/assets/389bb379-be32-47de-bf2a-f2c060570fe4" />
