using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    
    public String name;
    public String info;

    public enum Status { AVAILABLE, INVENTORY, USED};
    public Status status;

    public bool inventoryItem;

    //private bool selected;


    void Start() {
		// Debug.Log(name);
        if(Inventory.Instance.wasFound(name) && !inventoryItem) gameObject.SetActive(false);
    }
	
    /*void Update() {
		if (selected) {
			Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector2(cursorPos.x, cursorPos.y);
		}
		if(Input.GetMouseButtonUp(0)){
			selected = false;
		}
        
    }*/

	
    public void addToInventory() {
        if (status == Status.AVAILABLE){
			Inventory.Instance.activateItem(this);
            status = Status.INVENTORY;
			gameObject.SetActive(false);
		}
    }
	
	/*void OnMouseOver() {
		if (Input.GetMouseButtonDown(0))
			selected = true;
	}*/
}
