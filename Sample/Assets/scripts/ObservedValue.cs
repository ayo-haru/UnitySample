using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObservedValue<T>
{
    private T currentValue;
    public event Action OnValueChange;

    public ObservedValue(T initialValue)
    {
        currentValue = initialValue;
    }

    public T Value
    {
        get { return currentValue; }

        set
        {
            if (!currentValue.Equals(value))
            {
                currentValue = value;

                if (OnValueChange != null)
                {
                    OnValueChange();
                }
            }
        }
    }

    public void SetSilently(T value)
    {
        currentValue = value;
    }
}