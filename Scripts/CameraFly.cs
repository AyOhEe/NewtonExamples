using Godot;
using System;
using System.Runtime.InteropServices;

public partial class CameraFly : Camera3D
{
    [ExportCategory("Movement settings")]
    [Export] private float _XSpeed = 1;
    [Export] private float _YSpeed = 1;
    [Export] private float _ZSpeed = 1;
    [Export] private float _SprintModifier = 2;

    [ExportCategory("Rotation settings")]
    [Export] private float _XRotSpeed = 1;
    [Export] private float _YRotSpeed = 1;
    [Export] private Vector2 _XRotRange;


    private Vector2 _MouseMotion;
    private float _XRot = 0;
    private float _YRot = 0;


    public override void _Ready()
    {
        _XRot = GlobalRotation.X;
        _YRot = GlobalRotation.Y;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        HandleRotation((float)delta);
        HandleLinearMotion((float)delta);
    }

    private void HandleLinearMotion(float f_delta)
    {
        //get the input from the user
        float xMovement = 0;
        float yMovement = 0;
        float zMovement = 0;
        if (Input.IsActionPressed("cam_left"))
        {
            xMovement -= 1;
        }
        if (Input.IsActionPressed("cam_right"))
        {
            xMovement += 1;
        }
        if (Input.IsActionPressed("cam_up"))
        {
            yMovement += 1;
        }
        if (Input.IsActionPressed("cam_down"))
        {
            yMovement -= 1;
        }
        if (Input.IsActionPressed("cam_forward"))
        {
            zMovement += 1;
        }
        if (Input.IsActionPressed("cam_backward"))
        {
            zMovement -= 1;
        }

        //handle sprinting
        if (Input.IsActionPressed("cam_sprint"))
        {
            xMovement *= _SprintModifier;
            yMovement *= _SprintModifier;
            zMovement *= _SprintModifier;
        }

        //apply the motion to the camera, relative to its rotation
        GlobalPosition += GlobalBasis * Vector3.Right * xMovement * _XSpeed * f_delta;
        GlobalPosition += GlobalBasis * Vector3.Up * yMovement * _YSpeed * f_delta;
        GlobalPosition += GlobalBasis * Vector3.Forward * zMovement * _ZSpeed * f_delta;
    }

    private void HandleRotation(float f_delta)
    {
        _XRot = ClampedChange(_XRot, _MouseMotion.Y * f_delta * _XRotSpeed, _XRotRange.X, _XRotRange.Y);
        _YRot += _MouseMotion.X * f_delta * _YRotSpeed;
        _MouseMotion = Vector2.Zero;
        GlobalBasis = new Basis(Vector3.Up, _YRot) * new Basis(Vector3.Right, _XRot);
    }

    private float ClampedChange(float x, float dx, float minX, float maxX)
    {
        return Mathf.Clamp(x + dx, minX, maxX);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        switch (@event)
        {
            case InputEventMouseMotion motion:
                _MouseMotion = motion.Relative;
                break;
        }
    }
}
