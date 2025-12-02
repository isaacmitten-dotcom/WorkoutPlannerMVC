# Week14: Logging

This week’s project was Logging. The requirements were to log at least one success path and one error path, include useful fields such as request/correlation ID, entity ID, and action, and make logs readable and actionable. I was able to do this by implementing structured logging in both the ExercisesController and WorkoutsController of the WorkoutPlannerMVC application. For each key action, Create, Edit, and Delete, I added logs for both successful operations and failed operations. The logs include useful fields like Action (Create, Edit, Delete), Success status (True, False), entity identifiers such as ExerciseId or WorkoutId, entity names, and a ResponseId which serves as a correlation ID.  For example, when creating an exercise, a successful creation generates a log with the exercise ID, name, action, success status, and response ID, while a failed creation logs the same fields with a success value of false. A success looks something like this:
 Exercise created { Action = ExerciseCreate, Success = True, ExerciseId = 10, ExerciseName = Spoon, ResponseId = 0HNHHQN23LN1I:0000003D }

I did this by passing anonymous objects into the log message template with the relevant fields. This allows anyone reviewing the logs to quickly and efficiently understand what happened, which entity was affected, and whether the operation was a success or a failure. 

Here are some screenshots of a success and fail log:

## Success
<img width="1267" height="850" alt="StructureLogSuccess" src="https://github.com/user-attachments/assets/e133da05-6dbc-40f0-8ef3-dbb30033c585" />

## Fail
<img width="1269" height="684" alt="StructureLogFail" src="https://github.com/user-attachments/assets/ba79b40b-ce7b-49b4-afa4-62ad901a5c27" />
