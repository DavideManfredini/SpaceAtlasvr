using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

//
//Custom grabbable script which checks if the grabber "is mine" to actually grab
//

namespace Chiligames.MetaAvatarsPun
{
    [RequireComponent(typeof(PhotonView))]
    public class PunOVRGrabbable : OVRGrabbable
    {
        PhotonView pv;
        Rigidbody rb;
        [SerializeField] float throwMultiplier = 1;

        protected override void Start()
        {
            base.Start();
            pv = GetComponent<PhotonView>();
            rb = gameObject.GetComponent<Rigidbody>();
        }

        override public void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            m_grabbedBy = hand;
            m_grabbedCollider = grabPoint;

            pv.TransferOwnership(PhotonNetwork.LocalPlayer);

            pv.RPC("RPC_SetKinematic", RpcTarget.All, true); //changes the kinematic state of the object to all players when its grabbed
        }

        override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            rb.isKinematic = m_grabbedKinematic;
            pv.RPC("RPC_SetKinematic", RpcTarget.All, m_grabbedKinematic);
            rb.velocity = linearVelocity * throwMultiplier;
            rb.angularVelocity = angularVelocity;

            m_grabbedBy = null;
            m_grabbedCollider = null;
        }

        public Collider[] grabPoints
        {
            get { return m_grabPoints; }
            set { grabPoints = value; }
        }

        virtual public void CustomGrabCollider(Collider[] collider)
        {
            m_grabPoints = collider;
        }

        [PunRPC]
        public void RPC_SetKinematic(bool b)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = b;
        }
    }
}

