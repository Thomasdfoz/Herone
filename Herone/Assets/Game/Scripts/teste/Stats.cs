using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerConnection
{        
    [SerializeField]
    private BarScript bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }
        set
        {
            currentVal = value;
            bar.Value = currentVal;
            if (currentVal < 0)
            {
                currentVal = 0;
            }else if (currentVal > maxVal)
            {
                currentVal = maxVal;
            }
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            maxVal = value;
            bar.MaxValue = value;
        }
    }

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
