using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHandler : MonoBehaviour
{
    private readonly float _trapDamage = 20f;

    [SerializeField] private float _countdownTime = 1f;

    [SerializeField]
    private List<GameObject> visualFX;

    private GameObject effectToSpawn;

    [SerializeField]
    private bool _playerTakesDamage;

    private void Start()
    {
        if (visualFX.Count > 0)
            effectToSpawn = visualFX[0];
    }

    // private Animation trapAnimation;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        _playerTakesDamage = true;
        if (other.name == "Player")
        {
            var vfx = Instantiate(effectToSpawn, transform.position, Quaternion.identity);
            Destroy(vfx, 5);
            Debug.Log(Time.deltaTime);
            yield return new WaitForSeconds(_countdownTime);
            if (_playerTakesDamage)
                other.gameObject.SendMessage("ReceiveDamage", _trapDamage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            _playerTakesDamage = false;
        }
    }
}
