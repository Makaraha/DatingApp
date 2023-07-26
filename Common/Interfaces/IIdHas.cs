using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IIdHas<out TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
    }
}
