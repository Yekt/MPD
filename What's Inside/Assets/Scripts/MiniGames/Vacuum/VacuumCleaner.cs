using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumCleaner : MonoBehaviour
{
    public List<VacuumComponent> components = new List<VacuumComponent>();
    [HideInInspector]
    public VacuumComponent toRemove;

    // Start is called before the first frame update
    void Start()
    {
        foreach (VacuumComponent component in components)
        {
            component.solution.SetActive(false);
        }
        AudioManager.Instance.Play("StaubsaugerBetreten2");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(VacuumComponent component in components)
        {
            if (!Input.GetMouseButton(0))
            {
                if (ComponentPlacedInCorrectPosition(component))
                {
                    component.component.SetActive(false);
                    component.position.SetActive(false);
                    component.solution.SetActive(true);
                    toRemove = component;
                    //TODO Älexa: Minispiel-Staubsauger_Richtiges-Bauteil-Ausgewählt
                    if (components.Count > 3) AudioManager.Instance.Play("StaubsaugerRichtig");
                }
                if (ComponentPlacedInWrongPosition(component))
                {
                    component.ResetPosition();
                    AudioManager.Instance.Play("StaubsaugerFalsch");
                }
                if (toRemove != null)
                {
                    components.Remove(toRemove);
                    toRemove = null;
                }

                if (components.Count == 0)
                {
                    PersistentData.Instance.vacuumFixed = true;
                    Inventory.Instance.activateGameItem("Lampe");
                    Inventory.Instance.deactivateItem("Filter");
                    Inventory.Instance.deactivateItem("Staubsaugerbeutel");
                    Inventory.Instance.deactivateItem("Saugturbine");
                    Inventory.Instance.deactivateItem("Rohr");                    
                    AudioManager.Instance.Play("StaubsaugerAbgeschlossen");
                    // TODO Items aus dem Inventar löschen und neues Item hinzufügen
                }
            }
        }
    }

    bool ComponentPlacedInCorrectPosition(VacuumComponent component)
    {
        if (component.component.GetComponent<Collider2D>().bounds.Contains(component.position.transform.position)) {
            return true;
        } else
        {
            return false;
        }
    }

    bool ComponentPlacedInWrongPosition(VacuumComponent component)
    {
        foreach (VacuumComponent otherComponent in components)
        {
            if (!(component.Equals(otherComponent)))
            {
                if (component.component.GetComponent<Collider2D>().bounds.Contains(otherComponent.position.transform.position))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
