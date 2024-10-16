using GZ_Test_WPF_Application.Api;
using GZ_Test_WPF_Application.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GZ_Test_WPF_Application.ViewModel
{
    public class DoctorViewModel : INotifyPropertyChanged
    {
        private readonly Doctor _doctor;
        public Doctor Origin => _doctor;

        public List<Specialization> Specializations { get; set; }
        public List<Area> Areas { get; set; }
        public List<Cabinet> Cabinets { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public int Id { get => _doctor.Id; set { _doctor.Id = value; OnPropertyChanged("Id"); } }
        public string Surname { get => _doctor.Surname; set { _doctor.Surname = value; OnPropertyChanged("Surname"); } }
        public string Name { get => _doctor.Name; set { _doctor.Name = value; OnPropertyChanged("Name"); } }
        public string Father { get => _doctor.Father; set { _doctor.Father = value; OnPropertyChanged("Father"); } }
        public int SpecializationId { get => _doctor.SpecializationId; set { _doctor.SpecializationId = value; OnPropertyChanged("SpecializationId"); } }
        public int CabinetId { get => _doctor.CabinetId; set { _doctor.CabinetId = value; OnPropertyChanged("CabinetId"); } }
        public int AreaId { get => _doctor.AreaId; set { _doctor.AreaId = value; OnPropertyChanged("AreaId"); } }

        public Cabinet Cabinet { get => Cabinets.FirstOrDefault(c => c.Id == CabinetId);
            set { _doctor.Cabinet = value; CabinetId = _doctor.Cabinet.Id; OnPropertyChanged("Cabinet"); } }

        public Specialization Specialization { get => Specializations.FirstOrDefault( s => s.Id == SpecializationId); 
            set { _doctor.Specialization = value; SpecializationId = _doctor.Specialization.Id;  OnPropertyChanged("Specialization"); } }

        public Area Area { get => Areas.FirstOrDefault(a => a.Id == AreaId);
            set { _doctor.Area = value; AreaId = _doctor.Area.Id; OnPropertyChanged("Area"); } }


        public DoctorViewModel(Doctor doctor, List<Specialization> specializations, List<Cabinet> cabinets,
                                  List<Area> areas)
        {
            _doctor = doctor;
            Specializations = specializations;
            Cabinets = cabinets;
            Areas = areas;
        }
    }
}
