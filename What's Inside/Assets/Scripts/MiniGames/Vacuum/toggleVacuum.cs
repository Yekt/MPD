using UnityEngine;

public class toggleVacuum : MonoBehaviour
{
    public bool isFixedSprite;

    void Start()
    {
        if (isFixedSprite)
        {
            if (PersistentData.Instance.vacuumFixed) this.gameObject.SetActive(true);
            else this.gameObject.SetActive(false);
        }
        else
        {
            if (PersistentData.Instance.vacuumFixed) this.gameObject.SetActive(false);
            else this.gameObject.SetActive(true);
        }
    }
}
