using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

	public static Inventory Instance {get; private set;}
	
	public GameObject[] items;
	public GameObject[] slots;
	public GameObject itemWin;
	public GameObject textWin;
	public GameObject holder;
	
	
	void Awake(){
		slots = new GameObject[holder.transform.childCount];
		items = new GameObject[holder.transform.childCount];

		for (int i = 0; i < holder.transform.childCount; i++)
		{
			slots[i] = holder.transform.GetChild(i).gameObject;
			items[i] = holder.transform.GetChild(i).transform.GetChild(0).gameObject;
		}

		if(items.Length != slots.Length) Debug.LogError("missmatch of item- and slot-length");
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(gameObject);
			
			SortItems();
		}
		else Destroy(gameObject);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.I)){
			int s = SceneManager.GetActiveScene().buildIndex;
			if (itemWin.active)
			{
				itemWin.SetActive(false);
				textWin.SetActive(false);
			}
			else if(!itemWin.active && s!=0 && s!=1 && s!=2) {
				SortItems();
				showItems();
				ShowText(null);
				itemWin.SetActive(true);
				textWin.SetActive(true);

                SortItems();
                showItems();
            }
		}
        //SortItems();
        //showItems();
    }
	
	
	private void SortItems(){
		GameObject[] tmp = new GameObject[items.Length];
		
		int j = 0;
		for (int i = 0; i < items.Length; i++){
			if (items[i].GetComponent<Item>().status == Item.Status.INVENTORY){
				tmp[j] = items[i];
				j++;
			}
			
		}
		for (int i = 0; i < items.Length; i++){
			if (items[i].GetComponent<Item>().status != Item.Status.INVENTORY){
				tmp[j] = items[i];
				j++;
			}
		}
		
		Array.Copy(tmp, items, tmp.Length);
	}
	
	private void showItems(){
		for (int i = 0; i < slots.Length; i++){
			GameObject item = items[i];
			GameObject slot = slots[i];
			item.transform.SetParent(slot.transform, false);
			
			if (item.GetComponent<Item>().status == Item.Status.INVENTORY) {
				//Debug.Log(item.GetComponent<Item>().name + " should be visible in " + slot);
				item.SetActive(true);
			}
		}
	}
	
	
	// adds a new Item to your inventory
	public void activateItem(Item newItem){
		foreach (GameObject itemObject in items){
			Item item = itemObject.GetComponent<Item>();
			if (item.status == Item.Status.AVAILABLE && item.name.Equals(newItem.name))
            {
                item.status = Item.Status.INVENTORY;
				SortItems();
				showItems();
			}
		}
	}

	public void activateGameItem(String itemName)
	{
		foreach (GameObject itemObject in items){
			Item item = itemObject.GetComponent<Item>();
            if (item.status == Item.Status.AVAILABLE && item.name.Equals(itemName))
            {
                item.status = Item.Status.INVENTORY;
                SortItems();
                showItems();
            }
        }
	}

	// removes an Item from your inventory after it has been used
	public void deactivateItem(String itemName){
		foreach (GameObject itemObject in items){
			Item item = itemObject.GetComponent<Item>();
            //Debug.Log(item.name + ":");
			if (item.name.Equals(itemName) && item.status == Item.Status.INVENTORY){
                item.status = Item.Status.USED;
                itemObject.SetActive(false);
				SortItems();
				showItems();
			}
		}
	}
	
	public bool wasFound(string name){
		foreach (GameObject itemObject in items){
			Item item = itemObject.GetComponent<Item>();
            if (item.name.Equals(name)) {
                if (item.status != Item.Status.AVAILABLE) {
                    return true;
                }
            }
		}
		return false;
	}

	public void ShowText(Item item)
	{
		GameObject header = textWin.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
		GameObject body = textWin.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject;
		
		if (item == null)
		{
			//Debug.Log("on call");
			header.GetComponent<Text>().text = "Item information";
			body.GetComponent<Text>().text = "Klick auf ein Item um Informationen darüber zu sehen!";
			
		}
		else
		{
			// Debug.Log(item.name + "\n" + item.info);
			header.GetComponent<Text>().text = item.name;
			body.GetComponent<Text>().text = item.info;
		}
	}

}
