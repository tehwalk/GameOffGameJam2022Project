using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = Player.Instance;
    }

    // Update is called once per frame
    public void PlayAnim()
    {
        int[] abilityPoints = { player.AirNumber, player.FireNumber, player.EarthNumber, player.WaterNumber };
        int max = 0;
        foreach (int a in abilityPoints)
        {
            if (a > max) max = a;
        }
        if (abilityPoints[0] == max)
        {
            animator.SetTrigger("Flute");
            return;
        }
        else if (abilityPoints[1] == max)
        {
            animator.SetTrigger("Guitar");
            return;
        }
        else if (abilityPoints[2] == max)
        {
            animator.SetTrigger("Drum");
            return;
        }
        else if (abilityPoints[3] == max)
        {
            animator.SetTrigger("Voice");
            return;
        }
    }
}
