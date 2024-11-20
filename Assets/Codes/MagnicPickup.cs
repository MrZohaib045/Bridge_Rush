using UnityEngine;
public class MagnicPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cude"))
        {
            transform.GetComponentInParent<PLayerController>().MoveTileToBag(other.gameObject);
        }
    }
}
