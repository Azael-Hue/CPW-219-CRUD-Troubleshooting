namespace CPW219_CRUD_Troubleshooting.Models
{
    public static class StudentDb
    {
        public static Student Add(Student newStudent, SchoolContext db)
        {
            //Add student to context
            db.Students.Add(newStudent);
            //Send insert query to database
            db.SaveChanges();
            return newStudent;

        }

        public static List<Student> GetStudents(SchoolContext context)
        {
            return (from s in context.Students
                    select s).ToList();
        }

        public static Student GetStudent(SchoolContext context, int id)
        {
            Student studentRetrieved = context
                                        .Students
                                        .Where(s => s.StudentId == id)
                                        .Single();
            return studentRetrieved;
        }

        public static void Delete(SchoolContext context, Student s)
        {
            context.Students.Remove(s);
            context.SaveChanges();
        }

        public static void Update(SchoolContext context, Student s)
        {
            //Update the student in the context
            context.Students.Update(s);
            //Send update query to database
            context.SaveChanges();
        }
    }
}
