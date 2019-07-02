using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class tileHandler : MonoBehaviour
{
    // Rotation of the Tile 
    // 0 is no rotation
    //        0
    //     3  -  1
    //        2
    //
    public int rotationOfTheTile;
    
    // sortOfTile = 0;
    //             x
    //          x  -  x output
    //             x
    // 
    // sortOfTile = 1;
    //             x
    //    input x  -  x 
    //             x
    //
    // sortOfTile = 2;
    //             x
    //    input x  -  x output
    //             x
    //
    // sortOfTile = 3;
    //             x
    //    input x  -  x 
    //             x
    //           output
    public int sortOfTile;
    
    // Start is called before the first frame update
    void Start()
    {
        
        /*
        rotationOfTheTile = Random.Range(0, 4);
        rotateToPos(rotationOfTheTile);
        sortOfTile = Random.Range(1, 4);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //returns the input
    // 0 is top; 1 is right; 2 is bot; 3 is left
    public int getInput()
    {
        if (sortOfTile == 0) return 4; // if 4 appears, there is no input
        
        switch (rotationOfTheTile)
                {
                    case 0:
                        return 3;
                        break;
                    case 1:
                        return 0;
                        break;
                    case 2:
                        return 1;
                        break;
                    case 3:
                        return 2;
                        break;
                }

        return -1; // if -1 appears there is a bug with the rotation
    }

    // 0 is top; 1 is right; 2 is bot; 3 is left
    public int getOutput()
    {
        if (sortOfTile == 1) return 4; //if 4 appears, there is no output
        if (sortOfTile == 0 || sortOfTile == 2)
        {
            switch (rotationOfTheTile)
            {
                case 0:
                    return 1;
                    break;
                case 1:
                    return 2;
                    break;
                case 2:
                    return 3;
                    break;
                case 3:
                    return 0;
                    break;
            }
        }else if (sortOfTile == 3)
        {
            switch (rotationOfTheTile)
            {
                case 0:
                    return 2;
                    break;
                case 1:
                    return 3;
                    break;
                case 2:
                    return 0;
                    break;
                case 3:
                    return 1;
                    break;
                
            }
        }

        return -1; // if -1 appears there is a bug with the rotation
    }
    
    public void rotateToPos(int rotationOfTheTile)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.SetPositionAndRotation(rectTransform.position, default);
        switch (this.rotationOfTheTile)
        {
            case 0:
                break;
            case 1:
                rectTransform.Rotate( new Vector3( 0, 0, 90 ) );
                break;
            case 2:
                rectTransform.Rotate( new Vector3( 0, 0, 180 ) );
                break;
            case 3:
                rectTransform.Rotate( new Vector3( 0, 0, 270 ) );
                break;
        }
    }


    public void rotateRight()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.Rotate(new Vector3(0,0, 90));
        if (rotationOfTheTile == 3)
        {
            rotationOfTheTile = 0;
        }
        else
        {
            rotationOfTheTile++;
        }
    }



    }
