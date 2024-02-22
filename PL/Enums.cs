using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;

internal class FilterByEngineerExpCollection : IEnumerable
{
    static readonly IEnumerable<BO.FilterByEngineerExperience> e_enums =
    (Enum.GetValues(typeof(BO.FilterByEngineerExperience)) as IEnumerable<BO.FilterByEngineerExperience>)!;
    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}

internal class EngineerExpCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> e_enums =
    (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;
    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}