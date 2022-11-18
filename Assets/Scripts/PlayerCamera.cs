using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] float speed = 10f;

    Player player;
    Volume volume;
    Vignette vignette;
    Vector3 shakeOffset;

    public int size;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);

     
    }

    private void Update()
    {
        vignette.intensity.Override(1 - player.GetHPRatio());

        if (target == null)
        {
            return;
        }

        float targetX = target.transform.position.x;
        float targetY = target.transform.position.y;
        float cameraZ = transform.position.z;

        var targetPosition = new Vector3(targetX, targetY, cameraZ) + shakeOffset;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.unscaledDeltaTime);
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float shakeValue = 0.25f;
        while (shakeValue > 0)
        {
            shakeOffset.x = Mathf.Sin(shakeValue * 50f) * 0.5f;
            shakeValue -= Time.deltaTime;
            yield return null;
        }
        shakeOffset.x = 0;
    }
}
