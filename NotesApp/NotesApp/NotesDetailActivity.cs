using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NotesApp.Models;
using NotesApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotesApp
{
    [Activity(Label = "NotesDetailActivity")]
    public class NotesDetailActivity : Activity
    {
        EditText _headingEditText;
        EditText _contentEditText;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.notes_detail_layout);

            _headingEditText = FindViewById<EditText>(Resource.Id.headingEditText);
            _contentEditText = FindViewById<EditText>(Resource.Id.contentEditText);
            var saveButton = FindViewById<Button>(Resource.Id.saveButton);

            saveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var newNote = new Note();
            newNote.Heading = _headingEditText.Text;
            newNote.Content = _contentEditText.Text;
            newNote.ChangeDateTime = DateTime.Now;
            MainActivity.SqlService.InsertData(newNote);
            Finish();
        }
    }
}