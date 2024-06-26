using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationTree : MonoBehaviour
{
    [SerializeField] float _FlickerRythem;
    [SerializeField] int _timesToFlicker;
    [SerializeField] Transform playerGraphics;
    [SerializeField] List<Renderer> thisRenderer = new List<Renderer>();
    NavMeshAgent _enemyNav;
    Animator _enemyAnim;
    float _timer;
    int _timesOfFlicker = 0;
    public bool _isDead = false;
    

    void Start()
    {
        _enemyNav = GetComponent<NavMeshAgent>();
        _enemyAnim = GetComponentInChildren<Animator>();
        for (int i = 0; i < playerGraphics.childCount; i++)
        {
            thisRenderer.Add(playerGraphics.GetChild(i).gameObject.GetComponent<Renderer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        IdleToWalk();
        Death();
    }

    private void IdleToWalk()
    {
        _enemyAnim.SetFloat("speed", _enemyNav.velocity.magnitude / _enemyNav.speed);
    }
    public void Flickering()
    {
        _timer += Time.deltaTime;
        if(_timer >= _FlickerRythem) 
        {
            _timer = 0;
            if (thisRenderer[0].enabled) 
            {
                for (int i = 0; i < thisRenderer.Count; i++)
                {
                    thisRenderer[i].enabled = false;
                }
            }
            else 
            {
                _timesOfFlicker++;
                for (int i = 0; i < thisRenderer.Count; i++)
                {
                    
                    thisRenderer[i].enabled = true;
                }
            }
        }
        if (_timesOfFlicker >= _timesToFlicker)
        {
            gameObject.SetActive(false);
        }
    }
    public void Death()
    {
        if (_isDead)
        {
            _enemyAnim.SetBool("isDead", true);
            Flickering();
        }
    }
}
