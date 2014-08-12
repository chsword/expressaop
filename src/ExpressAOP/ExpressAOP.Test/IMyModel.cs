namespace ExpressAOP.Test
{
    [MyFilter("I:filter")]
    public interface IMyModel
    {

        [MyFilter("I-Method1:filter")]
        [MyFilter("I-Method2:filter")]
        void Foo(int i);

        string MyProperty
        {
            [MyFilter("I-prop-get:filter")]
            get;
        }
    }
}