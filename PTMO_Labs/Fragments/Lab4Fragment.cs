using Android.OS;
using Android.Views;
using Android.Widget;

using BottomNavigationViewPager.Fragments;

using Java.IO;

using System.Text;

using static Android.Views.View;

using Environment = Android.OS.Environment;

namespace PTMO_Labs.Fragments
{
    public class Lab4Fragment : TheFragment, IOnClickListener
    {
        private TextView rMessage, Message;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Lab4Fragment, container, false);

            rMessage = view.FindViewById<TextView>(Resource.Id.RMessage);
            Message = view.FindViewById<TextView>(Resource.Id.Message);

            Button btn = view.FindViewById<Button>(Resource.Id.BtnTest);
            btn.SetOnClickListener(this);

            return view;
        }

        public void OnClick(View v)
        {
            string state = Environment.ExternalStorageState;
            if (state != Environment.MediaMounted)
            {
                rMessage.Text = "No external storage mounted";
            }
            else
            {
                File externalDir = Environment.ExternalStorageDirectory;
                File textFile = new File(externalDir.AbsolutePath + File.Separator + "text.txt");

                try
                {
                    WriteTextFile(textFile, Message.Text);
                    string text = ReadTextFile(textFile);
                    rMessage.Text = text;
                    if (textFile.Delete())
                    {
                        ToastNotification.ToastMessage("File successfully was deleted!");
                    }
                    else
                    {
                        rMessage.Text = "Couldn't remove temporary file";
                    }
                }
                catch (IOException e)
                {
                    rMessage.Text = "something went wrong! " + e.Message;
                }
            }
        }


        private void WriteTextFile(File file, string text)
        {
            using (BufferedWriter writer = new BufferedWriter(new FileWriter(file)))
            {
                writer.Write(text);
                writer.Flush();
            }
        }

        private string ReadTextFile(File file)
        {
            using (BufferedReader reader = new BufferedReader(new FileReader(file)))
            {
                StringBuilder text = new StringBuilder();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    text.AppendLine(line);
                }
                return text.ToString();
            }
        }
    }
}