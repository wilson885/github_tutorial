using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.UI;
public class BodySourceView : MonoBehaviour 
{
	public GameObject _cam;
	public Vector3 R_Shoulder;
	public Vector3 L_Shoulder;
	public Vector3 R_Hand;
	public Vector3 L_Hand;
	public Vector3 L_Elbow;
	public Vector3 R_Elbow;
	public Vector3 Spine_SH;
	public Vector3 Spine_B;
	public Vector3 Head;
	public JointOrientation _joi;


    public Material BoneMaterial;
    public GameObject BodySourceManager;
    
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
	string first_sk;
	int i=0;
    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };
    
    void Update () 
    {
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
				//Debug.Log (body.TrackingId);
                trackedIds.Add (body.TrackingId);
				Debug.Log (body.TrackingId);
				i++;
				if(i>1)
				{
					first_sk = body.TrackingId.ToString ();
				}
				Debug.Log (body.TrackingId.ToString ());
				Head = new Vector3 (body.Joints [Windows.Kinect.JointType.Head].Position.X, body.Joints [Windows.Kinect.JointType.Head].Position.Y, body.Joints [Windows.Kinect.JointType.Head].Position.Z);
				R_Shoulder = new Vector3 (body.Joints[Windows.Kinect.JointType.ShoulderRight].Position.X,body.Joints[Windows.Kinect.JointType.ShoulderRight].Position.Y,body.Joints[Windows.Kinect.JointType.ShoulderRight].Position.Z);
				L_Shoulder = new Vector3 (body.Joints [Windows.Kinect.JointType.ShoulderLeft].Position.X, body.Joints [Windows.Kinect.JointType.ShoulderLeft].Position.Y, body.Joints [Windows.Kinect.JointType.ShoulderLeft].Position.Z);
				R_Hand = new Vector3 (body.Joints [Windows.Kinect.JointType.HandRight].Position.X, body.Joints [Windows.Kinect.JointType.HandRight].Position.Y, body.Joints [Windows.Kinect.JointType.HandRight].Position.Z);
				L_Hand = new Vector3 (body.Joints [Windows.Kinect.JointType.HandLeft].Position.X, body.Joints [Windows.Kinect.JointType.HandLeft].Position.Y, body.Joints [Windows.Kinect.JointType.HandLeft].Position.Z);
				L_Elbow = new Vector3 (body.Joints [Windows.Kinect.JointType.ElbowLeft].Position.X, body.Joints [Windows.Kinect.JointType.ElbowLeft].Position.Y, body.Joints [Windows.Kinect.JointType.ElbowLeft].Position.Z);
				R_Elbow = new Vector3 (body.Joints [Windows.Kinect.JointType.ElbowRight].Position.X, body.Joints [Windows.Kinect.JointType.ElbowRight].Position.Y, body.Joints [Windows.Kinect.JointType.ElbowRight].Position.Z);
				Spine_SH = new Vector3 (body.Joints [Windows.Kinect.JointType.SpineShoulder].Position.X, body.Joints [Windows.Kinect.JointType.SpineShoulder].Position.Y, body.Joints [Windows.Kinect.JointType.SpineShoulder].Position.Z);
				Spine_B = new Vector3 (body.Joints [Windows.Kinect.JointType.SpineBase].Position.X, body.Joints [Windows.Kinect.JointType.SpineBase].Position.Y, body.Joints [Windows.Kinect.JointType.SpineBase].Position.Z);
				if (body.TrackingId.ToString()==first_sk) {
					_cam.transform.position = new Vector3(Head.x*-10,Head.y*10,Head.z*10);
					_cam.transform.rotation = new Quaternion (_joi.toorientation_x (), _joi.toorientation_y (), _joi.toorientation_z (), 1);
				}
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        

        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.05f, 0.05f);
            
            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            Transform jointObj = bodyObject.transform.FindChild(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }

	public Vector3 HandLeft()
	{
		return L_Hand;
	}
	public Vector3 HandRight()
	{
		return R_Hand;
	}
	public Vector3 ShoulderLeft()
	{
		return L_Shoulder;
		}
	public Vector3 ShoulderRight()
	{
		return R_Shoulder;
		}
	public Vector3 ElbowRight()
	{
		return R_Elbow;
	}
	public Vector3 ElbowLeft()
	{
		return L_Elbow;
	}
	public Vector3 SpineSH()
	{
		return Spine_SH;
	}
	public Vector3 SpineB()
	{
		return Spine_B;
	}
}
