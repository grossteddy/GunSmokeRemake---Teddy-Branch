using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;




public class PostProccessingFX : MonoBehaviour
{
    [SerializeField] Volume _post;
    [SerializeField] float _speed;
    [SerializeField] float _start;
    [SerializeField] float _end;
    float updatedIntens;
    Vignette _vignette;

    float _tFactor = 1;
    float _reversedTFactor = 1;
    
    void Start()
    {
        updatedIntens = _start;
        _post.profile.TryGet<Vignette>(out _vignette);
    }

    public void StartVigAnim()
    {
        _tFactor = 0;
        _reversedTFactor = 0;
    }
    void VignetteFail()
    {
        if (_tFactor < 1)
        {
            updatedIntens = Mathf.SmoothStep(_start, _end, _tFactor += Time.deltaTime * _speed*3);
        }
        else if (_reversedTFactor < 1)
        {
            updatedIntens = Mathf.SmoothStep(_end, _start, _reversedTFactor += Time.deltaTime * _speed);

        }
    }
    // Update is called once per frame
    void Update()
    {
        _vignette.intensity.value = updatedIntens;
        VignetteFail();
    }
}
