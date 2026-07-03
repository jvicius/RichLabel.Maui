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

        private string dummyJsonData = "{  \"lines\": [    {      \"type\": \"Normal\",      \"alignment\": \"Center\",      \"spans\": [        {          \"FontFamily\": \"OpenSansSemibold\",          \"FontSize\" : 20,          \"text\": \"Center Title Font OpenSansSemibold / Size 20\"        }      ]    },    {      \"type\": \"Normal\",      \"spans\": [        {          \"color\": \"#ff2c2c\",          \"text\": \"Red Color text\"        }      ]    },    {      \"type\": \"Normal\",      \"spans\": [        {          \"bold\": true,          \"text\": \"This is a bold text\"        }      ]    },    {      \"type\": \"Normal\",      \"spans\": [        {          \"underline\": true,          \"text\": \"This is a underline text\"        }      ]    },    {      \"type\": \"Normal\",      \"alignment\": \"End\",      \"spans\": [        {          \"bold\": true,          \"text\": \"Alignment End Text\"        }      ]    },        {      \"type\": \"Normal\",      \"spans\": [        {          \"bold\": true,          \"text\": \"Bullet List\"        }      ]    },    {      \"type\": \"Bullet\",\t  \"bulletChar\" : \"•\",\t  \"indentLevel\": 1,      \"spans\": [        {           \"text\": \"Option A\" }      ]    },\t{      \"type\": \"Bullet\",\t  \"bulletChar\" : \"•\",\t  \"indentLevel\": 1,      \"spans\": [        { \"text\": \"Option B\" }      ]    },     {      \"type\": \"Normal\",      \"spans\": [        {          \"bold\": true,          \"text\": \"Auto Number List\"        }      ]    },    {      \"type\": \"Numbered\",      \"indentLevel\": 1,      \"spans\": [        { \"text\": \"Option A\" }      ]    },\t{      \"type\": \"Numbered\",      \"indentLevel\": 1,      \"spans\": [        { \"text\": \"Option B\" }      ]    },    {      \"type\": \"Normal\",      \"spans\": [        {          \"text\": \"Touch here -> \"        },        {          \"FontFamily\": \"RegularFont\",          \"text\": \"1-888-555-88888\",          \"bold\": true,          \"underline\": true,          \"color\": \"#FF8100\",          \"actionType\": \"phone\",          \"actionValue\": \"188855588888\"        }      ]    },     {      \"type\": \"Normal\",      \"spans\": [        {          \"FontFamily\": \"RegularFont\",          \"text\": \"Touch me large text ....\",          \"bold\": true,          \"underline\": true,          \"color\": \"#0000FF\",          \"actionType\": \"text\",          \"actionValue\": \"Thx for touch me\"        }      ]    },    {      \"type\": \"Normal\",      \"alignment\": \"Justify\",      \"spans\": [        {          \"text\": \"This is a Justify text, currently only works on Android. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc\"        }      ]    }  ]}";

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
