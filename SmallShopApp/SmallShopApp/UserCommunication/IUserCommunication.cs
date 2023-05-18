using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallShopApp.UserCommunication
{
    public interface IUserCommunication
    {
        string SelectOption();
        string GetValue();
    }
}
