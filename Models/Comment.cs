using System;

namespace Backend.Challenge.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string Identificador { get; set; }
        public string Texto { get; set; }
        public string Autor { get; set; }
        public DateTime Data { get; set; }

    }
}