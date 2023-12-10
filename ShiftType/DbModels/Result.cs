using ShiftType.Migrations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShiftType.DbModels
{
    public class Result
    {
        public int Id { get; set; }
        /// <summary>
        /// The text Typist
        /// </summary>
        public User? User { get; set; }
        /// <summary>
        /// Typed Text
        /// </summary>
        public string TypedText { get; set; }
        /// <summary>
        /// Original Text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Words per minute
        /// </summary>
        public double Wpm { get; set; }

        /// <summary>
        /// The type mode of a test
        /// </summary>
        public int TestType { get; set; }

        /// <summary>
        /// A JSON string that represents an array of every second typed WPM
        /// </summary>
        public string TypedSeconds { get; set; }
        /// <summary>
        /// Amount of incorrect letters in a test
        /// </summary>
        public int Errors { get; set; }

        /// <summary>
        /// Seconds Spent on a test 
        /// </summary>
        public double TimeSpent { get; set; }

        /// <summary>
        /// Seconds Spent on a test 
        /// </summary>
        //public string Language { get; set; }

        /// <summary>
        /// Date When the test was written
        /// </summary>
        public DateTime Date { get; set; }


        [NotMapped]
        /// <summary>
        /// Accuracy of the test
        /// </summary>
        public int Accuracy { get => (int)Math.Floor(((Wpm - Errors) / Math.Max(Wpm, 1)) * 100); }

        [NotMapped]
        /// <summary>
        /// Differenct between max and min speeds of the test
        /// </summary>
        public int Consistency { get => TypedSeconds == null ? 0 : (int)(((double)Math.Max(JsonSerializer.Deserialize<int[]>(TypedSeconds).Min(),1) / Math.Max(JsonSerializer.Deserialize<int[]>(TypedSeconds).Max(),1)) * 100); }
    }
}
