using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanEnvironment : MonoBehaviour
{
    List<Transform> _children;

    // Start is called before the first frame update
    void Start()
    {
        _children = new List<Transform>();
        _children = Util.GetChildObjectByTag(transform,"Collider").Select(t=>t.transform).ToList();
  
    }



    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 50), "Plan Creation"))
        {
            PlanCreation.CreatePlanFromTransforms(_children, 5, Vector3.zero);
        }

    }
}
