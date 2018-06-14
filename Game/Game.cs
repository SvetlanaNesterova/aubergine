using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Game : Space
    {
        class Health : Parameter<int> { }
        class Mana : Parameter<double> { }
        class NutritionalValue : Parameter<int> { }
        class Apple : ParametrizedGameObject { }
        class Hero : ParametrizedGameObject { }


        //With<Pararmeter<T>>(T min, T max, T current) where T: IComparable

        public Game()
        {
            var c = GameObjectFactory
                .GetParametrizedCharacter<Hero>()
                .WithParameter<int, Health>(50, 1, 100);

            var c1 = c.Create();
                
            var c2 = c
                .WithParameter<double, Mana>(100, 1, 100)
                .Create();
            c2.Set<int,Health>(9);
            /*
            var hero = Factory
                .GetParametrizedCharacter<Hero>()
                .OnPos(1, 2)
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100, 100)
                .AddInteraction<Apple>((her, appl) =>
                    {
                        her.Set<Health>(her.Get<Health> + appl.Get<Sytnost>);
                        appl.BeEaten();
                    })
                //.AddOnTickAction((her) => { her.Set<Health>(-1) });

            var wormFactory = MetaFactory
                .GetParametrizedCharacterFactory<Hero>()
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100)
                .AddInteraction<Apple>((her, appl) =>
                {
                    her.Set<Health>(her.Get < Health > +appl.Get<NutritionalValue>);
                    appl.BeEaten();
                })
                .AddOnTickAction((her) => { her.Set<Health>(-1) });
            var worm = wormFactory
                .OnPos(1, 2)
                .Set<Health>(50)
                .Set<Mana>(100)
                .Create();

            var apple = Factory
                .GetParametrizedCharacter<Apple>()
                .OnPos(1, 2)
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100, 100);

            worm.Get<Health>();
            c.Set<Health>();

            */
            objects = new List<GameObject>
            {
                new Player(new Position() {Coords = new Point(450,200)}),
                new Worm(new Position() {Coords = new Point(100, 100)}),
                new Worm(new Position() {Coords = new Point(900, 100)}),
                new Worm(new Position() {Coords = new Point(100, 300)}),
                new Worm(new Position() {Coords = new Point(400, 500)})
            };
        }

        public override bool Exist { get; }
        public override void Happen(Interaction<GameObject, GameObject> event_)
        {
            throw new NotImplementedException();
        }
    }
}
