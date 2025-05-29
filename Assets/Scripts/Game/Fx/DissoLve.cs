using QFramework;
using UnityEngine;

public class DissoLve : MonoBehaviour
{
    public Material Material;
    private static readonly int Color = Shader.PropertyToID("_Color");
    private static readonly int Fade = Shader.PropertyToID("_Fade");
    public Color DissolveColor;
    private void Start()
    {
        var material = Instantiate(Material);
        GetComponent<SpriteRenderer>().material = material;
        material.SetColor(Color, DissolveColor);
        ActionKit.Lerp(1f, 0f, 0.5f, (fade) =>
        {
            material.SetFloat(Fade, fade);
            this.LocalScale(0.1f + (1f - fade) * 0.1f);
        }).Start(this, () =>
        {
            Destroy(material);
            this.DestroyGameObjGracefully();
        });
    }
}