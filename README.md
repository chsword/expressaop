# Express AOP
[![install from nuget](http://img.shields.io/nuget/v/expressaop.svg?style=flat-square)](https://www.nuget.org/packages/expressaop)[![![Build status](https://ci.appveyor.com/api/projects/status/3665h8dcncl6nlhv?svg=true)](https://ci.appveyor.com/project/chsword/expressaop)

### NuGet Install
``` powershell
PM> Install-Package ExpressAOP
```

###　Test case
You can define a fillter like this:
```c#
    public class MyFilter : AOPFilterAttribute
    {
        private readonly string _key;
        public MyFilter(String key)
        {
            _key = key;
        }
        protected override void Executing(IProcesser processer)
        {
            Setting.List.Add("Executing-" + _key);
        }
        protected override void Executed(IProcesser processer)
        {
            Setting.List.Add("Executed-" + _key);
        }
    }
```
eg: You have a class like following:
```c#
    class MyClass {
        public void Foo1()
        {
        }
    }
```
And you want the filter is working on the Method.
You have 2 methods to use the filter;
### 1.Use the ContextBoundObject baseclass
```c#
    [AOPProxy]// Use the AOP
    class MyModel2 : ContextBoundObject// baseclass
    {
        [MyFilter(":filter")]//AFilter
        public void Foo1()
        {
            Setting.List.Add("method");
        }
    }
```
I write a test case for this way:
```c#
        [TestMethod]
        public void ClassContextBoundObject()
        {
            var o = new MyModel2();
            o.Foo1();
            Assert.AreEqual(new[]
                                {
                                    "Executing-:filter",
                                    "method",
                                    "Executed-:filter"
                                }.GetStr(), Setting.List.GetStr());
        }
```
It'll be working well.

### 2.Use the Interface
You must impl a interface for your class
```c#
    public interface IMyModel
    {
        void Foo(int i);
    }
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
    }
```

And use this code:
```c#
  var proxy = new AOPProxy<IMyModel>(new MyModel("1"));
  var obj = proxy.GetObject();
  obj.Foo(1);
```
It'll be running.

### 3.Other
You can use Filter like this:
```c#
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
```

Execute Order is

Order | Description 
------------- | -------------
1 | The Filter on Interface 
2 | The Filter on Class 
3 | The Filter on Interface's Method or Property 
4 | The Filter on Class 's Method or Property 
5 | Method or Property 

### References
http://en.wikipedia.org/wiki/Aspect-oriented_programming

http://weibo.com/chsword

http://lnkd.in/b6vqP_c
