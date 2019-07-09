using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingRod : OvenComponent
{
    //public List<GameObject> heatingRods = new List<GameObject>();
    public GameObject heatingRod;
    public GameObject heatingRodHot;

    private SpriteRenderer spriteRenderer;
    private float r, g, b, alpha;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = heatingRodHot.transform.GetComponent<SpriteRenderer>();
        r = spriteRenderer.color.r;
        g = spriteRenderer.color.g;
        b = spriteRenderer.color.b;
        alpha = 0;
        spriteRenderer.color = new Color(r, g, b, alpha);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Action()
    {
        if (!isCompleted)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                if (heatingRod.GetComponent<Collider2D>().bounds.Contains(position))
                {
                    alpha += 0.01f;
                    spriteRenderer.color = new Color(r, g, b, alpha);
                }
            } else
            {
                if (alpha >= 0.01f)
                {
                    alpha -= 0.01f;
                    spriteRenderer.color = new Color(r, g, b, alpha);
                }
            }
            if (alpha >= 1)
            {
                isCompleted = true;
            }
        }
    }
}
