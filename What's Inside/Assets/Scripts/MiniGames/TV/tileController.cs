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
    private static int WIDTH = 4;
    private static int HEIGHT = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject[] tmpTiles = new GameObject[controller.transform.childCount];
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

	    if (recursivCall(1, 7, Direction.LEFT) == true)
	    {
		    Debug.Log("JAAAAAAAAAAAAA");   
	    }
	    else
	    {
		    Debug.Log("NEEEEEEEIIIIIIIIIINNNNNN");   

	    }
    }

    
	
        //recursive Call to check if Game is connected correctly (Direction we entered is the direction we entered the current tile)
    bool recursivCall(int y, int x, Direction directionWeEntered)
    {     
		//Check if Starttile was found, if yes return true
        if (tiles[y, x].sortOfTile == SortOfTile.STARTTILE)
        {
            return true;
        }
		
		//Check if we left the playbox (which shouldnt be possible, but better check edge casses nonetheless), if yes, return false
		else if (y < 0 || y > WIDTH-1 || x < 0 || x > HEIGHT-1)
		{
			Debug.Log("Something went terribly wrong, you sould have a look at this.");
			return false;
		}
        
        //Check if current tile has a connection to the top and we didnt enter from the top (otherwise we would be going backwards, which would be stupid)
		else if ( (tiles[y,x].getConnections()[0] == Direction.TOP || tiles[y,x].getConnections()[1] == Direction.TOP)
			&& directionWeEntered != Direction.TOP)
		{
			//Check if there even is a tile above, or if we reached the end of the playbox
			if (y-1 < 0) {
				return false;
			}
			//There is a tile above! Check if tile above has a connection to the bottom, if yes, move forward, if not, we found a dead end
			else if (tiles[y-1,x].getConnections()[0] == Direction.BOT || tiles[y-1,x].getConnections()[1] == Direction.BOT) {
				//The tile above seems to have a connection to the bottom, call function recursive
				return recursivCall(y-1, x, Direction.BOT);
			}
			//The tile above doesnt seem to have a Connection to the bottom, we reached a dead end, return false
			else {
				return false;
			}
		}
		//Check if current tile has a connection to the right and we didnt enter from the right (otherwise we would be going backwards, which would be stupid)
		else if ( (tiles[y,x].getConnections()[0] == Direction.RIGHT || tiles[y,x].getConnections()[1] == Direction.RIGHT)
			&& directionWeEntered != Direction.RIGHT)
		{
			//Check if there even is a tile to the right, or if we reached the end of the playbox
			if (x+1 > WIDTH-1) {
				return false;
			}
			//There is a tile to the right! Check if tile to the right has a connection to the left, if yes, move forward, if not, we found a dead end
			else if (tiles[y,x+1].getConnections()[0] == Direction.LEFT || tiles[y,x+1].getConnections()[1] == Direction.LEFT) {
				//The tile to the right seems to have a connection to the left, call function recursive
				return recursivCall(y, x+1, Direction.LEFT);
			}
			//The tile to the right doesnt seem to have a Connection to the left, we reached a dead end, return false
			else {
				return false;
			}
		}
		//Check if current tile has a connection to the bottom and we didnt enter from the bottom (otherwise we would be going backwards, which would be stupid)
		else if ( (tiles[y,x].getConnections()[0] == Direction.BOT || tiles[y,x].getConnections()[1] == Direction.BOT)
			&& directionWeEntered != Direction.BOT)
		{
			//Check if there even is a tile to the bottom, or if we reached the end of the playbox
			if (y+1 > HEIGHT-1) {
				return false;
			}
			//There is a tile to the bottom! Check if tile to the bottom has a connection to the top, if yes, move forward, if not, we found a dead end
			else if (tiles[y+1,x].getConnections()[0] == Direction.TOP || tiles[y+1,x].getConnections()[1] == Direction.TOP) {
				//The tile to the bottom seems to have a connection to the top, call function recursive
				return recursivCall(y+1, x, Direction.TOP);
			}
			//The tile to the bottom doesnt seem to have a Connection to the top, we reached a dead end, return false
			else {
				return false;
			}
		}
		//Check if current tile has a connection to the left and we didnt enter from the left (otherwise we would be going backwards, which would be stupid)
		else if ( (tiles[y,x].getConnections()[0] == Direction.LEFT || tiles[y,x].getConnections()[1] == Direction.LEFT)
			&& directionWeEntered != Direction.LEFT)
		{
			//Check if there even is a tile to the left, or if we reached the end of the playbox
			if (x-1 < 0) {
				return false;
			}
			//There is a tile to the left! Check if tile to the left has a connection to the right, if yes, move forward, if not, we found a dead end
			else if (tiles[y,x-1].getConnections()[0] == Direction.RIGHT || tiles[y,x-1].getConnections()[1] == Direction.RIGHT) {
				//The tile to the left seems to have a connection to the right, call function recursive
				return recursivCall(y, x-1, Direction.RIGHT);
			}
			//The tile to the left doesnt seem to have a Connection to the right, we reached a dead end, return false
			else {
				return false;
			}
		}
		//Hm, all ifs returned false, this should never happen, but better check for edge cases nonetheless
		else {
			Debug.Log("Something went terribly wrong, you sould have a look at this.");
			return false;
		}
    }
    
    
    

}






/*

        if (tiles[y, x].getInput() == 0)
        {
            int tmp = y + 1;
            if (tmp <= 3 || tmp >= 0)
            {
                if (tiles[x, y].getOutput() == 2)
                { 
                    asdfMovieIstGeil.Add(tiles[tmp,x]); // TODO
                    recursivCall(tmp, x);
                }
            }


        }
        else if (tiles[y, x].getInput() == 1)
        {
            int tmp = x + 1;
            if (!(tmp > 7) || !(tmp < 0))
            {
                if (tiles[y, tmp].getOutput() == 3)
                {
                    asdfMovieIstGeil.Add(tiles[y,tmp]); // TODO
                    recursivCall(y, tmp);
                }
            }

            
        }
        else if (tiles[y, x].getInput() == 2)
        {

            int tmp = y - 1;
            if (!(tmp > 3) || !(tmp < 0))
            {
                if (tiles[tmp, x].getOutput() == 0)
                {
                    asdfMovieIstGeil.Add(tiles[tmp,x]); // TODO
                    recursivCall(tmp, x);
                }

            }

        }
        else if (tiles[y, x].getInput() == 3)
        {


            int tmp = x - 1;
            if (!(tmp > 7) || !(tmp < 0))
            {
                if (tiles[y, tmp].getOutput() == 1)
                {
                    asdfMovieIstGeil.Add(tiles[y,tmp]); // TODO
                    recursivCall(y, tmp);
                }
            }
        }
*/