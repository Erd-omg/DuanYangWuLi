using UnityEngine;

public class PotTarget : MonoBehaviour
{
    public GameManagerArrow gameManager;
    public ArrowFactory arrowFactory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            gameManager.AddScore(gameObject.tag);
        }
    }
}