using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tab {
    protected string m_tabName = "";

    public abstract void DisplayTab();

    public string TabName {
        get { return m_tabName; }
    }
}
