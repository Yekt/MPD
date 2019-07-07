using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameChecker : MonoBehaviour
{
    public Sprite sprite;

    void Start()
    {
        if (PersistentData.Instance.tvFixed &&
            PersistentData.Instance.vacuumFixed &&
            PersistentData.Instance.ovenFixed &&
            PersistentData.Instance.radioFixed)
        {
            gameObject.GetComponent<Image>().sprite = sprite;
            AudioManager.Instance.Play("SpielAbgeschlossen");
        }
    }
}
