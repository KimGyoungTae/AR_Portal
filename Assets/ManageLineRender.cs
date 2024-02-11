using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManageLineRender : MonoBehaviour
{
    private LineRenderer Line;

    private Camera cam;
    public GameObject linePrefab;

    private Vector3 mousePos;
    private Vector3 newPos;
    public List<Vector3> linePositions = new List<Vector3>();

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            mousePos.z = cam.nearClipPlane + 1f;
            newPos = cam.ScreenToWorldPoint(mousePos);
            linePositions.Add(newPos);

            GameObject obj = Instantiate(linePrefab);
            Line = obj.GetComponent<LineRenderer>();

            Line.positionCount = 1;
            Line.SetPosition(0, linePositions[0]);
        }

        else if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            mousePos.z = cam.nearClipPlane + 1f;
            newPos = cam.ScreenToWorldPoint(mousePos);

            // 마우스 포인터가 눌리고만 있어도 Position 값이 갱신 되는 것을 방지
            if (Vector3.Distance(linePositions[linePositions.Count - 1], newPos) > 0.1f)
            {
                

                linePositions.Add(newPos);

                Line.positionCount++;
                Line.SetPosition(Line.positionCount - 1, linePositions[Line.positionCount - 1]);
            }
            
        
        }

        else if(Input.GetMouseButtonUp(0))
        {
            linePositions.Clear();
        }
    }




    //void GenerateMeshCollider()
    //{
    //    //MeshCollider collider = GetComponent<MeshCollider>();
    //    EdgeCollider2D collider = GetComponent<EdgeCollider2D>();

    //    if (collider != null )
    //    {
    //        // collider = gameObject.AddComponent<MeshCollider>();
    //        collider = gameObject.AddComponent<EdgeCollider2D>();
    //    }

    //    Mesh mesh = new Mesh();
    //    Line.BakeMesh(mesh);
    //   // collider.sharedMesh = mesh;

    //}
}
