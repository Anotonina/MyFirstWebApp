using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApp.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class RequestProfile
    {
        [Key]
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public string Parametrs { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int TotalCount  { get; set; }

    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
