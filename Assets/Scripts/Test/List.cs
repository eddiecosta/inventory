using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class List : MonoBehaviour
{
    [SerializeField] private List<string> _name = new List<string>();
    [SerializeField] private List<int> _value = new List<int>();

    void Start()
    {
        _name.Add("Luana");
        _name.Add("James");
        _name.Add("Lily");
        _name.Add("Adriano");
        _value.Add(1);
        _value.Add(2);
        _value.Add(3);
        _value.Add(4);

        _name = ReverseSort(_name);
        //_value = ReverseSort(_value);

    }

    private List<T> ReverseSort<T>(List<T> listToReverse)
    {
        List<T> isReversed = new List<T>();
        for (int i = listToReverse.Count - 1; i >= 0; i--)
        {
            isReversed.Add(listToReverse[i]);
        }
        return isReversed;
    }
}
