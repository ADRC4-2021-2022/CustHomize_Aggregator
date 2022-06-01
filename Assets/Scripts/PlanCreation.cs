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
        Vector2Int grid2Size = new Vector2Int(48, 48);
        GameObject[,] dots = new GameObject[gridSize.x, gridSize.y];

        Texture2D textureA = new Texture2D(gridSize.x, gridSize.y);
        Texture2D textureB = new Texture2D(grid2Size.x, grid2Size.y);

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
                textureA.SetPixel(x,z, Color.white);
            }
        }

        for (int x = 0; x < grid2Size.x; x++)
        {
            for (int z = 0; z < grid2Size.y; z++)
            {
                var position = new Vector3(x * dotSize, level, z * dotSize) + origin;
                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                // remove the cube, use regular empty go
                // add box collider component to the go
                go.transform.position = position;
                go.transform.localScale = Vector3.one * dotSize;
                dots[x, z] = go;
                textureB.SetPixel(x, z, Color.white);
            }
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var dot = dots[x, y];

                foreach (var transform in transforms)
                {
                    var transformCollider = transform.GetComponent<Collider>();

                    var vec = Physics.ClosestPoint(dot.transform.position, transformCollider, transform.position, transform.rotation);
                    //if (Vector3.Distance(vec, dot.transform.position) < dotSize * 1.5f)
                    if (Util.PointInsideCollider(dot.transform.position, transformCollider))
                    {
                        dot.GetComponent<MeshRenderer>().material.color = Color.black;
                        textureA.SetPixel(x, y, Color.black);
                        break;
                    }
                }
            }
            
        }

        for (int x = 0; x < grid2Size.x; x++)
        {
            for (int y = 0; y < grid2Size.y; y++)
            {
                var dot = dots[x, y];

                foreach (var transform in transforms)
                {
                    var transformCollider = transform.GetComponent<Collider>();

                    var vec = Physics.ClosestPoint(dot.transform.position, transformCollider, transform.position, transform.rotation);
                    //if (Vector3.Distance(vec, dot.transform.position) < dotSize * 1.5f)
                    if (Util.PointInsideCollider(dot.transform.position, transformCollider))
                    {
                        dot.GetComponent<MeshRenderer>().material.color = Color.black;
                        textureB.SetPixel(x, y, Color.black);
                        break;
                    }
                }
            }

        }

        textureA.Apply();
        return textureA;


        textureB.Apply();
        return textureB;

       //these are for later

       // textureb.Apply();
       // return textureC;

       // textureb.Apply();
       // return textureD;

        //Attempt at texture merging script
        //Create an array with the picture needed to be merged
        Texture2D[] toMerge = { texture, textureB };

        //Create the finale picture
        Texture2D finaltexture = new Texture2D(256, 256);

        //Merge the mockup and screen into the finale picture
        for (int i = 0; i < toMerge.Length; i++)
        {
            for (int x = 0; x < toMerge[i].width; x++)
            {
                for (int y = 0; y < toMerge[i].height; y++)
                {
                    var color = toMerge[i].GetPixel(x, y).a == 0 ?
                        finaltexture.GetPixel(x, y) :
                        toMerge[i].GetPixel(x, y);

                    finaltexture.SetPixel(x, y, color);
                }
            }
        }
        finaltexture.Apply();

        //Create the save file of the final picture
        byte[] floorPlan = finaltexture.EncodeToPNG();

        //This is not correct obviously, but I forgot why this file path is not working 
        filePath.WriteAllBytes(floorPlan);

    }

}
