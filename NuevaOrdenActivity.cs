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
using Plugin.Media;

namespace appOrdenTecnica
{
    [Activity(Label = "NuevaOrdenActivity", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class NuevaOrdenActivity : Activity
    {
        TextView pathlbl;
        String PhotoPath = "";
        String Photo = "";
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
            btnimgrr.Click += async (sender, e) =>
            {
                await TakePhotoAsync();
                limpiar();
                
            };

            Button btnaddphotos = FindViewById<Button>(Resource.Id.btnaddphotos);
            btnaddphotos.Click += async (sender, e) =>
            {
                await PickPhotoAsync();
                limpiar();
            };
            
            

        }
    public void limpiar() {

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
                await CrossMedia.Current.Initialize();
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
        //public void CreateDirectory(string path) { this.isolatedStorageFile.CreateDirectory(path); }
        async Task PickPhotoAsync()
        {
            try
            {
                await CrossMedia.Current.Initialize();
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
                var newFile = Path.Combine(directoryname,photo.FileName);//nuevo lugar a copiar  DCIM/Camera/
                //var newFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures), photo.FileName);
                //var newFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures)+ "/App/", photo.FileName);

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