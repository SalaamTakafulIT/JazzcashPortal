using JazzcashPortal.DAL;
using JazzcashPortal.Models;
using System.Data;
using static JazzcashPortal.Controllers.AccountController;

namespace JazzcashPortal.BLL
{
    public class AccountService
    {
        private readonly AccountRepository _dal;
        public AccountService(AccountRepository dal)
        {
            _dal = dal;
        }

        public DataTable JazzcashValidate(Account model)
        {
            return _dal.JazzcashValidate(model);
        }
    }
}
