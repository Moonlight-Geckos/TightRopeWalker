using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class StickControl : MonoBehaviour
{

    RotationConstraint rotationConstraint;
    PositionConstraint positionConstraint;

    private void Awake()
    {
        rotationConstraint = GetComponent<RotationConstraint>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("StickSource")) {
            var ss = new ConstraintSource();
            ss.sourceTransform = obj.transform;
            ss.weight = 1.0f;
            rotationConstraint.AddSource(ss);
        }

        rotationConstraint.enabled = true;
    }
    public void Fall()
    {
        rotationConstraint.enabled = false;
        IEnumerator fall()
        {
            float duration = 5f;
            while(duration > 0)
            {
                transform.position = transform.position + new Vector3(0, -Time.deltaTime * 70, 0);
                duration -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        StartCoroutine(fall());

    }
    public void Win()
    {
       gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
