using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    public string[] dumbFuckers;
    public int szene;
    public SceneChangerAnimated sceneChanger;

    public void enterGame()
    {
        bool flag = true;
        foreach(string idiot in dumbFuckers)
        {
            if (!Inventory.Instance.wasFound(idiot)) {
                Debug.Log(idiot + " was not found");
                flag = false;
            } 
        }

        if (flag) sceneChanger.loadScene(szene);
        else {
            Debug.Log("Couldn't find all Items");
            AudioManager.Instance.Play("FehlendeTeile");
        }
        
    }


}
