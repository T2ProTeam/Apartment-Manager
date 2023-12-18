using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Utilities
{
    public delegate Task<List<TCombobox>> LoadCombobox<TCombobox>() where TCombobox : ComboboxBase;

    public class ComboboxBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<ComboboxBase> Items { get; private set; }

        protected virtual void Dispose()
        {
        }

        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public async Task LoadItemsAsync(LoadCombobox<ComboboxBase> loadComboboxDelegate)
        {
            if (loadComboboxDelegate != null)
            {
                Items = await loadComboboxDelegate.Invoke();
                OnPropertyChanged(nameof(Items));
            }
        }
    }
}