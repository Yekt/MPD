using UnityEngine;

public class toggleOven : MonoBehaviour
{
    public bool isFixedSprite;

    void Start()
    {
        if (isFixedSprite)
        {
            if (PersistentData.Instance.ovenFixed) this.gameObject.SetActive(true);
            else this.gameObject.SetActive(false);
        }
        else
        {
            if (PersistentData.Instance.ovenFixed) this.gameObject.SetActive(false);
            else this.gameObject.SetActive(true);
        }
    }
}
