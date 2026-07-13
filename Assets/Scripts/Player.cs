using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int numTomatoSeed = 0;
    public int score = 0;
    public GameObject tomatoSeedPrefab;

    public AudioClip doneSound;

    [Header("UI")]
    public GameObject doneText;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (doneText != null)
            doneText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DropTomatoSeed();
    }

    void DropTomatoSeed()
    {
        // Jika tekan Space dan player punya seed
        if (Input.GetKeyDown(KeyCode.Space) && numTomatoSeed > 0)
        {
            // Buat posisi sedikit di depan player
            Vector3 dropPos = transform.position + new Vector3(0.3f, 2.5f, 0);

            GameObject seed = Instantiate(tomatoSeedPrefab, dropPos, Quaternion.identity);

            TomatoSeed seedScript = seed.GetComponent<TomatoSeed>();
            if (seedScript != null)
            {
                StartCoroutine(StartGrowingAfterDelay(seedScript, this));
            }

            Rigidbody2D rb = seed.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0.3f;   // bikin jatuh pelan
            }

            numTomatoSeed--;
            Debug.Log("Tomato seed dropped at: " + dropPos);
        }
    }

    IEnumerator StartGrowingAfterDelay(TomatoSeed seedScript, Player player)
    {
        yield return new WaitForSeconds(0.5f); // tunggu seed jatuh sedikit
        seedScript.StartGrowing(player);
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (score >= 110 && doneText != null)
        {
            doneText.SetActive(true);
            if (doneSound != null)
                AudioSource.PlayClipAtPoint(doneSound, transform.position);
            Time.timeScale = 0f; // opsional: freeze game
        }
    }
}
