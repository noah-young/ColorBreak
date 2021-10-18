using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * This class handles the actions of the UI
 * 
 * Noah Young
 */

public class UIHandler : MonoBehaviour {
    
    public Text scoreText, line;
    public Image sceneReset, sceneReset2, sceneReset3, trophy, coins;
    public Button startButton, resetButton;
    bool cover, resetMoved, scoreResetted;

    void Start() {
        scoreText.text = PlayerPrefs.GetInt ("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update () {
        Debug.Log (GameHandler.gameStarted);
        if (GameHandler.currPlayerColor == 0) {
            sceneReset.GetComponent<Image>().color = new Color32(220, 85, 85, 255);
        } else if (GameHandler.currPlayerColor == 1) {
            sceneReset.GetComponent<Image>().color = new Color32(85, 215, 85, 255);
        } else if (GameHandler.currPlayerColor == 2) {
            sceneReset.GetComponent<Image>().color = new Color32(100, 154, 227, 255);
        }

        if (CollisionHandler.scoreCol)
            StartCoroutine(scoreTextAnim ());
            

        sceneResetter ();
        if (GameHandler.gameStarted && scoreResetted == false) {
            StartCoroutine(scoreReset ());
            StartCoroutine(UIAnim ());
            //StartCoroutine(rightSide ());
        } else if (GameHandler.gameStarted && scoreResetted) {
            scoreText.text = Data.currScore.ToString();
        }
        if (GameHandler.gameEnded)
            StartCoroutine(ResetButtonAnim ());
    }

    void sceneResetter () {
        if (cover) {
            sceneReset.GetComponent <RectTransform> ().localPosition =
                           Vector2.Lerp (sceneReset.GetComponent <RectTransform> ().localPosition, Vector2.zero, Time.deltaTime * 10.0f);
        } else if (cover == false && resetMoved == false) {
            sceneReset.GetComponent <RectTransform> ().localPosition =
                           Vector2.Lerp (sceneReset.GetComponent <RectTransform> ().
                                         localPosition, new Vector2 (-1000f, 0f), Time.deltaTime * 10.0f);
            if (sceneReset.GetComponent <RectTransform> ().localPosition.x < -950f) {
                sceneReset.GetComponent <RectTransform> ().localPosition = new Vector2 (1000f, 0f); 
                resetMoved = true;
            }
        }
    }

    public void clickRestart () {
        StartCoroutine (WaitBeforeRestart ());
    }

    public void clickStart () {
        Debug.Log ("working");
        startButton.interactable = false;
        GameHandler.gameStarted = true;
    }

    IEnumerator scoreReset () {
        scoreResetted = true;
        for (int i = PlayerPrefs.GetInt ("HighScore"); i > 0; i--) {
            if (i > 10)
                i -= Mathf.RoundToInt(i/5.0f);
            
            scoreText.text = i.ToString();
            yield return new WaitForSeconds (0.01f);
        }
        scoreText.text = Data.currScore.ToString();
    }

    IEnumerator WaitBeforeRestart () {
        cover = true;
        yield return new WaitForSeconds (0.3f);
        GameHandler.gameSpeed = 1.0f;
        GameHandler.gameStarted = false;
        GameHandler.gameEnded = false;
        SceneManager.LoadScene ("Main");
    }

    IEnumerator scoreTextAnim () {
        CollisionHandler.scoreCol = false;
        scoreText.GetComponent<RectTransform>().localScale = new Vector2 (5.5f, 5.5f);
        yield return new WaitForSeconds (0.3f);
        for (float i = 5.5f; i > 5.0f; i -= 0.05f) {
            scoreText.GetComponent<RectTransform>().localScale = new Vector2 (i, i);
            yield return new WaitForSeconds(0.01f);
        }
        scoreText.GetComponent<RectTransform>().localScale = new Vector2 (5f, 5f);
    }

    IEnumerator UIAnim () {
        for (float i = 1f; i > 0; i -= 0.1f) {
            foreach (GameObject g in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
                if (g.tag == "UI")
                    g.GetComponent <RectTransform> ().localScale = new Vector2 (i, i);
            }
            yield return new WaitForSeconds (0.01f);
        }
        foreach (GameObject g in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
            if (g.tag == "UI")
                g.gameObject.SetActive (false);
        }
    }

    IEnumerator ResetButtonAnim () {
        for (float i = 0f; i < 1f; i += 0.1667f) {
            resetButton.GetComponent <RectTransform> ().localScale = new Vector2 (i, i);
            yield return new WaitForSeconds (0.01f);
        }
        resetButton.interactable = true;
    }
}
