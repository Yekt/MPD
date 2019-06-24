using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneChangerAnimated : MonoBehaviour {
	
	public Animator animator;
	private int nextScene;
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)) {
			AudioManager.Instance.Play("Click");
			if(SceneManager.GetActiveScene().buildIndex == 1){
				loadPreviousScene();
			}
			else loadScene(1);
		}
	}
	
    public void loadScene(int scene) {
		if(Inventory.Instance != null) Inventory.Instance.itemWin.SetActive(false);
		animator.SetTrigger("FadeOut");
		if(PersistentData.Instance.sceneLog[PersistentData.Instance.sceneLog.Count - 1] != scene){
			PersistentData.Instance.sceneLog.Add(scene);
		}
		nextScene = scene;
	}
	
	public void loadPreviousScene() {
		if(PersistentData.Instance.sceneLog.Count > 1){
			if(Inventory.Instance != null) Inventory.Instance.itemWin.SetActive(false);
			animator.SetTrigger("FadeOut");
			PersistentData.Instance.sceneLog.RemoveAt(PersistentData.Instance.sceneLog.Count - 1);
			int scene = PersistentData.Instance.sceneLog[PersistentData.Instance.sceneLog.Count - 1];
			nextScene = scene;
		}
	}
	
	private void onFadeComplete(){
		//print()
		SceneManager.LoadScene(nextScene);
	}
	
	private void print(){
		string str = "sceneLog:";
		foreach (int i in PersistentData.Instance.sceneLog){
			str += " " + i;
		}
		Debug.Log(str);	
	}
}
