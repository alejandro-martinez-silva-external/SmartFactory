using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    private float targetAlpha;
    private bool changeAlpha = false;
    private Color targetColor;
    private Color initialColor;
    private Material material;


    private void Start() {
        material = GetComponent<Renderer>().material;
        initialColor = material.GetColor("_Color");
        ChangeAlpha(0f);
    }

    private void Update() {
        if(changeAlpha) SetTransparency();
    }

    public void StartSettingTransparency(float alpha){
        Color color = material.GetColor("_Color");
        targetAlpha = alpha;
        targetColor = new Color(0, initialColor.g * targetAlpha, initialColor.b * targetAlpha, 1);
        changeAlpha = true;
    }

    private void ChangeAlpha(float alpha){
        Color color = material.GetColor("_Color");
        Color newColor = new Color(0, color.g * alpha, color.b * alpha, 1);

        material.SetColor("_Color", newColor);
        print(material.GetColor("_Color"));
    }

    public void SetTransparency()
    {
        Color color = material.GetColor("_Color");
        Color newColor = new Color(0, Mathf.MoveTowards(color.g, targetColor.g, Time.deltaTime),
        Mathf.MoveTowards(color.b, targetColor.b, 2f * Time.deltaTime));
        material.SetColor("_Color", newColor);

        if(newColor == targetColor){
            changeAlpha = false;
        }
    }
}
