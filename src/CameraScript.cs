using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [HideInInspector]
    public static bool normal = true;
    [HideInInspector]
    public static bool shake;
    [HideInInspector]
    public static bool lostShake;

    public IEnumerator CameraShake (float magnitude, float dur) {
        Vector3 pos = transform.localPosition;

        float t = 0.0f;

        while (t < dur) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            t += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = pos;
    }

    void Update()
    {
        if (shake)
        {
            StartCoroutine(CameraShake(0.11f, 0.11f));
            shake = false;
        }

        if (lostShake)
        {
            StartCoroutine(CameraShake(0.2f, 0.2f));
            lostShake = false;
        }

        if (normal)
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 5, Time.deltaTime * 5f);
        else
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 6, Time.deltaTime * 10);
    }
}
