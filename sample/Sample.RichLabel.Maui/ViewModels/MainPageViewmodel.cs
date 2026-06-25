using Newtonsoft.Json;
using RichLabel.Maui.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sample.RichLabel.Maui.ViewModels
{
    public class MainPageViewmodel : INotifyPropertyChanged
    {
        private RichTextDocument _richDocument = new RichTextDocument();
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RichTextDocument RichDocument
        {
            get { return _richDocument; }
            set
            {
                if (_richDocument != value)
                {
                    _richDocument = value;
                    OnPropertyChanged();
                }
            }
        }

        private string dummyJsonData = "{  \"lines\": [    {      \"type\": \"Normal\",      \"alignment\": \"Justify\",      \"spans\": [        { \"text\": \"This User Agreement is effective as of November 1, 2015 and governs the terms under which you may access and use Envios.com’s website. For purposes of this User Agreement, “website” or “online service” includes www.Envios.com, and our mobile applications and any associated online services. The website enables you to send cash from the convenience of your computer or mobile device to beneficiaries virtually anywhere in the world, subject to these terms and conditions (a “money transfer”).\" }      ]    },\t{      \"type\": \"Normal\",      \"alignment\": \"Justify\",      \"spans\": [        { \"text\": \"\" }      ]    },    {      \"type\": \"Normal\",      \"alignment\": \"Justify\",      \"spans\": [        { \"text\": \"You are not authorized to use the website if you do not agree to be bound by the terms set forth in this User Agreement. By accessing or using the online service, you agree to be bound by the terms and conditions of this Agreement, as may be amended from time to time. Please read this User Agreement carefully and make sure you understand it fully before using the website.\" }      ]    }\t]}";

        public MainPageViewmodel() 
        {
            LoadData();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void LoadData()
        {
            RichDocument = JsonConvert.DeserializeObject<RichTextDocument>(dummyJsonData);
        }
    }
}
