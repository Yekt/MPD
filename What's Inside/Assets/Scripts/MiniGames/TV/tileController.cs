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
    private GameObject[] tmpTiles;
    private List<tileHandler> tilesConnected = new List<tileHandler>();
    
    
    // Start is called before the first frame update
    void Start()
    {
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
        
        

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void checkForConnection()
    {
		tilesConnected.Clear();
	    if (recursivCall(2, 0, Direction.RIGHT))
	    {
		    Debug.Log("JAAAAAAAAAAAAA");


		    foreach(tileHandler tile in tilesConnected)
		    {
			   Debug.Log(tile.name); 
		    }
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
	    
		tilesConnected.Add(tiles[y,x]);
		
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
}