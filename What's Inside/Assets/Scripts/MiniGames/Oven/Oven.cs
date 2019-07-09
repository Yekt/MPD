using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public List<OvenComponent> components = new List<OvenComponent>();
    private OvenComponent toRemove;
    private bool firstHeatingRodCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Play("OfenErstesBetreten");
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
            if (components.Count > 1)
            {
                if (toRemove is HeatingRod && !firstHeatingRodCompleted)
                {
                    firstHeatingRodCompleted = true;
                }
                else if (toRemove is HeatingRod && firstHeatingRodCompleted)
                {
                    AudioManager.Instance.Play("OfenHeizstabAbgeschlossen");
                }
                if (toRemove is LightBulb)
                {
                    AudioManager.Instance.Play("OfenLampeAbgeschlossen");
                }
                if (toRemove is Ventilator)
                {
                    AudioManager.Instance.Play("OfenVentilatorAbgeschlossen");
                }
            } else
            {
                AudioManager.Instance.Play("OfenAbgeschlossen");
            }
            components.Remove(toRemove);
            toRemove = null;
        }
        if (components.Count == 0)
        {
            //AudioManager.Instance.Play("OfenAbgeschlossen");
            PersistentData.Instance.ovenFixed = true;
            Inventory.Instance.deactivateItem("Lampe");
            Inventory.Instance.deactivateItem("Ventilator");
            Inventory.Instance.deactivateItem("Heizstab");
            Inventory.Instance.activateGameItem("Fernseherkabel");
        }
    }
}
