using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aubergine;

namespace Game
{
    class Game
    {
        class Health : Parameter<int> { }
        class Mana : Parameter<double> { }
        class NutritionalValue : Parameter<int> { }

        class Apple : ParametrizedGameObject
        {
            public void Die()
            {
                IsOnMap = false;
            }
        }
        class Hero : ParametrizedGameObject { }

        private Space world;
        private Player player;
        public List<GameObject> Objects => world.objects;

        //With<Pararmeter<T>>(T min, T max, T current) where T: IComparable

        public Game()
        {
            #region

            var c = GameObjectFactory
                .GetParametrizedCharacter<Hero>()
                .WithParameter<int, Health>(50, 1, 100);
                //.AddCollideInteraction<Apple>((hero, apple) =>
                //{
                //    if (hero.Position.IsIntersectedWith(apple.Position))
                //    {
                //        hero.Set<int, Health>(hero.Get<int, Health>() + apple.Get<int, NutritionalValue>());
                //        apple.Die();
                //    }
                //});

            var c1 = c.Create();

            var c2 = c
                .WithParameter<double, Mana>(100, 1, 100)
                .Create();
            c2.Set<int, Health>(9);
            /*
            var hero = Factory
                .GetParametrizedCharacter<Hero>()
                .OnPos(1, 2)
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100, 100)
                .AddCollideInteraction<Apple>((her, appl) =>
                    {
                        her.Set<Health>(her.Get<Health> + appl.Get<Sytnost>);
                        appl.BeEaten();
                    })
                //.AddOnTickAction((her) => { her.Set<Health>(-1) });

            var wormFactory = MetaFactory
                .GetParametrizedCharacterFactory<Hero>()
                .WithParameter<Health>(1, 100, 50)
                .WithParameter<Mana>(1, 100)
                .AddCollideInteraction<Apple>((her, appl) =>
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
            #endregion
            player = new Player(new Position() { Coords = new Point(450, 200), Size = new Size(50, 50)});

            var objects = new List<GameObject>
            {
                player,
                new Worm(new Position() {Coords = new Point(100, 100), Size = new Size(10, 10) }),
                new Worm(new Position() {Coords = new Point(900, 100), Size = new Size(10, 10) }),
                new Worm(new Position() {Coords = new Point(100, 300), Size = new Size(10, 10) }),
                new Worm(new Position() {Coords = new Point(400, 500), Size = new Size(10, 10) })
            };

            world = new Space(objects.ToArray());
            world.AddCollideInteraction(new Eat());
        }

        public void Tick()
        {
            world.Tick();
        }

        public Player GetPlayer()
        {
            return player;
        }

        public void MoveCameraView(Direction direction, int distance)
        {
            world.MoveCameraView(direction, distance);
        }
    }
}
