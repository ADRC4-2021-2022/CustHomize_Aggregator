using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class Tile
{
    #region public fields
    public List<TilePattern> PossiblePatterns;
    public Vector3Int Index;

    private Dictionary<int, GameObject> _goTilePatternPrefabs;                 //ADDED

    

    //A tile is set if there is only one possible pattern
    public bool Set
    {
        get
        {
            return (PossiblePatterns.Count == 1);
        }
    }

    public int NumberOfPossiblePatterns
    {
        get
        {
            return PossiblePatterns.Count;
        }
    }
    #endregion

    #region private fields
    private ConstraintSolver _solver;
    #endregion
    #region constructors
    public Tile(Vector3Int index, List<TilePattern> tileLibrary, ConstraintSolver solver)
    {
        PossiblePatterns = tileLibrary;
        Index = index;
        _solver = solver;
    }

    #endregion
    #region public functions
    public void AssignRandomPossiblePattern()
    {
        //Select a random pattern out of the list of possible patterns

        AssignPattern();
    }

    public void AssignPattern()
    {
        //HOW?????

        //Create a prefab of the selected pattern using the index and the voxelsize as position
                //creating a prefab of a SELECTED pattern. Where is this pattern being selected?
            //in TilePattern
            //using _goTilePrefab
            //Index
            //TileSize
        //Remove all possible patterns out of the list
        //will be using List<TilePattern> PossiblePatterns
        //remove the possible patterns that have not been selected


        //You could add some weighted randomness in here - IGNORE THIS FOR NOW UNTIL WE FIGURE OUT REST OF PROJECT

        //propogate the grid
        _solver.PropogateGrid(this);
    }

    public Dictionary<int, GameObject> GOPatternPrefabs                                                 //ADDED
    {
        get                                                                                             //ADDED
        {
            if (_goTilePatternPrefabs == null)                                                          //ADDED
            {
                _goTilePatternPrefabs = new Dictionary<int, GameObject>();                              //ADDED
                _goTilePatternPrefabs.Add(0, Resources.Load("Prefabs/PrefabPatternB") as GameObject);   //ADDED
                _goTilePatternPrefabs.Add(1, Resources.Load("Prefabs/PrefabPatternI") as GameObject);   //ADDED
                _goTilePatternPrefabs.Add(1, Resources.Load("Prefabs/PrefabPatternK") as GameObject);   //ADDED
                _goTilePatternPrefabs.Add(1, Resources.Load("Prefabs/PrefabPatternM") as GameObject);   //ADDED
            }
            return _goTilePatternPrefabs;                                                               //ADDED
        }
    }

    public void CrossreferenceConnectionPatterns(List<TilePattern> patterns)
    {
        //Check if the patterns exist in both lists
        List<TilePattern> newPossiblePatterns = new List<TilePattern>();
        foreach (var pattern in patterns)
        {
            if(PossiblePatterns.Contains(pattern))  //if the pattern is contained in both lists...
            {
                newPossiblePatterns.Add(pattern);   //add the pattern
            }
        }

        PossiblePatterns = newPossiblePatterns;   
    }

    #endregion
    #region private functions

    #endregion
}
