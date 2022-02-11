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

    private TilePattern _mat_ConPink;   //ADDED
    private TilePattern _mat_ConYellow; //ADDED
    private TilePattern _mat_ConBlue;   //ADDED
    private TilePattern _mat_Orange;    //ADDED
    private TilePattern _mat_Cyan;      //ADDED
    private TilePattern _mat_Green;     //ADDED

    //Eppy.Tuple<int, int>[] stack;     //ADDED
    int stacksize;                      //ADDED



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
                    _tileGrid[x, y, z] = new Tile(new Vector3Int(x, y, z), _patternLibrary,this);
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
        //Loop over all cartesian directions (list is in Util)  EP - loop that referneces Util

        //for ADDED LOL

        ////Get the neighbour of the set tile in the direction
        


        ////Get the connection of the set tile at the direction
        ////Get all the tiles with the same connection in oposite direction
        ////Remove all the possiblePatterns in the neighbour tile that are not in the connection list. 
        ////Run the CrossreferenceConnectionPatterns() on the neighbour tile
        ////If a tile has only one possiblePattern
        //////Set the tile
        //////PropogateGrid for this tile

    }

    private Tile GetNeighbour(Tile tile, Vector3Int direction)
    {
        //Get the neighbour of a tile in a certain direction
        return null;
    }
    private List<Tile> GetUnsetTiles()
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

    //public Voxel GetPatternVoxel()                          //ADDED
    //{
    //    List<TilePattern> patternVoxel = new List<patternVoxel>(GetPattern().Where(v => v.Status == PatternVoxelState.Available));
    //    return patternVoxel[Overlapping.Range(0, patternVoxel.Count)];      //ADDED
    //}

    //public void StartStopWaveFunctionCollapse()             //ADDED
    //{
    //    StopAllCoroutines();                                //ADDED
    //    if (!_generating)                                   //ADDED
    //    {
    //        StartCoroutine(WaveFunctionEngine());           //ADDED
    //    }
    //    _generating = !_generating;                         //ADDED
    //}

    //private IEnumerator WaveFunction()                      //ADDED
    //{
    //    while (_iterationCounter < _iterations)             //ADDED
    //    {
    //        Library.InitState(Seed++); //this should not be random, but instead should be related to the ruleset provided ((what is the command for this))
    //        WaveFunctionStep();                             //ADDED
    //        _iterationCounter++;                            //ADDED
    //        yield return new WaitForSeconds(0.01f);         //ADDED
    //    }
    //}


    //private void WaveFunctionStep()                         //ADDED
    //{
    //    _grid.PurgeAllBlocks();                             //ADDED
    //    _tryCounter = 0;                                    //ADDED
    //    while (_tryCounter < _triesPerIteration)            //ADDED
    //    {
    //        TryAddCorrrectBlock();                          //ADDED
    //        _tryCounter++;                                  //ADDED
    //    }

    //    UpdateEfficiency();                                 //ADDED
    //}

    //private bool TryAddCorrectBlock()                       //ADDED
    //{
    //    _grid.SetCorrectPatternIndex();                     //ADDED

    //    //Add function the gets the voxels around the generated block
    //    //==> Get the available neighbours of all the alive voxels


    //    //Random available voxel: Most accurate, but very expensive ==> slow EG Seed 0: 64% filled
    //    //_grid.AddBlock(_grid.GetRandomAvailableVoxel().Index, Util.RandomCarthesianRotation());

    //    IEnumerator WaveFunctionSeed()                      //ADDED
    //    {
    //        _grid.PurgeAllBlocks();                         //ADDED
    //        Random.InitState(Seed);                         //ADDED
    //        _tryCounter = 0;                                //ADDED
    //        while (_tryCounter < _triesPerIteration)        //ADDED
    //        {
    //            TryAddRandomBlock();                        //ADDED
    //            UpdateStats();                              //ADDED
    //            _tryCounter++;                              //ADDED
    //            yield return new WaitForSeconds(0.001f);    //ADDED
    //        }
    //        UpdateEfficiency();                             //ADDED
    //        yield break;                                    //ADDED
    //    }

    //    //Random voxel in the entire grid: Least accurate but non expensive ==> very fast EG Seed 0: 35%
    //    //_grid.AddBlock(Util.RandomIndex(_grid._gridDimensions), Util.RandomCarthesianRotation());
    //    //Random voxel in the non dead voxels: Medium accurate, low expensive ==> medium fast EG Seed 0: 59% filled
    //    //ADDED
    //    _grid.AddBlock(_nonDeadVoxels[Random.Range(0, _nonDeadVoxels.Count)].Index, Util.RandomCarthesianRotation());       //this is better than lines 123-136

    //    bool blockAdded = _grid.TryAddCurrentBlocksToGrid();    //ADDED
    //    _grid.PurgeUnplacedBlocks();                            //ADDED



    //    return blockAdded;                                  //ADDED

    //}

    //private void PropogateGrid(Tile changedTile)            //ADDED
    //{
    //    //Loop over the connections of the changedTile
    //    //Per connection: go to the neighbour tile in the connection direction
    //    //Crossreference the list of possible connections in the neighbour tile with the list of possilbepatterns in the connection

    //    //If one or multiple of the neighbours has no more possible tilepattern, solving failed, start over
    //    //you could assign a possible tile of the previous propogation, this will cause impurities but might make it easier to solve

    //    //If one or multiple of the neighbours has only one possible tilepattern, set the tile pattern
    //    //propogate the grid for the new set tile

    //}

    ////Loop over the connections of the changedTile
    //public void WaveFunctionLoop()      //ADDED
    //{
    //    if (conPink = conYellow)        //ADDED
    //    {
    //        continue;                   //ADDED
    //    }

    //    else (conPink == conPink)       //ADDED
    //    {
    //        Console.Write(i);           //ADDED
    //    }
    //}

    //Per connection: go to the neighbour tile in the connection direction


}

