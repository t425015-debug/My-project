using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    private List<PocketItem> _items
        = new List<PocketItem>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void _ItemGet(PocketItem _item)
    {
        PocketItem newItem =
       new PocketItem();
        newItem._name = _item._name;
        newItem._count = _item._count;

        _items.Add(newItem);

        Debug.Log($"{_item._name}‚ðŽæ“¾");
    }
}
