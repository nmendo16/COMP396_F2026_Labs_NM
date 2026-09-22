using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class TestVectors : MonoBehaviour
{
    public Vector3 v1, v2, v3;
    float k;

    
    // Start is called before the first frame update
    void Start()
    {
        v1 = new Vector3(1, 2, 3);
        v2 = new Vector3(2, -1, 1);
        k = 1.5f;
        Vector3 v1_plus_v2 = v1 + v2;
        Vector3 v2_plus_v1 = v2 + v1;
        print($"v1_plus_v2={v1_plus_v2},v2_plus_v1={v2_plus_v1}, diff ={v1_plus_v2 - v2_plus_v1}");

        // Implementing IntersectRayWithATriangle
        Vector3 P0 = new Vector3(0, 1, 0);
        Vector3 d;

        //Triangle
        Vector3 V0 = new Vector3(0, 0, 0);
        Vector3 V1 = new Vector3(1, 0, 0);
        Vector3 V2 = new Vector3(0, 0, 1);
        Vector3 n; //NORMAL TO TRIANGLE

        //Case 0
        Vector3 actualP_Case0 = (V0 + V1 + V2) / 3;
        d = actualP_Case0 -P0; 
        d.Normalize();

        //Method 1" Use Raycast : TODO
        RaycastHit hit;
        Physics.Raycast(P0,d, out hit);
        print($"hit.point = {hit.point}, hit.normal = {hit.normal}");
        
        //You'll need an object, (cube, blender tiangular prism very thin)

        Vector3 calculatedP_Case0 = IntersectRayWithTriangle(P0, d, V0, V1, V2, out n);
        print($"Case 0: actualP={actualP_Case0}, calculatedP={calculatedP_Case0}, diff ={calculatedP_Case0 - actualP_Case0}, normal = {n}");

        //Case 1
        Vector3 actualP_Case1 = (V0 + V1 + V2) / 3;
        d = actualP_Case1 - P0;
        d.Normalize();

        Vector3 calculatedP_Case1 = IntersectRayWithTriangle(P0, d, V0, V1, V2, out n); 
        print($"Case 1: actualP={actualP_Case1}, calculatedP={calculatedP_Case1}, diff ={calculatedP_Case1 - actualP_Case1}, normal = {n}");


        //Case 2
        Vector3 actualP_Case2 = (V0 + V1 + V2) / 3;
        d = actualP_Case2 - P0;
        d.Normalize();

        Vector3 calculatedP_Case2 = IntersectRayWithTriangle(P0, d, V0, V1, V2, out n);
        print($"Case 2: actualP={actualP_Case2}, calculatedP={calculatedP_Case2}, diff ={calculatedP_Case2 - actualP_Case2}, normal = {n}");

        //Calculate: v1_hat (unit vector), v2_hat
        //Calculate: v1.v2 (dot product), v1_hat.v2_hat
        //Vector3.Dot(v1, v2);
        //Calculate: v1 x v2 (Cross product), v2 x v1, compare them
        //Vector3.Cross(v1, v2);
        //Calculate. Magnitude, Distance, 1-2 Lerps

    }

    private Vector3 IntersectRayWithTriangle(Vector3 p0, Vector3 d, Vector3 v0, Vector3 v1, Vector3 v2, out Vector3 n)
    {
        //throw new NotImplementedException();
        Vector3 p = Vector3.zero, a, b, pv0;
        a = v1 - v0;
        b = v2 - v0;
        n = Vector3.Cross(b, a);

        float dn = Vector3.Dot(d, n);
        pv0 = p0 - v0;
        float pv0n = Vector3.Dot(pv0, n);
        float t;

        if (Mathf.Abs(dn) > 0.0000001)
        {
            t = pv0n / dn;
            p = p0 + t * d;

        }
        else
        {
            Debug.Log($"dn=0. Check that P0 is in tri or not.");
        }
        return p;
    }


    // Update is called once per frame
    void Update()
    {
        //
    }
}