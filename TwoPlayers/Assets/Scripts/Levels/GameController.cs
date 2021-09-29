using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int Points = 0;
    int MaxPoints = 0;
    public int GetPoints() => Points;
    public void AddPoints(int p) => Points += p;
    public int GetMaxPoints() => MaxPoints;

    static GameController instance;
    public static GameController GetInstance() => instance;

    void Start()
    {
        instance = this;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        MaxPoints += enemies * 10;
        //TODO coins
    }
}
