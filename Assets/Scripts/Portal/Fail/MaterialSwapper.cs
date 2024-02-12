// Portal�� �浹���� �� ��Ż �ȿ� ���� Material�� �ٲ㺸�� ����. -> ������ �ڵ�

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public Material[] materials; // Material �迭

    private MeshRenderer meshRenderer;// Mesh Renderer ������Ʈ

    private int currentMaterialIndex = 0; // ���� Material �ε���

    public float timeBetweenStateChanges = 1.0f; // �ð� ���� ����
    private float currentTime = 0.0f;

    //public PortalManager portalManager;


    // Start is called before the first frame update
    void Start()
    {
       
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component not found!");
            return;
        }

        // �ʱ� Material ����
        meshRenderer.material = materials[currentMaterialIndex];

       
    }

    private void Update()
    {
        // �� �����Ӹ��� 'M' Ű�� ������ Material ��ü
        if (Input.GetKeyDown(KeyCode.M))
        {
            // ���� Material �ε����� ����
            currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;

            // Material ��ü
            meshRenderer.material = materials[currentMaterialIndex];
        }

       

        //// Portal �ݶ��̴� �浹 �� (Portal ������ �� ��) 
        //if (PortalManager.instance.isChange == true)
        //{
        //    // ���� Material �ε����� ����
        //    currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;

        //    // Material ��ü
        //    meshRenderer.material = materials[currentMaterialIndex];

        //    // �ð� ����
        //    currentTime += Time.deltaTime;

        //    if (currentTime >= timeBetweenStateChanges)
        //    {
        //       // PortalManager.instance.isChange = false;
        //        currentTime = 0.0f;
        //    }

        //}
        
    }


    //public void ChangeMaterial()
    //{
    //   // ���� Material �ε����� ����
    //    currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;

    //    // Material ��ü
    //    meshRenderer.material = materials[currentMaterialIndex];
    //}


}
