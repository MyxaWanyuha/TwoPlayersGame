//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class FinishLevel : MonoBehaviour
//{
//    [SerializeField]
//    GameObject canvas;
//    [SerializeField]
//    Text points;
//    [SerializeField]
//    Text time;
//    void Start()
//    {
        
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Player")
//        {
//            var gc = other.gameObject.GetComponent<GameController>();
//            Finish(gc);
//        }
//    }
//    private void Finish(GameController gc)
//    {
//        canvas.SetActive(true);
//        points.text = "Points" +gc.GetPoints().ToString();
//        time.text = "время";
//        Time.timeScale = 0f;
//    }
//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
