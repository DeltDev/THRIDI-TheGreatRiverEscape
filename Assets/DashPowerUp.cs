using UnityEngine;

public class DashPowerUp : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Dash Power Up");
            GameManager.Instance.ObtainDash();
            Destroy(transform.parent.gameObject);
        }
    }
}
