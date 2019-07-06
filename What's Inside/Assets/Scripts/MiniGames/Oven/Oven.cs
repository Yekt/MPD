using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public List<OvenComponent> components = new List<OvenComponent>();
    private OvenComponent toRemove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (OvenComponent component in components)
        {
            if (!component.isCompleted)
            {
                component.Action();
            } else
            {
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
            //TODO
            //PersistentData.Instance.ovenFixed = true;
            //Älexa: Minispiel-Ofen_Minispiel-Abgeschlossen
        }
    }
}
