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
        AudioManager.Instance.Play("OfenLampe");
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
                    AudioManager.Instance.Play("Richtig");
                }
                else if (toRemove is HeatingRod && firstHeatingRodCompleted)
                {
                    AudioManager.Instance.Play("OfenHeizstabAbgeschlossen");
                    AudioManager.Instance.Play("Richtig");
                }
                if (toRemove is LightBulb)
                {
                    AudioManager.Instance.Play("OfenLampeAbgeschlossen");
                    AudioManager.Instance.Play("Richtig");
                }
                if (toRemove is Ventilator)
                {
                    AudioManager.Instance.Play("OfenVentilatorAbgeschlossen");
                    AudioManager.Instance.Play("Richtig");
                }
            } else
            {
                AudioManager.Instance.Play("OfenAbgeschlossen");
                AudioManager.Instance.Play("Richtig");
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
