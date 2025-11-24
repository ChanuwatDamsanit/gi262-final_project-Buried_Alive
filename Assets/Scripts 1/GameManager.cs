using Solution;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject gameOverScene;
    public OOPPlayer player;
    public int playerHp;

    public AudioSource audioSource;
    public AudioClip BgMusic;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(BgMusic);

        gameOverScene.SetActive(false);
        playerHp = player.energy;
    }

    // Update is called once per frame
    void Update()
    {
        playerHp = player.energy;
        if (playerHp <= 0)
        {
            gameOverScene.SetActive(true);

        }
    }
}
