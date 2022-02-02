using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintSolver
{
    #region public fields
    public Vector3Int GridDimensions;

    #endregion

    #region private fields
    Tile[,,] _tileGrid;
    List<TilePattern> _patternLibrary;
    List<Connection> _connections;


    #endregion
    #region constructors
    public ConstraintSolver(Vector3Int gridDimensions)
    {
        GridDimensions = gridDimensions;

        //Add all connections
        _connections = new List<Connection>();

        _connections.Add(new Connection(ConnectionType.conPink, "conPink"));
        _connections.Add(new Connection(ConnectionType.conYellow, "conYellow"));
    }

    #endregion
    #region public functions
    

    #endregion
    #region private functions
    private void MakeTiles()
    {
        _tileGrid = new Tile[GridDimensions.x, GridDimensions.y, GridDimensions.z];
        for (int x = 0; x < GridDimensions.x; x++)
        {
            for (int y = 0; y < GridDimensions.y; y++)
            {
                for (int z = 0; z < GridDimensions.z; z++)
                {
                    _tileGrid[x, y, z] = new Tile(new Vector3Int(x, y, z), _patternLibrary);
                }
            }
        }
    }
    
    private void GetNextTile()
    {
        //Check if there still are tiles to set
        //Find a tile in the grid that it not set with the least possible options
        //Loop over all the tiles
        //Count how many possible patterns there are
        //Find all the tiles with the least amount of possible patterns
        //Select a random tile out of this list
        //Assign one of the possible patterns to the tile
        //PropogateGrid on the set tile
    }

    private void PropogateGrid(Tile changedTile)
    {
        //Loop over the connections of the changedTile
        //Per connection: go to the neighbour tile in the connection direction
        //Crossreference the list of possible connections in the neighbour tile with the list of possilbepatterns in the connection

        //If one or multiple of the neighbours has no more possible tilepattern, solving failed, start over
        //you could assign a possible tile of the previous propogation, this will cause impurities but might make it easier to solve

        //If one or multiple of the neighbours has only one possible tilepattern, set the tile pattern
        //propogate the grid for the new set tile

    }

    #endregion
}
