using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanEnvironment : MonoBehaviour
{
    List<Transform> _children;

    // Start is called before the first frame update
    void Start()
    {
        _children = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            _children.Add(child);
        }
    }



    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 50), "PLAN!"))
        {
            PlanCreation.CreatePlanFromTransforms(_children, 0, Vector3.zero);
        }

    }
}
