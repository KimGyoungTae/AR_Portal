// ���۷��� ������ AR Portal ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalController : MonoBehaviour
{

    public GameObject HiddenWorld;
    public Material[] materials;

    private Vector3 camPosition;

    bool front;
    bool otherWorld;
    bool hasCollided;

    private void Start()
    {
        SetMaterials(false);
    }

    void SetMaterials(bool active)
    {
        // active == true : CompareFunction.NotEqual
        // active == false : CompareFunction.Equal
        var stencilTest = active ? CompareFunction.NotEqual : CompareFunction.Equal;

        foreach (var material in materials)
        {
            material.SetInt("_StencilComp", (int)stencilTest);
        }
    }

    bool GetIsFront()
    {
        GameObject myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 worldPos = myCamera.transform.position + myCamera.transform.forward * Camera.main.nearClipPlane;

      camPosition = transform.InverseTransformPoint(worldPos);
        return camPosition.y >=0 ? true : false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if(other.transform != myCamera.transform)
        {
            return;
        }

        front = GetIsFront();
        hasCollided = true;
     
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (other.transform != myCamera.transform)
            return;

        hasCollided = false;
      
    }

    void WhileCameraColliding()
    {
        if(!hasCollided)
        {
            return;
        }

        bool isFront = GetIsFront();

        // ���� ���¿� ���� ���¸� ���Ͽ� ī�޶� ��Ż �տ��� �ڷ�, �ڿ��� ������ �̵��ߴ� �� ���θ� �Ǵ�.
        if ((isFront && !front) || (front && !isFront))
        {
            otherWorld = !otherWorld;
            SetMaterials(otherWorld);
        }

        front = isFront;
    }

    private void OnDestroy()
    {
        SetMaterials(true);
    }

    private void Update()
    {
        WhileCameraColliding();
    }

}
