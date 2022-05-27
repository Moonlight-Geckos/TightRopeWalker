using UnityEngine.Events;

public static class EventsPool
{
    #region Private

    private static UnityEvent<bool> playerFallenEvent = new UnityEvent<bool>();
    private static UnityEvent playerWonEvent = new UnityEvent();
    private static UnityEvent clearPoolsEvent = new UnityEvent();
    private static UnityEvent balloonPopped = new UnityEvent();

    #endregion

    public static UnityEvent<bool> PlayerFallenEvent
    {
        get { return playerFallenEvent; }
    }
    public static UnityEvent PlayerWonEvent
    {
        get { return playerWonEvent; }
    }
    public static UnityEvent ClearPoolsEvent
    {
        get { return clearPoolsEvent; }
    }
    public static UnityEvent BalloonPopped
    {
        get { return balloonPopped; }
    }
}
