using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] AudioClip clip;
    [SerializeField] GameObject blockVFX;
    [SerializeField] float vfxDestroyDelay = 1f;
    [SerializeField] Sprite[] damageSprites;
    [SerializeField] int pointValue = 100;
    [Range(-1f, 1f)] [SerializeField] static float xOffset = -0.01f;
    [Range(-1f, 1f)] [SerializeField] float yOffset;

    // Cached references
    Level level;

    // State variables
    [SerializeField] int timesHit; // Only serialized for debug purposes

    /*
    public Block(float x, float y, string tag)
    {
        gameObject.transform.position = new Vector3(x, y, 0f);
        gameObject.tag = tag;
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void Update()
    {
        if (tag == "Scrolling")
        {
            Scroll();
            yOffset = 0.002f * Mathf.Sin(10f * gameObject.transform.position.x);
            if (gameObject.transform.position.x <= -0.5)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Scroll()
    {
        gameObject.transform.position += new Vector3(xOffset, yOffset, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable" || tag == "Scrolling")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int hitPoints = damageSprites.Length + 1;
        if (timesHit >= hitPoints)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextDamageSprite();
        }
    }

    private void ShowNextDamageSprite()
    {
        int spriteIndex = timesHit - 1;
        if (damageSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = damageSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array of " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        TriggerSFX();
        TriggerVFX();
        level.BlockDestroyed();
        FindObjectOfType<GameSession>().AddToScore(this);
    }

    private void TriggerSFX()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        Destroy(gameObject);
    }

    private void TriggerVFX()
    {
        GameObject vfx = Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(vfx, vfxDestroyDelay);
    }

    public int GetPoints()
    {
        return pointValue;
    }
}
