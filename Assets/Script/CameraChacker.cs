using UnityEngine;

public class CameraChacker : MonoBehaviour
{
    private bool hasBeenVisible = false;

    private void OnBecameVisible()
    {
        hasBeenVisible = true;
    }

    private void OnBecameInvisible()
    {
        if (hasBeenVisible)
        {
            Destroy(gameObject);
        }
    }
}
