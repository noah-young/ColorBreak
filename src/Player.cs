using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The purpose of this class is to handle the movement and animation of the player.
 * It also detects collisions and communicates to other scripts to determine whether
 * the game has ended or to add to the score. 
 * 
 * Noah Young
 */

public class Player : MonoBehaviour {
    
	public GameObject player, shadow;
	public ParticleSystem smoke, explosion;
	bool moveRight = true;
    bool shadowRaised;
    public bool controls = true;
    float speedModifier;
    public static float boostSpeed;
    Vector2 mousePos;

	void Update () {
		//Debug.Log (Mathf.Abs(player.transform.position.y));
		if (GameHandler.gameStarted) {
			if (shadowRaised == false) {
                raiseShadow();
			}
            if (controls)
			    clickHandler();
            else {
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                player.transform.position = Vector2.Lerp(player.transform.position, 
                                                         new Vector2 (mousePos.x, player.transform.position.y), Time.deltaTime * 4);
                shadow.transform.position =
                          Vector2.Lerp(shadow.transform.position, 
                                       new Vector2(player.transform.position.x, shadow.transform.position.y), Time.deltaTime * 40);
                shadow.transform.position = new Vector2 (shadow.transform.position.x, player.transform.position.y - 0.06f);
            }

            changeColor();
            if (player.transform.localPosition.y < -3f) {
                player.transform.localPosition = 
                    new Vector2(player.transform.localPosition.x,
                                player.transform.localPosition.y + (Mathf.Abs(player.transform.localPosition.y / 15) - .2f));
            }
            //player.transform.localPosition = Vector2.Lerp(player.transform.localPosition, new Vector2(0f, -3f), Time.deltaTime * 5);
			player.transform.Translate (Vector2.right * Time.deltaTime * speedModifier);
			player.transform.localPosition = 
                new Vector3 (player.transform.localPosition.x, player.transform.localPosition.y);
            
            if (CameraScript.normal) 
                player.transform.localPosition = 
                    Vector2.Lerp (player.transform.localPosition, 
                                  new Vector2 (player.transform.localPosition.x, -3f), Time.deltaTime * boostSpeed);
            else
                player.transform.localPosition = 
                    Vector2.Lerp (player.transform.localPosition, 
                                  new Vector2 (player.transform.localPosition.x, -1.5f), Time.deltaTime * boostSpeed);
		}
	}

	void clickHandler () {
		if (Input.GetMouseButton (0) && moveRight) {
			if (player.transform.rotation.y < 0.2f) {
				player.transform.Rotate (Vector2.up * Time.deltaTime * 45);
				shadow.transform.localPosition = 
                    Vector2.Lerp (shadow.transform.localPosition,
                                  new Vector2 (-0.03f, shadow.transform.localPosition.y), Time.deltaTime * 5);
			}
            if (speedModifier < 3.5f * GameHandler.gameSpeed) {
				speedModifier += 0.35f * GameHandler.gameSpeed;
			}
			smoke.Emit (1);
		} else if (Input.GetMouseButton (0) && moveRight == false) {
			if (player.transform.rotation.y > -0.2f) {
				player.transform.Rotate (Vector2.down * Time.deltaTime * 45);
				shadow.transform.localPosition =
                          Vector2.Lerp (shadow.transform.localPosition, 
                                        new Vector2 (0.03f, shadow.transform.localPosition.y), Time.deltaTime * 5);
			}
            if (speedModifier > -3.5f * GameHandler.gameSpeed) {
                speedModifier -= 0.35f * GameHandler.gameSpeed;
			}
			smoke.Emit (1);
		} else {
			/*if (player.transform.rotation.y > 0) {
				player.transform.Rotate (Vector2.down * Time.deltaTime * 45);
			} else if (player.transform.rotation.y < 0) {
				player.transform.Rotate (Vector2.up * Time.deltaTime * 45);
			}*/

			if (moveRight == false) {
				if (player.transform.rotation.y < 0.1f) {
					player.transform.Rotate (Vector2.up * Time.deltaTime * 35);
					shadow.transform.localPosition = 
                        Vector2.Lerp (shadow.transform.localPosition, 
                                      new Vector2 (-0.02f, shadow.transform.localPosition.y), Time.deltaTime * 5);
				}
                if (speedModifier < 3.0f * GameHandler.gameSpeed) {
                    speedModifier += 0.3f * GameHandler.gameSpeed;
				}
			} else if (moveRight) {
				if (player.transform.rotation.y > -0.1f) {
					player.transform.Rotate (Vector2.down * Time.deltaTime * 35);
					shadow.transform.localPosition = 
                        Vector2.Lerp (shadow.transform.localPosition, 
                                      new Vector2 (0.02f, shadow.transform.localPosition.y), Time.deltaTime * 5);
				}
                if (speedModifier > -3.0f * GameHandler.gameSpeed) {
                    speedModifier -= 0.3f * GameHandler.gameSpeed;
				}
			}

			//shadow.transform.localPosition = 
            //  Vector2.Lerp (shadow.transform.localPosition, new Vector2 (0f, shadow.transform.localPosition.y), Time.deltaTime * 5);
		}

		if (Input.GetMouseButtonUp (0)) {
			if (moveRight)
				moveRight = false;
			else
				moveRight = true;
		}
	}

	void raiseShadow () {
		if (shadow.transform.localPosition.y < -0.088f) {
			shadowRaised = true;
		} else {
			shadow.transform.localPosition = new Vector2 (shadow.transform.localPosition.x, shadow.transform.localPosition.y - 0.003f);
		}
	}

    void changeColor () {
        ParticleSystem.MainModule smokeMain = smoke.main;
        if (GameHandler.currPlayerColor == 0) {
            player.GetComponent<SpriteRenderer>().color =
                      Color.Lerp(player.GetComponent<SpriteRenderer>().color, new Color32(255, 100, 100, 255), Time.deltaTime * 5);
            smokeMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(245, 100, 100, 255));
        } else if (GameHandler.currPlayerColor == 1) {
            player.GetComponent<SpriteRenderer>().color =
                      Color.Lerp(player.GetComponent<SpriteRenderer>().color, new Color32(100, 240, 100, 255), Time.deltaTime * 5);
            smokeMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(100, 230, 100, 255));
        } else if (GameHandler.currPlayerColor == 2) {
            player.GetComponent<SpriteRenderer>().color =
                      Color.Lerp(player.GetComponent<SpriteRenderer>().color, new Color32(114, 174, 255, 255), Time.deltaTime * 5);
            smokeMain.startColor = new ParticleSystem.MinMaxGradient (new Color32(114, 174, 245, 255));
        }
    }
}
