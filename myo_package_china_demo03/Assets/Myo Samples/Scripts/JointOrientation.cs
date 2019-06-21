using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

// Orient the object to match that of the Myo armband.
// Compensate for initial yaw (orientation about the gravity vector) and roll (orientation about
// the wearer's arm) by allowing the user to set a reference orientation.
// Making the fingers spread pose or pressing the 'r' key resets the reference orientation.
public class JointOrientation : MonoBehaviour
{
    public Text _orientation_x;
    public Text _orientation_y;
    public Text _orientation_z;

    public Text _acceleration_x;
    public Text _acceleration_y;
    public Text _acceleration_z;

    public Text _gyro_x;
    public Text _gyro_y;
    public Text _gyro_z;

	public Text _emg01;
	public Text _emg02;
	public Text _emg03;
	public Text _emg04;
	public Text _emg05;
	public Text _emg06;
	public Text _emg07;
	public Text _emg08;
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;
    
    // A rotation that compensates for the Myo armband's orientation parallel to the ground, i.e. yaw.
    // Once set, the direction the Myo armband is facing becomes "forward" within the program.
    // Set by making the fingers spread pose or pressing "r".
    private Quaternion _antiYaw = Quaternion.identity;

    // A reference angle representing how the armband is rotated about the wearer's arm, i.e. roll.
    // Set by making the fingers spread pose or pressing "r".
    private float _referenceRoll = 0.0f;

    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

    // Update is called once per frame.
    void Update ()
    {
        // Access the ThalmicMyo component attached to the Myo object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
        _orientation_x.text = myo.transform.rotation.x.ToString();
        _orientation_y.text = myo.transform.rotation.y.ToString();
        _orientation_z.text = myo.transform.rotation.z.ToString();

        _acceleration_x.text = thalmicMyo.accelerometer.x.ToString();
        _acceleration_y.text = thalmicMyo.accelerometer.y.ToString();
        _acceleration_z.text = thalmicMyo.accelerometer.z.ToString();

        _gyro_x.text = thalmicMyo.gyroscope.x.ToString();
        _gyro_y.text = thalmicMyo.gyroscope.y.ToString();
        _gyro_z.text = thalmicMyo.gyroscope.z.ToString();

		_emg01.text = thalmicMyo.emg[0].ToString();
		//Debug.Log (thalmicMyo.emg[1]);
		/*_emg02.text = thalmicMyo.emg [1].ToString();
		_emg03.text = thalmicMyo.emg [2].ToString();
		_emg04.text = thalmicMyo.emg [3].ToString();
		_emg05.text = thalmicMyo.emg [4].ToString();
		_emg06.text = thalmicMyo.emg [5].ToString();
		_emg07.text = thalmicMyo.emg [6].ToString();
		_emg08.text = thalmicMyo.emg [7].ToString();*/

        //emg01.text = thalmicMyo.emg.ToString();
        // Debug.Log(thalmicMyo.emg[0]);

        /*Debug.Log(thalmicMyo.accelerometer.x);
        Debug.Log(thalmicMyo.accelerometer.y);
        Debug.Log(thalmicMyo.accelerometer.z);

        Debug.Log(thalmicMyo.gyroscope.x);
        Debug.Log(thalmicMyo.gyroscope.y);
        Debug.Log(thalmicMyo.gyroscope.z);*/

        //Debug.Log("==========================================");

        // Update references when the pose becomes fingers spread or the q key is pressed.
        bool updateReference = false;
        if (thalmicMyo.pose != _lastPose) {
            _lastPose = thalmicMyo.pose;

            if (thalmicMyo.pose == Pose.FingersSpread) {
                updateReference = true;

                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
        }
        if (Input.GetKeyDown ("r")) {
            updateReference = true;
        }

        // Update references. This anchors the joint on-screen such that it faces forward away
        // from the viewer when the Myo armband is oriented the way it is when these references are taken.
        if (updateReference) {
            // _antiYaw represents a rotation of the Myo armband about the Y axis (up) which aligns the forward
            // vector of the rotation with Z = 1 when the wearer's arm is pointing in the reference direction.
            _antiYaw = Quaternion.FromToRotation (
                new Vector3 (myo.transform.forward.x, 0, myo.transform.forward.z),
                new Vector3 (0, 0, 1)
            );

            // _referenceRoll represents how many degrees the Myo armband is rotated clockwise
            // about its forward axis (when looking down the wearer's arm towards their hand) from the reference zero
            // roll direction. This direction is calculated and explained below. When this reference is
            // taken, the joint will be rotated about its forward axis such that it faces upwards when
            // the roll value matches the reference.
            Vector3 referenceZeroRoll = computeZeroRollVector (myo.transform.forward);
            _referenceRoll = rollFromZero (referenceZeroRoll, myo.transform.forward, myo.transform.up);
        }

        // Current zero roll vector and roll value.
        Vector3 zeroRoll = computeZeroRollVector (myo.transform.forward);
        float roll = rollFromZero (zeroRoll, myo.transform.forward, myo.transform.up);

        // The relative roll is simply how much the current roll has changed relative to the reference roll.
        // adjustAngle simply keeps the resultant value within -180 to 180 degrees.
        float relativeRoll = normalizeAngle (roll - _referenceRoll);

        // antiRoll represents a rotation about the myo Armband's forward axis adjusting for reference roll.
        Quaternion antiRoll = Quaternion.AngleAxis (relativeRoll, myo.transform.forward);

        // Here the anti-roll and yaw rotations are applied to the myo Armband's forward direction to yield
        // the orientation of the joint.
        transform.rotation = _antiYaw * antiRoll * Quaternion.LookRotation (myo.transform.forward);

        // The above calculations were done assuming the Myo armbands's +x direction, in its own coordinate system,
        // was facing toward the wearer's elbow. If the Myo armband is worn with its +x direction facing the other way,
        // the rotation needs to be updated to compensate.
        if (thalmicMyo.xDirection == Thalmic.Myo.XDirection.TowardWrist) {
            // Mirror the rotation around the XZ plane in Unity's coordinate system (XY plane in Myo's coordinate
            // system). This makes the rotation reflect the arm's orientation, rather than that of the Myo armband.
            transform.rotation = new Quaternion(transform.localRotation.x,
                                                -transform.localRotation.y,
                                                transform.localRotation.z,
                                                -transform.localRotation.w);
        }
    }

    // Compute the angle of rotation clockwise about the forward axis relative to the provided zero roll direction.
    // As the armband is rotated about the forward axis this value will change, regardless of which way the
    // forward vector of the Myo is pointing. The returned value will be between -180 and 180 degrees.
    float rollFromZero (Vector3 zeroRoll, Vector3 forward, Vector3 up)
    {
        // The cosine of the angle between the up vector and the zero roll vector. Since both are
        // orthogonal to the forward vector, this tells us how far the Myo has been turned around the
        // forward axis relative to the zero roll vector, but we need to determine separately whether the
        // Myo has been rolled clockwise or counterclockwise.
        float cosine = Vector3.Dot (up, zeroRoll);

        // To determine the sign of the roll, we take the cross product of the up vector and the zero
        // roll vector. This cross product will either be the same or opposite direction as the forward
        // vector depending on whether up is clockwise or counter-clockwise from zero roll.
        // Thus the sign of the dot product of forward and it yields the sign of our roll value.
        Vector3 cp = Vector3.Cross (up, zeroRoll);
        float directionCosine = Vector3.Dot (forward, cp);
        float sign = directionCosine < 0.0f ? 1.0f : -1.0f;

        // Return the angle of roll (in degrees) from the cosine and the sign.
        return sign * Mathf.Rad2Deg * Mathf.Acos (cosine);
    }

    // Compute a vector that points perpendicular to the forward direction,
    // minimizing angular distance from world up (positive Y axis).
    // This represents the direction of no rotation about its forward axis.
    Vector3 computeZeroRollVector (Vector3 forward)
    {
        Vector3 antigravity = Vector3.up;
        Vector3 m = Vector3.Cross (myo.transform.forward, antigravity);
        Vector3 roll = Vector3.Cross (m, myo.transform.forward);

        return roll.normalized;
    }

    // Adjust the provided angle to be within a -180 to 180.
    float normalizeAngle (float angle)
    {
        if (angle > 180.0f) {
            return angle - 360.0f;
        }
        if (angle < -180.0f) {
            return angle + 360.0f;
        }
        return angle;
    }

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard) {
            myo.Unlock (UnlockType.Timed);
        }

