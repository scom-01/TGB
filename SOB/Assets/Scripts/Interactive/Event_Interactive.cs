using UnityEngine;


public class Event_Interactive : MonoBehaviour
{
    [SerializeField] private InteractiveObject interactiveObject;

    public void UnInteractive()
    {
        interactiveObject.UnInteractive();
    }

    public void Interactive()
    {
        interactiveObject.Interactive();
    }
}
