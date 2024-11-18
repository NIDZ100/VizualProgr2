using CommunityToolkit.Mvvm.ComponentModel;
using Lab2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Lab3.WpfApplication.ViewModels
{
    public class MainWindowViewModel: ObservableObject , INotifyPropertyChanged 
    {
        private List<Animal> _animals = new List<Animal>();
        private Owner _selectedOwner;

        public List<Animal> Animals
        {
            get
            {
                return _animals;
            }
            set
            {
                _animals = value;
                OnPropertyChanged();
            }
        }
       
        public Owner SelectedOwner { get; set; } 
        private OwnerDbContext _db;

        public void LoadAnimals()
        {
            if (SelectedOwner == null)
            {
                return;
            }
            Animals = _db.Animals.Where(o => o.Owner.Id == SelectedOwner.Id).ToList();
        }
        public MainWindowViewModel()
            {
                _db = new OwnerDbContext();
                SelectOwnerCommand = new RelayCommand(LoadAnimals);
            }
            private Owner[] _owners;
            public Owner[] Owners => _owners;

            public void Load()
            {
                _owners = _db.Owners.ToArray();
            }
        public ICommand SelectOwnerCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        

    }
}


