using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameInformation 
{
    private static InGameInformation _instance;
    public static InGameInformation Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InGameInformation();
            }
            return _instance;
        }
    }
}
