using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrunoSilvaLibrary.Models.Extended
{
    public class Media
    {
        public Media() { }
        public int MID { get; set; }
        public string Title { get; set; }
        public decimal  Budget { get; set; }
        public string Language { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int PublishYear { get; set; }
    }
}