using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumCleaner : MonoBehaviour
{
    public GameObject vacuumCleanerBroken;
    public GameObject vacuumCleaner;
    public List<GameObject> components = new List<GameObject>();
    [HideInInspector]
    public GameObject toRemove;

    // Start is called before the first frame update
    void Start()
    {
        vacuumCleanerBroken.SetActive(true);
        vacuumCleaner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject component in components)
        {
            if (vacuumCleanerBroken.GetComponent<Collider2D>().bounds.Contains(component.transform.position))
            {
                AudioManager.Instance.Play("Click");
                component.SetActive(false);
                toRemove = component;
            }
        }
        if (toRemove != null)
        {
            components.Remove(toRemove);
            toRemove = null;
        }

        if (components.Count == 0)
        {
            vacuumCleanerBroken.SetActive(false);
            vacuumCleaner.SetActive(true);
			PersistentData.Instance.vacuumFixed = true;
        }
    }
}
