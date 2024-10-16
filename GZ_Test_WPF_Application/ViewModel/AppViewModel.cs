using GZ_Test_WPF_Application.Api;
using GZ_Test_WPF_Application.Command;
using GZ_Test_WPF_Application.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace GZ_Test_WPF_Application.ViewModel
{

    public class AppViewModel : INotifyPropertyChanged
    {
        private const string URL_API = "http://localhost:5000";
        private List<Specialization> _specializations;
        private List<Cabinet> _cabinets;
        private List<Area> _areas;

        private bool isAddButtonEnabled;
        public bool IsAddButtonEnabled { get => isAddButtonEnabled; private set { isAddButtonEnabled = value; OnPropertyChanged("IsAddButtonEnabled"); } }

        private bool isEditButtonEnabled;
        public bool IsEditButtonEnabled { get => isEditButtonEnabled; private set { isEditButtonEnabled = value; OnPropertyChanged("IsEditButtonEnabled"); } }

        private DoctorViewModel selectedDoctor;
        public DoctorViewModel SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
                IsAddButtonEnabled = selectedDoctor != null && selectedDoctor.Id == 0;
                IsEditButtonEnabled = selectedDoctor != null && selectedDoctor.Id != 0;
            }
        }
        public ObservableCollection<DoctorViewModel> Doctors { get; set; }

        public RelayCommand RemoveCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand ApplyAddCommand { get; private set; }
        public RelayCommand ApplyChangesCommand { get; private set; }


        public AppViewModel()
        {
            CreateDoctorViewModel();
            CreateRelayCommand();
        }
        private void CreateDoctorViewModel()
        {
            using (var httpClient = new HttpApi(URL_API))
            {
                _specializations = httpClient.GetItems<Specialization>() as List<Specialization>;
                _areas = httpClient.GetItems<Area>() as List<Area>;
                _cabinets = httpClient.GetItems<Cabinet>() as List<Cabinet>;

                var doctors = httpClient.GetItems<Doctor>();
                Doctors = new ObservableCollection<DoctorViewModel>();
                foreach (var doctor in doctors) Doctors.Add(new DoctorViewModel(doctor, _specializations, _cabinets, _areas));
            }
        }

        private void CreateRelayCommand()
        {
            ApplyChangesCommand = new RelayCommand(ApplyChangesDoctor);
            RemoveCommand = new RelayCommand(RemoveDoctor, (obj) => Doctors.Count > 0);
            AddCommand = new RelayCommand(AddNewDoctor);
            ApplyAddCommand = new RelayCommand(ApplyAddDoctor);
        }
        private void RemoveDoctor(Object obj)
        {
            try
            {
                DoctorViewModel doctor = obj as DoctorViewModel;
                var err = CheckError(doctor);
                if (err != "") { MessageBox.Show(err); return; }
                using (var httpClient = new HttpApi(URL_API))
                {
                    httpClient.DeleteItem<Doctor>(doctor.Id);
                }
                Doctors.Remove(doctor);
                MessageBox.Show("Успешно удалено.");
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplyAddDoctor(Object obj)
        {
            try
            {
                DoctorViewModel doctor = obj as DoctorViewModel;
                var err = CheckError(doctor);
                if (err != "") { MessageBox.Show(err); return; }
                using (var httpClient = new HttpApi(URL_API))
                {
                    var doc = httpClient.AddItem<Doctor>(doctor.Origin);
                    doctor.Id = doc.Id;
                }
                Doctors.Insert(0, doctor);
                SelectedDoctor = doctor;
                MessageBox.Show("Успешно добавлено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ApplyChangesDoctor(Object obj)
        {
            try
            {
                DoctorViewModel doctor = obj as DoctorViewModel;
                var err = CheckError(doctor);
                if (err != "") { MessageBox.Show(err); return; }
                using (var httpClient = new HttpApi(URL_API))
                {
                    httpClient.UpdateItem<Doctor>(doctor.Id, doctor.Origin);
                }
                MessageBox.Show("Успешно изменено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddNewDoctor(Object obj)
        {
            DoctorViewModel doctor = new DoctorViewModel(new Doctor(), _specializations, _cabinets, _areas);
            SelectedDoctor = doctor;
        }

        private string CheckError(DoctorViewModel doctor)
        {
            if (doctor == null) return "Некорректный ввод данных.";
            if (doctor.Surname == null) return "Незаполнено обязательное поле \"Фамилия\"";
            if (doctor.Name == null) return "Незаполнено обязательное поле \"Имя\"";
            if (doctor.Father == null) return "Незаполнено обязательное поле \"Отчество\"";
            if (doctor.CabinetId <= 0)  return "Незаполнено обязательное поле \"Кабинет\""; 
            if (doctor.SpecializationId <= 0)  return "Незаполнено обязательное поле \"Специализация\""; 
            if (doctor.AreaId <= 0) return "Незаполнено обязательное поле \"Участок\""; 
            return "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
