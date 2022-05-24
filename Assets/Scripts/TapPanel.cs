using UnityEngine;
using UnityEngine.EventSystems;

public class TapPanel : MonoBehaviour
{
    #region Serialized

    #endregion

    #region Private

    private static EventTrigger.Entry entry = new EventTrigger.Entry();

    #endregion

    #region Public

    public static EventTrigger.Entry TapEntry
    {
        get { return entry; }
    }

    #endregion

    private void Awake()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        entry.eventID = EventTriggerType.PointerDown;
        trigger.triggers.Add(entry);
    }

}
