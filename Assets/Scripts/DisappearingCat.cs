using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisappearingCat : MonoBehaviour
{
    public GameObject playerAim;
    public float minDistanceForDisappering = 2f;
    private Animator animator;
    private bool isPlaying = false;

    public AudioSource audioSource;
    public static event Action onCatDestroy;

    public ParticleSystem destroyEffect;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

   
    void OnPointerEnter()
    {
        if (isPlaying)
        {
            return;
        }
         
        float distance = Vector3.Distance(playerAim.transform.position, transform.position);
        Debug.Log(distance);

        if (distance <= minDistanceForDisappering)
        {
            Debug.Log("Distance is suitable");
            StartCoroutine(PlayAnimationAndDestroy());
        }
    }

    IEnumerator PlayAnimationAndDestroy()
    {
        isPlaying = true;

        Collider col = GetComponent<Collider>();
        if (col)
        {
            col.enabled = false;
        }

        Debug.Log("PlayingAnim");
        animator.SetTrigger("PlaySpin");

        while (animator.GetCurrentAnimatorClipInfo (0).Length == 0) {
            print ("*******THE ARRAY IS ZERO, LETS WAIT");
            yield return null;
        }
        var clips = animator.GetCurrentAnimatorClipInfo(0);
        Debug.Log(clips);

        audioSource.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        audioSource.Pause();
        onCatDestroy.Invoke();
       
        if (destroyEffect != null)
        {
            ParticleSystem effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }
        Destroy(gameObject);
    }

    void OnPointerExit()
    {
        Debug.Log("Exited");
    }
}
