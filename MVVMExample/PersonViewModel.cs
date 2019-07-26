using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



namespace MVVMExample
{
    class PersonViewModel : ViewModelBase
    {
        #region Fields 

        public ObservableCollection<PersonModel> people { get; set; }
        public ObservableCollection<string> Jobs { get; set; }

        public ObservableCollection<PersonModel> FilteredPeopleList
        {
            get
            {
                if (_selectedJob == null || _selectedJob.Equals(""))
                {
                    return people;
                } else
                {
                    return new ObservableCollection<PersonModel>(from p in people where p.Job.Equals(_selectedJob) select p);
                }
                
            }
        }

        private string _name;
        public string name
        {
            get
            {
                return _name;
            } 
            set
            {
                Set(ref _name, value);

                //The line below is meant to change between execute() and canexecute()
                enterPersonCommand.RaiseCanExecuteChanged();   
            }
        }

        private int _age;
        public int age
        {
            get
            {
                return _age;
            } 
            set
            {
                Set(ref _age, value);
            }
        }


        private string _job;
        public string job
        {
            get
            {
                return _job;
            }
            set
            {
                if(!Jobs.Contains(value))
                {
                    Jobs.Add(value);
                }
                Set(ref _job, value);
                enterPersonCommand.RaiseCanExecuteChanged();
            }
        }



        private string _total;
        public string total
        {
            get
            {
                return _total;
            }
            set
            {
                Set(ref _total, value);
            }
        }


        private bool _selected;
        public bool selected
        {
            get
            {
                return _selected;
            }
            set
            {
                Set(ref _selected, value);
            }
        }


        private string _selectedJob;
        public string SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                //if (_selectedObject == value)
                //    return;
                Set(ref _selectedJob, value);
                RaisePropertyChanged("FilteredPeopleList");
            }
        }


        private PersonModel _selectedPerson;
        public PersonModel SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                Set(ref _selectedPerson, value);
                if (_selectedPerson != null)
                {
                    populatePersonDetailForm(value.Name, value.Age, value.Job, value.Selected);
                }
                updatePersonCommand.RaiseCanExecuteChanged();
            }
        }



        #endregion

        #region MVVMLight RelayCommand
        private RelayCommand enterPersonCommand;
        public RelayCommand EnterPersonCommand
        {
            get
            {
                if (enterPersonCommand == null)
                {
                    enterPersonCommand = new RelayCommand(() =>
                    {
                        enterPerson();
                        name = "";
                        age = 0;
                        job = "";
                    },
                   () => !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(job));
                }
                return enterPersonCommand;
            }

        }


        private RelayCommand deletePersonCommand;
        public RelayCommand DeletePersonCommand
        {
            get
            {
                if(deletePersonCommand == null)
                {
                    deletePersonCommand = new RelayCommand(() =>
                    {
                        removePerson();   
                    });
                }
                return deletePersonCommand;
            }
        }


        private RelayCommand updatePersonCommand;
        public RelayCommand UpdatePersonCommand
        {
            get
            {
                if(updatePersonCommand == null)
                {
                    updatePersonCommand = new RelayCommand(() =>
                    {
                        updatePerson();
                    },
                    () => _selectedPerson != null);
                }
                return updatePersonCommand;
            }
        }
        #endregion
        


        public PersonViewModel ()
        {
            people = new ObservableCollection<PersonModel>();
            Jobs = new ObservableCollection<string>();
            total = "Total: " + people.Count;
        }

        private void enterPerson ()
        {
            PersonModel p = new PersonModel() { Name = name, Age = age , Job = job, Selected = false};
            people.Add(p);
            total = "Total: " + people.Count;
        }


        private void removePerson()
        {
            foreach (PersonModel p in people.ToList())
            {
                Console.WriteLine(p.ToString());
                if (p.Selected)
                {                    
                    people.Remove(p);                    
                }
            }
            RaisePropertyChanged("FilteredPeopleList");
            total = "Total: " + people.Count;
        }


        private void populatePersonDetailForm(string n, int a, string j, bool isSelected)
        {
            name = n;
            age = a;
            job = j;
            selected = isSelected;
        }


        private void updatePerson ()
        {
            Console.WriteLine(_selectedPerson.ToString());
            SelectedPerson = null;
            name = "";
            age = 0;
            job = "";
        }

    }
}
