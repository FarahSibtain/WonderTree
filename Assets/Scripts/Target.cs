using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Target : MonoBehaviour
{
    [SerializeField]
    GameManager1 gameManager;    

    //public SpriteRenderer spriteRenderer { get; private set; }
    //public Sprite[] states = new Sprite[0];
    //public int health { get; private set; }
    public int points = 100;
    //public bool unbreakable;

    //private void Awake()
    //{
    //    this.spriteRenderer = GetComponent<SpriteRenderer>();
    //}

    private void Start()
    {
        ResetTarget();
    }

    public void ResetTarget()
    {
        gameObject.SetActive(false);
        Invoke(nameof(SetRandomPosition), 1f);
    }

    private void SetRandomPosition()
    {
        gameObject.SetActive(true);

        float xPos = gameManager.GetRandomXPosition();
        float yPos = gameManager.GetRandomYPosition();

        transform.position = new Vector2(xPos, yPos);
    }

    private void Hit()
    {
        ResetTarget();
        gameManager.Hit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Hit();
        }
    }
}
