using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;


public class ConstraintSolver : MonoBehaviour
{

    #region Serialized fields
    //[SerializeField]
    //private List<GameObject> _goPatterns;
    [SerializeField]
    public Vector3Int GridDimensions;
    [SerializeField]
    public float TileSize;         //CHANGED - FROM PRIVATE TO PUBLIC FOR USE IN TILE FILE, ASSIGN PATTERN


    #endregion

    #region public fields

    public GameObject[] GOPatternPrefabs;

    #endregion

    #region private fields
    public Tile[,,] TileGrid { private set; get; }
    List<TilePattern> _patternLibrary; //?? WHERE DOES THIS GO??
    List<Connection> _connections;
    //List<Vector3Int> Connections;
    //List<TilePattern> newPossiblePatterns;              //ADDED 2 - UNSURE IF THIS IS THE CORRECT WAY TO CALL IT

    public Vector3Int Index { get; private set; }       //ADDED 2
    //List<Vector3Int> Directions;


    private TilePattern _mat_ConPink;   //ADDED   00
    private TilePattern _mat_ConYellow; //ADDED   01
    private TilePattern _mat_ConBlue;   //ADDED   02
    private TilePattern _mat_Orange;    //ADDED   03
    private TilePattern _mat_Cyan;      //ADDED   04
    private TilePattern _mat_Green;     //ADDED   05

    //Eppy.Tuple<int, int>[] stack;     //ADDED
    //int stacksize;                      //ADDED

    //private IEnumerable<object> enumerable;     //ADDED 2

    /*public object GetTile { get; private set; }*/ //ADDED 2



    #endregion

    #region Unity Standard Methods

