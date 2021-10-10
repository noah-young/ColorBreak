using System.Collections;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

    public ParticleSystem explosion, playerEx, coinEx;
    public GameObject shadow;
    public static bool scoreCol;
    public Component[] lineArray;

    void OnTriggerEnter2D(Collider2D other) {
        ParticleSystem.MainModule playerExMain = playerEx.main;
        ParticleSystem.MainModule exMain = explosion.main;
        ParticleSystem.MainModule coinMain = coinEx.main;

        if (GameHandler.currPlayerColor == 0) {
            if (other.gameObject.tag == "green" || other.gameObject.tag == "blue") {
                GameHandler.gameEnd ();
                CameraScript.lostShake = true;
                playerEx.transform.position = transform.position;
                playerExMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(255, 100, 100, 255));
                playerEx.Emit (75);
                Destroy (gameObject);
                Destroy (shadow);
            } else if (other.gameObject.tag == "red") {
                CameraScript.shake = true;
                Data.currScore += 1;
                scoreCol = true;
                exMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(255, 100, 100, 255));
                explosion.transform.position = other.transform.position;
                explosion.Emit (100);
                other.gameObject.SetActive (false);
                lineArray = other.gameObject.transform.parent.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer spriteRenderer in lineArray)
                {
                    StartCoroutine(colorChange(spriteRenderer, new Color32(60, 60, 60, 255), .7f));
                }
            }
        } else if (GameHandler.currPlayerColor == 1) {
            if (other.gameObject.tag == "red" || other.gameObject.tag == "blue") {
                GameHandler.gameEnd ();
                CameraScript.lostShake = true;
                playerEx.transform.position = transform.position;
                playerExMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(100, 240, 100, 255));
                playerEx.Emit (75);
                Destroy (gameObject);
                Destroy (shadow);
            } else if (other.gameObject.tag == "green") {
                CameraScript.shake = true;
                Data.currScore += 1;
                scoreCol = true;
                exMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(100, 240, 100, 255));
                explosion.transform.position = other.transform.position;
                explosion.Emit (100);
                other.gameObject.SetActive (false);
                lineArray = other.gameObject.transform.parent.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer spriteRenderer in lineArray)
                {
                    StartCoroutine(colorChange(spriteRenderer, new Color32(60, 60, 60, 255), .7f));
                }
            }
        } else if (GameHandler.currPlayerColor == 2) {
            if (other.gameObject.tag == "red" || other.gameObject.tag == "green") {
                GameHandler.gameEnd ();
                CameraScript.lostShake = true;
                playerEx.transform.position = transform.position;
                playerExMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(114, 174, 255, 255));
                playerEx.Emit (75);
                Destroy (gameObject);
                Destroy (shadow);
            } else if (other.gameObject.tag == "blue") {
                CameraScript.shake = true;
                Data.currScore += 1;
                scoreCol = true;
                exMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(114, 174, 255, 255));
                explosion.transform.position = other.transform.position;
                explosion.Emit (100);
                other.gameObject.SetActive (false);
                lineArray = other.gameObject.transform.parent.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer spriteRenderer in lineArray)
                {
                    StartCoroutine(colorChange(spriteRenderer, new Color32(60, 60, 60, 255), .7f));
                }
            }
        }

        if (other.gameObject.tag == "coin") {
            coinEx.transform.position = transform.position;
            coinMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(255, 227, 0, 255));
            coinEx.Emit (25);
            other.gameObject.SetActive (false);
        }

        if (other.gameObject.tag == "changeRed") {
            StartCoroutine (zoomEffect ());
            GameHandler.currPlayerColor = 0;
            playerEx.transform.position = transform.position;
            playerExMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(255, 100, 100, 255));
            playerEx.Emit (75);
        } else if (other.gameObject.tag == "changeGreen") {
            StartCoroutine (zoomEffect ());
            GameHandler.currPlayerColor = 1;
            playerEx.transform.position = transform.position;
            playerExMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(100, 240, 100, 255));
            playerEx.Emit (75);
        } else if (other.gameObject.tag == "changeBlue") {
            StartCoroutine (zoomEffect ());
            GameHandler.currPlayerColor = 2;
            playerEx.transform.position = transform.position;
            playerExMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(114, 174, 255, 255));
            playerEx.Emit (75);
        }
    }

    IEnumerator zoomEffect ()
    {
        Player.boostSpeed = 6.0f;
        CameraScript.normal = false;
        for (float i = 6.0f; i > 0f; i -= 0.5f)
        {
            Player.boostSpeed = i;
            yield return new WaitForSeconds (0.02f);
        }
        CameraScript.normal = true;
        Player.boostSpeed = 0f;
        for (float i = 0f; i < 2.0f; i += 0.5f)
        {
            Player.boostSpeed = i;
            yield return new WaitForSeconds (0.03f);
        }
    }

    IEnumerator slowMotion ()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds (0.5f);
        Time.timeScale = 1.0f;
    }

    IEnumerator colorChange(SpriteRenderer spriteRenderer, Color32 col, float t)
    {
        float elapsedTime = 0;

        while (elapsedTime < t)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, col, elapsedTime / t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
