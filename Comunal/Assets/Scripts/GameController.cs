using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}
    public int world {get; private set;} = 1;
    public int stage {get; private set;} = 1;
    public int lives {get; private set;}
    public int coins {get; private set;}
    public int health {get; private set;}
    public int currentHeath {get; private set;}
    private void Awake(){
        if(Instance != null){
            DestroyImmediate(gameObject);   
        }else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy(){
        if(Instance == this){
            Instance = null;
        }
    }

   void Start()
    {
        NewGame();
    }

    private void NewGame(){
        health = 4;
        currentHeath = health;
        lives = 3;
        LoadLevel(world, stage);
    }

    private void LoadLevel(int world, int stage){
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }

    // public void NextLevel(){
    //   LoadLevel(world,stage+1);
    //}

    public void ResetLevel(float delay){
        Invoke("Reset", delay);
    }

    public void Reset(){
        lives--;
        if(lives > 0){
            LoadLevel(world, stage);
        }else{
            GameOver();
        }
    }

    private void GameOver(){
        NewGame();
    }

    public void AddCoin(){
        coins++;
        if(coins == 100){
            lives++;
            coins = 0;
        }
    }

    public void AddLife(){
        lives++;
    }
}