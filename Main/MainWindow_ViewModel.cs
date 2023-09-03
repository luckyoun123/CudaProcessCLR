using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CLR;
namespace Main
{
    class MainWindow_ViewModel : INotifyPropertyChanged
    {
        int m_nValue1;
        int m_nValue2;
        int m_nResult;

        public int p_nValue1
        {
            get => m_nValue1;
            set
            {
                m_nValue1 = value;
                OnPropertyChanged();
            }
        }

        public int p_nValue2
        {
            get => m_nValue2;
            set
            {
                m_nValue2 = value;
                OnPropertyChanged();
            }
        }

        public int p_nResult
        {
            get => m_nResult;
            set
            {
                m_nResult = value;
                OnPropertyChanged();
            }
        }

        public ICommand CmdCalcurate
        {
            get => new DelegateCommand(Calcurate);
        }

        public void Calcurate()
        {
            p_nResult = CLR.CLR_Calc.clr_sum(p_nValue1, p_nValue2);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //C# 6 null-safe operator. No need to check for event listeners
            //If there are no listeners, this will be a noop
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class DelegateCommand : ICommand
        {

            private readonly Func<bool> canExecute;
            private readonly Action execute;

            /// <summary>
            /// Initializes a new instance of the DelegateCommand class.
            /// </summary>
            /// <param name="execute">indicate an execute function</param>
            public DelegateCommand(Action execute) : this(execute, null)
            {
            }

            /// <summary>
            /// Initializes a new instance of the DelegateCommand class.
            /// </summary>
            /// <param name="execute">execute function </param>
            /// <param name="canExecute">can execute function</param>
            public DelegateCommand(Action execute, Func<bool> canExecute)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }
            /// <summary>
            /// can executes event handler
            /// </summary>
            public event EventHandler CanExecuteChanged;

            /// <summary>
            /// implement of icommand can execute method
            /// </summary>
            /// <param name="o">parameter by default of icomand interface</param>
            /// <returns>can execute or not</returns>
            public bool CanExecute(object o)
            {
                if (this.canExecute == null)
                {
                    return true;
                }
                return this.canExecute();
            }

            /// <summary>
            /// implement of icommand interface execute method
            /// </summary>
            /// <param name="o">parameter by default of icomand interface</param>
            public void Execute(object o)
            {
                this.execute();
            }

            /// <summary>
            /// raise ca excute changed when property changed
            /// </summary>
            public void RaiseCanExecuteChanged()
            {
                if (this.CanExecuteChanged != null)
                {
                    this.CanExecuteChanged(this, EventArgs.Empty);
                }
            }
        }
    }
}
