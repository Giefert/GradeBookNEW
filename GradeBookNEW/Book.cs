using System;
using System.Collections.Generic;
using System.IO;


namespace GradeBookNEW
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NameObject
    {
        public NameObject(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public interface IBook 
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    };

    public abstract class Book : NameObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {

            // MY Solution. Works perfectly it seems
            {//string path = @"C:\Users\Matt\source\repos\GradeBookNEW\GradeBookNEW\Scott's Grade Book.txt";
             //using (StreamWriter sw = File.AppendText(path))
             //{
             //    sw.WriteLine(grade);
             //}
            }

            // The tutorial introduces the solution as such to introduce a problem that comes with it as well
            {// Tutorial solution that crashes when you try to enter 2 grades (first grade doesn't get entered either
             // but at least the file is created.
             //{
             //    var writer = File.AppendText($"{Name}.txt");
             //    writer.WriteLine(grade);
             //}
            }

            //// Tutorial real solution
            // This is a case of an override for the 'using' keyword, it's not using a namespace.
            // Instead this is saying I want to use this object (in this case this 'writer'). Then when its done
            // with the block of code at the end of the curly brace, use the Dispose() method to clean things up and
            // prevent the resource from being blocked from use in the next lines of code.
            // It does this by essentially wrapping all the code in a try-finally statement for me.
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded !=null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }                
        }

        public override Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
    }

    public class InMemoryBook : Book
    {

        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public void AddLetterGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                case 'D':
                    AddGrade(60);
                    break;

                default:
                    AddGrade(0);
                    break;
            }
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else 
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            result.Average = 0.0;
            // sets highGrade to the lowest possible double value to allow any grade to be higher than it once
            // we enter data
            result.High = double.MinValue;
            //same thing for min now
            result.Low = double.MaxValue;

            for (var index = 0; index < grades.Count; index++)
            {
                // other way of coding if (number > highGrade) {highGrade = number}
                // what it actually does is take number, and highGrade and returns the Max value between the two
                result.High = Math.Max(grades[index], result.High);
                result.Low = Math.Min(grades[index], result.Low);
                result.Average += grades[index];
            };
            result.Average /= grades.Count; 

            switch (result.Average)
            {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;

                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;

                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;

                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;

                default:
                    result.Letter = 'F';
                    break;
            }

            return result;

        }


        //// Longform way of writing a property
        //public string Name
        //{
        //    get
        //    {
        //        return name;
        //    }
        //    set
        //    {
        //        if (!string.IsNullOrEmpty(value))
        //        {
        //            name = value;
        //        }
        //        else
        //        {
        //            throw new ArgumentNullException();
        //        }
        //    }
        //}
        //private string name;


        /* Shortform way of writing a property */
        private List<double> grades;


        readonly string category = "Science";
    }
}
