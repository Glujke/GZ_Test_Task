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
        private Doctor selectedDoctor;

        public ObservableCollection<Doctor> Doctors { get; set; }
        public Doctor SelectedDoctor
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

        private bool isAddButtonEnabled;
        public bool IsAddButtonEnabled { get => isAddButtonEnabled; private set { isAddButtonEnabled = value; OnPropertyChanged("IsAddButtonEnabled"); } }

        private bool isEditButtonEnabled;
        public bool IsEditButtonEnabled { get => isEditButtonEnabled; private set { isEditButtonEnabled = value; OnPropertyChanged("IsEditButtonEnabled"); } }

        public RelayCommand RemoveCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand ApplyAddCommand { get; private set; }
        public RelayCommand ApplyChangesCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public AppViewModel()
        {
            using (var httpClient = new HttpApi<Doctor>(URL_API))
            {
                Doctors = httpClient.GetItems();
            }
            ApplyChangesCommand = new RelayCommand(ApplyChangesDoctor);
            RemoveCommand = new RelayCommand(RemoveDoctor, (obj) => Doctors.Count > 0);
            AddCommand = new RelayCommand(AddNewDoctor);
            ApplyAddCommand = new RelayCommand(ApplyAddDoctor);
        }

        private void RemoveDoctor(Object obj)
        {
            try
            {
                Doctor doctor = obj as Doctor;
                var err = CheckError(doctor);
                if (err != "") { MessageBox.Show(err); return; }
                using (var httpClient = new HttpApi<Doctor>(URL_API))
                {
                    httpClient.DeleteItem(doctor.Id);
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
                Doctor doctor = obj as Doctor;
                var err = CheckError(doctor);
                if (err != "") { MessageBox.Show(err); return; }
                using (var httpClient = new HttpApi<Doctor>(URL_API))
                {
                    var doc = httpClient.AddItem(doctor);
                    doctor.Id = doc.Id;
                }
                Doctors.Insert(0, doctor);
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
                Doctor doctor = obj as Doctor;
                var err = CheckError(doctor);
                if (err != "") { MessageBox.Show(err); return; }
                using (var httpClient = new HttpApi<Doctor>(URL_API))
                {
                    httpClient.UpdateItem(doctor.Id, doctor);
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
            Doctor doctor = new Doctor();
            SelectedDoctor = doctor;
        }

        private string CheckError(Doctor doctor)
        {
            if (doctor == null) return "Некорректный ввод данных.";
            if (doctor.Surname == null) return "Незаполнено обязательное поле \"Фамилия\"";
            if (doctor.Name == null) return "Незаполнено обязательное поле \"Имя\"";
            if (doctor.Father == null) return "Незаполнено обязательное поле \"Отчество\"";
            if (doctor.Cabinet == null)  return "Незаполнено обязательное поле \"Кабинет\""; 
            if (doctor.Specialization == null)  return "Незаполнено обязательное поле \"Специализация\""; 
            if (doctor.Area == null) return "Незаполнено обязательное поле \"Участок\""; 
            return "";
        }
    }
}
