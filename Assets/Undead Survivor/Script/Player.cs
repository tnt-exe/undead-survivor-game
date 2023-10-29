using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Vector2 inputVec;
    public Scanner scanner;
    public float speed;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    public Menu uiRevive;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    public void Dead()
    {
        for (int index = 2; index < transform.childCount; index++)
        {
            transform.GetChild(index).gameObject.SetActive(false);
        }

        anim.SetTrigger("Dead");
        GameManager.instance.GameOver();
    }

    public void Revive()
    {
        GameManager.instance.health = GameManager.instance.maxHealth;
        GameManager.instance.reviveTime--;
        GameManager.instance.Resume();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        int enemyDmg = collision.transform.GetComponent<Enemy>().damage;

        GameManager.instance.health -= Time.deltaTime * enemyDmg;

        if (GameManager.instance.health <= 0)
        {
            if (Menu.gameMode == 1 && GameManager.instance.reviveTime > 0)
            {
                GameManager.instance.Pause();
                uiRevive.Show();
                return;
            }

            Dead();
        }
    }
}