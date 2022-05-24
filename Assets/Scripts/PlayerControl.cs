using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    [Range(1f, 8f)]
    private float animationSpeed = 3f;

    [SerializeField]
    [Range(0f, 4f)]
    private float pushDuration = 0.2f;

    [SerializeField]
    [Range(0f, 4f)]
    private float pushDecay = 0.12f;

    #endregion

    #region Private

    struct Move
    {
        public bool Right;
    };
    private Transform bodyTarget;
    Queue<Move> moves;

    #endregion
    private void Awake()
    {
        TapPanel.TapEntry.callback.AddListener(Tap);
        bodyTarget = GameObject.FindGameObjectWithTag("BodyTarget").transform;
        moves = new Queue<Move>();
    }

    private void Tap(BaseEventData arg0)
    {
        PointerEventData pointerEventData = (PointerEventData)arg0;
        float xPos = pointerEventData.position.x;
        IEnumerator move()
        {
            while (moves.Count > 0)
            {
                float elapsed = 0;
                Move move = moves.Peek();
                while (elapsed < pushDuration)
                {
                    if(move.Right)
                        bodyTarget.localPosition = new Vector3(bodyTarget.localPosition.x + Time.deltaTime * animationSpeed, 0, 0);
                    else
                        bodyTarget.localPosition = new Vector3(bodyTarget.localPosition.x - Time.deltaTime * animationSpeed, 0, 0);
                    elapsed += Time.deltaTime + Time.deltaTime * (pushDecay * Math.Abs(bodyTarget.localPosition.x / GameManager.Instance.PlayerBounds));
                    yield return new WaitForEndOfFrame();
                }
                moves.Dequeue();
            }
        }
        if (xPos > Screen.width / 2)
            moves.Enqueue(new Move { Right = true });
        else
            moves.Enqueue(new Move { Right = false });
        if (moves.Count < 2)
            StartCoroutine(move());
    }
}
