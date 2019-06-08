using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour {
	// Singelton
	public static PersistentData Instance {get; private set;}
	
	// Persistent Data Fields
	public List<int> sceneLog;
	
	[Range(0f, 1f)]
	public float volume;
	
	
	private void Awake(){
		if(Instance == null){
			// init singleton
			Instance = this;
			DontDestroyOnLoad(gameObject);
			
			// init persistent fields
			sceneLog = new List<int>();
			sceneLog.Add(1);
			volume = 0.5f;
		}
		else Destroy(gameObject);
	}
}
