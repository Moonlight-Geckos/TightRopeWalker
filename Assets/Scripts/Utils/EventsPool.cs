using UnityEngine.Events;

public static class EventsPool
{
    #region Private

    private static UnityEvent gameStarted = new UnityEvent();
    private static UnityEvent<bool> playerFallen = new UnityEvent<bool>();
    private static UnityEvent playerWon = new UnityEvent();
    private static UnityEvent clearPools = new UnityEvent();
    private static UnityEvent balloonPopped = new UnityEvent();
    private static UnityEvent updateUI = new UnityEvent();
    private static UnityEvent<int> updateSkin = new UnityEvent<int>();

    #endregion
    public static UnityEvent GameStartedEvent
    {
        get { return gameStarted; }
    }
    public static UnityEvent UpdateUI
    {
        get { return updateUI; }
    }
    public static UnityEvent<bool> PlayerFallenEvent
    {
        get { return playerFallen; }
    }
    public static UnityEvent PlayerWonEvent
    {
        get { return playerWon; }
    }
    public static UnityEvent ClearPoolsEvent
    {
        get { return clearPools; }
    }
    public static UnityEvent BalloonPopped
    {
        get { return balloonPopped; }
    }
    public static UnityEvent<int> UpdateSkinEvent
    {
        get { return updateSkin; }
    }
}
