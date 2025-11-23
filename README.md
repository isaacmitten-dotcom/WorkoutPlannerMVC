## Week 12: CRUD

This week's assignment was to create the CRUD functions for our program. This week I discovered that I had set up my database incorrectly and I had chosen the wrong kind of relationship for my entities. I previously had configured a one-to-many relationship with one workout having many exercises. However, I realized this wouldn’t support the functionality I needed, since each workout can have multiple exercises and each exercise can belong to multiple workouts. So I added a navigation property to each of my entities and updated my database context to properly handle the many-to-many relationship. After I finished that, I created asynchronous CRUD functions for both the workout and exercise entities. I struggled a little making the views, as I wasn’t sure how to best structure it to allow for multiple exercises to be assigned to a workout. I ended up going with a checkbox that would read from a SelectListItem named SelectedExerciseIds, and I think it turned out nicely. Lastly, I added validation to my models using the [Required] attribute. This allowed ASP.NET Core’s built-in validation system to provide immediate feedback to users when submitting forms, improving the overall usability of the application.

These are some screenshots of the validation feedback, and different pages.

# Validation
<img width="1682" height="824" alt="validation" src="https://github.com/user-attachments/assets/d94ad3dd-5524-43f1-bf89-1643aa3c7f20" />

# Create
<img width="1820" height="891" alt="CreatePage" src="https://github.com/user-attachments/assets/63bc723e-5d99-4d02-b259-d70cb769cc78" />

# Details
<img width="1687" height="817" alt="DetailsPage" src="https://github.com/user-attachments/assets/5dad3b9d-9aa0-4b09-86fd-f4f0ef676a83" />

# Delete
<img width="1030" height="590" alt="DeletePage" src="https://github.com/user-attachments/assets/a762f6fb-f23f-4c52-a5db-6053dea3b6b7" />
