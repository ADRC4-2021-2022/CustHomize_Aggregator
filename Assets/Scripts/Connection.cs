using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add all connection types here
public enum ConnectionType { conPink = 0 , conYellow = 1}
public class Connection
{
    #region public fields
    ConnectionType Type;
    public string Name;
    public List<TilePattern> ConnectingTiles;

    #endregion

    #region private fields

    #endregion
    #region constructors
    public Connection(ConnectionType type, string name)
    {
        Type = type;
    }

    #endregion
    #region public functions
    public void AddTilePatternToConnection(TilePattern pattern)
    {
        ConnectingTiles.Add(pattern);
    }

    #endregion
    #region private functions

    #endregion
}
