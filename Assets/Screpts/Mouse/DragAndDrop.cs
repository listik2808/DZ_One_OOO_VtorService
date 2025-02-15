using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public const string LayerHero = "Figure";
    private const string GroundZone = "GroundZone";
    private Collider _currentCollider;
    private Camera _camera;
    private Plane _dragPlane;
    private Vector3 _offSet;
    private Figure _figure;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectPart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Drop();
        }
        DragAndDropObject();
    }

    private void SelectPart()
    {
        RaycastHit hit;

        Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out hit, 1000f, LayerMask.GetMask(LayerHero
            )))
        {
            _currentCollider = hit.collider;
            if (_figure == null)
            {
                _figure = _currentCollider.GetComponent<Figure>();
            }
            _dragPlane = new Plane(Vector3.up, _currentCollider.transform.position);
            float planeDist;
            _dragPlane.Raycast(cameraRay, out planeDist);
            _offSet = _currentCollider.transform.position - cameraRay.GetPoint(planeDist);
        }
    }
        

    private void DragAndDropObject()
    {
        if (_currentCollider == null)
            return;

        RaycastHit hit;

        Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(cameraRay, out hit, 1000f, LayerMask.GetMask(GroundZone)))
        {
            float planeDist;
            _dragPlane.Raycast(cameraRay, out planeDist);
            _currentCollider.transform.position = cameraRay.GetPoint(planeDist) + _offSet;

            if (_currentCollider.transform.position.y < 0.47f || _currentCollider.transform.position.y > 0.47f)
            {
                _currentCollider.transform.position = new Vector3(_currentCollider.transform.position.x, 0.47f, _currentCollider.transform.position.z);
            }
        }
    }

    private void Drop()
    {
        if (_currentCollider == null || _figure == null)
            return;

        _figure.Cell.SetTransformPoint(_figure);
        _currentCollider = null;
        _figure = null;
    }

    private void Droping(bool value)
    {
        if (value)
        {
            if (_currentCollider == null || _figure == null)
                return;

            _figure.Cell.SetTransformPoint(_figure);
            _currentCollider = null;
            _figure = null;
        }
    }
}
