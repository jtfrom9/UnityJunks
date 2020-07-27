using Zenject;
using NSubstitute;
using NUnit.Framework;

public interface IFoo
{
    int foo();
}

public class Foo: IFoo
{
    int IFoo.foo() {
        return 1;
    }
}

[TestFixture]
public class ExtenjectUnitTest : ZenjectUnitTestFixture
{
    [SetUp]
    public void Installer(){
        Container.BindInterfacesTo<Foo>().AsSingle();
    }

    [Test]
    public void RunTest1()
    {
        IFoo foo = Container.Resolve<IFoo>();
        Assert.That(foo.foo(), Is.EqualTo(1));
    }

    [Test]
    public void RunTest2()
    {
        var foo = Substitute.For<IFoo>();
        foo.foo().Returns(2);
        Assert.That(foo.foo(), Is.EqualTo(2));
        foo.Received(1);
    }
}