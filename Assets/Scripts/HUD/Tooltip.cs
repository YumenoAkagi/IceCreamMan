using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip tooltip;

    [SerializeField]
    private Rigidbody2D body;

    private Text TooltipText;

    private void Awake()
    {
        TooltipText = transform.Find("text").GetComponent<Text>();
        HideTooltip();
    }

    private void Update()
    {
        Collider2D colider = body.GetComponent<Collider2D>();
        Vector2 playerPos = new Vector2(body.position.x, (transform.position.y) / 2);
        transform.position = playerPos;
    }

    private void ShowTooltip(string text)
    {
        gameObject.SetActive(true);
        TooltipText.text = text;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void Static_ShowTooltip(string text)
    {
        tooltip.ShowTooltip(text);
    }

    public static void Static_HideTooltip()
    {
        tooltip.HideTooltip();
    }
}
