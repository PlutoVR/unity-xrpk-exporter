using UnityEngine;
using WebXR;
using System.Text;

public class XRPKControllerInteraction : MonoBehaviour
{
    public TextMesh controllerInputText;
    public TextMesh leftControllerPosition;
    public TextMesh rightControllerPosition;
    private WebXRController _controller;
    private Rigidbody _rb;
    private StringBuilder _sb = new StringBuilder();
    void Start()
    {
        _controller = GetComponent<WebXRController>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PrintCurrentInput();
        // mainly to sync RigidBody position
        transform.SetPositionAndRotation(_controller.transform.position, _controller.transform.rotation);
    }
    void PrintCurrentInput()
    {
        _sb.Clear();

        // controller events
        if (_controller.GetAxis(WebXRController.AxisTypes.Grip) > 0)
        {
            _sb.Append(_controller.hand + " controller grip Value: " + _controller.GetAxis(WebXRController.AxisTypes.Grip).ToString() + "\n");
        }

        if (_controller.GetAxis(WebXRController.AxisTypes.Trigger) > 0)
        {
            _sb.Append(_controller.hand + " controller trigger Value: " + _controller.GetAxis(WebXRController.AxisTypes.Trigger).ToString() + "\n");
        }

        if (_controller.GetButton(WebXRController.ButtonTypes.ButtonA))
        {
            _sb.Append("Button A Pressed on " + _controller.hand + " Controller \n");
        }

        if (_controller.GetButton(WebXRController.ButtonTypes.ButtonB))
        {
            _sb.Append("Button B Pressed on " + _controller.hand + " Controller \n");

        }

        if (_controller.GetButton(WebXRController.ButtonTypes.Trigger))
        {
            _sb.Append("Trigger Pressed on " + _controller.hand + " Controller \n");

        }

        if (_controller.GetButton(WebXRController.ButtonTypes.Grip))
        {
            _sb.Append("Grip Pressed on " + _controller.hand + " Controller \n");
        }

        if (_sb.Length != 0)
        {
            controllerInputText.text = _sb.ToString();
        }

        // controller posrot
        switch (_controller.hand)
        {
            case WebXRControllerHand.LEFT:
                leftControllerPosition.text = "Left Controller Position:\n" + " " + (_controller.transform.position.x.ToString("F2") + " " + _controller.transform.position.y.ToString("F2") + " " + _controller.transform.position.z.ToString("F2"));
                leftControllerPosition.text += "\n Left Controller Rotation:\n" + " " + (_controller.transform.rotation.x.ToString("F2") + " " + _controller.transform.rotation.y.ToString("F2") + " " + _controller.transform.rotation.z.ToString("F2") + " " + _controller.transform.rotation.w.ToString("F2"));
                break;

            case WebXRControllerHand.RIGHT:
                rightControllerPosition.text = "Right Controller Position:\n" + (_controller.transform.position.x.ToString("F2") + " " + _controller.transform.position.y.ToString("F2") + " " + _controller.transform.position.z.ToString("F2"));
                rightControllerPosition.text += "\n Right Controller Rotation:\n" + (_controller.transform.rotation.x.ToString("F2") + " " + _controller.transform.rotation.y.ToString("F2") + " " + _controller.transform.rotation.z.ToString("F2") + " " + _controller.transform.rotation.w.ToString("F2"));
                break;

            default:
                break;
        }
    }
}
