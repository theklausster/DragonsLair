using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOConverter
{
    // T = Entity
    // TD = DTO Object
    public abstract class DTOAbstract <T, TD>
    {

        public IEnumerable<TD> Convert(IEnumerable<T> listWithEntities)
        {
            List<TD> ConvertedList = new List<TD>();
            foreach (var item in listWithEntities)
            {
                ConvertedList.Add(Convert(item));
            }
            return ConvertedList;
        }
        
        public abstract TD Convert(T t);

    }
}
