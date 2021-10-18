using UnityEngine;

/**
 * This class is used to handle the functionality of the game.
 * 
 * Noah Young
 */

public class GameHandler : MonoBehaviour {

    public static int currPlayerColor = -1; // 0 - Red, 1 - Green, 2 - Blue (Starts at -1 for no color)
    int spawnCount, maxLines;
    public GameObject linePrefab, changerPrefab, lineHolder, lineSpawn, coinPrefab;
    [HideInInspector]
    public static bool gameStarted, gameEnded;
    public static float gameSpeed = 1.0f;
    float t, spawnTime, coinT, coinSpawnTime;
    bool objectsStarted;
    public ParticleSystem backgroundLines;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Start() {
        spawnTime = Random.Range (0.75f / gameSpeed, 1.25f / gameSpeed);
        maxLines = Random.Range (5, 11);
        for (int i = 0; i < 12; i++) {
            GameObject g = Instantiate (linePrefab, lineHolder.transform.position, linePrefab.transform.rotation) as GameObject;
            g.SetActive (false);
        }

        for (int i = 0; i < 5; i++) {
            GameObject g = Instantiate (changerPrefab, lineHolder.transform.position, linePrefab.transform.rotation) as GameObject;
            g.SetActive (false);
        }

        for (int i = 0; i < 10; i++) {
            GameObject g = Instantiate (coinPrefab, lineHolder.transform.position, coinPrefab.transform.rotation) as GameObject;
            g.SetActive (false);
        }
    }

    void Update () {
        //Debug.Log (gameSpeed);
        t += Time.deltaTime;
        coinT += Time.deltaTime;
        /* 
         * Used to test the color switching of the player
            Debug.Log(currPlayerColor);
            if (Input.GetMouseButtonDown (0)) {
                if (currPlayerColor == 0 || currPlayerColor == 1)
                    currPlayerColor++;
                else
                    currPlayerColor = 0;
            }
        */

        if (gameStarted) {
            moveObjs();
            if (gameSpeed < 2f)
                gameSpeed += 0.00015f;
            if (objectsStarted == false)
                startObjs ();
        }
    }

    void startObjs () {
        backgroundLines.Play ();
        objectsStarted = true;
    }

    void moveObjs () {
        foreach (GameObject g in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
            if ((g.tag == "moveObj" || g.tag == "changeRed" 
                 || g.tag == "changeGreen" || g.tag == "changeBlue" || g.tag == "coin") && g.activeInHierarchy) {
                g.transform.Translate(Vector2.down * Time.deltaTime * gameSpeed * 6);

                if (g.transform.position.y < -10f) {
                    g.transform.position = lineHolder.transform.position;
                    g.SetActive (false);
                    if (g.tag == "changeRed" || g.tag == "changeGreen" || g.tag == "changeBlue") {
                        g.tag = "colChanger";
                    }
                }
            } else if (g.tag == "moveObj" && g.activeInHierarchy == false && spawnCount < maxLines) {
                if (t > spawnTime) {
                    g.transform.position = lineSpawn.transform.position;
                    g.SetActive (true);
                    t = 0;
                    spawnCount++;
                    spawnTime = Random.Range (0.85f / gameSpeed, 1.15f / gameSpeed);
                }
            } else if (g.tag == "colChanger" && g.activeInHierarchy == false && spawnCount >= maxLines) {
                if (t > spawnTime) {
                    g.transform.position = lineSpawn.transform.position;
                    g.SetActive (true);
                    t = 0;
                    spawnCount = 0;
                    maxLines = Random.Range (5, 11);
                    spawnTime = Random.Range (1.25f / gameSpeed, 1.75f / gameSpeed);
                }
            }
        }
    }

    public static void gameEnd () {
        gameStarted = false;
        gameEnded = true;
        if (Data.currScore > PlayerPrefs.GetInt ("HighScore", 0)) {
            PlayerPrefs.SetInt ("HighScore", Data.currScore);
        }
        Data.currScore = 0;
    }
}
