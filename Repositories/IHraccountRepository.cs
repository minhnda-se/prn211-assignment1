using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IHraccountRepository
    {
        Hraccount GetHrAccount(string email);
        List<Hraccount> GetHraccounts();
    }
}
