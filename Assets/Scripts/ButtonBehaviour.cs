using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public BattleSystem bs;

    public void OpenAttackMenu()
    {
        bs.OpenAttackMenu();
    }

    public void DoAttack()
    {
        bs.PunchAttack();
        bs.ResetMenu();
    }

    public void GoBack()
    {
        bs.ResetMenu();
    }
}
