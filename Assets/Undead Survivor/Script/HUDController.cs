using UnityEngine;

public class HUDController : MonoBehaviour
{
    public bool isGamePause;
    public bool isCleanerOn;
    public bool isPlayerDead;
    public WaitForSeconds wait;

    // Update is called once per frame
    void Update()
    {
        isGamePause = !GameManager.instance.isLive;
        isCleanerOn = GameManager.instance.enemyCleaner.activeSelf;
        isPlayerDead = GameManager.instance.health <= 0;
        if (isGamePause)
        {
            if (isCleanerOn || isPlayerDead)
            {
                CloseHUD();
            }
        }
    }

    void CloseHUD()
    {
        wait = new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}