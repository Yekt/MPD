using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{

    public String name;
    public Sprite sprite;
    public Collider col;
    public bool inInventory;
    public GameObject itemGameObject;

    void Start()
    {
        inInventory = false;
        col = GetComponent<Collider>();
    }

    void Update()
    {
        itemGameObject.SetActive(!inInventory);
    }

    void MouseDownEvent()
    {
        PersistentData.Instance.inventory.Add(itemGameObject);
    }
}
