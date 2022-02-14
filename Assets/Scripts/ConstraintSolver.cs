using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TMPro for interaction with TextMeshPro text
using TMPro;
//using Eppy; //ADDED ??DO WE NEED THIS??

//ADD A DIFFERENT COLOR ON THE TOP.
//PATTERN LIBRARY IS THE OLD SOLUTION
//GET RID OF ALL THE THINGS FROM THE OLD SOLUTION
//GO FROM GET CONNECTIONS - 
//FIRST THING WE NEED TO DO IS GET THE PATTERN LIBRARY
//WE NEED A MONOBEHAVIOUR SCRIPT SOMEWHERE - CONSTRAINT SOLVER
//MAKING A NEW BRANCH SO WE DON'T LOSE ALL THE ORIGINAL ONES
//CLEANING UP THE FILE - DELETING UNEEDED SCRIPTS.
//ADDING A GAME OBJECT

//FIRST, WE NEED ALL OUR PREFABS - REGION OF SERIALIZED FIELDS.
//LIST OF GAME OBJECTS
//GO PATTERNS
//CREATE EMPTY IN UNTIY
//ADD PREFABS TO IT
//WE WON'T HAVE A CONSTRUCTOR, BUT WE WILL HAVE A START
//ADD LIBRARY AS A FOR EACH LOOP IN THE CONSTRAINT SOLVER
//INDEX IS _patternlibrary.Count, goPattern, _connections
//change constraint solver to void start


//initialize connecting tiles (if..)
//pattern library is working, now we need to set up the grid.
//under pattern library, set up tile grid. (make tiles will make them in the grid)
//we want to 


//get next tile
//function that gives you a number - GetUnsetTiles
//loop over tiles and check which are not set --> add bool to tiles
//get set return PossiblePatterns.Count (the moment there is only one possible pattern, it is set)
//also want a private flattened list of tiles

//need to add integer to tiles for number of possible patterns.
//do a foreach loop to find the tile with the least number of possible connections
//want to select a random tile out of the list --. rndIndex (moved from return to Tile tile to set


//in Tile AssignRandomPossiblePattern ***
//will need a voxel size / tile size (added as serialize field in the constraint solver)
//create a prefab in Tile
//for now ignore add weighted randomness


//TileToSet.AssignRandomPossiblePattern


//now we need to do propogate grid ***
//private void propogate grid
//loop over all cartesian directions (list is in util)
//get the neighbours of the set tile in the direction
//get the connection of the set tile at the direction
//get all the tiles with the same connection in opposite direction
//remove all the possible patterns in the neighbour tile that are not in the connection list (cross reference lists)
//if a tile has only one possible pattern, 
//--> set the tile
//propogate grid for this tile --> you might have to call this from in the tile
//make a function in tile CrossReferenceConnectionPatterns
//run in propogate grid on the neighbour tile


//tile propogate grid
//tile public void AssignPattern
//want to assign a pattern when it has only one option

//get neighbour of tile in a certain direction


//start with creating a tile  -->ASSIGN PATTERN , write a function that loops over all the patterns that assigns random indexes
//see if it creates one of the tiles
//populate the grid randomly
//WFC functions

