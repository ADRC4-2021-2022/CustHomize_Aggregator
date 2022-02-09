using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    #region public fields
    public List<TilePattern> PossiblePatterns;
    public Vector3Int Index;

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
        //Create a prefab of the selected pattern using the index and the voxelsize as position
        //Remove all possible patterns out of the list

        //You could add some weighted randomness in here

        //propogate the grid
        _solver.PropogateGrid(this);
    }

    public void CrossreferenceConnectionPatterns(List<TilePattern> patterns)
    {
        //Check if the patterns exist in both lists
        List<TilePattern> newPossiblePatterns = new List<TilePattern>();
        foreach (var pattern in patterns)
        {
            if(PossiblePatterns.Contains(pattern))
            {
                newPossiblePatterns.Add(pattern); 
            }
        }

        PossiblePatterns = newPossiblePatterns;
    }

    #endregion
    #region private functions

    #endregion
}
