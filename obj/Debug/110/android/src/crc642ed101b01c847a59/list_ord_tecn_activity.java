package crc642ed101b01c847a59;


public class list_ord_tecn_activity
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("appOrdenTecnica.list_ord_tecn_activity, appOrdenTecnica", list_ord_tecn_activity.class, __md_methods);
	}


	public list_ord_tecn_activity ()
	{
		super ();
		if (getClass () == list_ord_tecn_activity.class)
			mono.android.TypeManager.Activate ("appOrdenTecnica.list_ord_tecn_activity, appOrdenTecnica", "", this, new java.lang.Object[] {  });
	}


	public list_ord_tecn_activity (int p0)
	{
		super (p0);
		if (getClass () == list_ord_tecn_activity.class)
			mono.android.TypeManager.Activate ("appOrdenTecnica.list_ord_tecn_activity, appOrdenTecnica", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
