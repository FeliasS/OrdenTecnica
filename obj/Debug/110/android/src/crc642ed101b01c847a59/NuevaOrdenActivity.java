package crc642ed101b01c847a59;


public class NuevaOrdenActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onStart:()V:GetOnStartHandler\n" +
			"";
		mono.android.Runtime.register ("appOrdenTecnica.NuevaOrdenActivity, appOrdenTecnica", NuevaOrdenActivity.class, __md_methods);
	}


	public NuevaOrdenActivity ()
	{
		super ();
		if (getClass () == NuevaOrdenActivity.class)
			mono.android.TypeManager.Activate ("appOrdenTecnica.NuevaOrdenActivity, appOrdenTecnica", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onStart ()
	{
		n_onStart ();
	}

	private native void n_onStart ();

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
