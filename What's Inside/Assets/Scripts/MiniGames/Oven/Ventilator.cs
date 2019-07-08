using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : OvenComponent
{
    public GameObject ventilator;
    private float angle;
    private float speed;
    private float decibel;
    private int timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        angle = 0;

        //if (Microphone.devices.Length == 0)
        //{
            angle = 0;
            speed = 10;
            isCompleted = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompleted)
        {
            angle = (angle + 10) % 360;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            ventilator.transform.rotation = rotation;
        }

        /*timeCounter += 1;

        if (timeCounter == 2000)
        {
            angle = 0;
            speed = 10;
            isCompleted = true;
        }*/

    }

    public override void Action()
    {
        /*angle = (angle + speed) % 360;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ventilator.transform.rotation = rotation;

        decibel = 20 * Mathf.Log10(Mathf.Abs(MicInput.MicLoudness));

        //Debug.Log(decibel);

        if (decibel > -20)
        {
            speed = speed + 0.1f;
        } else
        {
            if (speed >= 0.1)
            {
                speed = speed - 0.1f;
            }
        }
        if (speed >= 10)
        {
            isCompleted = true;
            AudioManager.Instance.Play("OfenVentilatorAbgeschlossen");
        }*/
    }
}
