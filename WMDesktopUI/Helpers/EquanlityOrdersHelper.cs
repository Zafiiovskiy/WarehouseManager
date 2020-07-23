using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMDesktopUI.Library.DataManaging.Models;

namespace WMDesktopUI.Helpers
{
    public class EquanlityOrdersHelper : IEqualityComparer<OReverseModel>
    {
        public bool Equals(OReverseModel x, OReverseModel y)
        {
            if(x.ProductId == y.ProductId && x.ClientId == y.ClientId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(OReverseModel obj)
        {
            return obj.GetHashCode();
        }
    }
}
