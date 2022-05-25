using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlanCreation
{
    public static Texture2D CreatePlanFromTiles(List<Tile> tiles, float level, Vector3 origin)
    {
        List<Transform> transforms = new List<Transform>();
        foreach (var tile in tiles)
        {
            var collider = tile.GetComponentCollider();
            if (collider != null) transforms.Add(collider);
        }

        return CreatePlanFromTransforms(transforms, level, origin);
    }

    public static Texture2D CreatePlanFromTransforms(List<Transform> transforms, float level, Vector3 origin)
    {
        float dotSize = 1f;
        Vector2Int gridSize = new Vector2Int(48, 48);
        GameObject[,] dots = new GameObject[gridSize.x, gridSize.y];

        Texture2D texture = new Texture2D(gridSize.x, gridSize.y);

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                var position = new Vector3(x * dotSize, level, z * dotSize) + origin;
                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                // remove the cube, use regular empty go
                // add box collider component to the go
                go.transform.position = position;
                go.transform.localScale = Vector3.one * dotSize;
                dots[x, z] = go;
                texture.SetPixel(x,z, Color.white);
            }
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var dot = dots[x, y];
                //var dotCollider = dot.GetComponent<BoxCollider>();
                //dotCollider.size = Vector3.one * dotSize;
                foreach (var transform in transforms)
                {
                    var transformCollider = transform.GetComponent<Collider>();

                    var vec = Physics.ClosestPoint(dot.transform.position, transformCollider, transform.position, transform.rotation);
                    //if (Vector3.Distance(vec, dot.transform.position) < dotSize * 1.5f)
                    if (Util.PointInsideCollider(dot.transform.position, transformCollider))
                    {
                        dot.GetComponent<MeshRenderer>().material.color = Color.black;
                        texture.SetPixel(x, y, Color.black);
                        break;
                    }
                }
            }
            
        }

        texture.Apply();
        return texture;
    }

}
