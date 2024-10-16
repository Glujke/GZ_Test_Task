using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GZ_Test_WPF_Application.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public Specialization Specialization { get; set; }
        public Cabinet Cabinet { get; set; }
        public Area Area { get; set; }
        public int CabinetId { get; set; }
        public int AreaId { get; set; }
        public int SpecializationId { get; set; }
        
    }
}
