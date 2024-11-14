using CommunityToolkit.Mvvm.ComponentModel;
using Lab2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Lab3.WpfApplication.ViewModels
{
    public class MainWindowViewModel: ObservableObject
    {
       
            private OwnerDbContext _db;


            public MainWindowViewModel()
            {
                _db = new OwnerDbContext();
            }
            private Owner[] _owners;
            public Owner[] Owners => _owners;

            public void Load()
            {
                _owners = _db.Owners.ToArray();
            }
        }
    }


