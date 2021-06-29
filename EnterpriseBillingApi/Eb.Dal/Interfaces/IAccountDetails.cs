using System;
using System.Collections.Generic;
using System.Text;

namespace Eb.Dal.Interfaces
{
    public interface IAccountDetails
    {
        void GetAccounts();
        void GetAccountDetails(int accountId);
    }
}
