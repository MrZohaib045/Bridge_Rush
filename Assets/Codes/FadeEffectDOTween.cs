using UnityEngine;
using DG.Tweening; 
public class FadeEffectDOTween : MonoBehaviour
{
    public float fadeDuration = 0.2f; // Kitni dair ka fade effect chahiye
    public MeshRenderer meshRenderer;
    private Material material;

    void Start()
    {
        material = meshRenderer.material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            material.DOFade(1f, fadeDuration);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            material.DOFade(0f, fadeDuration);
        }
    }


    //public void FadeIn()
    //{
    //    material.DOFade(1f, fadeDuration); // Alpha ko 1 tak increase karega
    //}

    //public void FadeOut()
    //{
    //    material.DOFade(0f, fadeDuration); // Alpha ko 0 tak reduce karega
    //}
}
