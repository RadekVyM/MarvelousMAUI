using System.ComponentModel;

namespace Marvelous.Core.Interfaces.ViewModels
{
    public interface IBaseViewModel : INotifyPropertyChanged
    {
        void OnPropertyChanged(string propertyName);
    }
}
