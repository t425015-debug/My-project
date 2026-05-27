using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public List<PocketItem> _items
        = new List<PocketItem>(); // ƒŠƒXƒg‚Ì‰Šú‰»

    int _count = 0;
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
        newItem._itemSprite = _item._itemSprite;
        newItem._explaningText = _item._explaningText;
        newItem._count = _item._count;


        _items.Add(newItem);

        Debug.Log($"{_item._name}‚ğæ“¾");
    }
}
