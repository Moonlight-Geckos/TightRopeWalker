using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    Dictionary<int, GameObject> cachedSkins = new Dictionary<int, GameObject>();

    GameObject currentSkin;

    private SkinItem[] allSkins;

    private void Awake()
    {
        EventsPool.UpdateSkinEvent.AddListener(ChangeSkin);
    }

    private void Start()
    {
        ChangeSkin(PlayerStorage.SkinSelected);
    }

    private void ChangeSkin(int skinNumber)
    {
        if(allSkins == null)
            allSkins = DataHolder.Instance.AllSkins;
        currentSkin?.SetActive(false);
        if (!cachedSkins.ContainsKey(skinNumber))
        {
            GameObject skin = Instantiate(allSkins[skinNumber].skinObject);
            skin.transform.parent = transform;
            skin.transform.localPosition = Vector3.zero;
            skin.transform.localEulerAngles = Vector3.zero;
            cachedSkins.Add(skinNumber, skin);
        }
        else
            cachedSkins[skinNumber].SetActive(true);
    }
}
