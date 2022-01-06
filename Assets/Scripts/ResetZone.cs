using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ResetZone : MonoBehaviour
{
    [SerializeField]
    GameManager1 gameManager = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.Miss();
    }

}
