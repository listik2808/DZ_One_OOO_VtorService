using UnityEngine;

public class GameCell : MonoBehaviour
{
    private Figure _figure;
    private bool _isFree = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isFree == false && _figure == null)
        {
            if (other.TryGetComponent(out Figure figure))
            {
                _figure = figure;
                _isFree = true;
                _figure.SetCell(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Figure figure))
        {
            if (_figure != null)
            {
                if (figure == _figure)
                {
                    _figure = null;
                    _isFree = false;
                }
            }
        }
    }


    public void SetTransformPoint(Figure figure)
    {
        _figure = figure;
        _figure.transform.position = new Vector3(transform.position.x, 0.47f, transform.position.z);
    }

}