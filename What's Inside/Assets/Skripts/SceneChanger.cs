using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneChanger : MonoBehaviour {
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)) {
			AudioManager.Instance.Play("Click");
			if(SceneManager.GetActiveScene().buildIndex == 1){
				loadPreviousScene();
			}
			else loadScene(1);
		}
	}
	
    public void loadScene(int sceneNumber) {		
		if(PersistentData.Instance.sceneLog[PersistentData.Instance.sceneLog.Count - 1] != sceneNumber){
			PersistentData.Instance.sceneLog.Add(sceneNumber);
		}
		//print();
		SceneManager.LoadScene(sceneNumber);
	}
	
	public void loadPreviousScene() {
		if(PersistentData.Instance.sceneLog.Count > 1){
			PersistentData.Instance.sceneLog.RemoveAt(PersistentData.Instance.sceneLog.Count - 1);
			int scene = PersistentData.Instance.sceneLog[PersistentData.Instance.sceneLog.Count - 1];
			//print();
			SceneManager.LoadScene(scene);
		}
	}
	
	private void print(){
		string str = "sceneLog:";
		foreach (int i in PersistentData.Instance.sceneLog){
			str += " " + i;
		}
		Debug.Log(str);	
	}
}
