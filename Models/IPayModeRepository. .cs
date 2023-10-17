using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket_mvp.Models
{
    internal interface IPayModeRepository
    {
        void add(PayModeModel payModeModel);
        void Edit(PayModeModel payModeModel);
        void Delete(int id);

        IEnumerable<PayModeModel> GetAll();
        IEnumerable<PayModeModel> GetByValues(string values);

    }
}
