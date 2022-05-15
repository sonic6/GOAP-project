using UnityEngine;

public class EscapeDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.ToLower() == "hunted")
        {
            print("here");
            Destroy(other.gameObject);
        }
    }
}
