
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using Common;
using Common.Entities;
using Common.Helpers;

namespace PTMO_Labs
{
    [Activity(Label = "SecondActivity")]
    public class AddEditActivity : Activity
    {
        private OpSystem opSystem = new OpSystem();
        private Fragments.TypeOfWork typeOfWork;

        private EditText nameEditText;
        private EditText companyEditText;
        private EditText progLangEditText;
        private EditText versionEditText;
        private EditText platformsEditText;
        private EditText marketEditText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_addEdit);

            typeOfWork = (Fragments.TypeOfWork)Intent.GetIntExtra("TypeOfWork", (int)Fragments.TypeOfWork.Add);

            nameEditText = FindViewById<EditText>(Resource.Id.nameText);
            companyEditText = FindViewById<EditText>(Resource.Id.companyText);
            progLangEditText = FindViewById<EditText>(Resource.Id.progLangText);
            versionEditText = FindViewById<EditText>(Resource.Id.versionText);
            platformsEditText = FindViewById<EditText>(Resource.Id.platformText);
            marketEditText = FindViewById<EditText>(Resource.Id.marketText);

            OpSystem temp;
            if ((temp = Intent.GetExtra<OpSystem>("Edit")) != null)
            {
                opSystem = temp;
                nameEditText.Text = opSystem.Name;
                companyEditText.Text = opSystem.CompanyName;
                progLangEditText.Text = opSystem.ProgrammingLanguage;
                versionEditText.Text = opSystem.LatestVersion;
                platformsEditText.Text = opSystem.SupportedPlatforms;
                marketEditText.Text = opSystem.MarketShare.ToString();
            }

            Button btn = FindViewById<Button>(Resource.Id.BtnSubmit);

            btn.Click += BtnSubmit_Click;

        }

        private void BtnSubmit_Click(object sender, System.EventArgs e)
        {
            string name = nameEditText.Text;
            string company = companyEditText.Text;
            string progLang = progLangEditText.Text;
            string version = versionEditText.Text;
            string platform = platformsEditText.Text;
            string market = marketEditText.Text;

            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(company) ||
                string.IsNullOrEmpty(progLang) ||
                string.IsNullOrEmpty(version) ||
                string.IsNullOrEmpty(platform) ||
                string.IsNullOrEmpty(market))
            {
                ToastNotification.ToastMessage("All fields should be filled!");
                return;
            }
            bool isParsed = double.TryParse(market, out double result);
            if (!isParsed)
            {
                ToastNotification.ToastMessage("Market share should be valid numeric");
                return;
            }

            opSystem.Name = name;
            opSystem.CompanyName = company;
            opSystem.ProgrammingLanguage = progLang;
            opSystem.LatestVersion = version;
            opSystem.SupportedPlatforms = platform;
            opSystem.MarketShare = result;

            using (ApplicationContext context = new ApplicationContext())
            {
                if (typeOfWork == Fragments.TypeOfWork.Add)
                    context.OperatingSystems.Add(opSystem);
                else if (typeOfWork == Fragments.TypeOfWork.Edit)
                    context.OperatingSystems.Update(opSystem);

                context.SaveChanges();
            }

            Intent myIntent = new Intent(this, typeof(MainActivity));
            myIntent.PutExtra("TypeOfWork", (int)typeOfWork);
            myIntent.PutExtra("OpSystem", opSystem);
            SetResult(Result.Ok, myIntent);
            Finish();
        }
    }
}