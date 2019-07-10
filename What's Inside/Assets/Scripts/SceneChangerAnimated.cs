using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	    if (Inventory.Instance != null)
	    {
		    Inventory.Instance.itemWin.SetActive(false);
		    Inventory.Instance.textWin.SetActive(false);
		    AudioManager.Instance.speechBubble.SetActive(false);
	    }
        AudioManager.Instance.Stop();
		animator.SetTrigger("FadeOut");
		if(PersistentData.Instance.sceneLog[PersistentData.Instance.sceneLog.Count - 1] != scene){
			PersistentData.Instance.sceneLog.Add(scene);
		}
		nextScene = scene;
	}
	
	public void loadPreviousScene() {
		if(PersistentData.Instance.sceneLog.Count > 1){
			if (Inventory.Instance != null)
			{
				Inventory.Instance.itemWin.SetActive(false);
				Inventory.Instance.textWin.SetActive(false);
				AudioManager.Instance.speechBubble.SetActive(false);
			}
            AudioManager.Instance.Stop();
			animator.SetTrigger("FadeOut");
			PersistentData.Instance.sceneLog.RemoveAt(PersistentData.Instance.sceneLog.Count - 1);
			int scene = PersistentData.Instance.sceneLog[PersistentData.Instance.sceneLog.Count - 1];
			nextScene = scene;
		}
	}
	
	private void onFadeComplete(){
		//print()
		SceneManager.LoadScene(nextScene);

        aelexaTalk();        
	}

    private void aelexaTalk()
    {
        PersistentData persistentData = PersistentData.Instance;
        AudioManager audioManager = AudioManager.Instance;
        switch (nextScene) {
            case (3):   // Flur
                if (!persistentData.enteredHallway)
                {
                    persistentData.enteredHallway = true;
                    audioManager.Play("FlurErstesBetreten1");
                }
                else if (!persistentData.creditsRolled && persistentData.tvFixed) audioManager.Play("Flush");
                else
                {
                    audioManager.Play("FlurBetreten");
                }
                break;
            case (4):   // Schlafzimmer
                if (!persistentData.enteredDorm)
                {
                    persistentData.enteredDorm = true;
                    audioManager.Play("SchlafzimmerErstesBetreten");
                }
                else audioManager.Play("SchlafzimmerBetreten");
                break;
            case (5):   // Wohnzimmer
                if (!persistentData.enteredLivingroom)
                {
                    persistentData.enteredLivingroom = true;
                    audioManager.Play("WohnzimmerErstesBetreten");
                }
                else audioManager.Play("WohnzimmerBetreten");
                break;
            case (6):   // Küche
                if (!persistentData.ovenFixed)
                {
                    persistentData.enteredKitchen = true;
                    audioManager.Play("KuecheErstesBetreten");
                }
                else
                {
                    audioManager.Play("KuecheBetreten");
                }
                break;
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
