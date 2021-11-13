using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using NotesApp.Models;
using System;

namespace NotesApp
{
    [Activity(Label = "NotesDetailActivity")]
    public class NotesDetailActivity : Activity
    {
        EditText _headingEditText;
        EditText _contentEditText;
        Note _note;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.notes_detail_layout);

            _headingEditText = FindViewById<EditText>(Resource.Id.headingEditText);
            _contentEditText = FindViewById<EditText>(Resource.Id.contentEditText);
            var saveButton = FindViewById<Button>(Resource.Id.saveButton);

            saveButton.Click += SaveButton_Click;

            var mode = Intent.GetStringExtra("mode");
            if(mode == "edit")
            {
                var id = Intent.GetIntExtra("id", 0);
                _note = MainActivity.SqlService.GetNote(id);
                _headingEditText.Text = _note.Heading;
                _contentEditText.Text = _note.Content;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(_note == null)
            {
                var newNote = new Note();
                newNote.Heading = _headingEditText.Text;
                newNote.Content = _contentEditText.Text;
                newNote.ChangeDateTime = DateTime.Now;
                MainActivity.SqlService.InsertData(newNote);
            }
            else
            {
                _note.Heading = _headingEditText.Text;
                _note.Content = _contentEditText.Text;
                _note.ChangeDateTime = DateTime.Now;
                MainActivity.SqlService.UpdateData(_note);
            }            
            Finish();
        }
    }
}