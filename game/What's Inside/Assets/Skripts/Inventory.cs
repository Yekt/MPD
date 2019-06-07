using System;
using System.Collections.Generic;
using UnityEngine;


    public class Inventory : MonoBehaviour {
    
        public static Inventory inv;
        public List<Item> items = new List<Item>();

        private void Awake()
        {
            Debug.Log("create inventory");
            inv = this;
        }

        public void AddItem(Item item)
        {
            if (!items.Contains(item))
                items.Add(item);
        }

        // Start is called before the first frame update
        void Start() {
        
        }

        // Update is called once per frame
        void Update() {
        
        }
        
    }

