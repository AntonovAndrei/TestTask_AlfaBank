using AlfaBank.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfaBank.DataAccess.IRepositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        void AddRange(IEnumerable<User> users);
        Task AddAsync(User user);
        void Add(User user);
    }
}
