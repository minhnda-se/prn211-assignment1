using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class HraccountDAO
    {
        private readonly CandidateManagementContext context;
        private static HraccountDAO instance = null;
        public static HraccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HraccountDAO();
                }
                return instance;
            }
        }
        public HraccountDAO()
        {
            context = new CandidateManagementContext();
        }

        public Hraccount GetHraccount(string email)
        {
            return context.Hraccounts.SingleOrDefault(a => a.Email.Equals(email));
        }
        public List<Hraccount> GetHraccounts()
        {
            return context.Hraccounts.ToList();
        }
    }
}
