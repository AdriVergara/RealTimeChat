package md5f94c88bc84b2976c5a5be24cbb1fa7f2;


public class PNFirebaseMessagingService
	extends com.google.firebase.messaging.FirebaseMessagingService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMessageReceived:(Lcom/google/firebase/messaging/RemoteMessage;)V:GetOnMessageReceived_Lcom_google_firebase_messaging_RemoteMessage_Handler\n" +
			"";
		mono.android.Runtime.register ("Plugin.FirebasePushNotification.PNFirebaseMessagingService, Plugin.FirebasePushNotification", PNFirebaseMessagingService.class, __md_methods);
	}


	public PNFirebaseMessagingService ()
	{
		super ();
		if (getClass () == PNFirebaseMessagingService.class)
			mono.android.TypeManager.Activate ("Plugin.FirebasePushNotification.PNFirebaseMessagingService, Plugin.FirebasePushNotification", "", this, new java.lang.Object[] {  });
	}


	public void onMessageReceived (com.google.firebase.messaging.RemoteMessage p0)
	{
		n_onMessageReceived (p0);
	}

	private native void n_onMessageReceived (com.google.firebase.messaging.RemoteMessage p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
