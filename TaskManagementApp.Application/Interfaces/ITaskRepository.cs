using System;
using System.Collections.Generic;
using System.Text;
using TaskManagmentApp.Core.Entities;

namespace TaskManagementApp.Application.Interfaces
{
    public interface ITaskRepository: IGenericRepository<Task>
    {
    }
}
