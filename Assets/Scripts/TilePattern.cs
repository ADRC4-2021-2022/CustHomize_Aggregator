using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum PatternType { mat_ConPink, mat_ConYellow, mat_ConBlue, mat_ConOrange, mat_ConCyan, mat_ConGreen }   //ADDED
public class TilePattern
{

    #region public fields
    public List<Connection> ConnectionTypes;
    public Connection[] Connections;
    public int Index;

    public Vector3Int[] Axes = new Vector3Int[6]
    {
        new Vector3Int(-1,0,0), //-x    0   //CHANGED
        new Vector3Int(1,0,0),  //x     1   //CHANGED
        new Vector3Int(0,1,0),  //y     2   //CHANGED
        new Vector3Int(0,-1,0), //-y    3   //CHANGED
        new Vector3Int(0,0,-1), //-z    4   //CHANGED
        new Vector3Int(0,0,1),  //z     5   //CHANGED
    };

    /* Dictionary<string, ConnectionType> ConnectionTypes = new Dictionary<string, ConnectionType>
     {
         {"conPink",ConnectionType.conPink },
         {"conYellow",ConnectionType.conYellow }
     };*/

    #endregion

    #region private fields
    GameObject _goTilePrefab;
    private PatternType mat_ConPink;
    private PatternType mat_ConYellow;      //ADDED
    private PatternType mat_ConBlue;        //ADDED
    private PatternType mat_ConOrange;      //ADDED
    private PatternType mat_ConCyan;        //ADDED
    private PatternType mat_ConGreen;       //ADDED
    private string r;                       //ADDED
    private string s;                       //ADDED
    private string t;                       //ADDED
    private string u;                       //ADDED
    private string v;
    private string w;                       //ADDED

    #endregion
    #region constructors
    public TilePattern(int index, GameObject goTilePrefab, List<Connection> connectionTypes)
    {
        Index = index;
        _goTilePrefab = goTilePrefab;
        ConnectionTypes = connectionTypes;

        GetConnections();
    }

    //public TilePattern(PatternType mat_ConPink, string v)
    //{
    //    this.mat_ConPink = mat_ConPink;
    //    this.v = v;
    //}

    public TilePattern(PatternType mat_ConPink, PatternType mat_ConYellow, PatternType mat_ConBlue, PatternType mat_ConOrange, PatternType mat_ConCyan, PatternType mat_ConGreen, string r, string s, string t, string u, string v, string w)   //ADDED
    {   
        this.mat_ConPink = mat_ConPink;         //ADDED 
        this.mat_ConYellow = mat_ConYellow;     //ADDED
        this.mat_ConBlue = mat_ConBlue;         //ADDED
        this.mat_ConOrange = mat_ConOrange;     //ADDED
        this.mat_ConCyan = mat_ConCyan;         //ADDED
        this.mat_ConGreen = mat_ConGreen;       //ADDED
        this.r = r;                             //ADDED
        this.s = s;                             //ADDED
        this.t = t;                             //ADDED
        this.u = u;                             //ADDED
        this.v = v;                             //ADDED
        this.w = w;                             //ADDED
    }

    #endregion
    #region public functions
    //Put this function into a UTIl class, you can use it in your entire project
    public List<GameObject> GetChildObjectByTag(Transform parent, string tag) //?? WHERE DO WE DO THIS??
    {
        List<GameObject> taggedChildren = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == tag)
            {
                taggedChildren.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObjectByTag(child, tag);
            }
        }

        return taggedChildren;
    }

    //Put this function into a UTIl class, you can use it in your entire project
    public List<GameObject> GetChildObjectByLayer(Transform parent, int layer) //?? WHERE DO WE DO THIS??
    {
        List<GameObject> layerChildren = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.gameObject.layer == layer)
            {
                layerChildren.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObjectByLayer(child, layer);
            }
        }

        return layerChildren;
    }
    #endregion
    #region private functions
    private void GetConnections()
    {
        Connections = new Connection[6];

        List<GameObject> goConnections = GetChildObjectByLayer(_goTilePrefab.transform, LayerMask.NameToLayer("Connections"));

        foreach (var goConnection in goConnections)
        {
            var connection = ConnectionTypes.First(c => c.Name == goConnection.tag);
            connection.AddTilePatternToConnection(this);
            Vector3 rotation = goConnection.transform.rotation.eulerAngles;
            if (rotation.x != 0)
            {
                //we know it is a vertical connection
                if (rotation.x == 90)
                {
                    Connections[3] = connection;
                }
                else
                {
                    Connections[2] = connection;
                }
            }
            //else
            //{
            //    Connections[(int)rotation.y % 90] = connection;
            else if (rotation.y == 90)                              //ADDED
            //we know it is a connection in the positive x axis     //ADDED
            {
                Connections[1] = connection;                        //ADDED
            }
            else if (rotation.y == 180)                             //ADDED
            //we know it is a connection in the negative z axis     //ADDED
            {
                Connections[4] = connection;                        //ADDED
            }
            else if (rotation.y == 270) //ADDED
            //we know it is a connection in the negative x axis     //ADDED
            {
                Connections[0] = connection;                        //ADDED
            }
            else                                                    //ADDED
            //we know it is a connection in the positive z axis     //ADDED
            {
                Connections[5] = connection;                        //ADDED
            }
        }
    }
    #endregion
}
