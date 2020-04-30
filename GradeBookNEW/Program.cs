using System;
using System.Collections.Generic;


namespace GradeBookNEW
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Scott's Grade Book");
            book.GradeAdded += OnGradeAdded;
            EnterGrades(book);

            //// Obsolete code
            {
                //// Ran a test here (was placed before loop)
                //try
                //{
                // Book book = new Book(null);
                //}
                //catch(ArgumentNullException ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                //finally
                //{
                //    Console.WriteLine("Program had to quit unexpectedly.");
                //}


                //// This was my attempt at solving the loop
                //var done = false;

                //while (!done)
                //{
                //    Console.WriteLine("Enter a grade or 'q' to quit: ");
                //    var input = Console.ReadLine();

                //    if (input.Length == 0)
                //    {
                //        Console.WriteLine("No grade entered.");
                //    }
                //    else if(input == "q")
                //    {
                //        done = true;
                //    }
                //    else
                //    {
                //        var grade = double.Parse(input);
                //        book.AddGrade(grade);
                //    }
                //};

                //// This was the super basic body of the program at the beginning of the tutorial
                //book.AddGrade(89.1);
                //book.AddGrade(90.5);
                //book.AddGrade(77.5);
                //book.GetStatistics();
            }

            var stats = book.GetStatistics();

            // result:N1 formats double to 1 decimal, N2 to 2, etc.)
            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The average grade is {stats.Average:N1}. Highest grade was {stats.High}. Lowest grade was {stats.Low}.");
            Console.WriteLine($"The letter grade is {stats.Letter}.");
        }

        private static void EnterGrades(IBook book)
        {
            //// This is the solution found in the tutorial
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit: ");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Tou use the finally block to execute code that has to happen after any catch blocks
                    // Perhaps you need to close a file or wrap up some logic
                    Console.WriteLine("**");
                }


            }
        }

        static void OnGradeAdded (object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added.");
        }
    }
}
