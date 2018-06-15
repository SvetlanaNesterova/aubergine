using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FakeItEasy;
using System.Drawing;

namespace Aubergine
{
    [TestFixture]
    class World_should
    {
        public class Object1 : GameObject { }
        public class Object2 : GameObject { }

        class FakeEvent : IConditionalEvent<Object1, Object2>
        {
            public int HappenCount { get; private set; } = 0;
            public int ShouldCount { get; private set; } = 0;

            public void Happen(Object1 object1, Object2 object2)
            {
                HappenCount++;
            }

            public bool ShouldHappenNow(Object1 object1, Object2 object2)
            {
                ShouldCount++;
                return false;
            }
        }

        class FakeCollideInteraction : CollideInteraction<Object1, Object2>
        {
            public int HappenCount { get; private set; } = 0;
            public int ShouldCount { get; private set; } = 0;

            public override void Happen(Object1 object1, Object2 object2)
            {
                HappenCount++;
            }

            public override bool ShouldHappenNow(Object1 object1, Object2 object2)
            {
                ShouldCount++;
                return base.ShouldHappenNow(object1, object2);
            }
        }

        public CollideInteraction<Object1, Object2> collideEvent;

        [Test]
        public void SaveCondEvent_fake()
        {
            var obj1 = new Object1() { Position = new Position(new Point(0, 0), new Size(1, 1)) };
            var obj2 = new Object2() { Position = new Position(new Point(1, 1), new Size(1, 1)) };
            collideEvent = A.Fake<CollideInteraction<Object1, Object2>>();
            var world = new World(new GameObject[] { obj1, obj2 });
            world.AddIConditionalEvent(collideEvent);

            world.Tick();

            A.CallTo(() => collideEvent.ShouldHappenNow(obj1, obj2)).
                MustHaveHappenedOnceExactly();
            A.CallTo(() => collideEvent.Happen(obj1, obj2)).
                MustNotHaveHappened();
        }

        [Test]
        public void CheckCondEvent()
        {
            var obj1 = new Object1();// { Position = new Position(new Point(0, 0), new Size(1, 1)) };
            var obj2 = new Object2();// { Position = new Position(new Point(1, 1), new Size(1, 1)) };
            var condEvent = new FakeEvent();
            var world = new World(new GameObject[] { obj1, obj2 });
            world.AddIConditionalEvent(condEvent);
            world.Tick();
            Assert.AreEqual(condEvent.ShouldCount, 1);
            Assert.AreEqual(condEvent.HappenCount, 0);
        }

        [Test]
        public void CheckCollideEvent()
        {
            var obj1 = new Object1() { Position = 
                new Position(new Point(0, 0), new Size(2, 2)) };
            var obj2 = new Object2() { Position = 
                new Position(new Point(2, 2), new Size(2, 2)) };
            var world = new World(new GameObject[] { obj1, obj2 });
            var fake = new FakeCollideInteraction();
            world.AddIConditionalEvent(fake);
            world.Tick();
            Assert.AreEqual(fake.ShouldCount, 1);
            Assert.AreEqual(fake.HappenCount, 0);
            obj2.Position = new Position(new Point(1, 1), new Size(2, 2));
            world.Tick();
            Assert.AreEqual(fake.ShouldCount, 2);
            Assert.AreEqual(fake.HappenCount, 1);
        }



    }

}
