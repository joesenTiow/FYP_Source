using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitLine : MonoBehaviour
{
    //xRadius and yRadius are the distance of the orbiting body from the center in the editor.
    public float xRadius;
    public float zRadius;
    public int segments;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        // line.SetVertexCount(segments + 1);

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePoints()
    {
        float x;
        float y = 0f;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xRadius / 0.2f;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * zRadius / 0.2f;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
