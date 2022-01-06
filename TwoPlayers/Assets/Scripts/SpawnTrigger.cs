using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField]
    bool isFinish;
    [SerializeField]
    Vector3 SpawnP;
    [SerializeField]
    GameObject UI;
    [SerializeField]
    Text pointsUI;
    [SerializeField]
    Text timerUI;
    public static event SetSpawnPoint setSpawnPoint;
    public  delegate void SetSpawnPoint(Vector3 sp);
    private void Start()
    {
        SpawnP = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isFinish && other.tag == "Player" && UI!=null)
        {
            GetComponent<AudioSource>().Play();
            UI.SetActive(true);
            pointsUI.text ="Points " + GameController.GetInstance().points.ToString();
            timerUI.text = "Time " + Math.Round(GameController.GetInstance().timer,2);
            GameController.GetInstance().isGameFinished = true;
            Time.timeScale = 0f;
            return;
        }
        if (other.tag == "Player")
        {
            print("Player detected");
            setSpawnPoint?.Invoke(SpawnP);
            this.gameObject.SetActive(false);
        }
    }
}
