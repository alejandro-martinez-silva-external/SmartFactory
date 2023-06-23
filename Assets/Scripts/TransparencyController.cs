using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    public float targetAlpha;
    private bool changeAlpha;
    private Color targetColor;
    private Material material;


    private void Start() {
        material = GetComponent<Renderer>().material;
        ChangeAlpha(0f);
        StartSettingTransparency();
    }

    private void Update() {
        if(changeAlpha) SetTransparency();
    }

    private void StartSettingTransparency(){

        Color color = material.GetColor("_Color");
        targetColor = new Color(0, color.g * targetAlpha, color.b * targetAlpha, 1);
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
        Color newColor = new Color(0, Mathf.MoveTowards(color.g, targetColor.g, 0.2f * Time.deltaTime),
         Mathf.MoveTowards(color.b, targetColor.b, 2f * Time.deltaTime));
        material.SetColor("_Color", newColor);
        if(newColor == targetColor){
            changeAlpha = false;
            print("code 244");
        }
    }
}
