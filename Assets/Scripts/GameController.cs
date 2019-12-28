using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {


    public Camera cam;
    public GameObject Coin;
    public float timeLeft;
    public Text timerText;
    public GameObject gameOverText;
    public GameObject restartButton;

    private float maxWidth;
    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float CoinWidth = Coin.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - CoinWidth;
        StartCoroutine(Spawn());
        UpdateText();
    }

    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        UpdateText();
        if (timeLeft < 0)
            timeLeft = 0;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);
        while (timeLeft>0)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-maxWidth, maxWidth),
                transform.position.y,
                0.0f);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Coin, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
        yield return new WaitForSeconds(2.0f);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        restartButton.SetActive(true);
    }
    void UpdateText()
    {
        timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);

    }

}
