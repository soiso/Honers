using UnityEngine;
using System.Collections;

public class ShaderChanger : MonoBehaviour
{
    [SerializeField, HeaderAttribute("朝の色")]
    Color MorningColor;
    [SerializeField, HeaderAttribute("昼の色")]
    Color NoonColor;
    [SerializeField, HeaderAttribute("夜の色")]
    Color NightColor;

    public void Change(PanelParametor.TIMEZONE Time)
    {
        Renderer[] Renderer = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach( Renderer renderer  in Renderer )
        {
            //renderer.material.shader = Shader.Find("Custom/Lighting");
            //renderer.material.color = MorningColor;
            switch (Time)
            {
                case PanelParametor.TIMEZONE.morning:
                    //renderer.material.shader = Shader.Find("Custom/MorningLighting");
                    renderer.material.color = MorningColor;
                    break;
                case PanelParametor.TIMEZONE.noon:
                    //renderer.material.shader = Shader.Find("Custom/NoonLighting");
                    renderer.material.color = NoonColor;
                    break;
                case PanelParametor.TIMEZONE.night:
                    //renderer.material.shader = Shader.Find("Custom/NightLighting");
                    renderer.material.color = NightColor;
                    break;
            }
        }
    }

}
