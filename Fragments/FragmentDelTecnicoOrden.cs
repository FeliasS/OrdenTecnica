using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using appOrdenTecnica.Adapter;
using AndroidX.RecyclerView.Widget;
using appOrdenTecnica.Model;

namespace appOrdenTecnica.Fragments
{
    public class FragmentDelTecnicoOrden : AndroidX.Fragment.App.Fragment
    {
        //FOTOS
        TextView pathlbl;
        String PhotoPath = "";
        String Photo = "";
        String alm = "";

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.Title = "REPORTE";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.layout_nueva_orden, container, false);

            //Modelando una vista con c#
            FrameLayout layoutv_nuevaOrden2 = view.FindViewById<FrameLayout>(Resource.Id.layoutv_nuevaOrden2);
            layoutv_nuevaOrden2.Visibility = Android.Views.ViewStates.Visible;

            Button btnAgregarLista = view.FindViewById<Button>(Resource.Id.btnAgregarLista);
            btnAgregarLista.Visibility = Android.Views.ViewStates.Gone;

            Button btnAsignarTecnico = view.FindViewById<Button>(Resource.Id.btnAsignarTecnico);
            btnAsignarTecnico.Visibility = Android.Views.ViewStates.Gone;

            Button btnGenerarOrden = view.FindViewById<Button>(Resource.Id.btnGenerarOrden);
            btnGenerarOrden.Visibility = Android.Views.ViewStates.Gone;

            LinearLayout part1 = view.FindViewById<LinearLayout>(Resource.Id.part1);
            part1.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtFecha = view.FindViewById<EditText>(Resource.Id.txtFecha);
            txtFecha.Clickable = false; txtFecha.Focusable = false; txtFecha.SetOnKeyListener(null);
            txtFecha.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtHora = view.FindViewById<EditText>(Resource.Id.txtHora);
            txtHora.Clickable = false; txtHora.Focusable = false; txtHora.SetOnKeyListener(null);
            txtHora.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtCliente = view.FindViewById<EditText>(Resource.Id.txtCliente);
            txtCliente.Clickable = false; txtCliente.Focusable = false; txtCliente.SetOnKeyListener(null);
            txtCliente.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtSucursal = view.FindViewById<EditText>(Resource.Id.txtSucursal);
            txtSucursal.Clickable = false; txtSucursal.Focusable = false; txtSucursal.SetOnKeyListener(null);
            txtSucursal.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtModeloDisp = view.FindViewById<EditText>(Resource.Id.txtModeloDisp);
            txtModeloDisp.Clickable = false; txtModeloDisp.Focusable = false; txtModeloDisp.SetOnKeyListener(null);
            txtModeloDisp.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            EditText txtProblema = view.FindViewById<EditText>(Resource.Id.txtProblema);
            txtProblema.Clickable = false; txtProblema.Focusable = false; txtProblema.SetOnKeyListener(null);
            txtProblema.Background = Context.GetDrawable(Resource.Drawable.custom_input_notedit);

            //FOTOS
            Button btnTakephoto = view.FindViewById<Button>(Resource.Id.btnTakephoto);
            Button btnAddphotos = view.FindViewById<Button>(Resource.Id.btnAddphotos);
            pathlbl = view.FindViewById<TextView>(Resource.Id.textViewlista);//xamarin
            pathlbl.Text = "";

            btnTakephoto.Click += async (sender, e) =>
            {
                await TakePhotoAsync();
                limpiar();

            };

            btnAddphotos.Click += async (sender, e) =>
            {
                await PickPhotoAsync();
                limpiar();
            };
            
            return view;
        }

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($" Captura{PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }

        }
        async Task PickPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"Explorer COMPLETED: {PhotoPath}");

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;

                return;
            }
            else
            {
                string directoryname = "/storage/emulated/0/Pictures/App/";
                if (!Directory.Exists(directoryname))
                {
                    Directory.CreateDirectory(directoryname);
                }

                // save the file into local storage 
                var newFile = Path.Combine(directoryname, photo.FileName);

                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                PhotoPath = newFile;
                Photo = photo.FileName;
            }
        }

        public void limpiar()
        {

            Console.WriteLine(alm);
            alm = pathlbl.Text;
            pathlbl.Text = "";

            if (PhotoPath != null)
            {
                pathlbl.Text = alm + "\n" + Photo + "*"; ;

            }
            else
            {
                pathlbl.Text = alm + "" + "|";
            }

        }

        //FIN FOTOS
    }
}