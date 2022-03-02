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

    

    /* Dictionary<string, ConnectionType> ConnectionTypes = new Dictionary<string, ConnectionType>
     {
         {"conPink",ConnectionType.conPink },
         {"conYellow",ConnectionType.conYellow }
     };*/

    #endregion

    #region private fields
    GameObject _goTilePrefab;
    /*private PatternType mat_ConPink;
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
    */
    #endregion
    #region constructors
    public TilePattern(int index, GameObject goTilePrefab, List<Connection> connectionTypes)
    {
        Index = index;
        _goTilePrefab = goTilePrefab;
        ConnectionTypes = connectionTypes;
        GetConnections();
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
    public void GetConnections()
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
                    Connections[2] = connection;
                }
                else
                {
                    Connections[3] = connection;
                }
            }
            //else
            //{
            //    Connections[(int)rotation.y % 90] = connection;
            else if (rotation.y == 90)                              //ADDED
            //we know it is a connection in the positive x axis     //ADDED
            {
                Connections[1] = connection;

            }
            else if (rotation.y == 180)                             //ADDED
            //we know it is a connection in the negative z axis     //ADDED
            {
                Connections[4] = connection;//ADDED
            }
            else if (rotation.y == 270) //ADDED
            //we know it is a connection in the negative x axis     //ADDED
            {
                Connections[0] = connection;//ADDED
            }
            else                                                    //ADDED
            //we know it is a connection in the positive z axis     //ADDED
            {
                Connections[5] = connection;//ADDED
            }
        }
    }
    #endregion
}
