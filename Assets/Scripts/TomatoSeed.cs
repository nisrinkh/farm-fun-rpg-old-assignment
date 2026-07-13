using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSeed : MonoBehaviour
{
    public int amount = 1;
    public GameObject plantPrefab;
    public bool isDropped = false;

    private Player playerRef;

    public void StartGrowing(Player player)
    {
        if (!isDropped)
        {
            isDropped = true;
            playerRef = player;
            StartCoroutine(TransformIntoPlant());
        }
    }

    IEnumerator TransformIntoPlant()
    {
        Debug.Log("Transform coroutine started");

        yield return new WaitForSeconds(10f);

        if (plantPrefab == null)
        {
            Debug.LogError("plantPrefab is NULL! Cannot spawn plant.");
            Destroy(gameObject);
            yield break;
        }

        //Spawn hanya satu kali, dan simpan referensinya
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0);
        GameObject plant = Instantiate(plantPrefab, spawnPos, Quaternion.identity);

        Player player = FindObjectOfType<Player>();
        if (playerRef != null)
        {
            playerRef.AddScore(10); // +10 poin
        }
        else
        {
            Debug.LogWarning("Player not found! Cannot add score.");
        }

        Debug.Log("Destroying seed...");
        Destroy(gameObject);
    }
}