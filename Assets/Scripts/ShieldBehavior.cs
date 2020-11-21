using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    private Player _player = null;
    // Start is called before the first frame update
    void Start()
    {
        _player = gameObject.GetComponentInParent<Player>();
        if (_player == null)
        {
            Debug.LogError("_player is equal to NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShieldDisplay();
    }

    private void ShieldDisplay()
    {
        SpriteRenderer _shieldRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(_shieldRenderer == null)
            return;
        int _shieldStrength = _player.GetShieldStrength();
        switch (_shieldStrength)
        {
            case 3:
                _shieldRenderer.color = Color.white;
                break;
            case 2:
                _shieldRenderer.color = Color.yellow;
                break;
            case 1:
                _shieldRenderer.color = Color.red;
                break;
            default:
                _shieldRenderer.color = Color.white;
                break;
        }

    }
}
