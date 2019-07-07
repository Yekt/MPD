using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    public string[] items;
    public int szene;
    public SceneChangerAnimated sceneChanger;

    public void enterGame()
    {
        bool flag = true;
        foreach(string item in items)
        {
            if (!Inventory.Instance.wasFound("item")) flag = false;
        }

        if (flag) sceneChanger.loadScene(szene);
        else Debug.Log("Couldn't find all Items");
    }


}
