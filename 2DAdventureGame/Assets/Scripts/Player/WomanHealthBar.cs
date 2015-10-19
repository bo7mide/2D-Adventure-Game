using UnityEngine;
using System.Collections;

public class WomanHealthBar : MonoBehaviour
{

    public WomanScript woman;
    public Transform ForegroundSprite;
    public SpriteRenderer ForegroundRenderer;
    public Color MaxHealthColor = new Color(255 / 255f, 63 / 255f, 63 / 255f),
        MinHealthColor = new Color(64 / 255f, 137 / 255f, 255 / 255f);

    public void Update()
    {
        var HealthPercent = woman.Health / (float)woman.Life;
        ForegroundSprite.localScale = new Vector3(HealthPercent, 1, 1);
        ForegroundRenderer.color = Color.Lerp(MaxHealthColor, MinHealthColor, HealthPercent);
    }
}
