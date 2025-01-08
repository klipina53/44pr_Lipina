﻿using System;
using System.Collections.Generic;
using System.Text;
using TaskManager_Lipina.Classes;
using TaskManager_Lipina.Models;
using TaskManager_Lipina.Context;
using System.Linq;
using System.Collections.ObjectModel;

namespace TaskManager_Lipina.ViewModels
{
    public class VM_Tasks : Notification
    {
        public TasksContext tasksContext = new TasksContext();
        public ObservableCollection<Tasks> Tasks { get; set; }
        public VM_Tasks() =>
            Tasks = new ObservableCollection<Tasks>(tasksContext.Tasks.OrderBy(x => x.Done));
        public RelayCommand OnAddTask
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Tasks NewTask = new Tasks()
                    {
                        DateExecute = DateTime.Now
                    };
                    Tasks.Add(NewTask);
                    tasksContext.Tasks.Add(NewTask);
                    tasksContext.SaveChanges();
                });
            }
        }
    }
}
