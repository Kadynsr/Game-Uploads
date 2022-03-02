using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {
    int score = 0;

    static ScoreKeeper instance;

    void Awake() {
        ManageSingleton();
    }
    void ManageSingleton() {
        if (instance is not null) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //Getter
    public int GetScore() => score;
    //Method
    public void ModifyScore(int value) {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(score);
    }

    public void ResetScore() {
        score = 0;
    }
}
