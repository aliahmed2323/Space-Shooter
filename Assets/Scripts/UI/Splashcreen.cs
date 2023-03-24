using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Splashcreen : MonoBehaviour
{
    public GameObject loadingCircle;
    public GameObject mainMenu;
    Tweener twen;

    private void Start()
    {
        twen = loadingCircle.transform.DORotate(new Vector3(0, 0, 180), 3f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Flash);
        Invoke("stop", 3f);
    }
    private void OnEnable()
    {
        twen.Play();
    }
    private void OnDisable()
    {
        twen.Pause();
    }
    void stop()
    {
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }    
}
