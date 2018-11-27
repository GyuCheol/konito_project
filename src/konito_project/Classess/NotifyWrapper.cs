using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Classess {
    public class NotifyWrapper<T> : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private T data;
        private string propertyName;
        private Type type;

        public string Text => type.GetProperty(propertyName).GetMethod.Invoke(data, null).ToString();

        public NotifyWrapper(T data, string propertyName) {
            this.data = data;
            this.propertyName = propertyName;

            type = data.GetType();
        }

        public void ChangeValue(object value) {
            type.GetProperty(propertyName).SetMethod.Invoke(data, new object[] { value });
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
        }

        public T GetSourceData => data;

    }
}
