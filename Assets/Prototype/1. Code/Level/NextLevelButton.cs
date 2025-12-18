//using UnityEditor.Build.Content;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color interactableColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.color = baseColor;
        LevelManager.Instance = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) spriteRenderer.color = interactableColor;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) spriteRenderer.color = baseColor;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && Input.GetButton("Interact") && LevelManager.Instance.levelIsOver)
        {
            LevelManager.Instance.NextLevel();
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }
}
