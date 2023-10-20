using UnityEngine;

public class FollowHealthBar : MonoBehaviour
{
    RectTransform rect;

    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
