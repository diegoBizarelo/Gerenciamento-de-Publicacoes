﻿using LibraryManagement.Interfaces.Repository;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Repositories
{
    public interface IAuthorRepository : IRepository<Guid, Author>
    {
    }
}
