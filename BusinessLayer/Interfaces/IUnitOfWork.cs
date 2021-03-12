﻿using BusinessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region Methode
        int Commit();
        #endregion

        MessageRepository Message { get; }
        CarRepository Car { get; }
        AccountRepository Account { get; }
        RoleRepository Role { get; }

    }
}
