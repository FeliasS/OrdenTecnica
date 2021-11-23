using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using AndroidX.AppCompat.App;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace appOrdenTecnica
{
    [Activity(Label = "NuevaOrdenActivity")]
    public class NuevaOrdenActivity : Activity
    {
        TextView pathlbl;
        String PhotoPath = "";
        String Photo = "";
        ImageView imgv;
        String alm = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.activity_view_ord_tecn_tec_1);
            pathlbl = FindViewById<TextView>(Resource.Id.textView8);//xamarin
            pathlbl.Text = "";

            Button btnimgrr = FindViewById<Button>(Resource.Id.btnAgregarImg);
            btnimgrr.Click += (sender, e) =>
            {
                Console.WriteLine("funciona");
            };


        }
        protected override void OnStart()
        {
            base.OnStart();
            /*
            Button btnimg = FindViewById<Button>(Resource.Id.btnAgregarImg);
            btnimg.Click += async (sender, e) => {
                Console.WriteLine(alm);
                alm = pathlbl.Text;
                pathlbl.Text = "";
                await TakePhotoAsync();
            };*/
            /*async void button_Click(object sender, EventArgs e)
            {
                Console.WriteLine(alm);
                alm = pathlbl.Text;
                pathlbl.Text = "";
                await TakePhotoAsync();
            }*/

            
        }
        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
                if (PhotoPath != null)
                {
                    pathlbl.Text = alm + "\n" + Photo + "*"; ;

                }
                else
                {
                    pathlbl.Text = alm + "" + "|";
                }

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
                // save the file into local storage
                var newFile = Path.Combine("/storage/emulated/0/DCIM/Camera/", photo.FileName);//nuevo lugar a copiar

                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                PhotoPath = newFile;
                Photo = photo.FileName;
            }

        }
        /*async void button_Click(object sender, EventArgs e)
           {
            Console.WriteLine(alm);
               alm = pathlbl.Text;
               pathlbl.Text = "";
               await TakePhotoAsync();
           }*/

        /*public void button2_Click(View view, object sender, EventArgs e)
        {
            Button boton = (Button)view;
            Console.WriteLine("funciona");
        }*/

    }
}