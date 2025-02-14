using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField]
    PlaneController _planeController;

    [SerializeField]
    UIDocument _uiDocument;

    Label _speedLabel;
    Label _throttleLabel;
    Label _positionLabel;

    private void Awake()
    {
        _speedLabel = _uiDocument.rootVisualElement.Q<Label>(name: "Speed");
        _throttleLabel = _uiDocument.rootVisualElement.Q<Label>(name: "Throttle");
        _positionLabel = _uiDocument.rootVisualElement.Q<Label>(name: "Position");
    }

    private void Update()
    {
        _speedLabel.text = $"Velocity: {_planeController.Velocity.magnitude:0} m/s";
        _throttleLabel.text = $"Throttle: {(_planeController.Throttle * 100):0} %";
        _positionLabel.text = $"Position: {_planeController.Position}";
    }
}
