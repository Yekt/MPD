﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour {
	// Singelton
	public static PersistentData Instance {get; private set;}
	
	// Persistent Data Fields
	public List<int> sceneLog;
	
	[Range(0f, 1f)]
	public float volume;
	
	public bool vacuumFixed;
    public bool radioFixed;
    public bool ovenFixed;
    public bool tvFixed;
    public bool enteredHallway;
    public bool enteredKitchen;
    public bool enteredDorm;
    public bool enteredLivingroom;
    public bool creditsRolled;
	
	
	private void Awake(){
		if(Instance == null){
			// init singleton
			Instance = this;
			DontDestroyOnLoad(gameObject);
			
			// init persistent fields
			sceneLog = new List<int>();
			sceneLog.Add(SceneManager.GetActiveScene().buildIndex); // add start scene to the list
			volume = 0.5f;
			vacuumFixed = false;
            radioFixed = false;
            ovenFixed = false;
            tvFixed = false;
            enteredHallway = false;
            enteredKitchen = false;
            enteredDorm = false;
            enteredLivingroom = false;
            creditsRolled = false;
}
		else Destroy(gameObject);
	}
}
