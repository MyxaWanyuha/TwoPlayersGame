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
    GameController controller;
    public static event SetSpawnPoint setSpawnPoint;
    public  delegate void SetSpawnPoint(Vector3 sp);
    private void Start()
    {
        SpawnP = transform.position;
        controller = GameController.GetInstance();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isFinish && other.tag == "Player" && UI!=null)
        {
            UI.SetActive(true);
            pointsUI.text ="Points " + controller.points.ToString();
            timerUI.text = "Time " + Math.Round(controller.timer,2);
            controller.isGameFinished = true;
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
