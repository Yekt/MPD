using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameChecker : MonoBehaviour
{
    public Sprite sprite;
    public SceneChangerAnimated sceneChanger;
    public GameObject back;

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
            if (!PersistentData.Instance.creditsRolled)
            {
                back.SetActive(false);
                AudioManager.Instance.Play("SpielAbgeschlossen");
            }
            gameObject.GetComponent<Image>().sprite = sprite;
        }
    }

    void Update()
    {
        if ((Time.time - start) >= 17f && flag && !PersistentData.Instance.creditsRolled)
        {
            PersistentData.Instance.creditsRolled = true;
            sceneChanger.loadScene(2);
        }
    }
}
