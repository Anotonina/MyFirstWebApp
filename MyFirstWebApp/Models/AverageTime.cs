using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApp.Models
{
    public class RequestProfile
    {
        [Key]
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public string Parametrs { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int TotalCount  { get; set; }

    }
}
