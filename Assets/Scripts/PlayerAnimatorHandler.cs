using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    public Animator animator;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Battle Idle Transition")) // if animator state is not currentWeapon
        {
            animator.SetTrigger(player.currentWeapon.weaponName.ToLower());
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName(player.currentWeapon.weaponName)) // if animator state is not currentWeapon
        {
            animator.SetTrigger("weaponChanged");
        }
    }
}
