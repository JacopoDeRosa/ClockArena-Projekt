using System;
using System.Collections.Generic;
public interface IBarAction
{
    public IEnumerable<BarAction> GetBarActions();

    public event Action onActionEnd;
}
