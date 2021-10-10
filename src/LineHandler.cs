using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is used to handle the movement and behaviors of the individual color
 * bricks in each line set. Some behaviors are randomized, such as horizontal speed,
 * colors, and size. This class is attached to each set of lines that are inititalized
 * by the Game Handler script.
 * 
 * Noah Young
 */

public class LineHandler : MonoBehaviour {

    public GameObject[] lines;
    bool moveRight;
    bool resetted = true;
    float lineSpeed;

	void Start () {
        moveRight = (Random.value < 0.5f);
        lineSpeed = Random.Range(0.012f, 0.018f);
	}
	
	void Update () {
        handleLines ();
        findLastLine ();
        resetLine ();
	}

    GameObject findLastLine () {
        GameObject last = lines [0];
        foreach (GameObject g in lines) {
            if (moveRight) {
                if (g.transform.localPosition.x < last.transform.localPosition.x) {
                    last = g;
                }
            } else {
                if (g.transform.localPosition.x > last.transform.localPosition.x) {
                    last = g;
                }
            }
        }
        return last;
    }

    void handleLines () {
        foreach (GameObject g in lines) {
            if (moveRight) {
                if (g.transform.localPosition.x > 6f) {
                    g.transform.localPosition =
                         new Vector2 (findLastLine().transform.localPosition.x - 1.95f, findLastLine().transform.localPosition.y);
                }
                g.transform.Translate(Vector2.right * lineSpeed * GameHandler.gameSpeed);
            }
            else {
                if (g.transform.localPosition.x < -6f) {
                    g.transform.localPosition =
                         new Vector2(findLastLine().transform.localPosition.x + 1.95f, findLastLine().transform.localPosition.y);
                }
                g.transform.Translate(Vector2.left * lineSpeed * GameHandler.gameSpeed);
            }
        }
    } 

    void resetLine () {
        if (transform.position.y > 9f && resetted == false) {
            foreach (GameObject g in lines) {
                g.SetActive(true);
                if (g.tag == "red") {
                    g.GetComponent<SpriteRenderer>().color = new Color32(255, 100, 100, 255);
                    g.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 100, 100, 255);
                }
                else if (g.tag == "green") {
                    g.GetComponent<SpriteRenderer>().color = new Color32(100, 240, 100, 255);
                    g.GetComponentInChildren<SpriteRenderer>().color = new Color32(100, 240, 100, 255);
                }
                else if (g.tag == "blue") {
                    g.GetComponent<SpriteRenderer>().color = new Color32(114, 174, 255, 255);
                    g.GetComponentInChildren<SpriteRenderer>().color = new Color32(114, 174, 255, 255);
                }
            }
            moveRight = (Random.value < 0.5f);
            lineSpeed = Random.Range(0.012f, 0.018f);
        } else if (transform.position.y < 9f && resetted == true) {
            resetted = false;
        }
    }
}
