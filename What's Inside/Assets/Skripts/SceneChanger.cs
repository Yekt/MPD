using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneChanger : MonoBehaviour{
	
	
    public void loadScene(int sceneNumber){		
		PersistentData.Instance.sceneLog.Add(sceneNumber);
		SceneManager.LoadScene(sceneNumber);
	}
	
	public void loadPreviousScene(){
		if(PersistentData.Instance.sceneLog.Count > 1){
			PersistentData.Instance.sceneLog.RemoveAt(PersistentData.Instance.sceneLog.Count - 1);
			int scene = PersistentData.Instance.sceneLog[PersistentData.Instance.sceneLog.Count - 1];
			SceneManager.LoadScene(scene);
		}
	}
}
