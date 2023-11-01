using UnityEngine;

public class BombTimer : MonoBehaviour
{
    RectTransform rect;
    public GameObject bomb;

    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (!bomb.activeSelf)
        {
            rect.localScale = Vector3.zero; // Keep scale at zero if bomb is not active
            return;
        }

        rect.localScale = Vector3.one; // Set scale to one if bomb is active
        rect.position = Camera.main.WorldToScreenPoint(bomb.transform.position);
    }
}