using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultButton : MonoBehaviour
{
    private SpawnManager spawnManager;

    private Button button;

    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        button.onClick.AddListener(SetDifficulty);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        Debug.Log(gameObject.name + " was clicked");
        spawnManager.StartGame(difficulty);
    }
}
