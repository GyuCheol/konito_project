using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Commands {
    public class NotifyWrapper<T> : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public T Data { get; private set; }

        private string propertyName;
        private Type type;

        public string Text => type.GetProperty(propertyName).GetMethod.Invoke(Data, null).ToString();

        public NotifyWrapper(T data, string propertyName) {
            Data = data;
            this.propertyName = propertyName;

            type = data.GetType();
        }

        public void ChangeValue(object value) {
            type.GetProperty(propertyName).SetMethod.Invoke(Data, new object[] { value });
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
        }
    }
}
