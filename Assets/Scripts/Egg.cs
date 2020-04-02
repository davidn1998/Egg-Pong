using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    //Configuration Params
    [SerializeField] float force = 500f;
    [SerializeField] float ballSpeed = 15f;
    [SerializeField] float minYVelocity = 15f;
    [SerializeField] Sprite goodEgg = null;
    [SerializeField] Sprite brokenEgg = null;
    [SerializeField] PhysicsMaterial2D bounceMaterial = null;

    [SerializeField] AudioClip bounceSound = null;
    [SerializeField] AudioClip scoreSound = null;

    //Cache Variables
    Rigidbody2D rb;
    SpriteRenderer sr;
    AudioSource audioSource;

    bool cracked = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        pushEgg();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!cracked)
        {
            if (collision.gameObject.CompareTag("Paddle"))
            {
                Vector2 newDir = (this.transform.position - collision.gameObject.transform.position).normalized;
                rb.velocity = newDir * ballSpeed;
                audioSource.PlayOneShot(bounceSound);
            }


            if ((rb.velocity.y < minYVelocity) && (Mathf.Sign(rb.velocity.y) == 1))
            {
                rb.AddForce(new Vector2(0, 1f));
            }
            else if ((rb.velocity.y < minYVelocity) && (Mathf.Sign(rb.velocity.y) == -1))
            {
                rb.AddForce(new Vector2(0, -1f));
            }


            rb.velocity = ballSpeed * (rb.velocity.normalized);

            if (collision.gameObject.CompareTag("Player1Wall"))
            {
                BreakEgg();
                GameManager.Instance.IncreaseP2Score();
            }

            if (collision.gameObject.CompareTag("Player2Wall"))
            {
                BreakEgg();
                GameManager.Instance.IncreaseP1Score();
            }
        }


    }

    void pushEgg()
    {
        float direction = Random.Range(-1f, 1f);

        if (direction > 0)
        {
            //Push to the right
            rb.AddForce(new Vector2(force, 0));
        }
        else
        {
            //Push to the left
            rb.AddForce(new Vector2(-force, 0));
        }
    }

    void BreakEgg()
    {
        cracked = true;
        audioSource.PlayOneShot(scoreSound);
        sr.sprite = brokenEgg;
        rb.velocity = new Vector2(0, 0);
        this.GetComponent<BoxCollider2D>().sharedMaterial = null;

        StartCoroutine(ResetEgg());
    }

    IEnumerator ResetEgg()
    {
        yield return new WaitForSeconds(3f);

        transform.position = new Vector2(0, 4f);
        sr.sprite = goodEgg;
        pushEgg();
        this.GetComponent<BoxCollider2D>().sharedMaterial = bounceMaterial;
        cracked = false;
    }
}
