using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;


public class tileController : MonoBehaviour
{
    public tileHandler[,] tiles;
    public GameObject controller;

    // Zeit zum lösen in Sekunden
    private float timerSpeed = 90f;

    GameObject[] tmpTiles;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.Play("TVErstesBetreten1");

        tmpTiles = new GameObject[controller.transform.childCount];
        this.tiles = new tileHandler[4,8];
        
        
        for (int i = 0; i < this.controller.transform.childCount; i++)
        {
            tmpTiles[i] = this.controller.transform.GetChild(i).transform.gameObject;
        }


        int counter = 0;
        for (int y = 0; y < 4; y++)
        {
            
            for (int x = 0; x < 8; x++)
            {
                this.tiles[y,x] = tmpTiles[counter].GetComponent(typeof(tileHandler)) as tileHandler;
                counter++;
            }
        }


        InvokeRepeating("rerollTiles", timerSpeed, timerSpeed);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void checkForConnection()
    {

	    if (recursivCall(2, 0, Direction.RIGHT))
	    {
            PersistentData.Instance.tvFixed = true;
            Inventory.Instance.activateGameItem("Antenne");
            Inventory.Instance.deactivateItem("Fernseherkabel");            
            AudioManager.Instance.Play("TVAbgeschlossen");
        }
        else
        {
            AudioManager.Instance.Play("VersuchsNochmal1");
        }
	    
    }

    
	
    //recursive Call to check if Game is connected correctly (Direction we entered is the direction we entered the current tile)
    bool recursivCall(int y, int x, Direction directionWeGo)
    {
	    //Check if Starttile was found, if yes return true
	    if (tiles[y, x].sortOfTile == SortOfTile.STARTTILE)
	    {
		    return true;
	    }

	    //Check if we left the playbox (which shouldnt be possible, but better check edge casses nonetheless), if yes, return false
	    else if (y < 0 || y > 3 || x < 0 || x > 7)
	    {
		    Debug.Log("Out of Playbox. Something went terribly wrong, you sould have a look at this.");
		    return false;
	    }


	    switch (directionWeGo)
	    {
		    case Direction.TOP:

			    if (y - 1 < 0)
			    {
				    Debug.Log("Out of Playbox. Going to top. at Tile: X: " + x + "Y:" + y);
				    return false;
			    }
			    
			    if (tiles[y-1, x].sortOfTile == SortOfTile.STARTTILE)
			    {
				    return true;
			    }


			    if (tiles[y - 1, x].getConnections()[0] == Direction.BOT)
			    {
				    if (recursivCall(y - 1, x, tiles[y - 1, x].getConnections()[1])) return true;
			    }
			    else if (tiles[y - 1, x].getConnections()[1] == Direction.BOT)
			    {
				    if (recursivCall(y - 1, x, tiles[y - 1, x].getConnections()[0])) return true;
			    }
			    break;
		    
		    
		    
		    case Direction.RIGHT:
			    if (x + 1 > 7)
			    {
				    Debug.Log("Out of Playbox. Coming from top. at Tile: X: " + x + "Y:" + y);
				    return false;
			    }

			    if (tiles[y, x+1].sortOfTile == SortOfTile.STARTTILE)
			    {
				    return true;
			    }

			    
			    if (tiles[y, x+1].getConnections()[0] == Direction.LEFT)
			    {
				    if (recursivCall(y, x+1, tiles[y, x+1].getConnections()[1])) return true;
			    }
			    else if (tiles[y, x+1].getConnections()[1] == Direction.LEFT)
			    {
				    if (recursivCall(y, x+1, tiles[y, x+1].getConnections()[0])) return true;
			    }
			    
			    break;
		    
		    
		    
		    
		    case Direction.BOT:
			    if (y + 1 > 3)
			    {
				    Debug.Log("Out of Playbox. Coming from top. at Tile: X: " + x + "Y:" + y);
				    return false;
			    }

			    
			    if (tiles[y+1, x].sortOfTile == SortOfTile.STARTTILE)
			    {
				    return true;
			    }

			    
			    if (tiles[y + 1, x].getConnections()[0] == Direction.TOP)
			    {
				    if (recursivCall(y + 1, x, tiles[y + 1, x].getConnections()[1])) return true;
			    }
			    else if (tiles[y + 1, x].getConnections()[1] == Direction.TOP)
			    {
				    if (recursivCall(y + 1, x, tiles[y + 1, x].getConnections()[0])) return true;
			    }
			    
			    break;
		    
		    
		    
		    
		    case Direction.LEFT:
			    if (x - 1 < 0)
			    {
				    Debug.Log("Out of Playbox. Coming from top. at Tile: X: " + x + "Y:" + y);
				    return false;
			    }

			    if (tiles[y, x-1].sortOfTile == SortOfTile.STARTTILE)
			    {
				    return true;
			    }

			    
			    
			    if (tiles[y, x-1].getConnections()[0] == Direction.RIGHT)
			    {
				    if (recursivCall(y, x-1, tiles[y, x-1].getConnections()[1])) return true;
			    }
			    else if (tiles[y, x-1].getConnections()[1] == Direction.RIGHT)
			    {
				    if (recursivCall(y, x-1, tiles[y, x-1].getConnections()[0])) return true;
			    }



			    break;


	    }

	    return false;
    }



    public void rerollTiles()
    {

        foreach (GameObject tileTransform in tmpTiles)
        {
            tileHandler tile = tileTransform.transform.gameObject.GetComponent(typeof(tileHandler)) as tileHandler;
            tile.rdmRotation();
        }

    }



}