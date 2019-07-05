using UnityEngine;
using System.Collections;

public class VacuumComponent : MonoBehaviour
{
    public GameObject component;
    public GameObject position;
    public GameObject solution;
    [HideInInspector]
    private Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        startPosition = component.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetPosition()
    {
        component.transform.position = startPosition;
    }
}
