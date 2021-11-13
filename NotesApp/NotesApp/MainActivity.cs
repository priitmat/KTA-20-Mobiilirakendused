using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using NotesApp.Adapters;
using NotesApp.Models;
using NotesApp.Services;
using System.Collections.Generic;

namespace NotesApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        static public SqlService SqlService = new SqlService();
        ListView _listView;
        List<Note> _notes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _listView = FindViewById<ListView>(Resource.Id.notesListView);
            _listView.ItemClick += _listView_ItemClick; 
            //DB            

            var addButton = FindViewById<Button>(Resource.Id.addButton);
            addButton.Click += AddButton_Click;
        }

        private void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(NotesDetailActivity));
            intent.PutExtra("id", _notes[e.Position].Id);
            intent.PutExtra("mode", "edit");
            StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _notes = SqlService.GetAllNotes();
            _listView.Adapter = null;
            _listView.Adapter = new NotesAdapter(this, _notes);
        }

        private void AddButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(NotesDetailActivity));
            intent.PutExtra("mode", "add");
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}