//public class Movement : MonoBehaviour                                   //ADDED
//{
//    private bool isMatch = false;                                       //ADDED

//    void Update()                                                       //ADDED
//    {
//        if (Input.GetMouseButtonDown(0))                                //ADDED
//        {
//            if (isMatch == false)                                       //ADDED
//            {
//                transform.rotation = Quaternion.Euler(0, 0, 0);         //ADDED
//                isMatch = true;                                         //ADDED
//                Debug.Log("continued along direction");                 //ADDED
//            }
//            else
//            {
//                transform.rotation = Quaternion.Euler(90, -45, 0);      //ADDED
//                Debug.Log("Turn Left and try again");                   //ADDED
//                isMatch = false;                                        //ADDED
//            }
//        }
//    }

//}

////Crossreference the list of possible connections in the neighbour tile with the list of possilbepatterns in the connection

//public abstract class ConnectionTypes                                   //ADDED
//{
//}

//public class Voxel : voxelConnect                                       //ADDED
//{
//    private HashSet<VoxelPatternRelationship> _voxelConnect =           //ADDED
//        new HashSet<VoxelPatternRelationship>();                        //ADDED

//    public virtual IEnumerable<VoxelPatternRelationship> voxelConnect   //ADDED
//    {
//        get { return this._voxelConnect; }                              //ADDED
//    }

//    public virtual void AddVoxelConnect(VoxelConnect voxelConnect, RelationshipKind relationshipKind) //ADDED
//    {
//        var relationship = new VoxelPatternRelationship()               //ADDED
//        {
//            Parent = this,                                              //ADDED
//            Child = voxelConnect,                                       //ADDED
//            RelationshipKind = relationshipKind                         //ADDED
//        };

//        this._children.Add(relationship);                               //ADDED
//        child.AddParent(relationship);                                  //ADDED
//    }
//}

//public class Child : Voxel                                              //ADDED
//{
//    private HashSet<ParentChildRelationship> _parents =                 //ADDED
//        new HashSet<ParentChildRelationship>();                         //ADDED

//    public virtual IEnumerable<VoxelPatternRelationship> Parents        //ADDED
//    {
//        get { return this._parents; }                                   //ADDED
//    }

//    internal virtual void AddParent(VoxelPatternRelationship relationship)      //ADDED
//    {
//        this._parents.Add(relationship);                                //ADDED
//    }
//}

//public class ParentChildRelationship                                    //ADDED
//{
//    public virtual Parent Parent { get; protected internal set; }       //ADDED

//    public virtual Child Child { get; protected internal set; }         //ADDED

//    public virtual RelationshipKind RelationshipKind { get; set; }      //ADDED
//}

//public enum RelationshipKind    //ADDED
//{
//    conPink,                    //ADDED
//    conYellow                   //ADDED
//}
////If one or multiple of the neighbours has no more possible tilepattern, solving failed, start over
////you could assign a possible tile of the previous propogation, this will cause impurities but might make it easier to solve

////If one or multiple of the neighbours has only one possible tilepattern, set the tile pattern
////propogate the grid for the new set tile
#endregion