public class ConstraintSolver : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
    private List<GameObject> _goPatterns;
    [SerializeField]
    private Vector3Int _gridDimensions;
    [SerializeField]
    public float TileSize;         //CHANGED - FROM PRIVATE TO PUBLIC FOR USE IN TILE FILE, ASSIGN PATTERN

    #endregion
    #region public fields


    #endregion

    #region private fields
    Tile[,,] _tileGrid;
    List<TilePattern> _patternLibrary; //?? WHERE DOES THIS GO??
    List<Connection> _connections;
    List<Vector3Int> Connections;

    public Vector3Int Index { get; private set; }     //ADDED 2
    //List<Vector3Int> Directions;

    private TilePattern _mat_ConPink;   //ADDED
    private TilePattern _mat_ConYellow; //ADDED
    private TilePattern _mat_ConBlue;   //ADDED
    private TilePattern _mat_Orange;    //ADDED
    private TilePattern _mat_Cyan;      //ADDED
    private TilePattern _mat_Green;     //ADDED

    //Eppy.Tuple<int, int>[] stack;     //ADDED
    int stacksize;                      //ADDED

    //private IEnumerable<object> enumerable;     //ADDED 2

    /*public object GetTile { get; private set; }*/ //ADDED 2



    #endregion
    #region constructors
    void Start()
    {
        //Add all connections
        _connections = new List<Connection>();

        _connections.Add(new Connection(ConnectionType.conPink, "conPink"));
        _connections.Add(new Connection(ConnectionType.conYellow, "conYellow"));
        _connections.Add(new Connection(ConnectionType.conBlue, "conBlue"));            //ADDED
        _connections.Add(new Connection(ConnectionType.conOrange, "conOrange"));        //ADDED
        _connections.Add(new Connection(ConnectionType.conCyan, "conCyan"));            //ADDED
        _connections.Add(new Connection(ConnectionType.conGreen, "conGreen"));          //ADDED

        //Add all patterns
        _patternLibrary = new List<TilePattern>();
        foreach (var goPattern in _goPatterns)
        {
            _patternLibrary.Add(new TilePattern(_patternLibrary.Count, goPattern, _connections));
        }

        //Set up the tile grid
        MakeTiles();

    }


    #endregion
    #region public functions


    #endregion
    #region private functions
    /// <summary>
    /// Create the tile grid
    /// </summary>
    private void MakeTiles()
    {
        _tileGrid = new Tile[_gridDimensions.x, _gridDimensions.y, _gridDimensions.z];
        for (int x = 0; x < _gridDimensions.x; x++)
        {
            for (int y = 0; y < _gridDimensions.y; y++)
            {
                for (int z = 0; z < _gridDimensions.z; z++)
                {
                    _tileGrid[x, y, z] = new Tile(new Vector3Int(x, y, z), _patternLibrary, this);
                }
            }
        }
    }

    private void GetNextTile()
    {
        List<Tile> unsetTiles = GetUnsetTiles();
        //Check if there still are tiles to set
        if (unsetTiles.Count == 0)
        {
            Debug.Log("all tiles are set");
            return;
        }


        //Count how many possible patterns there are
        //Find all the tiles with the least amount of possible patterns
        //Select a random tile out of this list
        //Get the tiles with the least amount of possible patterns
        List<Tile> lowestTiles = new List<Tile>();
        int lowestTile = int.MaxValue;

        foreach (Tile tile in unsetTiles)
        {
            if (tile.NumberOfPossiblePatterns < lowestTile)
            {
                lowestTiles = new List<Tile>();

                lowestTile = tile.NumberOfPossiblePatterns;
            }
            if (tile.NumberOfPossiblePatterns == lowestTile)
            {
                lowestTiles.Add(tile);
            }
        }

        //Select a random tile out of the list
        int rndIndex = Random.Range(0, lowestTiles.Count);
        Tile tileToSet = lowestTiles[rndIndex];


        //Assign one of the possible patterns to the tile
        tileToSet.AssignRandomPossiblePattern();


        //PropogateGrid on the set tile


    }

    public void PropogateGrid(Tile setTile)
    {
        
    }

    public List<Tile> GetTileDirectionList()                                            //ADDED 2         
    {
        List<Tile> tileDirections = new List<Tile>();                                   //ADDED 2       
        foreach (Vector3Int tileDirection in Util.Directions)                           //ADDED 2
        {
            Vector3Int tileDirectionsIndex = Index + tileDirection;                     //ADDED 2
            if (Util.CheckInBounds(_tileGrid._gridDimensions, tileDirectionsIndex));    //ADDED 2
        }
        return tileDirections;                                                          //ADDED 2
    }    

    public List<Tile> GetRelatedTileSides(List<Vector3Int> relativeDirections)          //ADDED 2
    {
        List<Tile> relatedTileSides = new List<Tile>();                                 //ADDED 2
        foreach (Vector3Int relativeDirection in relativeDirections)                    //ADDED 2
        {
            //if Direction = y+                                                         //ADDED 2
            { //WORKING OFF OF THE CONNECTIONS LABELED IN TILEPATTERN
                //relativeDirection = Connection [3]                                    //ADDED 2
            }

        }

        return relatedTileSides;                                                        //ADDED 2
    }
        
    
        //Loop over all cartesian directions (list is in Util)  EP - loop that referneces Util
        //We will be referencing Directions from Util
        

        ////Get the neighbour of the set tile in the direction
        ////Get the connection of the set tile at the direction
        ////Get all the tiles with the same connection in oposite direction
        ////Remove all the possiblePatterns in the neighbour tile that are not in the connection list. 
        ////Run the CrossreferenceConnectionPatterns() on the neighbour tile
        ////If a tile has only one possiblePattern
        //////Set the tile
        //////PropogateGrid for this tile

    //private Vector3Int Directions;
    //private string directions;


    //IEnumerator Directions(IEnumerable enumerable);

    //foreach (int Direction in enumerable)
    //{

    //}

    //get set is used to encapsulate the field in a property

    //IEnumerator enumerator = enumerable.GetEnumerator();    //ADDED

    //List<Vector3Int> Directions = Directions();
    //foreach (object Tile in enumerable)                     //ADDED
    //{
    //    // Perform logic on the item
    //    GetTile {var setTiles};                           //ADDED
    //    setTile {var Connection};                           //ADDED
    //}








    private Tile GetNeighbour(Tile tile, Vector3Int direction)
    {
        

        //Get the neighbour of a tile in a certain direction
        //ADDED add what the opposite direction is.
        return null;
    }

    public List<Tile> GetUnsetTiles() //??HOW DO WE KNOW THAT IT'S GETTING THE UNSET TILES? IT DOESN'T APPEAR ANYWHERE ELSE?
    {
        List<Tile> unsetTiles = new List<Tile>();

        //Loop over all the tiles and check which ones are not set
        foreach (var tile in GetTilesFlattened())
        {
            if (tile.Set) unsetTiles.Add(tile);
        }
        return unsetTiles;
    }

    private List<Tile> GetTilesFlattened()
    {
        List<Tile> tiles = new List<Tile>();
        for (int x = 0; x < _gridDimensions.x; x++)
        {
            for (int y = 0; y < _gridDimensions.y; y++)
            {
                for (int z = 0; z < _gridDimensions.z; z++)
                {
                    tiles.Add(_tileGrid[x, y, z]);
                }
            }
        }
        return tiles;
    }

    //Find a tile in the grid that it not set with the least possible options


////If one or multiple of the neighbours has no more possible tilepattern, solving failed, start over
////you could assign a possible tile of the previous propogation, this will cause impurities but might make it easier to solve

////If one or multiple of the neighbours has only one possible tilepattern, set the tile pattern
////propogate the grid for the new set tile

}
#endregion

