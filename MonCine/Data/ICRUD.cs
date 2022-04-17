using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace MonCine.Data
{
    public interface ICRUD<T>
    {
        string CollectionName { get; set; }


        List<T> ReadItems();

        Task<bool> AddItem(T pObj);
        Task<bool> UpdateItem(T pObj);
        Task<bool> DeleteItem(T pObj);

    }
}
