# Unity AR Portal
![image](https://github.com/KimGyoungTae/AR_Portal/assets/83820089/2bc3e135-5091-483b-9965-999335d4cad7)
<br>

## 📝 프로젝트  소개

###  AR Portal using Unity Stencil Buffer :

🎥 구현 영상 : https://www.youtube.com/watch?v=3BNphi6mY9Y

📌 프로젝트 경로 및 핵심 파트
- Assets - Scenes - Portal - Sample Portal
- Assets - Scripts - Portal - Portal Controller & AR Object Controller
- Assets - Materials - Shaders - Custom Specular & Portal Mask

⚙️ 개발 버전
- `2021.3.21`

<br>

## 🔎 핵심 구현 부분들

### 1. Custom Specular | Portal 내부 커스텀 쉐이더 : 
- _StencilComp 스탠실 비교를 정의 하는 데 사용되는 Enum 타입 변수 선언
- _StencilComp 의 값이 3인 경우는 CompareFunction.Equal에 해당합니다.

```cs
Shader "Custom/Custom Specular"
{
    Properties{
    ........
[Enum(CompareFunction)] _StencilComp("Stencil Comp", Int) = 3
    }

 SubShader
    {
 LOD 300
 Tags { "RenderType" = "Opaque" "PerformanceChecks" = "False" }

    Stencil
    {
            Ref 1
            Comp[_StencilComp]
    }
    ........
  }
}
```

### 2. Portal Mask | Portal 마스크 커스텀 쉐이더 :

```cs
Shader "Custom/PortalMask"
{
  Properties
    {
        
    }
    SubShader
    {
        Tags 
        { 
            
            "RenderType"="Opaque"
            "Queue" = "Geometry"
        }

        LOD 100
        ColorMask 0 // Portal 자체는 마스크 용도이기에, 렌더링 하지 않음 -> 투명색으로 변환
        Zwrite off  // 개체가 앞이든 뒤이든 마스크를 통해서 보여지고 싶을 때 
        Cull off

        Pass
        {
           Stencil
            {
                Ref 1
                Comp Always
                Pass replace
            }
      ........
        }
}
```

### 3. ARObjectController :
   
- 평면을 터치한 곳의 위치를 받아와 Prefab 오브젝트 생성

![image](https://github.com/KimGyoungTae/AR_Portal/assets/83820089/32b702a5-e6b9-4d2d-acdb-4aa7e3f1c3f9)


### 4. PortalController :
  
  ![image](https://github.com/KimGyoungTae/AR_Portal/assets/83820089/6ca76178-afec-4d68-b972-6972ee13ab29)
  ![image](https://github.com/KimGyoungTae/AR_Portal/assets/83820089/22535760-8dfa-4380-b704-e06752bae847)

- Prefab 내부 오브젝트에 만들어준다.
- 카메라(사용자)가 Portal 내부로 통과할 때 → active 매개변수 true → materials 배열 모든 요소에 대해 _StencilComp 속성을 NotEqual로 설정
- Portal을 통해 외부로 통과 할 때 → active 매개변수 false → materials 배열 모든 요소에 대해 _StencilComp 속성을 Equal로 설정

```cs
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
```

![image](https://github.com/KimGyoungTae/AR_Portal/assets/83820089/e2998c07-d897-4668-806b-6f988d374c40)

![2024-02-1219-54-34-ezgif com-video-to-gif-converter](https://github.com/KimGyoungTae/AR_Portal/assets/83820089/54712b2f-f091-4053-8a08-f67b6c9460f7)
