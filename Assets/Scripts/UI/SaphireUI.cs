using UnityEngine;

public class SaphireUI : MonoBehaviour
{
    public GameObject saphirePrefab;

    void Start()
    {
        BossHealth.OnBossDefeated += CreateSaphire;
    }

    void OnDestroy()
    {
        BossHealth.OnBossDefeated -= CreateSaphire;
    }

    public void CreateSaphire(Vector3 position)
    {
        Instantiate(saphirePrefab, position, Quaternion.identity);
    }
}
