using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using GameOfChallengers.Services;

namespace GameOfChallengers.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //created the datastore object which will store either mock data or sql data
        private IDataStore DataStoreMock => DependencyService.Get<IDataStore>() ?? MockDataStore.Instance;
        private IDataStore DataStoreSql => DependencyService.Get<IDataStore>() ?? SQLDataStore.Instance;

        public IDataStore DataStore;

        //default datastore is selected as mock
        public BaseViewModel()
        {
            SetDataStore(DataStoreEnum.Mock);
        }

        public enum DataStoreEnum { Unknown = 0, Sql = 1, Mock = 2 }

        //switch case for selecting datastore as per the switch toggled on about page
        public void SetDataStore(DataStoreEnum data)
        {
            switch (data)
            {
                case DataStoreEnum.Mock:
                    DataStore = DataStoreMock;//this will select the mockdatastore
                    break;

                case DataStoreEnum.Sql: 
                case DataStoreEnum.Unknown:  //this will select sql
                default:
                    DataStore = DataStoreSql; 
                    break;
            }
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        string title = string.Empty;
        //setting title
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        //setting the property
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        //event occurs when the property is changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
