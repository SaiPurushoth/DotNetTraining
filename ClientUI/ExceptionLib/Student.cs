using CustomException;

namespace ExceptionLib
{
    public class Student
    {
        private string name;
        private int mark1;
        private int mark2;

        public Student(string username,int m1,int m2)
        {
 
            name = username;
            mark1 = m1;
            mark2 = m2;

        }

        public void CheckMark()
        {
            try
            {

                if (mark1 < 0 || mark2 < 0)
                {
                    throw new InvalidInputException("Both marks should be greater than 0");
                }
            }
            catch
            {
                throw;
            }

        }

        public  int AddMarks()
        {

            CheckMark();
            int value = mark1 + mark2;
            return value;


        }

        public int SubMarks()
        {
            CheckMark();
            try
            {
                int value = mark1 - mark2;
                if (value < 0)
                {
                    throw new Exception("Negative exception mark1 is less than mark2");
                }
                return value;

            }
            catch
            {
                throw;
            }
      

        }

        public int MulMarks()
        {
            CheckMark();
            int value = mark1 * mark2;
            return value;
        }

        public int DivMarks()
        {
            CheckMark();
            try { 
            int value = mark1 / mark2;
                return value;
            }
            catch 
            {
                throw;
            }
 
        }
       
    }

}