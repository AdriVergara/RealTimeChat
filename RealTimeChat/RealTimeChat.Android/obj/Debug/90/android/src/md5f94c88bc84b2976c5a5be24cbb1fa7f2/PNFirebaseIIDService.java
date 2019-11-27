package md5f94c88bc84b2976c5a5be24cbb1fa7f2;


public class PNFirebaseIIDService
	extends com.google.firebase.iid.FirebaseInstanceIdService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenRefresh:()V:GetOnTokenRefreshHandler\n" +
			"";
		mono.android.Runtime.register ("Plugin.FirebasePushNotification.PNFirebaseIIDService, Plugin.FirebasePushNotification", PNFirebaseIIDService.class, __md_methods);
	}


	public PNFirebaseIIDService ()
	{
		super ();
		if (getClass () == PNFirebaseIIDService.class)
			mono.android.TypeManager.Activate ("Plugin.FirebasePushNotification.PNFirebaseIIDService, Plugin.FirebasePushNotification", "", this, new java.lang.Object[] {  });
	}


	public void onTokenRefresh ()
	{
		n_onTokenRefresh ();
	}

	private native void n_onTokenRefresh ();

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
