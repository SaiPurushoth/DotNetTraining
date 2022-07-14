using ExceptionLib;
class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Enter Name,Mark 1,Mark 2");
        string studentname = Console.ReadLine();
        int mark1 = int.Parse(Console.ReadLine());
        int mark2 = int.Parse(Console.ReadLine());

        Student students = new Student(studentname,mark1,mark2);

       
        try
        {
         Console.WriteLine(   students.AddMarks());
            Console.WriteLine(students.SubMarks());
            Console.WriteLine(students.MulMarks());
            Console.WriteLine(students.DivMarks());
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }

    }
}