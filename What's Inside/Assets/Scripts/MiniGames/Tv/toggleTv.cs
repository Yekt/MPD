using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleTv : MonoBehaviour
{
    public bool isFixedSprite;

    void Start()
    {
        if (isFixedSprite)
        {
            if (PersistentData.Instance.tvFixed) this.gameObject.SetActive(true);
            else this.gameObject.SetActive(false);
        }
        else
        {
            if (PersistentData.Instance.tvFixed) this.gameObject.SetActive(false);
            else this.gameObject.SetActive(true);
        }
    }
}
