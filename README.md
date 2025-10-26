# Workout Planner – ASP.NET Core MVC Project

## Project Summary

This is a web application designed to help users create and manage personalized workout routines. Users will be able to build and edit individual workouts and assign exercises to them. This project is built using ASP.NET Core MVC and Entity Framework Core. It is intended for users who want a structured, customizable workout tool that supports their fitness journey.


## Planning Table


| Week    | Concept                                       | Feature                                                                   | Goal                                                  | Acceptance Criteria                                                                                                                                              | Evidence in README.md                                                                                                             | Test Plan                                                                                                       |
| ------- | --------------------------------------------- | ------------------------------------------------------------------------- | ----------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------- |
| Week 10 | Modeling                                      | Create models for Workout and Exercise                                    | Define the data structure and relationships           | - Workout and Exercise models exist<br>- Workout has a collection of Exercises<br>- EF Core relationships set up                         | Implemented code; README write-up(200 words); screenshots as needed                                                                           | - Unit tests for model validation<br>- Use EF Core to check DB creation and schema                              |
| Week 11 | Separation of Concerns / Dependency Injection | Create WorkoutService and ExerciseService, inject into controllers        | Separate business logic from UI/controller layer      | - Service interfaces and classes created<br>- Services injected into controllers<br>- At least one method per service called in controller | Implemented code; README write-up(200 words); screenshots as needed                                                                           | - Unit tests using mock services<br>- Confirm DI works with controller logic                                    |
| Week 12 | CRUD                                          | Implement Create, Read, Update, Delete for Workout and Exercise           | Allow full management of workouts and their exercises | - Create/edit/delete forms for both models<br>- Validation and confirmation messages<br>- All views functional and styled                | Implemented code; README write-up(200 words); screenshots as needed                                                                           | - Manual testing of all CRUD paths<br>- Unit tests for controller actions<br>- Validation error testing         |
| Week 13 | Diagnostics                                   | Add `/healthz` endpoint                                                   | Report if DB is up or down                            | - Healthy when DB is up<br>- Unhealthy when DB is down                                                                                   | Implemented code; README write-up(200 words); screenshots as needed                                                                           | - Stop DB and hit `/healthz` to confirm response                                                              |
| Week 14 | Logging                                       | Log when workouts and exercises are created successfully                  | Add visibility into system behavior                   | - Logging added to key controller and service methods<br>- Logs show successful and failed actions                                      | Implemented code; README write-up(200 words); screenshots as needed                                                                           | - Test logs during CRUD actions                                                                                 |
| Week 15 | Stored Procedures                             | Create and use a stored procedure to return top 5 most used exercises     | Showcase database-side processing                     | - Stored proc written in SQL Server<br>- Called from EF Core<br>- Results displayed in a view                                             | Implemented code; README write-up(200 words); screenshots as needed                                                                           | - Execute proc directly in DB<br>- Test from UI and compare results                                            |


## Week 10 Modeling summary

This week's goal was to create the Workout and Exercise models that I will be using for the rest of the project and define their data structure and relationship. The properties for the Workout entity are name, start date, description, and a list of exercises. The properties for the exercise entity are name, description, reps, sets, a foreign key, and a workout entity. The relationship between the Workout entity and the Exercise entity is one-to-many, where one workout has many exercises.  After creating the models, I created the WorkoutPlannerMVCContext, which has data sets for the Workout and Exercise entities. I created the one-to-many relationship using Fluent AI in the model builder like this: 

`modelBuilder.Entity<Workout>()
    .HasMany(w => w.Exercises)
    .WithOne(e => e.Workout)
    .HasForeignKey(e => e.WorkoutId);`


    
I then made the migration titled InitMigration to initialize the database schema. I scaffolded the view and controllers so that I could ensure the one-to-many relationship was created correctly. That view ended up looking like this:

<img width="1885" height="512" alt="Models" src="https://github.com/user-attachments/assets/0bc5bbdd-ae30-4a7c-8c9e-36cb7797fc64" />


I would like to note that the CRUD operations created during this process are not finished, and they were just created to help ensure the entities' relationship was correct. The focus this week was solely on creating the entities, DB context, and the migration. The CRUD operations will be completed at a later week.

