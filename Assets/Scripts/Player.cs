using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRb;
    public float bounceForce = 6;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioManager.Play("bounce");
        playerRb.velocity = new Vector3(playerRb.velocity.x, bounceForce, playerRb.velocity.z);
        Debug.Log(collision.transform.GetComponent<MeshRenderer>().material.name);
        string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;

        if (materialName == "Safe (Instance)")
        {
            //ball hits safe area
        } else if(materialName == "Unsafe (Instance)")
        {
            //ball hits unsafe area
            Debug.Log("Game Over");
            GameManager.gameOver = true;
            audioManager.Play("game over");

        } else if(materialName == "Last Ring (Instance)" && !GameManager.levelCompleted)
        {
            //ball hits last ring
            Debug.Log("Level Completed");
            GameManager.levelCompleted = true;
            audioManager.Play("win level");

        }
    }
}
