using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{
    static int collectedKeys = 0;
    TMP_Text keyCounter;

    private void Start()
    {
        keyCounter = GameManager.manager.keyCounter;
    }

    public void GetTaken()
    {
        collectedKeys++;
        keyCounter.text = collectedKeys.ToString();

        CheckNumberOfKeys();
        Destroy(gameObject);

    }

    void CheckNumberOfKeys()
    {
        if(collectedKeys == 1)
        {
            foreach(GoapAgent agent in GameManager.manager.huntedPlayers)
            {
                Memory memory = new Memory() { state = WorldState.FoundAllKeys, target = GameManager.manager.escapeDoor };
                agent.memory.AddMemory(memory, new Goal(WorldState.targetSeen));
            }
        }
    }
}
