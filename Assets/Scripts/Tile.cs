using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    #region public fields
    List<TilePattern> PossiblePatterns;
    Vector3Int Index;

    #endregion

    #region private fields

    #endregion
    #region constructors
    public Tile(Vector3Int index, List<TilePattern> tileLibrary)
    {
        PossiblePatterns = tileLibrary;
        Index = index;
    }

    #endregion
    #region public functions
    public void AssignRandomPossiblePattern()
    {
        //Select a random pattern out of the list of possible patterns
        //Create a prefab of the selected pattern
        //Remove all possible patterns out of the list

        //You could add some weighted randomness in here
    }

    #endregion
    #region private functions

    #endregion
}
