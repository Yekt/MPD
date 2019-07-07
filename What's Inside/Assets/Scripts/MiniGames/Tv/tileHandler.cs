using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum TileRotation
{
    NOROTATION = 0, //0 Degrees
    TILTEDRIGHT = 1, //90 Degrees
    TURNEDARROUND = 2, //180 Degrees
    TILTEDLEFT = 3 //270 Degrees
}


public enum SortOfTile
{
    STARTTILE,
    ENDTILE,
    STREIGHTTILE,
    CURVETILE
    
}

public enum Direction
{
    TOP = 0,
    RIGHT = 1,
    BOT = 2,
    LEFT = 3
}

public class tileHandler : MonoBehaviour
{
    // Rotation of the Tile 
    // 0 is no rotation
    //        0
    //     3  -  1
    //        2
    //
    //public int rotationOfTheTile;

    public TileRotation rotation; 
    
    // sortOfTile = StartTile;
    //             x
    //          x  -  x output
    //             x
    // 
    // sortOfTile = EndTile;
    //             x
    //    input x  -  x 
    //             x
    //
    // sortOfTile = StreightTile;
    //             x
    //    input x  -  x output
    //             x
    //
    // sortOfTile = CurveTile;
    //             x
    //    input x  -  x 
    //             x
    //           output
    //public int sortOfTile;
    public SortOfTile sortOfTile;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    
    public Direction[] getConnections()
    {
        
        Direction[] connections = new Direction[2];
        
        if (sortOfTile == SortOfTile.STREIGHTTILE)
        {

            switch (rotation)
            {
                case TileRotation.NOROTATION:
                    connections[0] = Direction.RIGHT;
                    connections[1] = Direction.LEFT;
                    return connections;
                
                
                case TileRotation.TILTEDRIGHT:
                    connections[0] = Direction.TOP;
                    connections[1] = Direction.BOT;
                    return connections;
                
                
                case TileRotation.TURNEDARROUND:
                    connections[0] = Direction.RIGHT;
                    connections[1] = Direction.LEFT;
                    return connections;
                
                
                case TileRotation.TILTEDLEFT:
                    connections[0] = Direction.TOP;
                    connections[1] = Direction.BOT;
                    return connections;
                
            }
            
            
            
            
        }else if (sortOfTile == SortOfTile.CURVETILE)
        {
            switch (rotation)
            {
                case TileRotation.NOROTATION:
                    connections[0] = Direction.LEFT;
                    connections[1] = Direction.BOT;
                    return connections;
                    
                case TileRotation.TILTEDRIGHT:
                    connections[0] = Direction.TOP;
                    connections[1] = Direction.LEFT;
                    return connections;
                
                case TileRotation.TURNEDARROUND:
                    connections[0] = Direction.TOP;
                    connections[1] = Direction.RIGHT;
                    return connections;
                
                case TileRotation.TILTEDLEFT:
                    connections[0] = Direction.RIGHT;
                    connections[1] = Direction.BOT;
                    return connections;
            }
            
            
            
        }else if (sortOfTile == SortOfTile.ENDTILE)
        {
            switch (rotation)
            {
                case TileRotation.NOROTATION:
                    connections[0] = Direction.LEFT;
                    return connections;
                case TileRotation.TILTEDRIGHT:
                    connections[0] = Direction.TOP;
                    return connections;
                case TileRotation.TURNEDARROUND:
                    connections[0] = Direction.RIGHT;
                    return connections;
                case TileRotation.TILTEDLEFT:
                    connections[0] = Direction.BOT;
                    return connections;
                    
            }
        }
        else
        {
            throw new Exception("getConnections should not be executed on Starttile. It should have finished the process. ");
            
        }
        
        return null;
    }
    
    
    public void rotateToPos(int rotationOfTheTile)
    {
        
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.SetPositionAndRotation(rectTransform.position, default);
        switch (this.rotation)
        {
            case TileRotation.NOROTATION:
                Debug.Log("Rotation 0");
                break;
            case TileRotation.TILTEDRIGHT:
                rectTransform.Rotate( new Vector3( 0, 0, -90 ) );
                Debug.Log("Rotation -90");
                break;
            case TileRotation.TURNEDARROUND:
                rectTransform.Rotate( new Vector3( 0, 0, -180 ) );
                Debug.Log("Rotation -180");
                break;
            case TileRotation.TILTEDLEFT:
                rectTransform.Rotate( new Vector3( 0, 0, -270 ) );
                Debug.Log("Rotation -270");
                break;
        }
    }


    public void rotateRight()
    {
        if (this.sortOfTile == SortOfTile.STARTTILE || this.sortOfTile == SortOfTile.ENDTILE) return;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.Rotate(new Vector3(0,0, -90));
        if (rotation == TileRotation.TILTEDLEFT)
        {
            rotation = 0;
        }
        else
        {
            rotation++;
        }
    }



    }
