using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
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
        NotesAdapter _notesAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Title = "Xamarin ";
            

            _listView = FindViewById<ListView>(Resource.Id.notesListView);
            var addButton = FindViewById<Button>(Resource.Id.addButton);
            
            _listView.ItemClick += _listView_ItemClick;                              
            addButton.Click += AddButton_Click;

            _notes = SqlService.GetAllNotes();
            _notesAdapter =  new NotesAdapter(this, _notes);
            _listView.Adapter = _notesAdapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Menu.mainmenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem1:
                    {
                        // add your code  
                        return true;
                    }
            

            }

            return base.OnOptionsItemSelected(item);
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
            RunOnUiThread(() => {
                _notesAdapter.UpdateData(_notes);
                _notesAdapter.NotifyDataSetChanged();
            });            
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