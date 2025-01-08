using System;
using System.Collections.Generic;
using System.Text;
using TaskManager_Lipina.Classes;
using System.Text.RegularExpressions;
using Schema = System.ComponentModel.DataAnnotations.Schema;
using TaskManager_Lipina.Models;
using System.Windows;

namespace TaskManager_Lipina.Models
{
    public class Tasks : Notification
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                Match match = Regex.Match(value, "^.{1,50}$");
                if (!match.Success)
                    MessageBox.Show("Наименование не должно быть пустым, и не более 50 символов.", "Некорректный ввод значения.");
                else
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string priority;
        public string Priority
        {
            get { return priority; }
            set
            {
                Match match = Regex.Match(value, "^.{1,30}$");
                if (!match.Success)
                    MessageBox.Show("Приоритет не должен быть пустым, и не более 30 символов.", "Некорректный ввод значения.");
                else
                {
                    priority = value;
                    OnPropertyChanged("Priority");
                }
            }
        }
        private DateTime dateExecute;
        public DateTime DateExecute
        {
            get { return dateExecute; }
            set
            {
                if (value.Date < DateTime.Now.Date)
                    MessageBox.Show("Дата выполнения не может быть меньше текущей.", "Некорректный ввод значения.");
                else
                {
                    dateExecute = value;
                    OnPropertyChanged("DateExecute");
                }
            }
        }
        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                Match match = Regex.Match(value, "^.{1,1000}$");
                if (!match.Success)
                    MessageBox.Show("Комментарий не должен быть пустым, и не более 1000 символов.", "Некорректный ввод значения.");
                else
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        public bool done;
        public bool Done
        {
            get { return done; }
            set
            {
                done = value;
                OnPropertyChanged("Done");
                OnPropertyChanged("IsDoneText");
            }
        }
        [Schema.NotMapped]
        private bool isEnable;
        [Schema.NotMapped]
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                OnPropertyChanged("IsEnable");
                OnPropertyChanged("IsEnableText");
            }
        }
        [Schema.NotMapped]
        public string IsEnableText
        {
            get
            {
                if (IsEnable) return "Сохранить";
                else return "Изменить";
            }
        }
        [Schema.NotMapped]
        public string IsDoneText
        {
            get
            {
                if (Done) return "Не выполнено";
                else return "Выполнено";
            }
        }
        [Schema.NotMapped]
        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    IsEnable = !IsEnable;
                    if (!IsEnable)
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.tasksContext.SaveChanges();
                });
            }
        }
        [Schema.NotMapped]
        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить задачу?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.Tasks.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.tasksContext.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.tasksContext.SaveChanges();
                    }
                });
            }
        }
        [Schema.NotMapped]
        public RelayCommand OnDone
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Done = !Done;
                });
            }
        }
    }
}