    void Start()
    {
        GOPatternPrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Prefabs/PrefabPatternA"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternB"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternC"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternD"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternE"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternF"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternG"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternH"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternI"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternJ"),
            Resources.Load<GameObject>("Prefabs/PrefabPatternK")
        };
        //Add all connections
        _connections = new List<Connection>();

        _connections.Add(new Connection(ConnectionType.conPink, "conPink"));          //00
        _connections.Add(new Connection(ConnectionType.conYellow, "conYellow"));      //01
        _connections.Add(new Connection(ConnectionType.conBlue, "conBlue"));          //02
        _connections.Add(new Connection(ConnectionType.conOrange, "conOrange"));      //03
        _connections.Add(new Connection(ConnectionType.conCyan, "conCyan"));          //04
        _connections.Add(new Connection(ConnectionType.conGreen, "conGreen"));        //05

        //Add all patterns
        _patternLibrary = new List<TilePattern>();
        for (int i = 0; i < GOPatternPrefabs.Length; i++)
        {
            var goPattern = GOPatternPrefabs[i];
            _patternLibrary.Add(new TilePattern(i, goPattern, _connections));
        }
       
        //Set up the tile grid
        MakeTiles();
        // add a random tile to a random position
        TileGrid[5, 5, 5].AssignPattern(_patternLibrary[1]);
        
        GetNextTile();

        //look into making this into a bounding box
        
    }

    

    public void GetPlan()
    {
        List<Tile> tiles = new List<Tile>();
        foreach (var tile in TileGrid)
        {
            if (tile.Index.y == 0 && tile.Set) tiles.Add(tile);
        }
        PlanCreation.CreatePlanImage(tiles, 0f, Vector3.zero);
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10,200,50 ),"WFC step"))
        {
            GetNextTile();
        }
        if (GUI.Button(new Rect(10, 100, 200, 50), "Getplan"))
        {
            VoidVisability();
            GetPlan();
        }
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
        TileGrid = new Tile[GridDimensions.x, GridDimensions.y, GridDimensions.z];
        for (int x = 0; x < GridDimensions.x; x++)
        {
            for (int y = 0; y < GridDimensions.y; y++)
            {
                for (int z = 0; z < GridDimensions.z; z++)
                {
                    TileGrid[x, y, z] = new Tile(new Vector3Int(x, y, z), _patternLibrary, this);
                }
            }
        }
    }

    private void GetNextTile()
    {
        // <summary>
        // get all unset tiles -> tiles that have no tile pattern assinged (tile.CurrentPattern)
        // for each of the unset, get the PossibleConnections
        // sort your unset tiles by the length of possible connection -> 0 == smallest lenght
        // get index 0 from the unset
        // do tile.AssingPattern() and assign a random tile pattern from its PossibleConnections
        // OUTSIDE THIS METHOD: Reapeat until no more tiles are left unset
        // <summary>

        List<Tile> UnsetTiles = GetUnsetTiles();     // unsetTiles to newPossibleNeighbours
      
        //Check if there still are tiles to set

        if (UnsetTiles.Count == 0)
        {
            Debug.Log("all tiles are set");
            return;
        }                     

        //this is currently not going to give you the lowest tile
        List<Tile> lowestTiles = new List<Tile>();
        int lowestTile = int.MaxValue;

        //Moved this section to under the propagate grid function                    
      
        


        //PropogateGrid on the set tile                     

        foreach (Tile tile in UnsetTiles)                                 
        {
            if (tile.NumberOfPossiblePatterns < lowestTile)
            {
                lowestTiles = new List<Tile>() { tile };

                lowestTile = tile.NumberOfPossiblePatterns;
                
                            }
            else if (tile.NumberOfPossiblePatterns == lowestTile)
            {
                lowestTiles.Add(tile);
            }
            else if (tile.NumberOfPossiblePatterns != lowestTile)
            {
                tile.AssignRandomPossiblePattern();
            }

            Debug.Log("Propagating Grid");
        }
        //Select a random tile out of the list
        int rndIndex = Random.Range(0, lowestTiles.Count);
        Tile tileToSet = lowestTiles[rndIndex];

        Debug.Log(" Random Index " + lowestTiles.Count);


        //Assign one of the possible patterns to the tile
        tileToSet.AssignRandomPossiblePattern();
    }

    //Cardinal Directions Establishment 
    public List<Vector3Int> GetTileDirectionList()                                                  
    {
        List<Vector3Int> tileDirections = new List<Vector3Int>();                                 
        foreach (Vector3Int tileDirection in Util.Directions)                                      
        {
            //if (Util.CheckInBounds(_tileGrid._gridDimensions, tileDirectionsIndex))
            if (Util.CheckInBounds(GridDimensions, Index))                                 
            {
                tileDirections.Add((Vector3Int)tileDirection);
            }
        }
        return tileDirections;                                                             
                                                                                            
    }

    //tile to unsetTile, added possibleNeighbours, List<Tile> newPossiblePatterns
    public List<Tile> GetNeighbour(List<TilePattern>newPossiblePatterns) 
    {                                         
        List<Tile> possibleNeighbours = new List<Tile>();                       
        IEnumerable<object> tileDirections = null;
        foreach (var unsetTiles in tileDirections)                              
        {
            if (unsetTiles == newPossiblePatterns)
            {
                possibleNeighbours.Add((Tile)unsetTiles);
            }                       
        }

        return possibleNeighbours;
    }

    public List<Tile> GetUnsetTiles() 
    {
        List<Tile> unsetTiles = new List<Tile>();

        //Loop over all the tiles and check which ones are not set
        foreach (var tile in GetTilesFlattened())
        {
            if (!tile.Set) unsetTiles.Add(tile);
           
            Debug.Log(tile.PossiblePatterns.Count);
        }
        Debug.Log(unsetTiles.Count);
        return unsetTiles;
    }

    private List<Tile> GetTilesFlattened()  
    {
        List<Tile> tiles = new List<Tile>();
        for (int x = 0; x < GridDimensions.x; x++)
        {
            for (int y = 0; y < GridDimensions.y; y++)
            {
                for (int z = 0; z < GridDimensions.z; z++)
                {
                    tiles.Add(TileGrid[x, y, z]);
                }
            }
        }
        return tiles;
    }

    public void VoidVisability()
    {
        foreach (var tile in TileGrid)
        {
            if (tile.Set)
            {
                tile.VisibilitySwitch();
            }
        }
    
    }


}
    
    #endregion



