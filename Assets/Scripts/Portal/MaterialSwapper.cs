// Portal에 충돌했을 때 포탈 안에 공간 Material을 바꿔보려 했음. -> 실패한 코드

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public Material[] materials; // Material 배열

    private MeshRenderer meshRenderer;// Mesh Renderer 컴포넌트

    private int currentMaterialIndex = 0; // 현재 Material 인덱스

    public float timeBetweenStateChanges = 1.0f; // 시간 간격 설정
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

        // 초기 Material 설정
        meshRenderer.material = materials[currentMaterialIndex];

       
    }

    private void Update()
    {
        // 매 프레임마다 'M' 키를 누르면 Material 교체
        if (Input.GetKeyDown(KeyCode.M))
        {
            // 다음 Material 인덱스로 변경
            currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;

            // Material 교체
            meshRenderer.material = materials[currentMaterialIndex];
        }

       

        //// Portal 콜라이더 충돌 시 (Portal 안으로 들어갈 때) 
        //if (PortalManager.instance.isChange == true)
        //{
        //    // 다음 Material 인덱스로 변경
        //    currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;

        //    // Material 교체
        //    meshRenderer.material = materials[currentMaterialIndex];

        //    // 시간 측정
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
    //   // 다음 Material 인덱스로 변경
    //    currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;

    //    // Material 교체
    //    meshRenderer.material = materials[currentMaterialIndex];
    //}


}
