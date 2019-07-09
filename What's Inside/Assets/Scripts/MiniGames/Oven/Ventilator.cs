using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : OvenComponent
{
    public GameObject ventilator;
    public float angle;
    public float speed;
    private float decibel;
    private int timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        angle = 360;
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompleted)
        {
            angle = (angle + 350) % 360;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            ventilator.transform.rotation = rotation;
        }       
    }

    public override void Action()
    {
        if (!isCompleted)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                if (ventilator.GetComponent<Collider2D>().bounds.Contains(position))
                {
                    speed += 0.005f;
                    if (speed >= 4)
                    {
                        isCompleted = true;
                        AudioManager.Instance.Play("OfenVentilatorAbgeschlossen");
                    }

                    Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ventilator.transform.position;
                    angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 360) % 360;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    ventilator.transform.rotation = Quaternion.Slerp(ventilator.transform.rotation, rotation, speed * Time.deltaTime);
                }
            }
        }
    }
}
