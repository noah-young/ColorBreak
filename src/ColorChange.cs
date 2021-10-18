using System.Collections;
using UnityEngine;

public class ColorChange : MonoBehaviour {

    int randNewColor;
    public GameObject[] arrows = new GameObject[3];
    public GameObject[] arrowShadows = new GameObject[3];
    bool playAgain = true;

    // Use this for initialization
    void Start () {
        resetColor ();
    }
    
    // Update is called once per frame
    void Update () {
        //Debug.Log (randNewColor);
        if (transform.position.y > 9) {
            resetColor ();
            playAgain = true;
        } else if (playAgain) {
            StartCoroutine (colorAnim ());
            playAgain = false;
        }
    }

    void resetColor () {
        randNewColor = GameHandler.currPlayerColor;
        while (randNewColor == GameHandler.currPlayerColor) {
            randNewColor = Random.Range (0, 3);
        }

        if (randNewColor == 0)
            gameObject.tag = "changeRed";
        else if (randNewColor == 1)
            gameObject.tag = "changeGreen";
        else if (randNewColor == 2)
            gameObject.tag = "changeBlue";
        
    }

    /*void resetLine () {
        if (transform.position.y > 9f && resetted == false) {
            foreach (GameObject g in lines) {
                g.SetActive (true);
            }
            moveRight = (Random.value < 0.5f);
            lineSpeed = Random.Range(0.006f, 0.01f);
        } else if (transform.position.y < 9f && resetted == true) {
            resetted = false;
        }
    }*/

    IEnumerator colorAnim () {
        if (randNewColor == 0) {
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (255, 150, 150, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (255, 150, 150, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (255, 100, 100, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (200, 130, 130, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (200, 130, 130, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (200, 90, 90, 255);
            yield return new WaitForSeconds (0.5f);
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (255, 100, 100, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (255, 150, 150, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (255, 150, 150, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (200, 90, 90, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (200, 130, 130, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (200, 130, 130, 255);
            yield return new WaitForSeconds (0.5f);
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (255, 150, 150, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (255, 100, 100, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (255, 150, 150, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (200, 130, 130, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (200, 90, 90, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (200, 130, 130, 255);
            yield return new WaitForSeconds (0.5f);
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (255, 150, 150, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (255, 150, 150, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (255, 100, 100, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (200, 130, 130, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (200, 130, 130, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (200, 90, 90, 255);
        } else if (randNewColor == 1) {
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (180, 240, 180, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (180, 240, 180, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (100, 240, 100, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (130, 200, 130, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (130, 200, 130, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (90, 200, 90, 255);
            yield return new WaitForSeconds (0.5f);
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (100, 240, 100, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (180, 240, 180, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (180, 240, 180, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (90, 200, 90, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (130, 200, 130, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (130, 200, 130, 255);
            yield return new WaitForSeconds (0.5f);
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (180, 240, 180, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (100, 240, 100, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (180, 240, 180, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (130, 200, 130, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (90, 200, 90, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (130, 200, 130, 255);
            yield return new WaitForSeconds (0.5f);
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (180, 240, 180, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (180, 240, 180, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (100, 240, 100, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (130, 200, 130, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (130, 200, 130, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (90, 200, 90, 255);
        } else if (randNewColor == 2) {
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (170, 206, 255, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (170, 206, 255, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (114, 174, 255, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (130, 171, 227, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (130, 171, 227, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (68, 136, 166, 255);
            yield return new WaitForSeconds (0.5f);
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (114, 174, 255, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (170, 206, 255, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (170, 206, 255, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (68, 136, 166, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (130, 171, 227, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (130, 171, 227, 255);
            yield return new WaitForSeconds (0.5f);
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (170, 206, 255, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (114, 174, 255, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (170, 206, 255, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (130, 171, 227, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (68, 136, 166, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (130, 171, 227, 255);
            yield return new WaitForSeconds (0.5f);
            arrows[0].GetComponent <SpriteRenderer> ().color = new Color32 (170, 206, 255, 255);
            arrows[1].GetComponent <SpriteRenderer> ().color = new Color32 (170, 206, 255, 255);
            arrows[2].GetComponent <SpriteRenderer> ().color = new Color32 (114, 174, 255, 255);

            arrowShadows[0].GetComponent <SpriteRenderer> ().color = new Color32 (130, 171, 227, 255);
            arrowShadows[1].GetComponent <SpriteRenderer> ().color = new Color32 (130, 171, 227, 255);
            arrowShadows[2].GetComponent <SpriteRenderer> ().color = new Color32 (68, 136, 166, 255);
        }
        playAgain = true;
    }
}
