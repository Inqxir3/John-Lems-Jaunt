using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount = 70f;
    public float boostDuration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Debug.Log("Calling ApplySpeedBoost with 30x multiplier for 5 seconds.");

            player.ApplySpeedBoost(70f, 10f);
            Destroy(gameObject);
        }
    }
}
