using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameChecker : MonoBehaviour
{
    public Sprite sprite;
    public SceneChangerAnimated sceneChanger;
    private bool flag;
    private float start;

    void Start()
    {
        if (PersistentData.Instance.tvFixed &&
            PersistentData.Instance.vacuumFixed &&
            PersistentData.Instance.ovenFixed &&
            PersistentData.Instance.radioFixed)
        {
            flag = true;
            start = Time.time;
            gameObject.GetComponent<Image>().sprite = sprite;
            AudioManager.Instance.Play("SpielAbgeschlossen");
        }
    }

    void Update()
    {
        if ((Time.time - start) >= 17f && flag) sceneChanger.loadScene(2);
    }
}