        myo.NotifyUserAction ();
    }

    public float toorientation_x()
    {
        return float.Parse(_orientation_x.text);
    }
    public float toorientation_y()
    {
        return float.Parse(_orientation_y.text);
    }
    public float toorientation_z()
    {
        return float.Parse(_orientation_z.text);
    }

    public float toacceleration_x() 
    {
        return float.Parse(_acceleration_x.text);
    }
    public float toacceleration_y()
    {
        return float.Parse(_acceleration_y.text);
    }
    public float toacceleration_z()
    {
        return float.Parse(_acceleration_z.text);
    }

    public float togyro_x() 
    {
        return float.Parse(_gyro_x.text);
    }
    public float togyro_y()
    {
        return float.Parse(_gyro_y.text);
    }
    public float togyro_z()
    {
        return float.Parse(_gyro_z.text);
    }

	public float toemg01()
	{
		return float.Parse(_emg01.text);
	}
	/*public float toemg02()
	{
		return float.Parse(_emg02.text);
	}
	public float toemg03()
	{
		return float.Parse(_emg03.text);
	}
	public float toemg04()
	{
		return float.Parse(_emg04.text);
	}
	public float toemg05()
	{
		return float.Parse(_emg05.text);
	}
	public float toemg06()
	{
		return float.Parse(_emg06.text);
	}
	public float toemg07()
	{
		return float.Parse(_emg07.text);
	}
	public float toemg08()
	{
		return float.Parse(_emg08.text);
	}*/
}
