using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    private RectTransform thisRect;
    [SerializeField] private Image mouseCircle;
    [SerializeField] private Image mouseLines;
    [SerializeField] private Color color;
    [SerializeField] private Vector2 sizeDelta;
    [SerializeField] private Vector3 rotationVal;
    
    private Vector2 originScale;

    private Vector3 _updatedPos;
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
        originScale = mouseCircle.rectTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        thisRect.position = _playerMovement.GetMouseUIPosition();
        if(_playerMovement.GetHitFromRay().collider != null)
        {
            if (_playerMovement.GetHitFromRay().collider.CompareTag("Enemy") || _playerMovement.GetHitFromRay().collider.CompareTag("Destructable"))
            {
                mouseCircle.color = color;
                mouseLines.color = color;
                mouseCircle.rectTransform.sizeDelta = sizeDelta;
                mouseLines.rectTransform.Rotate(rotationVal * Time.deltaTime);
            }
            else
            {
                mouseCircle.color = Color.white;
                mouseLines.color = Color.white;
                mouseCircle.rectTransform.sizeDelta = originScale;
                mouseLines.rectTransform.rotation = Quaternion.identity;

            }
        }
        
    }
}
