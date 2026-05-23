using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;  // Prefab olarak kaydettiğiniz GameObject'i buraya atayın.
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    public float health;

    public void Defeated()
    {
        animator.SetTrigger("DefeatedTrigger");
    }

    public void RemoveEnemy()
    {
        StartCoroutine(RemoveEnemyCoroutine());
    }

    private IEnumerator RemoveEnemyCoroutine()
    {
        // Ölme animasyonunu tetikle
        animator.SetTrigger("DefeatedTrigger");

        // 2 saniye bekleyin, bu sırada animasyon oynayacak
        yield return new WaitForSeconds(0.05f);

        // Respawn fonksiyonunu çağır (önce respawn yaparsan, ardından yok edebilirsin)
        RespawnEnemy();

        // GameObject'i yok edin
        Destroy(gameObject);
    }

    void RespawnEnemy()
    {
        // X ve Y pozisyonlarını -0.5 ile 0.5 arasında rastgele al
        float randomX = Random.Range(-0.5f, 0.5f);
        float randomY = Random.Range(-0.5f, 0.5f);

        // Prefab'i rastgele pozisyonda yeniden yarat
        Vector2 randomPosition = new Vector2(randomX, randomY);
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

}
