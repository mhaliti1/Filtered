using Filtered.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Filtered
{
    public class ViewModel: ViewModelBase
    {

        #region Properties 

        private int itemPerPage = 10;
        private int itemcount;
        private int _currentPageIndex;

        private Boolean _isBusy;
        public Boolean IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);

            }

        }

        private int _total;
        public int Total
        {
            get { return _total; }
            set
            {
                SetProperty(ref _total, value);

            }

        }

        public int TotalItems
        {
            get { return itemcount; }
            set
            {
                itemcount = value;
                OnPropertyChanged("TotalItems");
            }
        }
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set
            {
                _currentPageIndex = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        public int CurrentPage
        {
            get { return _currentPageIndex + 1; }
        }
        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            private set
            {
                _totalPages = value;
                OnPropertyChanged("TotalPage");
            }
        }
        public int PerPage
        {
            get { return itemPerPage; }
            private set
            {
                itemPerPage = value;
                OnPropertyChanged("PerPage");
            }
        }

        #endregion

        #region Commands

        private DelegateCommand searchCommand;
        public DelegateCommand SearchCommand
        {
            get
            {
                return this.searchCommand;
            }
            set
            {
                this.searchCommand = value;
            }
        }

        private DelegateCommand saveproject;
        public DelegateCommand SaveProject
        {
            get
            {
                return this.saveproject;
            }
            set
            {
                this.saveproject = value;
            }
        }


        private async void ExecuteSaveProjectCommand(object parameter)
        {
            IsBusy = true;

            await Task.Run(() =>
            {
                //Run your update to databse 
                Thread.Sleep(3000);
                
                OnPropertyChanged("SelectedProject");

            });

            IsBusy = false;
        }



        private DelegateCommand command;
        public DelegateCommand Command
        {
            get
            {
                return this.command;
            }
            set
            {
                this.command = value;
            }
        }

        private DelegateCommand previouscommand;
        public DelegateCommand PreviousCommand
        {
            get
            {
                return this.previouscommand;
            }
            set
            {
                this.previouscommand = value;
            }
        }
        private DelegateCommand nextcommand;
        public DelegateCommand NextCommand
        {
            get
            {
                return this.nextcommand;
            }
            set
            {
                this.nextcommand = value;
            }
        }
        private DelegateCommand firstcommand;
        public DelegateCommand FirstCommand
        {
            get
            {
                return this.firstcommand;
            }
            set
            {
                this.firstcommand = value;
            }
        }

        private DelegateCommand lastcommand;
        public DelegateCommand LastCommand
        {
            get
            {
                return this.lastcommand;
            }
            set
            {
                this.lastcommand = value;
            }
        }

        private DelegateCommand filterCommand;
        public DelegateCommand FilterCommand
        {
            get
            {
                return this.filterCommand;
            }
            set
            {
                this.filterCommand = value;
            }
        }

        private DelegateCommand openfilterWindow;
        public DelegateCommand OpenFilterWindow
        {
            get
            {
                return this.openfilterWindow;
            }
            set
            {
                this.openfilterWindow = value;
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            private set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");

                
            }
        }

        #endregion

        #region Pagination 

        public void ShowNextPage()
        {
            CurrentPageIndex++;
            
        }

        public void ShowPreviousPage()
        {
            CurrentPageIndex--;
            
        }

        public void ShowFirstPage()
        {
            CurrentPageIndex = 0;
            
        }

        public void ShowLastPage()
        {
            CurrentPageIndex = TotalPages - 1;
           
        }
 
        void view_Filter(object sender, FilterEventArgs e)
        {
            int index = ((Project)e.Item).Row - 1;
            if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void CalculateTotalPages()
        {
            if (itemcount % itemPerPage == 0)
            {
                TotalPages = (itemcount / itemPerPage);
            }
            else
            {
                TotalPages = (itemcount / itemPerPage) + 1;
            }
        }

        #endregion

        #region Filter
        private ObservableCollection<CheckedListItem<string>> _developers;
        private ObservableCollection<CheckedListItem<string>> _status;
        private ObservableCollection<CheckedListItem<string>> _business;
        private ObservableCollection<CheckedListItem<string>> _months;
        private ObservableCollection<CheckedListItem<string>> _years;

        private Boolean _isAllDevelopers;
        private Boolean _isAllStatus;
        private Boolean isAllBusiness;
        private Boolean _isAllMonths;
        private Boolean _isAllYears;

        public ObservableCollection<CheckedListItem<string>> Developers
        {
            get { return _developers; }
            set
            {
                SetProperty(ref _developers, value);

            }

        }
        public ObservableCollection<CheckedListItem<string>> Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);

            }

        }
        public ObservableCollection<CheckedListItem<string>> Business
        {
            get { return _business; }
            set
            {
                SetProperty(ref _business, value);

            }

        }
        public ObservableCollection<CheckedListItem<string>> Months
        {
            get { return _months; }
            set
            {
                SetProperty(ref _months, value);

            }

        }
        public ObservableCollection<CheckedListItem<string>> Years
        {
            get { return _years; }
            set
            {
                SetProperty(ref _years, value);

            }

        }


        public Boolean IsAllDevelopers
        {
            get { return _isAllDevelopers; }
            set
            {
                SetProperty(ref _isAllDevelopers, value);

                if (value)
                {
                    foreach (CheckedListItem<string> item in Developers)
                    {
                        item.IsChecked = false;
                    }

                   
                }
                else
                {
                    foreach (CheckedListItem<string> item in Developers)
                    {
                        item.IsChecked = true;
                    }
                }

                OnPropertyChanged("Developers");

               

            }

        }
        public Boolean IsAllStatus
        {
            get { return _isAllStatus; }
            set
            {
                SetProperty(ref _isAllStatus, value);

                if (value)
                {
                    foreach (CheckedListItem<string> item in Status)
                    {
                        item.IsChecked = false;
                    }


                }
                else
                {
                    foreach (CheckedListItem<string> item in Status)
                    {
                        item.IsChecked = true;
                    }
                }

                OnPropertyChanged("Status");

            }

        }
        public Boolean IsAllBusiness
        {
            get { return isAllBusiness; }
            set
            {
                SetProperty(ref isAllBusiness, value);

                if (value)
                {
                    foreach (CheckedListItem<string> item in Business)
                    {
                        item.IsChecked = false;
                    }


                }
                else
                {
                    foreach (CheckedListItem<string> item in Business)
                    {
                        item.IsChecked = true;
                    }
                }

                OnPropertyChanged("Business");

            }

        }

        public Boolean IsAllMonths
        {
            get { return _isAllMonths; }
            set
            {
                SetProperty(ref _isAllMonths, value);

                if (value)
                {
                    foreach (CheckedListItem<string> item in Months)
                    {
                        item.IsChecked = false;
                    }


                }
                else
                {
                    foreach (CheckedListItem<string> item in Months)
                    {
                        item.IsChecked = true;
                    }
                }

                OnPropertyChanged("Months");

            }

        }

        public Boolean IsAllYears
        {
            get { return _isAllYears; }
            set
            {
                SetProperty(ref _isAllYears, value);

                if (value)
                {
                    foreach (CheckedListItem<string> item in Years)
                    {
                        item.IsChecked = false;
                    }


                }
                else
                {
                    foreach (CheckedListItem<string> item in Years)
                    {
                        item.IsChecked = true;
                    }
                }

                OnPropertyChanged("Years");

            }

        }
        #endregion


        private int _MaxRecords = 500000;
        public int MaxRecords
        {
            get { return _MaxRecords; }
            set
            {
                SetProperty(ref _MaxRecords, value);

            }

        }


        private List<string> BusinessList;
        private List<string> DeveloperList;
        private List<string> StatusList;
        private List<Project> _ProjectQuery;
        public ViewModel()
        {
            this.Command = new DelegateCommand(this.ExecuteCommand, this.CanRunCommand);

            this.NextCommand = new DelegateCommand(this.ExecuteNextCommand, this.CanExecuteNext);
            this.PreviousCommand = new DelegateCommand(this.ExecutePreviousCommand, this.CanExecutePrevious);
            this.FirstCommand = new DelegateCommand(this.ExecuteFirstCommand, this.CanExecuteFirst);
            this.LastCommand = new DelegateCommand(this.ExecuteLastCommand, this.CanExecuteLast);

            this.FilterCommand = new DelegateCommand(this.ExecuteFilterCommand, this.CanExecuteFilter);

            this.SearchCommand = new DelegateCommand(this.ExecuteSearchCommand, this.CanExecuteSearch);

            this.SaveProject = new DelegateCommand(this.ExecuteSaveProjectCommand, this.CanRunSaveProjectCommand);
            

            DeveloperList = new List<string>();
            StatusList = new List<string>();
            BusinessList = new List<string>();

         
            DeveloperList.Add("Muhamet Haliti");
            DeveloperList.Add("David Miller");
            DeveloperList.Add("Wayne Preston");
            DeveloperList.Add("Ted Badje");
            DeveloperList.Add("Katie Ho");
            DeveloperList.Add("Byan Grant");

            StatusList.Add("Open");
            StatusList.Add("Working");
            StatusList.Add("Hold");
            StatusList.Add("Completed");

            BusinessList.Add("Account Services");
            BusinessList.Add("Bankruptcy");
            BusinessList.Add("Legal");
            BusinessList.Add("Finance");
            BusinessList.Add("Marketing");
            BusinessList.Add("Escrow");


        }



        private async void ExecuteSearchCommand(object parameter)
        {
            IsBusy = true;

           
            await Task.Run(() =>
            {
                int i = 0;

               
                _ProjectQuery = (from item in _projects
                .Where(pr => pr.Developer.Contains(SearchText) || pr.Status.Contains(SearchText) || pr.Description.Contains(SearchText))
                 



                                 select new Project
                                 {

                                     Id = item.Id,
                                     Row = i++,
                                     Name = item.Name,
                                     Developer = item.Developer,
                                     Status = item.Status,


                                 }

                ).ToList();




            });


            _total = _ProjectQuery.Count();
            _ViewList = new CollectionViewSource();
            _ViewList.Source = _ProjectQuery;
            _ViewList.Filter += new FilterEventHandler(view_Filter);



            CurrentPageIndex = 0;
            itemcount = _ProjectQuery.Count;
            CalculateTotalPages();



            OnPropertyChanged("ViewList");
            OnPropertyChanged("Total");
            OnPropertyChanged("TotalPages");
            OnPropertyChanged("CurrentPageIndex");
            OnPropertyChanged("CurrentPageIndex");
            OnPropertyChanged("CurrentPage");
            OnPropertyChanged("Users");

            ViewList.View.Refresh();



            IsBusy = false;
        }

        public bool CanExecuteSearch(object parameter)
        {
            return !string.IsNullOrEmpty(SearchText) && !IsBusy;
        }




        private async void ExecuteFilterCommand(object parameter)
        {
            IsBusy = true;

            await Task.Run(() =>
            {
                int i=0;
                _ProjectQuery = (from item in _projects

                .Where(pr => Developers.FirstOrDefault(p => pr.Developer == p.Item && p.IsChecked == true) != null)
                .Where(pr => Status.FirstOrDefault(p => pr.Status == p.Item && p.IsChecked == true) != null)
                .Where(pr => Business.FirstOrDefault(p => pr.Business == p.Item && p.IsChecked == true) != null)
                .Where(pr => Months.FirstOrDefault(p => pr.Date.ToString("MMMM") == p.Item && p.IsChecked == true) != null)
                .Where(pr => Years.FirstOrDefault(p => pr.Date.Year.ToString() == p.Item && p.IsChecked == true) != null)



                                 select new Project
                                 {

                                     Id = item.Id,
                                     Row = i++,
                                     Name = item.Name,
                                     Developer = item.Developer,
                                     Status = item.Status,

                                     
                                 }
                                 
                ).ToList();




            });


                _total = _ProjectQuery.Count();
                _ViewList = new CollectionViewSource();
                _ViewList.Source = _ProjectQuery;
                _ViewList.Filter += new FilterEventHandler(view_Filter);



                CurrentPageIndex = 0;
                itemcount = _ProjectQuery.Count;
                CalculateTotalPages();



                OnPropertyChanged("ViewList");
                OnPropertyChanged("Total");
                OnPropertyChanged("TotalPages");
                OnPropertyChanged("CurrentPageIndex");
                OnPropertyChanged("CurrentPageIndex");
                OnPropertyChanged("CurrentPage");


                  OnPropertyChanged("Users");
            ViewList.View.Refresh();

          

            IsBusy = false;
        }

        public bool CanExecuteFilter(object parameter)
        {
            return !IsBusy;
        }


        private async void ExecuteFirstCommand(object parameter)
        {
            IsBusy = true;

            await Task.Run(() =>
            {

                ShowFirstPage();

                OnPropertyChanged("ViewList");
            });

            ViewList.View.Refresh();

            IsBusy = false;
        }
        private async void ExecuteLastCommand(object parameter)
        {
            IsBusy = true;

            await Task.Run(() =>
            {

                ShowLastPage();

                OnPropertyChanged("ViewList");
            });

            ViewList.View.Refresh();

            IsBusy = false;
        }
        public bool CanExecuteLast(object parameter)
        {
            return CurrentPage != TotalPages;
        }
        public bool CanExecuteFirst(object parameter)
        {
            return CurrentPageIndex != 0;
        }
        private async void ExecutePreviousCommand(object parameter)
        {
            IsBusy = true;

            await Task.Run(() =>
            {

                ShowPreviousPage();

                OnPropertyChanged("ViewList");
            });

            ViewList.View.Refresh();

            IsBusy = false;
        }

        public bool CanExecutePrevious(object parameter)
        {
            return CurrentPageIndex != 0;
        }

        private async void ExecuteNextCommand(object parameter)
        {
            IsBusy = true;

            await Task.Run(() =>
            {

                ShowNextPage();

                OnPropertyChanged("ViewList");
            });

            ViewList.View.Refresh();

            IsBusy = false;
        }

        public bool CanExecuteNext(object parameter)
        {
            return TotalPages - 1 > CurrentPageIndex;
        }

      
        static Random rnd = new Random();
        private async void ExecuteCommand(object parameter)
        {
            IsBusy = true;

            await Task.Run(() =>
            {

 

                _projects = new List<Project>();

                int f = 0;
                for (int i = 0; i < MaxRecords; i++)
                {

                    int r = rnd.Next(0, DeveloperList.Count);
                    int rs = rnd.Next(0, StatusList.Count);
                    int rt = rnd.Next(0, BusinessList.Count);

                    int x = rnd.Next(0, 6);

                    _projects.Add(new Project
                    {
                        Id = i,
                        Row = f ++,
                        Name = "Project " + i ,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat",
                        Developer = (string)DeveloperList[r],
                        Status = (string)StatusList[rs],
                        Business = (string)BusinessList[rt],
                        Date = DateTime.Now.AddMonths(x),
                    });
                }

               
            });

         

            _developers = new ObservableCollection<CheckedListItem<string>>();

            foreach (string cust in Projects.Select(w => w.Developer).Distinct().OrderBy(w => w))
            {
                _developers.Add(new CheckedListItem<string> { Item = cust, IsChecked = true });

            }

            _users = new List<User>();


            foreach (string cust in Projects.Select(w => w.Developer).Distinct().OrderBy(w => w))
            {
                _users.Add(new User
                {
                    Name = cust,
                     IsChecked= false
                });

            }





            _status = new ObservableCollection<CheckedListItem<string>>();

            foreach (string cust in Projects.Select(w => w.Status).Distinct().OrderBy(w => w))
            {
                _status.Add(new CheckedListItem<string> { Item = cust, IsChecked = true });

            }

            _business = new ObservableCollection<CheckedListItem<string>>();

            foreach (string cust in Projects.Select(w => w.Business).Distinct().OrderBy(w => w))
            {
                _business.Add(new CheckedListItem<string> { Item = cust, IsChecked = true });

            }

            _months = new ObservableCollection<CheckedListItem<string>>();

            foreach (string cust in Projects.Select(w => w.Date.ToString("MMMM")).Distinct().OrderBy(w => w))
            {
                _months.Add(new CheckedListItem<string> { Item = cust, IsChecked = true });

            }


            _years = new ObservableCollection<CheckedListItem<string>>();

            foreach (string cust in Projects.Select(w => w.Date.Year.ToString()).Distinct().OrderBy(w => w))
            {
                _years.Add(new CheckedListItem<string> { Item = cust, IsChecked = true });

            }



            _total = _projects.Count();


            _ViewList = new CollectionViewSource();
            _ViewList.Source = _projects;
            _ViewList.Filter += new FilterEventHandler(view_Filter);
           

            CurrentPageIndex = 0;
            itemcount = _projects.Count;
            CalculateTotalPages();


          

            OnPropertyChanged("Filtered");
            OnPropertyChanged("Total");

            OnPropertyChanged("ViewList");

            OnPropertyChanged("TotalPages");
            OnPropertyChanged("CurrentPageIndex");
            OnPropertyChanged("CurrentItem");

            OnPropertyChanged("CurrentPage");

           

            OnPropertyChanged("Users");



            IsBusy = false;
        }



        private CollectionViewSource _ViewList;
        public CollectionViewSource ViewList
        {
            get { return _ViewList; }
            set
            {
                SetProperty(ref _ViewList, value);

            }

        }


        private List<Project> _projects;
        public List<Project> Projects
        {
            get { return _projects; }
            set
            {
                SetProperty(ref _projects, value);

            }

        }


        private Project _selectedProject;
        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                SetProperty(ref _selectedProject, value);

            }

        }
        private List<Project> _filtered;
        public List<Project> Filtered
        {
            get { return _filtered; }
            set
            {
                SetProperty(ref _filtered, value);

            }

        }


        private List<User> _users;
        public List<User> Users
        {
            get { return _users; }
            set
            {
                SetProperty(ref _users, value);

            }

        }




        public bool CanRunSaveProjectCommand(object parameter)
        {
            return SelectedProject !=null;
        }





        public bool CanRunCommand(object parameter)
        {
            return true;
        }

    }


    

   



    public class CheckedListItem<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isChecked;
        private T item;

        public CheckedListItem()
        {

        }

        public CheckedListItem(T item, bool isChecked = false)
        {
            this.item = item;
            this.isChecked = isChecked;
        }

        public T Item
        {
            get { return item; }
            set
            {
                item = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Item"));
            }
        }

        public bool IsChecked
        {

            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
            }
        }
    }

 




    public class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }



    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
