using UnityEngine;

public class toggleRadio : MonoBehaviour
{
    public bool isFixedSprite;

    void Start()
    {
        if (isFixedSprite)
        {
            if (PersistentData.Instance.radioFixed) this.gameObject.SetActive(true);
            else this.gameObject.SetActive(false);
        }
        else
        {
            if (PersistentData.Instance.radioFixed) this.gameObject.SetActive(false);
            else this.gameObject.SetActive(true);
        }
    }
    
}
