using System.Text;

namespace ExpressAOP.Test
{
    [MyFilter("class:filter")]
    public class MyModel : IMyModel
    {
        private readonly string _key;

        public MyModel(string key)
        {
            _key = key;
            
        }

        [MyFilter("method:filter")]
        public void Foo(int i)
        {
            Setting.List.Add("method-foo-" + _key);
        }


        public string MyProperty
        {
            [MyFilter("prop-get:filter")]
            get
            {
                Setting.List.Add("prop-ins-" + _key);
                return _key;
            }

        }
    }
}