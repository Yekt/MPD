using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

     public GameObject inventory;
     public GameObject slotHolder;
     public bool invEnabled;
     private int slotAmount;
     private GameObject[] slots;

     void Start()
     {
          slotAmount = 16;
          slots = new GameObject[slotAmount];

          for (int i = 0; i < slotAmount; i++)
          {
               slots[i] = slotHolder.transform.GetChild(i).gameObject;
          }

     }

     void Update()
     {
          UpdateSlots();
          inventory.SetActive(invEnabled);
     }


     void UpdateSlots()
     {
          for (int i = 0; i < PersistentData.Instance.inventory.Count; i++) {
               
          }
     }

}
