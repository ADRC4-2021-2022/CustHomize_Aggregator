using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlanCreation
{
    public static Texture2D CreatePlanImage(List<Tile> tiles, float level, Vector3 origin)
    {
        float dotSize = 0.1f;
        Vector2Int gridSize = new Vector2Int(64, 64);
        GameObject[,] dots = new GameObject[gridSize.x, gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                //dots[x, z] = new Vector3(x * dotSize, level, z * dotSize) + origin;
                var position = new Vector3(x * dotSize, level, z * dotSize) + origin;
                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = position;
                go.transform.localScale = Vector3.one * dotSize;
                dots[x, z] = go;
            }
        }

        Texture2D texture = new Texture2D(gridSize.x, gridSize.y);

        List<Transform> colliders = new List<Transform>();
        foreach (var tile in tiles)
        {
            var collider = tile.GetComponentCollider();
            if (collider != null) colliders.Add(collider);
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                var dot = dots[x, z];
                bool isInside = false;
                foreach (var transform in colliders)
                {
                    //isInside = Util.PointInsideCollider(dot, collider);
                    var collider = transform.GetComponentInChildren<Collider>();
                    if (collider == null) break;
                    isInside = Physics.ComputePenetration(
                        dot.GetComponent<Collider>(), 
                        dot.transform.position, 
                        Quaternion.identity, 
                        collider, 
                        transform.parent.position,
                        Quaternion.identity,
                        out Vector3 direction, 
                        out float distance);
                    if (isInside)
                    {
                        
                        break;
                    }
                }
                if (!isInside)
                {
                    // add white pixel to image
                    Debug.Log("white pixel");
                    texture.SetPixel(x, z, Color.white);
                }
                else
                {
                    // add black pixel to image
                    texture.SetPixel(x, z, Color.black);
                    dot.GetComponent<MeshRenderer>().material.color = Color.black;
                    Debug.Log("black pixel");
                }
            }
        }
        texture.Apply();
        return texture;
    }

}
