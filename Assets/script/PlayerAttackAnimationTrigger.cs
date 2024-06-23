using UnityEngine;

public class PlayerAttackAnimationTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Player player => GetComponentInParent<Player>();

    public void CallTrigger()
    {
        player.CallAnimationTrigger();
    }
}
