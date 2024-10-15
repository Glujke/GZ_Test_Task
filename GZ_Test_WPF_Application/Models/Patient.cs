using System;

namespace GZ_Test_WPF_Application.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }    
        public string Father { get; set; }
        public Area Area { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public DateTime BurnDate { get; set; }
    }
}
