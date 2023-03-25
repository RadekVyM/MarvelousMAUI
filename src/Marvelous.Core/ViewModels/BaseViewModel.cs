using Marvelous.Core.Interfaces.ViewModels;
using System.ComponentModel;

namespace Marvelous.Core.ViewModels
{
    public abstract class BaseViewModel : IBaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}