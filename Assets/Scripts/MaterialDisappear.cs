using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDisappear : MonoBehaviour
{
    public float fadeDuration = 1f; 
    private Renderer rend;

    private Color initialColor;
    private float initialMetallic;
    private float initialSmoothness;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        initialColor = rend.material.color;
        initialMetallic = rend.material.GetFloat("_Metallic");
        initialSmoothness = rend.material.GetFloat("_Glossiness");
    }

    private void OnEnable()
    {
        ResetMaterialProperties();
        StartCoroutine(FadeOut());
    }

    void ResetMaterialProperties()
    {
        rend.material.color = initialColor;
        rend.material.SetFloat("_Metallic", initialMetallic);
        rend.material.SetFloat("_Glossiness", initialSmoothness);
    }

    IEnumerator FadeOut()
    {
        Color startColor = rend.material.color;
        float startValueMetallic = rend.material.GetFloat("_Metallic");
        float startValueSmoothness = rend.material.GetFloat("_Glossiness");

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float newValue = Mathf.Lerp(startValueMetallic, 0, elapsedTime / fadeDuration);
            rend.material.SetFloat("_Metallic", newValue);

            newValue = Mathf.Lerp(startValueSmoothness, 0, elapsedTime / fadeDuration);
            rend.material.SetFloat("_Glossiness", newValue);

            Color newColor = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0), elapsedTime / fadeDuration);
            rend.material.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rend.material.SetFloat("_Metallic", 0);
        rend.material.SetFloat("_Glossiness", 0);
        rend.material.color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }
}
