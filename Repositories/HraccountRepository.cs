using BusinessObjects.Models;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class HraccountRepository : IHraccountRepository
    {
        public Hraccount GetHrAccount(string email)
            => HraccountDAO.Instance.GetHraccount(email);

        public List<Hraccount> GetHraccounts()
            => HraccountDAO.Instance.GetHraccounts();
    }
}
