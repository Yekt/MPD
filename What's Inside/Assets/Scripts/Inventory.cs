using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{

	public static Inventory Instance {get; private set;}
	
	public GameObject[] items;
	public GameObject[] slots;
	public GameObject window;	
	
	
	void Awake(){
		if(items.Length != slots.Length) Debug.LogError("missmatch of item- and slot-length");
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(gameObject);
			
			sortItems();
		}
		else Destroy(gameObject);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.I)){
			int s = SceneManager.GetActiveScene().buildIndex;
			if(window.active) window.SetActive(false);
			else if(!window.active && s!=0 && s!=1 && s!=2) {
				sortItems();
				showItems();
				window.SetActive(true);
			}
		}
		sortItems();
		showItems();
	}
	
	
	private void sortItems(){
		GameObject[] tmp = new GameObject[items.Length];
		
		int j = 0;
		for (int i = 0; i < items.Length; i++){
			if (items[i].GetComponent<Item>().available){
				tmp[j] = items[i];
				j++;
			}
			
		}
		for (int i = 0; i < items.Length; i++){
			if (!items[i].GetComponent<Item>().available){
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
			
			if (item.GetComponent<Item>().available) {
				//Debug.Log(item.GetComponent<Item>().name + " should be visible in " + slot);
				item.SetActive(true);
			}
		}
	}
	
	
	// adds a new Item to your inventory
	public void activateItem(Item newItem){
		foreach (GameObject itemObject in items){
			Item item = itemObject.GetComponent<Item>();
			if (!item.available){
				if (item.name.Equals(newItem.name)){
					item.available = true;
					item.found = true;
					sortItems();
					showItems();
				}
			}
		}
	}
	
	// removes an Item from your inventory after it has been used
	public void deactivateItem(Item usedItem){
		foreach (GameObject itemObject in items){
			Item item = itemObject.GetComponent<Item>();
			if (item.available){
				if (item.name.Equals(usedItem.name)){
					item.available = false;
					sortItems();
					showItems();
				}
			}
		}
	}
	
	public bool wasFound(Item itemInQuestion){
		foreach (GameObject itemObject in items){
			Item item = itemObject.GetComponent<Item>();
			if (item.name.Equals(itemInQuestion.name)){
				return item.found;
			}
		}
		return false;
	}

}
