using System;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    private static DataHolder _instance;

    #region Serialized

    [SerializeField]
    private SkinItem[] allSkins = new SkinItem[6];

    #endregion Public

    public static DataHolder Instance
    {
        get { return _instance; }
    }
    public SkinItem[] AllSkins
    {
        get { return allSkins; }
    }

    #region Methods
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            for(int i = 0; i < AllSkins.Length; i++)
            {
                allSkins[i].skinNumber = i;
            }
        }

    }
    void OnValidate()
    {
        if (allSkins.Length > 6)
        {
            Debug.LogWarning("Skins are more than 6 !");
            Array.Resize(ref allSkins, 6);
        }
    }
    #endregion
}
