package crc640dbacfffad81f79c;


public class ListaOrdenAdapter_MyViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("appOrdenTecnica.Adapter.ListaOrdenAdapter+MyViewHolder, appOrdenTecnica", ListaOrdenAdapter_MyViewHolder.class, __md_methods);
	}


	public ListaOrdenAdapter_MyViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == ListaOrdenAdapter_MyViewHolder.class)
			mono.android.TypeManager.Activate ("appOrdenTecnica.Adapter.ListaOrdenAdapter+MyViewHolder, appOrdenTecnica", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);

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
