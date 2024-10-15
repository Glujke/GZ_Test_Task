using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GZ_Test_WPF_Application.Models
{
    public class Doctor : INotifyPropertyChanged
    {
        private int id;
        private string surname;
        private string name;
        private string father;
        private Specialization specialization;
        private Cabinet cabinet;
        private Area area;

        public int Id { get => id; set { id = value; OnPropertyChanged("Id"); } }
        public string Surname { get => surname; set { surname = value; OnPropertyChanged("Surname"); } }
        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }
        public string Father { get => father; set { father = value; OnPropertyChanged("Father"); } }
        public int CabinetId { get => Cabinet.Id; }
        public int AreaId { get => Area.Id; }
        public int SpecializationId { get => Specialization.Id; }
        public Cabinet Cabinet { get => cabinet; set { cabinet = value; OnPropertyChanged("Cabinet"); } }
        public Specialization Specialization { get => specialization; set { specialization = value; OnPropertyChanged("Specialization"); } }
        public Area Area { get => area; set { area = value; OnPropertyChanged("Area"); } }



        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
                       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        
    }
}
