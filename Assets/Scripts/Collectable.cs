using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioClip pickupSound;
    public float volume = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TomatoSeed seed = GetComponent<TomatoSeed>();
        Player player = collision.GetComponent<Player>();

        if (player != null && seed != null && !seed.isDropped)
        {
            player.numTomatoSeed++;
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
            }
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
