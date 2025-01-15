using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Image heartPrefab;

    private readonly List<Image> heartObjects = new();

    private void Start()
    {
        PlayerController.OnPlayerRespawn += UpdateHearts;
    }

    public void UpdateHearts(int lives)
    {
        foreach (var heart in heartObjects)
        {
            Destroy(heart.gameObject);
        }

        heartObjects.Clear();

        for (int i = 0; i < lives; i++)
        {
            var heart = Instantiate(heartPrefab, transform);
            heartObjects.Add(heart);
        }
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerRespawn -= UpdateHearts;
    }
}
