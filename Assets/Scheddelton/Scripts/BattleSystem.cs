using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYER_TURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public Transform playerHP;
    public Transform enemyHP;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        Setup();
    }

    void Setup()
    {
        Instantiate(player, playerHP);
        Instantiate(enemy, enemyHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
