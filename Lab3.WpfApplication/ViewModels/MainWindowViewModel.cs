using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab2.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Lab3.WpfApplication.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private List<Animal> _animals = new List<Animal>();
        private Owner[] _owners;
        private string _searchDescription;
        private Owner _selectedOwner;
        private Owner _editableOwner;
        private string _selectedIdRange;
        private readonly OwnerDbContext _db;

        public List<Animal> Animals
        {
            get => _animals;
            set
            {
                _animals = value;
                OnPropertyChanged();
            }
        }

        public Owner[] Owners
        {
            get => _owners;
            set
            {
                _owners = value;
                OnPropertyChanged();
            }
        }

        public string SearchDescription
        {
            get => _searchDescription;
            set
            {
                _searchDescription = value;
                OnPropertyChanged();
            }
        }

        public Owner SelectedOwner
        {
            get => _selectedOwner;
            set
            {
                _selectedOwner = value;
                OnPropertyChanged();
                LoadAnimals();
                EditableOwner = _selectedOwner != null ? new Owner
                {
                    Id = _selectedOwner.Id,
                    Surname = _selectedOwner.Surname,
                    Height = _selectedOwner.Height
                } : null;
                RaiseCanExecuteChanged(); 
            }
        }

        public Owner EditableOwner
        {
            get => _editableOwner;
            set
            {
                _editableOwner = value;
                OnPropertyChanged();
            }
        }

        public List<string> IdRanges { get; } = new() { "1-10", "10-20", "20+" };

        public string SelectedIdRange
        {
            get => _selectedIdRange;
            set
            {
                _selectedIdRange = value;
                OnPropertyChanged();
                FilterData(); 
            }
        }

        public ICommand SelectOwnerCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand DeleteOwnerCommand { get; }
        public ICommand UpdateOwnerCommand { get; }

        public MainWindowViewModel()
        {
            _db = new OwnerDbContext();
            SelectOwnerCommand = new RelayCommand(SelectOwner);
            SearchCommand = new RelayCommand(FilterData);
            DeleteOwnerCommand = new RelayCommand(DeleteOwner, CanDeleteOwner);
            UpdateOwnerCommand = new RelayCommand(UpdateOwner, CanUpdateOwner);
            Load(); 
        }

        public void Load()
        {
            Owners = _db.Owners.ToArray();
        }

        public void LoadAnimals()
        {
            if (SelectedOwner == null)
            {
                Animals = new List<Animal>();
                return;
            }

            Animals = _db.Animals.Where(a => a.Owner.Id == SelectedOwner.Id).ToList();
        }

        public void FilterData()
        {
            if (SelectedOwner == null)
            {
                return; 
            }

            var query = _db.Animals.Where(a => a.Owner.Id == SelectedOwner.Id);

            
            if (!string.IsNullOrWhiteSpace(SearchDescription))
            {
                query = query.Where(a => a.Description.ToLower().Contains(SearchDescription.ToLower()));
            }

           
            if (!string.IsNullOrEmpty(SelectedIdRange))
            {
                query = SelectedIdRange switch
                {
                    "1-10" => query.Where(a => a.Id >= 1 && a.Id <= 10),
                    "10-20" => query.Where(a => a.Id > 10 && a.Id <= 20),
                    "20+" => query.Where(a => a.Id > 20),
                    _ => query
                };
            }

            Animals = query.ToList();
        }

        private void SelectOwner()
        {
            
            if (SelectedOwner != null)
            {
                LoadAnimals();
            }
        }

        private bool CanDeleteOwner() => SelectedOwner != null;

        private void DeleteOwner()
        {
            if (SelectedOwner == null) return;

            
            var animalsToDelete = _db.Animals.Where(a => a.Owner.Id == SelectedOwner.Id).ToList();
            _db.Animals.RemoveRange(animalsToDelete);
            _db.Owners.Remove(SelectedOwner);
            _db.SaveChanges();

            Load();
            Animals = new List<Animal>();
        }

        private bool CanUpdateOwner() => SelectedOwner != null;

        private void UpdateOwner()
        {
            if (SelectedOwner == null) return;

           
            SelectedOwner.Surname = EditableOwner.Surname;
            SelectedOwner.Height = EditableOwner.Height;

           
            _db.Update(SelectedOwner);
            _db.SaveChanges();
            Load(); 
        }

        private void RaiseCanExecuteChanged()
        {
            (DeleteOwnerCommand as RelayCommand)?.NotifyCanExecuteChanged();
            (UpdateOwnerCommand as RelayCommand)?.NotifyCanExecuteChanged();
        }
    }
}
