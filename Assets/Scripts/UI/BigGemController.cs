using UnityEngine;

public class BigGemController : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var chat = collision.GetComponent<PlayerChat>();

        if (chat != null)
        {
            audioManager.PlaySFX(audioManager.bigGem, gameObject);
            chat.DisplayCollectingBigGemMessage();
        }
    }
}
