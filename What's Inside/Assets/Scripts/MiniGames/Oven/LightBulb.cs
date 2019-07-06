using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : OvenComponent
{
    public GameObject lightBulbOff;
    public GameObject lightBulbOn;

    private int timeDelay;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        lightBulbOn.SetActive(false);
        timeDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Action()
    {
        if (!isCompleted)
        {
            if (lightBulbOn.activeSelf && timeDelay == (counter * 3))
            {
                lightBulbOn.SetActive(false);
                timeDelay = 0;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                if (lightBulbOff.GetComponent<Collider2D>().bounds.Contains(position))
                {
                    lightBulbOn.SetActive(true);
                    counter += 1;
                    if (counter == 5)
                    {
                        isCompleted = true;
                    }
                }
            }
            if (lightBulbOn.activeSelf)
            {
                timeDelay += 1;
            }
        }
    }
}
